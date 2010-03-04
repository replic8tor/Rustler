using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// This plugin was made possible by
// GodOfDream and px from their work in
// Steal! and pxlinks respectively.

namespace FarmVille.Bot.Scripts
{
    public class NeighborLinks
        : Script
    {
        public override bool GlobalStartup()
        {
            if (Program.Instance.Config.ReadCustomInt("neighborlynx", "configversion", 0) < 2)
            {
                Program.Instance.Config.WriteCustomInt("neighborlynx", "configversion", 2);
                Program.Instance.Config.WriteCustomBool("neighborlynx", "active", false);

            }
            return true;
        }
        public override string GetName()
        {
            return "Neighbor Links";
        }

        private Dictionary<string, string> _visitedLinks = new Dictionary<string, string>();

        public override bool OnFarmWork(GameSession session)
        {
            if (!Program.Instance.Config.ReadCustomBool("neighborlynx", "active", false))
                return true;

            string[] rewardGiftTypes = new string[] {
                "CollectionsFriendReward", 
                "DairyFarmFertilizerFriendReward", 
                "EggFriendReward", 
                "FlowerFriendReward", 
                "HolidayTreeFriendReward", 
                "HorseStableFriendReward", 
                "lonelyAnimals", 
                "StorageExpansionFriendReward", 
                "ValentineFriendReward", 
                "ValentineRedeemFriendReward"
               };
            string[] rewardCoinTypes = new string[] {
                "AchievementFriendReward",
                "CoinFriendReward",
                "FertilizeThankFriendReward", 
                "MasteryFriendReward"
            };

            Dictionary<string, List<string>> links = new Dictionary<string, List<string>>();

            // if (!Program.Instance.Config.ReadCustomBool("neighborlinks", "active", false))
            //    return true;

            int x = 0;
            int y = 0;
            foreach (string info in Program.Instance.GameSession.Player.Neighbors)
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "NeighborLynx", "Visiting {0} of {1}", ++x, Program.Instance.GameSession.Player.Neighbors.Count);
                Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("test") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
                Game.Requests.LoadWorldRequest loadWorld = new Game.Requests.LoadWorldRequest(1, info);
                req.BatchedRequests.Add(loadWorld);
                Server.ServerSession.BlockingCallback callback = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);
                object[] data;
                if (callback.Success == false || !Server.ServerSession.GetRequestData(callback, out data))
                    return false;

                FluorineFx.ASObject dataObj = data[0] as FluorineFx.ASObject;
                dataObj = dataObj["data"] as FluorineFx.ASObject;
                FluorineFx.ASObject playerObj = dataObj["player"] as FluorineFx.ASObject;
                FluorineFx.ASObject friendRewards = playerObj["friendRewards"] as FluorineFx.ASObject;
                if (friendRewards != null)
                {
                    foreach (string reward in rewardGiftTypes)
                    {
                        if (friendRewards.ContainsKey(reward))
                        {
                            FluorineFx.ASObject theobj = friendRewards[reward] as FluorineFx.ASObject;
                            foreach (string key in theobj.Keys)
                            {
                                FluorineFx.ASObject rewardData = theobj[key] as FluorineFx.ASObject;
                                FluorineFx.ASObject helpers = rewardData["helpers"] as FluorineFx.ASObject;

                                if (reward == "FlowerFriendReward" && helpers != null && helpers.Count >= ((int)rewardData["m_numItems"]))
                                    continue;
                                else if (rewardData.ContainsKey("helpers") && helpers != null && helpers.Count >= 10)
                                    continue;
                                if (reward == "EggFriendReward" && helpers != null && helpers.Count >= 3)
                                    continue;
                                if (reward == "ValentineRedeemFriendReward" && helpers != null && helpers.Count >= 1)
                                    continue;
                                if (helpers == null || !helpers.ContainsKey(Program.Instance.GameSession.ServerSession.FbId))
                                {
                                    if (_visitedLinks.ContainsKey(key))
                                        continue;
                                    _visitedLinks.Add(key, key);
                                    // we haven't helped.

                                    string like = string.Format("http://apps.facebook.com/onthefarm/reward.php?frHost={0}&frId={1}&frType={2}", info, key, reward);
                                    if (!links.ContainsKey(reward))
                                        links.Add(reward, new List<string>());
                                    links[reward].Add(like);
                                }
                                y++;


                            }
                        }
                    }
                }


            }
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "NeighborLynx", "{0} links generated.", y);
            y = 0;
            using (System.IO.TextWriter tw = new System.IO.StreamWriter(string.Format("{0}.links.html", Everworld.Utility.Time.UnixTime())))
            {
                foreach (string key in links.Keys)
                {
                    List<string> linkslist = links[key];
                    linkslist.Sort();
                    tw.WriteLine(key+"<br />");
                    foreach (string str in linkslist)
                        tw.WriteLine("<a href=\"" + str + "\">" + (y++) + "</a><br />");
                }
                tw.Flush();
                tw.Close();
            }


            return false;
        }
    }
}
