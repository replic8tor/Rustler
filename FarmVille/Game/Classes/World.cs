using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Classes
{
    public class World
        : AMFObject
    {
        [AMFTypedObjectArray("objectsArray", typeof(AMFTypedObjectArrayAttribute))]
        private List<BaseObject> _objectsArray;

        public List<BaseObject> ObjectsArray
        {
            get { return _objectsArray; }
            set { _objectsArray = value; }
        }
        [AMF("sizeX")]
        private int? _sizeX;
        [AMF("sizeY")]
        private int? _sizeY;
        [AMF("tileSet")]
        private string _tileSet;
        [AMF("versionFromDB")]
        private int? _versionFromDB;
        [AMFArray("messages",typeof(string))]
        private List<string> _messages;
        [AMFDictionary("animalsFedData", typeof(int?))]
        private Dictionary<string, int?> _animalsFedData;
        [AMFArray("extraItemData",typeof(string))]
        private List<string> _extraItemData;
        [AMF("loadedEmpty")]
        private bool? _loadedEmpty;
        [AMFArray("tempToIdMap",typeof(string))]
        private List<string> _tempToIdMap;
        [AMFArray("tempToIdMapTime",typeof(double?))]
        private List<double?> _tempToIdMapTime;
        [AMF("v")]
        private int? _v;
        [AMF("className")]
        private string _className;
        [AMF("syncBack")]
        private string _syncBack;

        public override void FromAMF(FluorineFx.ASObject obj)
        {
            base.FromAMF(obj);

            CropCounters.Clear();
            try
            {

                List<BaseObject> plots = _objectsArray.FindAll(x => x is Game.Objects.PlotObject);
                foreach (Game.Objects.PlotObject plot in plots)
                {
                    string hashKey = plot.Position.X.ToString() + "," + plot.Position.Y.ToString();
                    if (!_superPlots.ContainsKey(hashKey))
                        _superPlots.Add(hashKey, new List<Game.Objects.PlotObject>());
                    _superPlots[hashKey].Add(plot);
                    if (plot.ItemName == null)
                        continue;
                    if (!CropCounters.ContainsKey(plot.ItemName))
                        CropCounters.Add(plot.ItemName, 0);
                    CropCounters[plot.ItemName] = CropCounters[plot.ItemName] + 1;
                }
                List<List<Game.Objects.PlotObject>> nonSuper = _superPlots.Values.ToList().FindAll(x => x.Count == 1);
                foreach (List<Game.Objects.PlotObject> list in nonSuper)
                    _superPlots.Remove(list[0].Position.X.ToString() + "," + list[0].Position.Y.ToString());


            }
            catch (Exception ex)
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "World", "Problem loading world.\n{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }



        private Dictionary<string, List<Game.Objects.PlotObject>> _superPlots = new Dictionary<string, List<Game.Objects.PlotObject>>();

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
        public Dictionary<string, List<Game.Objects.PlotObject>> SuperPlots
        {
            get { return _superPlots; }
            set { _superPlots = value; }
        }

    }
}
