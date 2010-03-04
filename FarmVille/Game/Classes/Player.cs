using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Classes
{
    class Player :
        AMFObject
    {
        [AMF("gold")]
        private double? _gold;
        [AMF("cash")]
        private double? _cash;
        [AMF("cashEarned")]
        private double? _cashEarned;
        [AMF("cashPurchased")]
        private double? _cashPurchased;
        [AMF("cashPurchasedCPA")]
        private double? _cashPurchasedCPA;
        [AMF("averagePurchasedCashPrice")]
        private double? _averagePurchasedCashPrice;
        [AMF("averageCPACashPrice")]
        private double? _averageCPACashPrice;
        [AMF("level")]
        private double? _level;
        [AMF("xp")]
        private double? xp;
        [AMF("energyMax")]
        private double? _energyMax;
        [AMF("energy")]
        private double? _energy;
        [AMF("goldPurchasedTotal")]
        private int? _goldPurchasedTotal;
        [AMF("goldSurplus")]
        private int? _goldSurplus;
        [AMF("cashPurchasedTotal")]
        private int? _cashPurchasedTotal;
        [AMFArray("transactionLog", typeof(string))]
        private System.Collections.Generic.List<string> _transactionLog;
        [AMF("numCashTransactions")]
        private int? _numCashTransactions;
        [AMF("numCPATransactions")]
        private int? _numCPATransactions;
        [AMF("lastEnergyCheck")]
        private double? _lastEnergyCheck;
        [AMF("fertilizer")]
        private int? _fertilizer;
        [AMF("animalFood")]
        private int? _animalFood;
        [AMF("hadUnlimitedLights")]
        private bool? _hasUnlimitedLights;
        [AMF("numOpenedPresents")]
        private int _numOpenedPresents;
        [AMF("lastPlotCountCheck")]
        private double? _lastPlotCountCheck;
        [AMF("valentinesCredits")]
        private int? _valentinesCredits;
        [AMF("valentinesCreditsReceived")]
        private int? _valentinesCreditsReceived;
        [AMF("timezoneOffset")]
        private int? _timezoneOffset;
        [AMF("timezoneOffsetLastUpdate")]
        private int? _timezoneOffsetLastUpdate;
        [AMFArray("promos", typeof(string))]
        private System.Collections.Generic.List<string> _promos;
        [AMFArray("neighbors", typeof(string))]
        private System.Collections.Generic.List<string> _neighbors;
        [AMFArray("sentGifts", typeof(double?))]
        private Dictionary<string, double?> _sentGifts;
        [AMFArray("pendingNeighbors", typeof(string))]
        private System.Collections.Generic.List<string> _pendingNeighbors;
        [AMFArray("upgrades", typeof(string))]
        private System.Collections.Generic.List<string> _upgrades;
        [AMFArray("lonelyAnimals", typeof(string))]
        private System.Collections.Generic.List<string> _lonelyAnimals;
        [AMF("lastLonelyAnimalCheck")]
        private double? _lastLonelyAnimalCheck;

        public class PlayerLootItemHistory
            : AMFObject {
            public class LootItemEntry
                : AMFObject 
            {
                private string _name;

                public string Name
                 {
                    get { return _name; }
                    set { _name = value; }
                }
                private List<string> _objects = new List<string>();

                public List<string> Objects
                {
                  get { return _objects; }
                  set { _objects = value; }
                }
            }

            private List<PlayerLootItemHistory.LootItemEntry> _lootItemEntries;
            public override void FromAMF(FluorineFx.ASObject obj)
            {
                base.FromAMF(obj);
                _lootItemEntries = new List<PlayerLootItemHistory.LootItemEntry>();

                foreach (string key in obj.Keys)
                {
                    LootItemEntry entry = new LootItemEntry();
                    entry.Name = key;
                    foreach (object str in (object[])obj[key])
                        entry.Objects.Add((string)str);
                    _lootItemEntries.Add(entry);
                }
            }
                
        }

        [AMFObject("lootItemHistory", typeof(PlayerLootItemHistory))]
        private PlayerLootItemHistory _lootItemHistory;
        [AMFArray("friendsFertilized", typeof(string))]
        private List<string> _friendsFertilized;
        [AMF("totalFriendsFertilized")]
        private int? _totalFriendsFertilized;
        [AMFArray("friendsFedAnimals", typeof(string))]
        private List<string> _friendsFedAnimals;
        [AMF("totalFriendsFedAnimals")]
        private int? _totalFriendsFedAnimals;
        [AMFArray("bumperCrops", typeof(string))]
        private List<string> _bumperCrops;
        [AMFArray("presents", typeof(string))]
        private List<string> _presents;
        public class PlayerExternalLevels :
            AMFObject 
        {
            [AMF("mafiawars")]
            private int? _mafiaWars;
            [AMF("seederhotrod")]
            private int? _seederHotRod;
        }
        [AMFObject("externalLevels", typeof(PlayerExternalLevels))]
        private PlayerExternalLevels _externalLevels;
        [AMF("unwitherPerson")]
        private int? _unwitherPerson;
        [AMF("witherOn")]
        private bool? _witherOn;
        [AMFDictionary("achievements", typeof(int?))]
        private Dictionary<string, int?> _achievements;
        [AMFDictionary("achCounters", typeof(double?))]
        private Dictionary<string, double?> _achCounters;
        private Dictionary<string, List<string>> _achUniques;

        public override void FromAMF(FluorineFx.ASObject obj)
        {
            base.FromAMF(obj);
            if (obj["achUniques"] != null)
            {
                FluorineFx.ASObject asobj =  ((FluorineFx.ASObject)obj["achUniques"]);
                _achUniques = new Dictionary<string, List<string>>();
                foreach (string key in asobj.Keys)
                {
                    List<string> entries = new List<string>();
                    foreach (object strobj in (object[])asobj[key])
                        entries.Add((string)strobj);
                    _achUniques.Add(key, entries);
                }
                
            }
        }

        [AMFArray("achPassive", typeof(string))]
        private List<string> _achPassive;
        [AMFDictionary("mastery", typeof(int?))]
        private Dictionary<string, int?> _mastery;
        [AMFDictionary("masteryCounters", typeof(int?))]
        private Dictionary<string, int?> _masteryCounters;
        public class PlayerMasteryPendingGift
            : AMFObject{
            [AMF("code")]
            private string _code;
            [AMF("level")]
            private int? _level;
        }
        [AMFArray("masteryPendingGifts", typeof(PlayerMasteryPendingGift))]
        private List<PlayerMasteryPendingGift> _masteryPendingGifts;
        public class PlayerToDoFinished
            : AMFObject {
            [AMF("3")]
                private int _3;

            public int Finished3
            {
                get { return _3; }
                set { _3 = value; }
            }
        }
        [AMFDictionary("toDoFinished", typeof(int?))]
        private Dictionary<string, int?> _toDoFinished;
        [AMFDictionary("toDoCounters", typeof(int?))]
        private Dictionary<string, int?> _toDoCounters;
        [AMF("toDoNextExpirationTime")]
        private double? _toDoNextExpirationTime;
        [AMF("toDoSeenCongratulationsPopup")]
        private bool? _toDoSeenCongratulationsPopup;
        [AMF("debugToDoForceUpdateNextExpirationTime")]
        private bool? _debugToDoForceUpdateNextExpirationTime;
        [AMFDictionary("collectionCounters", typeof(int?))]
        private Dictionary<string, int?> _collectionCounters;
        public class PlayerOptions :
            AMFObject
        {
            [AMF("musicDisabled")]
            private int? _musicDisabled;
            [AMF("sfxDisabled")]
            private int? _sfxDisabed;
            [AMF("graphicsLowQuality")]
            private int? _graphicsLowQuality;
        }
        [AMFObject("options", typeof(PlayerOptions))]
        private PlayerOptions _options;
        [AMF("nextBookmark")]
        private double? _nextBookmark;
        [AMF("motdVersion")]
        private string _motdVersion;
        [AMF("seenExpandFarm")]
        private int? _seenExpandFarm;
        [AMF("seenFVCash")]
        private int? _seenFVCash;
        [AMF("hasVisitFriend")]
        private int? _hasVisitFriend;
        [AMF("hasExtraFertilizer")]
        private int? _hasExtraFertilizer;
        [AMF("lastWitheredTime")]
        private double? _lastWitheredTime;
        [AMFArray("licenses", typeof(string))]
        private List<string> _licenses;
        [AMFArray("buffs", typeof(string))]
        private List<string> _buffs;
        [AMF("hasEmailPermission")]
        private bool? _hasEmailPermission;
        [AMF("seenRequestEmail")]
        private int? _seenRequestEmail;
        [AMF("hourLastSeenRequestEmail")]
        private int? _hourLastSeenRequestEmail;
        [AMF("hasReceivedCarrierPigeon")]
        private bool? _hasReceivedCarrierPigeon;
        [AMF("lastHorseStableSendTime")]
        private double? _lastHorseStableSendTime;
        [AMF("isFan")]
        private bool? _isFan;
        [AMF("lastFanCheckTime")]
        private double? _lastFanCheckTime;

        [AMFDictionary("seenFlags",typeof(bool?))]
        private Dictionary<string,bool?> _seenFlags;
        [AMFDictionary("actionCounts", typeof(int?))]
        private Dictionary<string, int?> _actionCounts;
        [AMFDictionary("featureFrequency", typeof(double?))]
        private Dictionary<string, double?> _featureFrequency;
        [AMF("lastLonelyCowCheck")]
        private int? _lastLonelyCowCheck;
        [AMF("timesSeenBuyPottingShed")]
        private int? _timesSeenBuyPottingShed;

        public class ExpandFarmLogEntry
            : AMFObject {
            [AMF("farm")]
            private string _farm;
            public string Farm
            {
                get { return _farm; }
                set { _farm = value; }
            }
            [AMF("stamp")]
            private double? _stamp;
            public double? Stamp
            {
                get { return _stamp; }
                set { _stamp = value; }
            }
            
        }
        [AMFArray("expandFarmLog", typeof(ExpandFarmLogEntry))]
        private List<ExpandFarmLogEntry> _expandFarmLog;
        [AMFArray("itemPurchasLog", typeof(string))]
        private List<string> _itemPurchaseLog;
        [AMFArray("fuelWeekData", typeof(string))]
        private List<string> _fuelWeekData;
        [AMF("exchangeLastSeen")]
        private int? _exchangeLastSeen;
        [AMFDictionary("pastVelocityAmounts", typeof(double?))]
        private Dictionary<string, double?> _pastVelocityAmounts;
        [AMF("className")]
        private string _className;
        [AMF("unwither")]
        private int? _unwither;
        [AMF("isFarmvilleFan")]
        private string _isFarmvilleFan;
        


    



        



        




    }
}
