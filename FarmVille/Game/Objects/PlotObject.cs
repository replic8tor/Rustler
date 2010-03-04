using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmVille.Game.Classes;

namespace FarmVille.Game.Objects
{
    [AMFConstructableObject("Plot")]
    public class PlotObject
        : PlantableObject
    {
        [AMF("isBigPlot")]
        private bool? _isBigPlot;

        public bool? IsBigPlot
        {
            get { return _isBigPlot; }
            set { _isBigPlot = value; }
        }
        [AMF("isJumbo")]
        private bool? _isJumbo;

        public bool? IsJumbo
        {
            get { return _isJumbo; }
            set { _isJumbo = value; }
        }
        [AMF("isProduceItem")]
        private bool? _isProduceItem;

        public bool? IsProduceItem
        {
            get { return _isProduceItem; }
            set { _isProduceItem = value; }
        }
        [AMF("hasGiftRemaining")]
        private bool? _hasGiftRemaining;
        
        public bool? HasGiftRemaining
        {
            get { return _hasGiftRemaining; }
            set { _hasGiftRemaining = value; }
        }

              
        protected virtual bool OnPlantResult(Game.Requests.PlantPlotSubRequest request, FluorineFx.ASObject requestResponse) {

            if ((int?)requestResponse["errorType"] == 0)
            {
                // Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "PlotObject", "Planted {0}({1}) @ {2},{3}", request.PlantRequest, this.Id, this.Position.X, this.Position.Y);

                this.ItemName = request.PlantRequest;
                this.PlantTime = request.SentPlantTime / 1000;
                this.IsBigPlot = false;
                this.IsJumbo = false;
                this.IsProduceItem = false;
                this.HasGiftRemaining = false;
                this.UsesAltGraphic = false;
                this.State = "planted";

                Program.Instance.GameSession.Player.Gold -= Game.Settings.SeedSetting.SeedSettings[this.ItemName].Cost;
                if (!Program.Instance.GameSession.World.CropCounters.ContainsKey(this.ItemName))
                    Program.Instance.GameSession.World.CropCounters.Add(this.ItemName, 0);
                Program.Instance.GameSession.World.CropCounters[this.ItemName] = Program.Instance.GameSession.World.CropCounters[this.ItemName] + 1;

                if (!Program.Instance.GameSession.World.ObjectsArray.Contains(this))
                    Program.Instance.GameSession.World.ObjectsArray.Add(this);
            }
            else
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "PlotObject", "Plant result: Error returned from server.");
                Bot.Scripts.ScriptManager.Instance.RaiseSessionError((int)requestResponse["errorType"], (string)requestResponse["errorData"]);
                return false;
            }

