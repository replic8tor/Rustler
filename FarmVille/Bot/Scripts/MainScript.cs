using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

namespace FarmVille.Bot.Scripts
{
    public class MainScript
        : Script
    {
        private Scripts.SeedPicker _seedPicker;

        public Scripts.SeedPicker SeedPicker
        {
            get { return _seedPicker; }
            set { _seedPicker = value; }
        }

        protected virtual Bot.GameSession GenerateGameSession ( string fbId, string token, string flashRevision )
        {
            return new Bot.GameSession(fbId, token, flashRevision);
        }

        public override bool SessionStartup(Bot.GameSession session)
        {
            while (true)
            {
                Dictionary<string, string> sessionParameters = new Dictionary<string, string>();
                bool result = GetLoginParameters(Program.Instance.Config.User.Username, Program.Instance.Config.User.Password, sessionParameters);
                if ( !result)
                {
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error,"Login","Login session info was not set, retrying in 30 seconds.");
                    System.Threading.Thread.Sleep(60 * 1000);
                    continue;
                }

                if (!sessionParameters.ContainsKey("fb_sig_user") ||
                  !sessionParameters.ContainsKey("token") ||
                  !sessionParameters.ContainsKey("flashRevision"))
                {
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error,"Login","Login session info was not set, retrying in 30 seconds.");
                    System.Threading.Thread.Sleep(60 * 1000);
                    continue;
                }

                Program.Instance.GameSession = GenerateGameSession(sessionParameters["fb_sig_user"], sessionParameters["token"], sessionParameters["flashRevision"]);
                Program.Instance.GameSession.LoadGameSettings();
                foreach (Script script in ScriptManager.Instance.Scripts)
                    if (!script.SessionStartup(Program.Instance.GameSession))
                        return false;
                break;
            }
            return true;
        }
        public override bool OnBeforeFarmLoad(Bot.GameSession session)
        {
            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnBeforeFarmLoad(session))
                    return false;


            session.ServerSession.Sequence = 1;
            
            if (!session.LoadWorld())
                return false;

            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnFarmLoad(session))
                    return false;

            return true;
        }

        public override bool OnBeforeFarmWork(Bot.GameSession session)
        {
            UpdateWorldState(session);
            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnBeforeFarmWork(session))
                    return false;
            return true;
        }

        private DateTime _nextHarvest = DateTime.Now;

        private void UpdateWorldState(GameSession session)
        {
            List<Game.Objects.BaseObject> plots = session.World.FarmObjects.FindAll(x => x is Game.Objects.PlotObject && x.State == "planted");

            Dictionary<string, int> plantedCrops = new Dictionary<string, int>();

            foreach (Game.Objects.PlotObject plot in plots)
            {
                if (plot.State == "planted" && (plot.PlantTime + Game.Settings.SeedSetting.SeedSettings[plot.ItemName].GrowTimeInSeconds < Everworld.Utility.Time.UnixTime(session.ServerSession.ServerTimeOffset)))
                        plot.State = "grown";
                if (!plantedCrops.ContainsKey(plot.ItemName))
                    plantedCrops.Add(plot.ItemName, 0);
                plantedCrops[plot.ItemName] = plantedCrops[plot.ItemName] + 1;
            }

            plots = session.World.FarmObjects.FindAll(x => x is Game.Objects.PlotObject && x.State == "planted");

            IEnumerable<double> growTimes = plots.Select(x => 
                ((Game.Objects.PlotObject)x).PlantTime + Game.Settings.SeedSetting.SeedSettings[((Game.Objects.PlotObject)x).ItemName].GrowTimeInSeconds
                );

            growTimes =   from n in growTimes
                          where n > Everworld.Utility.Time.UnixTime(session.ServerSession.ServerTimeOffset)
                                select n;
            if (growTimes == null || growTimes.Count() == 0)
                ScriptManager.Instance.SetNextUpdate( DateTime.Now.AddMinutes(5) );
            else
            {
                double nextHarvest = growTimes.Min();
                double secondsToNextHarvest = nextHarvest - Everworld.Utility.Time.UnixTime(session.ServerSession.ServerTimeOffset);
                try
                {
                    ScriptManager.Instance.SetNextUpdate(DateTime.Now.AddSeconds(secondsToNextHarvest));
                }
                catch (Exception ex)
                {
                    secondsToNextHarvest = secondsToNextHarvest;
                }
            }

            string output = "Crops planted: ";
            bool first = true;
            List<string> keys = plantedCrops.Keys.ToList();
            keys.Sort();
            foreach (string key in keys)
            {
                if (!first)
                    output += string.Format(", {0} {1}", key, plantedCrops[key].ToString());
                else
                    output += string.Format("{0} {1}", key, plantedCrops[key].ToString());
                first = false;
            }

            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", output);

        }

        public override bool OnBeforeHarvest(Bot.GameSession session)
        {
            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnBeforeHarvest(session))
                    return false;
            
            List<Game.Objects.BaseObject> harvestablePlots = new List<Game.Objects.BaseObject>();
            if (!Program.Instance.Config.Farm.OnlyWorkSuperPlots)
                harvestablePlots = session.World.FarmObjects.FindAll(x => x is Game.Objects.PlotObject && x.State == "grown");
            else
            {
                foreach (List<Game.Objects.PlotObject> list in session.World.SuperPlots.Values)
                    harvestablePlots.AddRange(list.FindAll(x => x.State == "grown").ConvertAll(delegate(Game.Objects.PlotObject p)
                    {
                        return p as Game.Objects.BaseObject;
                    }));
            }
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "{0} objects to harvest", harvestablePlots.Count);
            int count = harvestablePlots.Count;
            bool error = false;
            for (int x = 0; x < count && !error; x += 20)
            {
                if (!Game.Objects.PlotObject.MassHarvest(harvestablePlots.GetRange(x, Math.Min(20, harvestablePlots.Count - x)).ToArray()))
                    return false;
            }            
            

            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnHarvest(session))
                    return false;

            return true;
        }
        public override bool OnBeforeHarvestAnimals(Bot.GameSession session)
        {
            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnBeforeHarvestAnimals(session))
                    return false;

            List<Game.Objects.BaseObject> harvestableAnimals = session.World.FarmObjects.FindAll(x => x is Game.Objects.AnimalObject && x.State == "ripe" && x.ItemName != "uglyduck" );
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "{0} animals to harvest", harvestableAnimals.Count);
            int count = harvestableAnimals.Count;
            bool error = false;
            for (int x = 0; x < count && !error; x += 20)
            {
                if (!Game.Objects.AnimalObject.MassHarvest(harvestableAnimals.GetRange(x, Math.Min(20, harvestableAnimals.Count - x)).ToArray()))
                    return false;
            }

            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnHarvestAnimals(session))
                    return false;
            return true;
        }
        public override bool OnBeforeHarvestTrees(Bot.GameSession session)
        {
            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnBeforeHarvestTrees(session))
                    return false;

            List<Game.Objects.BaseObject> harvestableTrees = session.World.FarmObjects.FindAll(x => x is Game.Objects.TreeObject && x.State == "ripe");
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "{0} trees to harvest", harvestableTrees.Count);
            int count = harvestableTrees.Count;
            bool error = false;
            for (int x = 0; x < count && !error; x += 20)
            {
                if (!Game.Objects.TreeObject.MassHarvest(harvestableTrees.GetRange(x, Math.Min(20, harvestableTrees.Count - x)).ToArray()))
                    return false;
            }

            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnHarvestTrees(session))
                    return false;

            return true;
        }
        public override bool OnBeforePlow(Bot.GameSession session)
        {
            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnBeforePlow(session))
                    return false;
            
            List<Game.Objects.BaseObject> plowablePlots = new List<Game.Objects.BaseObject>();
            if ( !Program.Instance.Config.Farm.OnlyWorkSuperPlots )
                plowablePlots = session.World.FarmObjects.FindAll(x => x.State == "fallow" || (Program.Instance.Config.Farm.PlowWitheredPlots && x.State == "withered"));
            else
            {
                foreach (List<Game.Objects.PlotObject> list in session.World.SuperPlots.Values)
                    plowablePlots.AddRange(list.FindAll(x => x.State == "fallow" || x.State == "withered").ConvertAll(delegate(Game.Objects.PlotObject p)
                    {
                        return p as Game.Objects.BaseObject;
                    }));
            }
            
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "{0} objects to plow", plowablePlots.Count);
            int count = plowablePlots.Count;
            if ((count * 15) > (Program.Instance.GameSession.World.Player.Gold / 2))
            {
                count = Math.Min((int)((Program.Instance.GameSession.World.Player.Gold / 2.0) / 15), count);
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "Only {0} objects will be plowed because of low cash.", count);
            }
            for (int x = 0; x < count; x += 20)
            {
                if (!Game.Objects.PlotObject.MassPlow(plowablePlots.GetRange(x, Math.Min(20, count - x)).ToArray()))
                    return false;
            }

            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnPlow(session))
                    return false;
            return true;
        }
        public override bool OnBeforePlanting(Bot.GameSession session)
        {
            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnBeforePlanting(session))
                    return false;
            
            List<Game.Objects.BaseObject> plantablePlots = new List<Game.Objects.BaseObject>();
            if ( !Program.Instance.Config.Farm.OnlyWorkSuperPlots )
                plantablePlots = session.World.FarmObjects.FindAll(x => x.State == "plowed");
            else
            {
                foreach (List<Game.Objects.PlotObject> list in session.World.SuperPlots.Values)
                    plantablePlots.AddRange(list.FindAll(x => x.State == "plowed").ConvertAll(delegate(Game.Objects.PlotObject p)
                    {
                        return p as Game.Objects.BaseObject;
                    }));
            }
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "{0} objects to plant", plantablePlots.Count);
            int count = plantablePlots.Count;
            if ( count > 0 && (count * Game.Settings.SeedSetting.SeedSettings[Bot.Scripts.ScriptManager.Instance.Main.SeedPicker.PickSeed((Game.Objects.PlotObject)plantablePlots[0])].Cost)
                    > Program.Instance.GameSession.World.Player.Gold)
            {
                count = Math.Min((int)Program.Instance.GameSession.World.Player.Gold / Game.Settings.SeedSetting.SeedSettings[Bot.Scripts.ScriptManager.Instance.Main.SeedPicker.PickSeed((Game.Objects.PlotObject)plantablePlots[0])].Cost, count);
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "Only plainting {0} because of low cash.", count);
            }
            for (int x = 0; x < count; x += 20)
            {
                if (!Game.Objects.PlotObject.MassPlant(plantablePlots.GetRange(x, Math.Min(20, count - x)).ToArray(), Program.Instance.Config.Farm.PlantSeed))
                    return false;
            }
            
            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnPlanting(session))
                    return false;

            foreach (Script script in ScriptManager.Instance.Scripts)
                if (!script.OnFarmWork(session))
                    return false;

            return true;
        }

        private bool GetLoginParameters(string user, string pass, Dictionary<string, string> sessionParameters)
        {
            try
            {
                CookieContainer cookieJar = new CookieContainer();
                HttpWebRequest request;
                HttpWebResponse response;
                Stream resStream;
                StreamReader reader;
                string responseFromServer;

                GetSessionCookies(cookieJar, out request, out response, out resStream, out reader, out responseFromServer);
                PostSessionLogin(user, pass, cookieJar, ref request, ref response, ref resStream, ref reader, ref responseFromServer);

                bool sessionParametersFound = false;
                CheckSessionParameters(sessionParameters, cookieJar, ref request, ref response, ref resStream, ref reader, ref responseFromServer, ref sessionParametersFound);
            }
            catch (Exception ex)
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "Main", "Error on login. {0}:{1}", ex.Message, ex.StackTrace);
                return false;
            }
            return true;

        }

        protected virtual void CheckSessionParameters(Dictionary<string, string> sessionParameters, CookieContainer cookieJar, ref HttpWebRequest request, ref HttpWebResponse response, ref Stream resStream, ref StreamReader reader, ref string responseFromServer, ref bool sessionParametersFound)
        {
            string iframetag = "farmville.com/flash.php";
            int loc0 = responseFromServer.IndexOf(iframetag);

            if (loc0 != -1)
            {
                loc0 = responseFromServer.LastIndexOf('\"', loc0) + 1;
                int loc1 = responseFromServer.IndexOf("\"", loc0);
                string url3 = responseFromServer.Substring(loc0, loc1 - loc0).Replace("&amp;", "&");
                //url3 = url3.Replace("fb-0", "fb-1");
                request = (HttpWebRequest)WebRequest.Create(url3);
                request.Method = "GET";
                request.Referer = response.ResponseUri.ToString();
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7";
                request.Credentials = CredentialCache.DefaultCredentials;
                request.CookieContainer = cookieJar;

                response = (HttpWebResponse)request.GetResponse();
                resStream = response.GetResponseStream();
                reader = new StreamReader(resStream);
                responseFromServer = reader.ReadToEnd();
                string token = responseFromServer.Split(new string[] { "var flashVars =" }, StringSplitOptions.None)[1].Split(new string[] { "var g_userInfo" }, StringSplitOptions.None)[0];
                token = token.Trim();

                token = token.Replace("{", "");
                token = token.Replace("}", "");
                string[] paramArray = token.Split(new string[] { "\"," }, StringSplitOptions.None);
                foreach (string str in paramArray)
                {
                    string[] keyvalpair = str.Split(new string[] { "\":" }, StringSplitOptions.None);
                    sessionParameters.Add(keyvalpair[0].Replace("\"", ""), keyvalpair[1].Replace("\"", ""));
                }
                sessionParametersFound = true;

           
                

            }
            else
            {
                sessionParametersFound = false;
            }
        }

        protected virtual void PostSessionLogin(string user, string pass, CookieContainer cookieJar, ref HttpWebRequest request, ref HttpWebResponse response, ref Stream resStream, ref StreamReader reader, ref string responseFromServer)
        {
            byte[] data;
            Stream reqStream;

            string url2 = "https://login.facebook.com/login.php?login_attempt=1&popup=1&fbconnect=1&display=popup&next=http%3A%2F%2Ffarmville.com%2F.%2Fxd_receiver.htm%3Ffb_login%26fname%3D_opener%26guid%3D0.98236961295448318";
            request = (HttpWebRequest)WebRequest.Create(url2);
            request.Method = "POST";
            request.Referer = response.ResponseUri.ToString();
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.CookieContainer = cookieJar;

            string url2DataPart0 = "charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&next=http%3A%2F%2Fapps.facebook.com%2Fonthefarm%2Findex.php%3Fnew%3D1%26source%3Dnone%26affiliate%3D%26creative%3D&version=1.0&api_key=80c6ec6628efd9a465dd223190a65bbc&return_session=0&session_key_only=0&charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=";
            CookieCollection col = cookieJar.GetCookies(request.RequestUri);

            string url2DataLSD = col["lsd"].Value;
            string url2DataPart1 = "&email=";
            string url2DataEmailAndPass
                =
                System.Web.HttpUtility.UrlEncode(user) +
                "&pass=" + System.Web.HttpUtility.UrlEncode(pass);
            string url2DataFull = url2DataPart0 + url2DataLSD + url2DataPart1 + url2DataEmailAndPass;
            data = System.Text.Encoding.UTF8.GetBytes(url2DataFull);

            reqStream = request.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();


            response = (HttpWebResponse)request.GetResponse();
            resStream = response.GetResponseStream();
            reader = new StreamReader(resStream);
            responseFromServer = reader.ReadToEnd();
        }

        protected virtual void GetSessionCookies(CookieContainer cookieJar, out HttpWebRequest request, out HttpWebResponse response, out Stream resStream, out StreamReader reader, out string responseFromServer)
        {
            string url1 = "http://apps.facebook.com/onthefarm/";
            request = (HttpWebRequest)WebRequest.Create(url1);
            request.Method = "GET";
            request.Referer = "http://fb-0.farmville.com/";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.CookieContainer = cookieJar;

            response = (HttpWebResponse)request.GetResponse();
            resStream = response.GetResponseStream();
            reader = new StreamReader(resStream);
            responseFromServer = reader.ReadToEnd();
        }
    }
}
