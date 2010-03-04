using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace FarmVille.Bot.Scripts
{
    public class WitherHack
        : Script
    {
        public override bool GlobalStartup()
        {
            if (Program.Instance.Config.ReadCustomInt("witherhack", "configversion", 0) < 1)
            {
                Program.Instance.Config.WriteCustomInt("witherhack", "configversion", 1);
                Program.Instance.Config.WriteCustomBool("witherhack", "active", false);
                Program.Instance.Config.WriteCustomInt("witherhack", "witherx", 0);
                Program.Instance.Config.WriteCustomInt("witherhack", "withery", 0);
            }
            return true;
        }
        public override string GetName()
        {
            return "Wither Hack";
        }
        public override bool OnFarmWork(GameSession session)
        {
            try
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Wither Hack", "Before active check");
                if (!Program.Instance.Config.ReadCustomBool("witherhack", "active", false))
                    return true;

                int posx = Program.Instance.Config.ReadCustomInt("witherhack", "witherx", 0);
                int posy = Program.Instance.Config.ReadCustomInt("witherhack", "withery", 0);


                List<Game.Objects.BaseObject> objlist = Program.Instance.GameSession.World.FarmObjects.FindAll(
                    x => x is Game.Objects.PlotObject &&
                        (x.Position.X >= (posx * 4)) &&
                        (x.Position.X <= ((posx + 5) * 4)) &&
                        (x.Position.Y >= (posy * 4)) &&
                        (x.Position.Y <= ((posy + 5) * 4)));

                if (objlist.Count > 0)
                    Game.Objects.PlotObject.MassRemove(objlist.ToArray());
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Wither Hack", "After active check");

                // Run script constantly.
                ScriptManager.Instance.SetNextUpdate(DateTime.Now.AddSeconds(-5));

                Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("test") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };

                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Wither Hack", "Cycle starting.");

                string seed = Scripts.ScriptManager.Instance.Main.SeedPicker.PickSeed(new Game.Objects.PlotObject()
                {
                    ClassName = "Plot",
                    Giftable = false,
                    GiftSenderId = null,
                    HasGiftRemaining = false,
                    Id = -1,
                    IsBigPlot = false,
                    IsJumbo = false,
                    IsProduceItem = false,
                    ItemName = "",
                    PlantTime = 0,
                    Position = new Game.Objects.ObjectPosition()
                    {
                        X = Program.Instance.Config.ReadCustomInt("witherhack", "witherx", 0) * 4,
                        Y = Program.Instance.Config.ReadCustomInt("witherhack", "withery", 0) * 4,
                        Z = 0
                    },
                    State = "plowed",
                    UsesAltGraphic = false


                }
                );

                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Wither Hack", "Withering {0}.", seed);

                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        req.BatchedRequests.Add(
                            new Game.Requests.WitherHackDecorationRequest(1,
                                                                    session.GetNextTempId(),
                                                                    (x + Program.Instance.Config.ReadCustomInt("witherhack", "witherx", 0)) * 4,
                                                                    (y + Program.Instance.Config.ReadCustomInt("witherhack", "withery", 0)) * 4,
                                                                    seed));
                    }
                }
                Bot.Server.ServerSession.BlockingCallback result = session.ServerSession.MakeBlockingRequest(req);
                if (result.Success == false)
                    return false;

                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Wither Hack", "Withered crops placed.");

                FluorineFx.ASObject resultObj = result.Result.Result as FluorineFx.ASObject;
                object[] resultData = resultObj["data"] as object[];
                Dictionary<string, object> dataDictionary = null;
                req = new Game.Requests.BatchRequest("test") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
                for (int x = 0, startIndex = 0; x < 5; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        dataDictionary = ((resultData[startIndex++] as FluorineFx.ASObject)["data"] as Dictionary<string, object>);
                        try
                        {
                            req.BatchedRequests.Add(new Game.Requests.WitherHackHarvestRequest(1,
                                (int)dataDictionary["id"],
                                (x + Program.Instance.Config.ReadCustomInt("witherhack", "witherx", 0)) * 4,
                                (y + Program.Instance.Config.ReadCustomInt("witherhack", "withery", 0)) * 4,
                                seed));
                        }
                        catch (Exception ex)
                        {
                            ex = ex;
                        }
                    }
                }
                if (session.ServerSession.MakeBlockingRequest(req).Success == false)
                    return false;

                string seedName = (req.BatchedRequests[0] as Game.Requests.WitherHackHarvestRequest).PlantRequest;
                if (Game.Settings.SeedSetting.SeedSettings[seedName].Mastery == true)
                {
                    if (!Program.Instance.GameSession.World.Player.MasteryCounters.ContainsKey(seedName))
                        Program.Instance.GameSession.World.Player.MasteryCounters.Add(seedName, 0);

                    Program.Instance.GameSession.World.Player.MasteryCounters[seedName] = Program.Instance.GameSession.World.Player.MasteryCounters[seedName] + req.BatchedRequests.Count; // one more mastery point
                }


                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Wither Hack", "Withered crops harvested.");
                req = new Game.Requests.BatchRequest("test") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
                for (int x = 0, startIndex = 0; x < 5; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        dataDictionary = ((resultData[startIndex++] as FluorineFx.ASObject)["data"] as Dictionary<string, object>);
                        req.BatchedRequests.Add(new Game.Requests.WitherHackClearRequest(1,
                            (int)dataDictionary["id"],
                             (x + Program.Instance.Config.ReadCustomInt("witherhack", "witherx", 0)) * 4,
                             (y + Program.Instance.Config.ReadCustomInt("witherhack", "withery", 0)) * 4,
                           seed));
                    }
                }
                if (session.ServerSession.MakeBlockingRequest(req).Success == false)
                    return false;
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Wither Hack", "Withered plots removed.");
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Wither Hack", "Cycle complete.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + "\n\nError in witherhack " + ex.Message);
                return false;
            }
        }
    }
}
