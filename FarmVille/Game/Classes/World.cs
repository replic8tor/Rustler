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

        public int? SizeX
        {
            get { return _sizeX; }
            set { _sizeX = value; }
        }
        [AMF("sizeY")]
        private int? _sizeY;

        public int? SizeY
        {
            get { return _sizeY; }
            set { _sizeY = value; }
        }
        [AMF("tileSet")]
        private string _tileSet;

        public string TileSet
        {
            get { return _tileSet; }
            set { _tileSet = value; }
        }
        [AMF("versionFromDB")]
        private int? _versionFromDB;

        public int? VersionFromDB
        {
            get { return _versionFromDB; }
            set { _versionFromDB = value; }
        }
        [AMFArray("messages",typeof(string))]
        private List<string> _messages;

        public List<string> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }
        [AMFDictionary("animalsFedData", typeof(int?))]
        private Dictionary<string, int?> _animalsFedData;

        public Dictionary<string, int?> AnimalsFedData
        {
            get { return _animalsFedData; }
            set { _animalsFedData = value; }
        }
        [AMFArray("extraItemData",typeof(string))]
        private List<string> _extraItemData;

        public List<string> ExtraItemData
        {
            get { return _extraItemData; }
            set { _extraItemData = value; }
        }
        [AMF("loadedEmpty")]
        private bool? _loadedEmpty;

        public bool? LoadedEmpty
        {
            get { return _loadedEmpty; }
            set { _loadedEmpty = value; }
        }
        [AMFArray("tempToIdMap",typeof(string))]
        private List<string> _tempToIdMap;

        public List<string> TempToIdMap
        {
            get { return _tempToIdMap; }
            set { _tempToIdMap = value; }
        }
        [AMFArray("tempToIdMapTime",typeof(double?))]
        private List<double?> _tempToIdMapTime;

        public List<double?> TempToIdMapTime
        {
            get { return _tempToIdMapTime; }
            set { _tempToIdMapTime = value; }
        }
        [AMF("v")]
        private int? _v;

        public int? V
        {
            get { return _v; }
            set { _v = value; }
        }
        [AMF("className")]
        private string _className;

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        [AMF("syncBack")]
        private string _syncBack;

        public string SyncBack
        {
            get { return _syncBack; }
            set { _syncBack = value; }
        }

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
