using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Objects
{
    [FarmObjectClass("Animal")]
    public class AnimalObject
        : PlantableObject
    {
        private int _direction;
        private bool _canWander;

        public override void FromObject(FluorineFx.ASObject obj)
        {
            _direction = (int)obj["direction"];
            _canWander = (bool)obj["canWander"];
            base.FromObject(obj);
        }


        public static bool MassHarvest(BaseObject[] animals)
        {
            Game.Requests.BatchRequest req = new Game.Requests.BatchRequest("laughy") { SessionInfo = new Game.Requests.BasicSessionInfo(float.NaN, Program.Instance.GameSession.ServerSession.Token, Program.Instance.GameSession.ServerSession.FlashRevision, Program.Instance.GameSession.ServerSession.FbId) };
            foreach (AnimalObject animal in animals)
                req.BatchedRequests.Add(new Game.Requests.HarvestAnimalSubRequest(1, animal));
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
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "AnimalObject", "Harvest: Skipping harvest. Blank data received.");
                return false;
            }

            for (int x = 0; x < dataArray.Length; x++)
            {
                FluorineFx.ASObject firstObject = dataArray[x] as FluorineFx.ASObject;

                if ((int)firstObject["errorType"] == 0)
                {
                    AnimalObject curAnimal = animals[x] as AnimalObject;
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "AnimalObject", "Harvested {0}({1}) @ {2},{3}", curAnimal.ItemName, curAnimal.Id, curAnimal.Position.X, curAnimal.Position.Y);

                    curAnimal.PlantTime = Everworld.Utility.Time.UnixTime(Program.Instance.GameSession.ServerSession.ServerTimeOffset);
                    curAnimal.UsesAltGraphic = false;
                    curAnimal.State = "bare";

                }
                else
                {
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "AnimalObject", "Harvest: Error returned from server.");
                    Bot.Scripts.ScriptManager.Instance.RaiseSessionError((int)firstObject["errorType"], (string)firstObject["errorData"]);
                    return false;
                }

            }
            return true;
        }
    }
}