            return true;
        }

        public virtual bool Plant(string seedName)
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };

            req.BatchedRequests.Add(new Game.Requests.PlantPlotSubRequest(1, this, seedName));

            Bot.Server.ServerSession.BlockingCallback result = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);           

            object[] dataArray;

            if (!Bot.Server.ServerSession.GetRequestData(result, out dataArray))
                return false;

            FluorineFx.ASObject firstObject = dataArray[0] as FluorineFx.ASObject;
            if (!OnPlantResult(req.BatchedRequests[0] as Game.Requests.PlantPlotSubRequest, firstObject))
                return false;
            return true;
        }
        public static bool MassPlant(BaseObject[] plots, string seedName, bool usePicker = true)
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
            foreach (PlotObject plot in plots)
            {
                if (!usePicker)
                    req.BatchedRequests.Add(new Game.Requests.PlantPlotSubRequest(1, plot, seedName));
                else
                    req.BatchedRequests.Add(new Game.Requests.PlantPlotSubRequest(1, plot, Bot.Scripts.ScriptManager.Instance.Main.SeedPicker.PickSeed(plot)));
            }
            Bot.Server.ServerSession.BlockingCallback result = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);
            object[] dataArray;
            if (!Bot.Server.ServerSession.GetRequestData(result, out dataArray))
                return false;
           
            for (int x = 0; x < dataArray.Length; x++)
            {
                PlotObject curPlot = plots[x] as PlotObject;
                FluorineFx.ASObject firstObject = dataArray[x] as FluorineFx.ASObject;
                if (!curPlot.OnPlantResult(req.BatchedRequests[x] as Game.Requests.PlantPlotSubRequest, firstObject))
                    return false;
            }
            return true;
        }

        protected virtual bool OnPlowResult(Game.Requests.PlowPlotSubRequest request, FluorineFx.ASObject requestResponse) {
            if ((int?)requestResponse["errorType"] == 0)
            {
                //Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "PlotObject", "Plowed plot({0}) @ {1},{2}", this.Id, this.Position.X, this.Position.Y);
                this.IsBigPlot = false;
                this.IsJumbo = false;
                this.IsProduceItem = false;
                this.HasGiftRemaining = false;
                this.PlantTime = 0;
                this.UsesAltGraphic = false;
                this.State = "plowed";
                this.ItemName = null;

                Program.Instance.GameSession.Player.Gold -= 15;

            }
            else
            {
                Bot.Scripts.ScriptManager.Instance.RaiseSessionError((int)requestResponse["errorType"], (string)requestResponse["errorData"]);
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "PlotObject", "Plow: Error returned from server.");
                return false;
            }

            return true;
        }
        
        public virtual bool Plow()
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
            req.BatchedRequests.Add(new Game.Requests.PlowPlotSubRequest(1, this));
            Bot.Server.ServerSession.BlockingCallback result = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);
            object[] dataArray;

            if (!Bot.Server.ServerSession.GetRequestData(result, out dataArray))
                return false;

            if (!OnPlowResult(req.BatchedRequests[0] as Game.Requests.PlowPlotSubRequest, dataArray[0] as FluorineFx.ASObject))
                return false;
            return true;
        }
        public static bool MassPlow(BaseObject[] plots)
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
            foreach (PlotObject plot in plots)
                req.BatchedRequests.Add(new Game.Requests.PlowPlotSubRequest(1, plot));
            Bot.Server.ServerSession.BlockingCallback result = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);

            object[] dataArray;
            if ( !Bot.Server.ServerSession.GetRequestData(result, out dataArray) )
                return false;

            for (int x = 0; x < dataArray.Length; x++)
            {
                PlotObject curPlot = plots[x] as PlotObject;
                if (!curPlot.OnPlowResult(req.BatchedRequests[x] as Game.Requests.PlowPlotSubRequest, dataArray[x] as FluorineFx.ASObject))
                    return false;
            }
            return true;
        }

        protected virtual bool OnHarvestResult(Game.Requests.HarvestPlotSubRequest request, FluorineFx.ASObject requestResponse) {
            if ((int?)requestResponse["errorType"] == 0)
            {

                Program.Instance.GameSession.Player.Gold += Game.Settings.SeedSetting.SeedSettings[this.ItemName].CoinYield;
                if (!Program.Instance.GameSession.World.CropCounters.ContainsKey(this.ItemName))
                    Program.Instance.GameSession.World.CropCounters.Add(this.ItemName, 0);
                Program.Instance.GameSession.World.CropCounters[this.ItemName] = Program.Instance.GameSession.World.CropCounters[this.ItemName] - 1; // One less planted
                if (Game.Settings.SeedSetting.SeedSettings[this.ItemName].Mastery == true)
                {
                    if (!Program.Instance.GameSession.Player.MasteryCounters.ContainsKey(this.ItemName))
                        Program.Instance.GameSession.Player.MasteryCounters.Add(this.ItemName, 0);

                    Program.Instance.GameSession.Player.MasteryCounters[this.ItemName] = Program.Instance.GameSession.Player.MasteryCounters[this.ItemName] + 1; // one more mastery point?

                }
                

               // string message = string.Format("Harvested {0}({1}) @ {2},{3}", this.ItemName, this.Id, this.Position.X, this.Position.Y);

               // if (Program.Instance.GameSession.Player.CountToMastery(this.ItemName) > 0)
               //     message += " Master in " + (Program.Instance.GameSession.Player.CountToMastery(this.ItemName)).ToString() + " (" + ((Program.Instance.GameSession.Player.CountToMastery(this.ItemName)) - Program.Instance.GameSession.World.GetPlantedCount(this.ItemName)).ToString() + ")";

                // Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "PlotObject", message);

                this.IsBigPlot = false;
                this.IsJumbo = false;
                this.IsProduceItem = false;
                this.HasGiftRemaining = false;
                this.PlantTime = 0;
                this.UsesAltGraphic = false;
                this.State = "fallow";
                this.ItemName = null;
            }
            else
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "PlotObject", "Harvest: Error returned from server.");
                Bot.Scripts.ScriptManager.Instance.RaiseSessionError((int)requestResponse["errorType"], (string)requestResponse["errorData"]);
                return false;
            }

            return true;
        }

        public virtual bool Harvest()
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
            req.BatchedRequests.Add(new Game.Requests.HarvestPlotSubRequest(1, this));
            Bot.Server.ServerSession.BlockingCallback result = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);

            object[] dataArray;

            if (!Bot.Server.ServerSession.GetRequestData(result, out dataArray))
                return false;
            if (!OnHarvestResult(req.BatchedRequests[0] as Game.Requests.HarvestPlotSubRequest, dataArray[0] as FluorineFx.ASObject))
                return false;

            return true;

        }
        
        public static bool MassHarvest(BaseObject[] plots)
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
            foreach ( PlotObject plot in plots )
                req.BatchedRequests.Add(new Game.Requests.HarvestPlotSubRequest(1, plot));
            Bot.Server.ServerSession.BlockingCallback result = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);

            object[] dataArray;

            if (!Bot.Server.ServerSession.GetRequestData(result, out dataArray))
                return false;
            for (int x = 0; x < dataArray.Length; x++)
            {
                PlotObject curPlot = plots[x] as PlotObject;
                if (!curPlot.OnHarvestResult(req.BatchedRequests[x] as Game.Requests.HarvestPlotSubRequest, dataArray[x] as FluorineFx.ASObject))
                    return false;
            }

            return true;
        }

        protected virtual bool OnRemoveResult(Game.Requests.RemovePlotSubRequest request, FluorineFx.ASObject requestResponse)
        {
            if ((int?)requestResponse["errorType"] == 0)
            {
                Program.Instance.GameSession.World.ObjectsArray.Remove(this);
                if (Program.Instance.GameSession.World.SuperPlots.ContainsKey(this.Position.X + "," + this.Position.Y))
                    Program.Instance.GameSession.World.SuperPlots[this.Position.X + "," + this.Position.Y].Remove(this);
            }
            else
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "PlotObject", "Remove: Error returned from server.");
                Bot.Scripts.ScriptManager.Instance.RaiseSessionError((int)requestResponse["errorType"], (string)requestResponse["errorData"]);
                return false;
            }
            return true;
        }
        public static bool MassRemove(BaseObject[] plots)
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
            foreach (PlotObject plot in plots)
                req.BatchedRequests.Add(new Game.Requests.RemovePlotSubRequest(1, plot));
            Bot.Server.ServerSession.BlockingCallback result = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);
            
            object[] dataArray;
            if (!Bot.Server.ServerSession.GetRequestData(result, out dataArray))
                return false;

            for (int x = 0; x < dataArray.Length; x++)
            {
                PlotObject curPlot = plots[x] as PlotObject;
                if (!curPlot.OnRemoveResult(req.BatchedRequests[x] as Game.Requests.RemovePlotSubRequest, dataArray[x] as FluorineFx.ASObject))
                    return false;
            }
            return true;
        }


        

       
    }
}
