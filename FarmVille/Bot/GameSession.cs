using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO.Compression;

using FarmVille.Game.Objects;

namespace FarmVille.Bot
{
    public class GameSession
    {
        public GameSession(string facebookId, string token, string flashRevision) {
            _serverSession = new Server.ServerSession(token, facebookId, flashRevision);
        }
        private Server.ServerSession _serverSession = null;

        public Server.ServerSession ServerSession
        {
            get { return _serverSession; }
            set { _serverSession = value; }
        }
        private Game.Classes.World _world;

        public Game.Classes.World World
        {
            get { return _world; }
            set { _world = value; }
        }

        private Game.Classes.Player _player;

        public Game.Classes.Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

     

        public virtual bool LoadWorld() {
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "GameSession", "Requesting world info.");
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, _serverSession.Token, _serverSession.FlashRevision, _serverSession.FbId) };
            req.BatchedRequests.Add(new Game.Requests.InitUserSubRequest(1));
            Server.ServerSession.BlockingCallback result =_serverSession.MakeBlockingRequest(req);
            if (result.Success == false)
                return false;
            
            FluorineFx.ASObject res = result.Result.Result as FluorineFx.ASObject;
            if (res != null)
            {
                object[] dataArray = res["data"] as object[];

                FluorineFx.ASObject firstObject = dataArray[0] as FluorineFx.ASObject;
                double? serverTime = firstObject["serverTime"] as double?;

                FluorineFx.ASObject gameDataObject = firstObject["data"] as FluorineFx.ASObject;
                Game.Classes.InitUserResponseData initUserResponse = new Game.Classes.InitUserResponseData();
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "GameSession", "Processing world info.");
                initUserResponse.FromAMF(gameDataObject);
                _world = initUserResponse.UserInfo.World;
                _player = initUserResponse.UserInfo.Player;
            }
            
            
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "GameSession", "World loaded.");
            return true;
            
        }
        public virtual void LoadSeeds(XmlNodeList seedlist) { 
        }
        public virtual void LoadGameSettings() {
            Game.Settings.SeedSetting.SeedSettings.Clear();
            Game.Settings.SeedSetting.SeedByCode.Clear();
            string url = "http://static-facebook.farmville.com/v" + _serverSession.FlashRevision + "/gameSettings.xml";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();

            if (response.ContentEncoding.ToLower().Contains("gzip"))
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            else if (response.ContentEncoding.ToLower().Contains("deflate"))
                responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);    
            
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(responseStream);
            XmlNodeList items = doc.SelectNodes("//settings/items/item[@type='seed']");

            foreach (XmlNode node in items)
            {
                Game.Settings.SeedSetting setting = Game.Settings.SeedSetting.FromXmlNode(node);
                Game.Settings.SeedSetting.SeedSettings.Add(setting.Name, setting);

                int cropProfit = setting.CoinYield - setting.Cost;
                double growTimeInHours = 23.0 * setting.GrowTime;
                double costPlowsForDay = (24.0 / growTimeInHours) * 15;
                double hourProfit = cropProfit / growTimeInHours;
                setting.CropValue = (float)((hourProfit * 24.0) - costPlowsForDay);

                // Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "GameSession", "Loaded {0}.", setting.Name);
            }
            List<Game.Settings.SeedSetting> list = Game.Settings.SeedSetting.SeedSettings.Values.ToList();
            list.Sort( delegate( Game.Settings.SeedSetting s1, Game.Settings.SeedSetting s2 ) {
                           return s2.CropValue.CompareTo( s1.CropValue );
                       } );
            foreach (Game.Settings.SeedSetting crop in list)
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "GameSession", "Loaded {0} {1}", crop.Name, crop.CropValue);
            }
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "GameSession", "Loaded {0} seeds.", Game.Settings.SeedSetting.SeedSettings.Keys.Count);            
        }
        private int _tempId = 63000;
        public int GetNextTempId() {
            return _tempId++;
        }


      
    }
}
