using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Objects
{
    [FarmObjectClass("Tree")]
    public class TreeObject
        : PlantableObject
    {

        public static bool MassHarvest(BaseObject[] trees)
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
            foreach (TreeObject tree in trees)
                req.BatchedRequests.Add(new Game.Requests.HarvestTreeSubRequest(1, tree));
            Bot.Server.ServerSession.BlockingCallback result = Program.Instance.GameSession.ServerSession.MakeBlockingRequest(req);
            if (result.Success == false)
                return false;
            FluorineFx.ASObject res = result.Result.Result as FluorineFx.ASObject;

            int? errorType = res["errorType"] as int?;
            string errorData = res["errorData"] as string;
            if (errorType.HasValue && errorType.Value != 0)
            {
                Bot.Scripts.ScriptManager.Instance.RaiseSessionError(errorType.Value, errorData);
                return false;
            }

            object[] dataArray = res["data"] as object[];
            if (dataArray.Length == 0)
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "TreeObject", "Harvest: Skipping harvest. Blank data received.");
                Bot.Scripts.ScriptManager.Instance.RaiseSessionError(-1, "Blank data received");
                return false;
            }

            for (int x = 0; x < dataArray.Length; x++)
            {
                FluorineFx.ASObject firstObject = dataArray[x] as FluorineFx.ASObject;

                if ((int)firstObject["errorType"] == 0)
                {
                    TreeObject curTree = trees[x] as TreeObject;
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "TreeObject", "Harvested {0}({1}) @ {2},{3}", curTree.ItemName, curTree.Id, curTree.Position.X, curTree.Position.Y);

                    curTree.PlantTime = Everworld.Utility.Time.UnixTime(Program.Instance.GameSession.ServerSession.ServerTimeOffset);
                    curTree.UsesAltGraphic = false;
                    curTree.State = "bare";


                }
                else
                {
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "TreeObject", "Harvest: Error returned from server.");
                    Bot.Scripts.ScriptManager.Instance.RaiseSessionError((int)firstObject["errorType"], (string)firstObject["errorData"]);
                    return false;
                }

            }
            return true;
        }
    }
}
