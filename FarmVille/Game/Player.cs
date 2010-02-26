using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game
{
    public class Player
    {
        private double _gold = 0;

        public double Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        private double _level = 1;

        public double Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public bool LoadFromInitRequest(FluorineFx.ASObject amfObject) {

            _gold = (double)amfObject["gold"];
            if ( amfObject["level"] is double )
                _level = ((double)amfObject["level"]);
            else if ( amfObject["level"] is int )
                _level = ((double)(int)amfObject["level"]);
            else if (amfObject["level"] is string)
                _level = double.Parse((string)amfObject["level"]);

            MasteryCounters.Clear();

            FluorineFx.ASObject playerMasteryCounters = amfObject["masteryCounters"] as FluorineFx.ASObject;
            FluorineFx.ASObject playerMastery = amfObject["mastery"] as FluorineFx.ASObject;
            if (playerMasteryCounters == null)
                return false;

            

            foreach (string key in playerMasteryCounters.Keys)
                MasteryCounters.Add(Game.Settings.SeedSetting.SeedByCode[key], Convert.ToInt32(playerMasteryCounters[key]));

            if ( playerMastery != null )
                foreach(string key in playerMastery.Keys)
                    if ( playerMastery.ContainsKey(key) )
                        MasteryLevels.Add(Game.Settings.SeedSetting.SeedByCode[key], Convert.ToInt32(playerMastery[key]));


            return true;
        }
        public int CountToMastery(string seedName)
        {
            if ( !Game.Settings.SeedSetting.SeedSettings[seedName].Mastery )
                return 0;
            if ( !MasteryCounters.ContainsKey(seedName) )
                return Game.Settings.SeedSetting.SeedSettings[seedName].Mastery2Count;

            int count = Game.Settings.SeedSetting.SeedSettings[seedName].Mastery2Count;

            count -= MasteryCounters[seedName];

            return count;
        }
        public Dictionary<string, int> MasteryCounters = new Dictionary<string, int>();
        public Dictionary<string, int> MasteryLevels = new Dictionary<string, int>();
    }
}
