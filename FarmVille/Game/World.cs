using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmVille.Game.Objects;
namespace FarmVille.Game
{
    public class World
    {
        private List<NeighborInfo> _neighbors = new List<NeighborInfo>();

        public List<NeighborInfo> Neighbors
        {
            get { return _neighbors; }
            set { _neighbors = value; }
        }
        private List<BaseObject> _farmObjects = new List<BaseObject>();

        public List<BaseObject> FarmObjects
        {
            get { return _farmObjects; }
            set { _farmObjects = value; }
        }
        private Dictionary<string, List<PlotObject>> _superPlots = new Dictionary<string, List<PlotObject>>();

        private Dictionary<string, int> _cropCounters = new Dictionary<string, int>();

        public Dictionary<string, int> CropCounters
        {
            get { return _cropCounters; }
            set { _cropCounters = value; }
        }
        public int GetPlantedCount(string itemName)
        {
            int retVal = 0;
            if (CropCounters.ContainsKey(itemName))
                retVal = CropCounters[itemName];
            return retVal;
        }
        public Dictionary<string, List<PlotObject>> SuperPlots
        {
            get { return _superPlots; }
            set { _superPlots = value; }
        }

        public Player Player = null;


        public virtual void ProcessFromUserInitResponse(FluorineFx.ASObject response)
        {
            try
            {
                if (response != null)
                {
                    object[] dataArray = response["data"] as object[];

                    FluorineFx.ASObject firstObject = dataArray[0] as FluorineFx.ASObject;
                    double? serverTime = firstObject["serverTime"] as double?;

                    FluorineFx.ASObject gameDataObject = firstObject["data"] as FluorineFx.ASObject;
                    FluorineFx.ASObject gameUserInfo = gameDataObject["userInfo"] as FluorineFx.ASObject;
                    Game.Classes.User user = new Classes.User();
                    user.FromAMF(gameUserInfo);
                    object[] neighborInfo = gameDataObject["neighbors"] as object[];
                    FluorineFx.ASObject gameWorldInfo = gameUserInfo["world"] as FluorineFx.ASObject;
                    FluorineFx.ASObject gamePlayerInfo = gameUserInfo["player"] as FluorineFx.ASObject;
                    object[] gameObjectInfo = gameWorldInfo["objectsArray"] as object[];
                    LoadNeighbors(neighborInfo);
                    LoadObjects(gameObjectInfo);
                    this.Player = new Game.Player();
                    Player.LoadFromInitRequest(gamePlayerInfo);
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "World", "World successfully loaded.");
                }
            }
            catch (Exception ex)
            {
                 Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "World", "There was a problem loading world from UserInit : {0}\n{1}", ex.Message, ex.StackTrace);            
            }
        }

        private void LoadNeighbors(object[] neighborInfo)
        {
            _neighbors.Clear();
            try
            {
                foreach (FluorineFx.ASObject obj in neighborInfo)
                    _neighbors.Add(NeighborInfo.FromASObject(obj));
            }
            catch (Exception ex)
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Warning, "World", "There was an error loading neighbors.");
            }
        }

      

        public virtual void LoadObjects(object[] objects)
        {
            _farmObjects.Clear();
            CropCounters.Clear();
            try
            {
                foreach (object obj in objects)
                {
                    _farmObjects.Add(ObjectBuilder.Instance.BuildObject(((FluorineFx.ASObject)obj)["className"] as string, obj as FluorineFx.ASObject));
                }
                List<BaseObject> plots = _farmObjects.FindAll(x => x is PlotObject);
                foreach (PlotObject plot in plots)
                { 
                    string hashKey = plot.Position.X.ToString() + "," + plot.Position.Y.ToString();
                    if (!_superPlots.ContainsKey(hashKey))
                        _superPlots.Add(hashKey, new List<PlotObject>());
                    _superPlots[hashKey].Add(plot);
                    if (plot.ItemName == null)
                        continue;
                    if (!CropCounters.ContainsKey(plot.ItemName))
                        CropCounters.Add(plot.ItemName, 0);
                    CropCounters[plot.ItemName] = CropCounters[plot.ItemName] + 1;
                }
                List<List<PlotObject>> nonSuper = _superPlots.Values.ToList().FindAll(x => x.Count == 1);
                foreach (List<PlotObject> list in nonSuper)
                    _superPlots.Remove(list[0].Position.X.ToString() + "," + list[0].Position.Y.ToString());


            }
            catch (Exception ex)
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "World", "There was a problem loading world objects : {0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

    }
}
