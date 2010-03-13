using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Classes
{
    public class Player :
        AMFObject
    {
        [AMF("gold")]
        private double? _gold;

        public double? Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        [AMF("cash")]
        private double? _cash;

        public double? Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }
        [AMF("cashEarned")]
        private double? _cashEarned;

        public double? CashEarned
        {
            get { return _cashEarned; }
            set { _cashEarned = value; }
        }
        [AMF("cashPurchased")]
        private double? _cashPurchased;

        public double? CashPurchased
        {
            get { return _cashPurchased; }
            set { _cashPurchased = value; }
        }
        [AMF("cashPurchasedCPA")]
        private double? _cashPurchasedCPA;

        public double? CashPurchasedCPA
        {
            get { return _cashPurchasedCPA; }
            set { _cashPurchasedCPA = value; }
        }
        [AMF("averagePurchasedCashPrice")]
        private double? _averagePurchasedCashPrice;

        public double? AveragePurchasedCashPrice
        {
            get { return _averagePurchasedCashPrice; }
            set { _averagePurchasedCashPrice = value; }
        }
        [AMF("averageCPACashPrice")]
        private double? _averageCPACashPrice;

        public double? AverageCPACashPrice
        {
            get { return _averageCPACashPrice; }
            set { _averageCPACashPrice = value; }
        }
        [AMF("level")]
        private double? _level;

        public double? Level
        {
            get { return _level; }
            set { _level = value; }
        }
        [AMF("xp")]
        private double? xp;

        public double? Xp
        {
            get { return xp; }
            set { xp = value; }
        }
        [AMF("energyMax")]
        private double? _energyMax;

        public double? EnergyMax
        {
            get { return _energyMax; }
            set { _energyMax = value; }
        }
        [AMF("energy")]
        private double? _energy;

        public double? Energy
        {
            get { return _energy; }
            set { _energy = value; }
        }
        [AMF("goldPurchasedTotal")]
        private int? _goldPurchasedTotal;

        public int? GoldPurchasedTotal
        {
            get { return _goldPurchasedTotal; }
            set { _goldPurchasedTotal = value; }
        }
        [AMF("goldSurplus")]
        private int? _goldSurplus;

        public int? GoldSurplus
        {
            get { return _goldSurplus; }
            set { _goldSurplus = value; }
        }
        [AMF("cashPurchasedTotal")]
        private int? _cashPurchasedTotal;

        public int? CashPurchasedTotal
        {
            get { return _cashPurchasedTotal; }
            set { _cashPurchasedTotal = value; }
        }
        [AMFArray("transactionLog", typeof(string))]
        private System.Collections.Generic.List<string> _transactionLog;

        public System.Collections.Generic.List<string> TransactionLog
        {
            get { return _transactionLog; }
            set { _transactionLog = value; }
        }
        [AMF("numCashTransactions")]
        private int? _numCashTransactions;

        public int? NumCashTransactions
        {
            get { return _numCashTransactions; }
            set { _numCashTransactions = value; }
        }
        [AMF("numCPATransactions")]
        private int? _numCPATransactions;

        public int? NumCPATransactions
        {
            get { return _numCPATransactions; }
            set { _numCPATransactions = value; }
        }
        [AMF("lastEnergyCheck")]
        private double? _lastEnergyCheck;

        public double? LastEnergyCheck
        {
            get { return _lastEnergyCheck; }
            set { _lastEnergyCheck = value; }
        }
        [AMF("fertilizer")]
        private int? _fertilizer;

        public int? Fertilizer
        {
            get { return _fertilizer; }
            set { _fertilizer = value; }
        }
        [AMF("animalFood")]
        private int? _animalFood;

        public int? AnimalFood
        {
            get { return _animalFood; }
            set { _animalFood = value; }
        }
        [AMF("hadUnlimitedLights")]
        private bool? _hasUnlimitedLights;

        public bool? HasUnlimitedLights
        {
            get { return _hasUnlimitedLights; }
            set { _hasUnlimitedLights = value; }
        }
        [AMF("numOpenedPresents")]
        private int _numOpenedPresents;

        public int NumOpenedPresents
        {
            get { return _numOpenedPresents; }
            set { _numOpenedPresents = value; }
        }
        [AMF("lastPlotCountCheck")]
        private double? _lastPlotCountCheck;

        public double? LastPlotCountCheck
        {
            get { return _lastPlotCountCheck; }
            set { _lastPlotCountCheck = value; }
        }
        [AMF("valentinesCredits")]
        private int? _valentinesCredits;

        public int? ValentinesCredits
        {
            get { return _valentinesCredits; }
            set { _valentinesCredits = value; }
        }
        [AMF("valentinesCreditsReceived")]
        private int? _valentinesCreditsReceived;

        public int? ValentinesCreditsReceived
        {
            get { return _valentinesCreditsReceived; }
            set { _valentinesCreditsReceived = value; }
        }
        [AMF("timezoneOffset")]
        private int? _timezoneOffset;

        public int? TimezoneOffset
        {
            get { return _timezoneOffset; }
            set { _timezoneOffset = value; }
        }
        [AMF("timezoneOffsetLastUpdate")]
        private int? _timezoneOffsetLastUpdate;

        public int? TimezoneOffsetLastUpdate
        {
            get { return _timezoneOffsetLastUpdate; }
            set { _timezoneOffsetLastUpdate = value; }
        }
        [AMFArray("promos", typeof(string))]
        private System.Collections.Generic.List<string> _promos;

        public System.Collections.Generic.List<string> Promos
        {
            get { return _promos; }
            set { _promos = value; }
        }
        [AMFArray("neighbors", typeof(string))]
        private System.Collections.Generic.List<string> _neighbors;

        public System.Collections.Generic.List<string> Neighbors
        {
            get { return _neighbors; }
            set { _neighbors = value; }
        }
        [AMFArray("sentGifts", typeof(double?))]
        private Dictionary<string, double?> _sentGifts;

        public Dictionary<string, double?> SentGifts
        {
            get { return _sentGifts; }
            set { _sentGifts = value; }
        }
        [AMFArray("pendingNeighbors", typeof(string))]
        private System.Collections.Generic.List<string> _pendingNeighbors;

        public System.Collections.Generic.List<string> PendingNeighbors
        {
            get { return _pendingNeighbors; }
            set { _pendingNeighbors = value; }
        }
        [AMFArray("upgrades", typeof(string))]
        private System.Collections.Generic.List<string> _upgrades;

        public System.Collections.Generic.List<string> Upgrades
        {
            get { return _upgrades; }
            set { _upgrades = value; }
        }
        [AMFArray("lonelyAnimals", typeof(string))]
        private System.Collections.Generic.List<string> _lonelyAnimals;

        public System.Collections.Generic.List<string> LonelyAnimals
        {
            get { return _lonelyAnimals; }
            set { _lonelyAnimals = value; }
        }
        [AMF("lastLonelyAnimalCheck")]
        private double? _lastLonelyAnimalCheck;

        public double? LastLonelyAnimalCheck
        {
            get { return _lastLonelyAnimalCheck; }
            set { _lastLonelyAnimalCheck = value; }
        }

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

        public PlayerLootItemHistory LootItemHistory
        {
            get { return _lootItemHistory; }
            set { _lootItemHistory = value; }
        }
        [AMFArray("friendsFertilized", typeof(string))]
        private List<string> _friendsFertilized;

        public List<string> FriendsFertilized
        {
            get { return _friendsFertilized; }
            set { _friendsFertilized = value; }
        }
        [AMF("totalFriendsFertilized")]
        private int? _totalFriendsFertilized;

        public int? TotalFriendsFertilized
        {
            get { return _totalFriendsFertilized; }
            set { _totalFriendsFertilized = value; }
        }
        [AMFArray("friendsFedAnimals", typeof(string))]
        private List<string> _friendsFedAnimals;

        public List<string> FriendsFedAnimals
        {
            get { return _friendsFedAnimals; }
            set { _friendsFedAnimals = value; }
        }
        [AMF("totalFriendsFedAnimals")]
        private int? _totalFriendsFedAnimals;

        public int? TotalFriendsFedAnimals
        {
            get { return _totalFriendsFedAnimals; }
            set { _totalFriendsFedAnimals = value; }
        }
        [AMFArray("bumperCrops", typeof(string))]
        private List<string> _bumperCrops;

        public List<string> BumperCrops
        {
            get { return _bumperCrops; }
            set { _bumperCrops = value; }
        }
        [AMFArray("presents", typeof(string))]
        private List<string> _presents;

        public List<string> Presents
        {
            get { return _presents; }
            set { _presents = value; }
        }
        public class PlayerExternalLevels :
            AMFObject 
        {
            [AMF("mafiawars")]
            private int? _mafiaWars;

            public int? MafiaWars
            {
                get { return _mafiaWars; }
                set { _mafiaWars = value; }
            }
            [AMF("seederhotrod")]
            private int? _seederHotRod;

            public int? SeederHotRod
            {
                get { return _seederHotRod; }
                set { _seederHotRod = value; }
            }
        }
        [AMFObject("externalLevels", typeof(PlayerExternalLevels))]
        private PlayerExternalLevels _externalLevels;

        public PlayerExternalLevels ExternalLevels
        {
            get { return _externalLevels; }
            set { _externalLevels = value; }
        }
        [AMF("unwitherPerson")]
        private int? _unwitherPerson;

        public int? UnwitherPerson
        {
            get { return _unwitherPerson; }
            set { _unwitherPerson = value; }
        }
        [AMF("witherOn")]
        private bool? _witherOn;

        public bool? WitherOn
        {
            get { return _witherOn; }
            set { _witherOn = value; }
        }
        [AMFDictionary("achievements", typeof(int?))]
        private Dictionary<string, int?> _achievements;

        public Dictionary<string, int?> Achievements
        {
            get { return _achievements; }
            set { _achievements = value; }
        }
        [AMFDictionary("achCounters", typeof(double?))]
        private Dictionary<string, double?> _achCounters;

        public Dictionary<string, double?> AchCounters
        {
            get { return _achCounters; }
            set { _achCounters = value; }
        }
        private Dictionary<string, List<string>> _achUniques;

        public Dictionary<string, List<string>> AchUniques
        {
            get { return _achUniques; }
            set { _achUniques = value; }
        }

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

        public List<string> AchPassive
        {
            get { return _achPassive; }
            set { _achPassive = value; }
        }
        [AMFDictionary("mastery", typeof(int?))]
        private Dictionary<string, int?> _mastery;

        public Dictionary<string, int?> Mastery
        {
            get { return _mastery; }
            set { _mastery = value; }
        }
        [AMFDictionary("masteryCounters", typeof(int?))]
        private Dictionary<string, int?> _masteryCounters;

        public Dictionary<string, int?> MasteryCounters1
        {
            get { return _masteryCounters; }
            set { _masteryCounters = value; }
        }
        public class PlayerMasteryPendingGift
            : AMFObject{
            [AMF("code")]
                private string _code;

            public string Code
            {
                get { return _code; }
                set { _code = value; }
            }
            [AMF("level")]
            private int? _level;

            public int? Level
            {
                get { return _level; }
                set { _level = value; }
            }
        }
        [AMFArray("masteryPendingGifts", typeof(PlayerMasteryPendingGift))]
        private List<PlayerMasteryPendingGift> _masteryPendingGifts;

        public List<PlayerMasteryPendingGift> MasteryPendingGifts
        {
            get { return _masteryPendingGifts; }
            set { _masteryPendingGifts = value; }
        }
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

        public Dictionary<string, int?> ToDoFinished
        {
            get { return _toDoFinished; }
            set { _toDoFinished = value; }
        }
        [AMFDictionary("toDoCounters", typeof(int?))]
        private Dictionary<string, int?> _toDoCounters;

        public Dictionary<string, int?> ToDoCounters
        {
            get { return _toDoCounters; }
            set { _toDoCounters = value; }
        }
        [AMF("toDoNextExpirationTime")]
        private double? _toDoNextExpirationTime;

        public double? ToDoNextExpirationTime
        {
            get { return _toDoNextExpirationTime; }
            set { _toDoNextExpirationTime = value; }
        }
        [AMF("toDoSeenCongratulationsPopup")]
        private bool? _toDoSeenCongratulationsPopup;

        public bool? ToDoSeenCongratulationsPopup
        {
            get { return _toDoSeenCongratulationsPopup; }
            set { _toDoSeenCongratulationsPopup = value; }
        }
        [AMF("debugToDoForceUpdateNextExpirationTime")]
        private bool? _debugToDoForceUpdateNextExpirationTime;

        public bool? DebugToDoForceUpdateNextExpirationTime
        {
            get { return _debugToDoForceUpdateNextExpirationTime; }
            set { _debugToDoForceUpdateNextExpirationTime = value; }
        }
        [AMFDictionary("collectionCounters", typeof(int?))]
        private Dictionary<string, int?> _collectionCounters;

        public Dictionary<string, int?> CollectionCounters
        {
            get { return _collectionCounters; }
            set { _collectionCounters = value; }
        }
        public class PlayerOptions :
            AMFObject
        {
            [AMF("musicDisabled")]
            private int? _musicDisabled;

            public int? MusicDisabled
            {
                get { return _musicDisabled; }
                set { _musicDisabled = value; }
            }
            [AMF("sfxDisabled")]
            private int? _sfxDisabed;

            public int? SfxDisabed
            {
                get { return _sfxDisabed; }
                set { _sfxDisabed = value; }
            }
            [AMF("graphicsLowQuality")]
            private int? _graphicsLowQuality;

            public int? GraphicsLowQuality
            {
                get { return _graphicsLowQuality; }
                set { _graphicsLowQuality = value; }
            }
        }
        [AMFObject("options", typeof(PlayerOptions))]
        private PlayerOptions _options;

        public PlayerOptions Options
        {
            get { return _options; }
            set { _options = value; }
        }
        [AMF("nextBookmark")]
        private double? _nextBookmark;

        public double? NextBookmark
        {
            get { return _nextBookmark; }
            set { _nextBookmark = value; }
        }
        [AMF("motdVersion")]
        private string _motdVersion;

        public string MotdVersion
        {
            get { return _motdVersion; }
            set { _motdVersion = value; }
        }
        [AMF("seenExpandFarm")]
        private int? _seenExpandFarm;

        public int? SeenExpandFarm
        {
            get { return _seenExpandFarm; }
            set { _seenExpandFarm = value; }
        }
        [AMF("seenFVCash")]
        private int? _seenFVCash;

        public int? SeenFVCash
        {
            get { return _seenFVCash; }
            set { _seenFVCash = value; }
        }
        [AMF("hasVisitFriend")]
        private int? _hasVisitFriend;

        public int? HasVisitFriend
        {
            get { return _hasVisitFriend; }
            set { _hasVisitFriend = value; }
        }
        [AMF("hasExtraFertilizer")]
        private int? _hasExtraFertilizer;

        public int? HasExtraFertilizer
        {
            get { return _hasExtraFertilizer; }
            set { _hasExtraFertilizer = value; }
        }
        [AMF("lastWitheredTime")]
        private double? _lastWitheredTime;

        public double? LastWitheredTime
        {
            get { return _lastWitheredTime; }
            set { _lastWitheredTime = value; }
        }
        [AMFArray("licenses", typeof(string))]
        private List<string> _licenses;

        public List<string> Licenses
        {
            get { return _licenses; }
            set { _licenses = value; }
        }
        [AMFArray("buffs", typeof(string))]
        private List<string> _buffs;

        public List<string> Buffs
        {
            get { return _buffs; }
            set { _buffs = value; }
        }
        [AMF("hasEmailPermission")]
        private bool? _hasEmailPermission;

        public bool? HasEmailPermission
        {
            get { return _hasEmailPermission; }
            set { _hasEmailPermission = value; }
        }
        [AMF("seenRequestEmail")]
        private int? _seenRequestEmail;

        public int? SeenRequestEmail
        {
            get { return _seenRequestEmail; }
            set { _seenRequestEmail = value; }
        }
        [AMF("hourLastSeenRequestEmail")]
        private int? _hourLastSeenRequestEmail;

        public int? HourLastSeenRequestEmail
        {
            get { return _hourLastSeenRequestEmail; }
            set { _hourLastSeenRequestEmail = value; }
        }
        [AMF("hasReceivedCarrierPigeon")]
        private bool? _hasReceivedCarrierPigeon;

        public bool? HasReceivedCarrierPigeon
        {
            get { return _hasReceivedCarrierPigeon; }
            set { _hasReceivedCarrierPigeon = value; }
        }
        [AMF("lastHorseStableSendTime")]
        private double? _lastHorseStableSendTime;

        public double? LastHorseStableSendTime
        {
            get { return _lastHorseStableSendTime; }
            set { _lastHorseStableSendTime = value; }
        }
        [AMF("isFan")]
        private bool? _isFan;

        public bool? IsFan
        {
            get { return _isFan; }
            set { _isFan = value; }
        }
        [AMF("lastFanCheckTime")]
        private double? _lastFanCheckTime;

        public double? LastFanCheckTime
        {
            get { return _lastFanCheckTime; }
            set { _lastFanCheckTime = value; }
        }

        [AMFDictionary("seenFlags",typeof(bool?))]
        private Dictionary<string, bool?> _seenFlags;

        public Dictionary<string, bool?> SeenFlags
        {
            get { return _seenFlags; }
            set { _seenFlags = value; }
        }
        [AMFDictionary("actionCounts", typeof(int?))]
        private Dictionary<string, int?> _actionCounts;

        public Dictionary<string, int?> ActionCounts
        {
            get { return _actionCounts; }
            set { _actionCounts = value; }
        }
        [AMFDictionary("featureFrequency", typeof(double?))]
        private Dictionary<string, double?> _featureFrequency;

        public Dictionary<string, double?> FeatureFrequency
        {
            get { return _featureFrequency; }
            set { _featureFrequency = value; }
        }
        [AMF("lastLonelyCowCheck")]
        private int? _lastLonelyCowCheck;

        public int? LastLonelyCowCheck
        {
            get { return _lastLonelyCowCheck; }
            set { _lastLonelyCowCheck = value; }
        }
        [AMF("timesSeenBuyPottingShed")]
        private int? _timesSeenBuyPottingShed;

        public int? TimesSeenBuyPottingShed
        {
            get { return _timesSeenBuyPottingShed; }
            set { _timesSeenBuyPottingShed = value; }
        }

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

        public List<ExpandFarmLogEntry> ExpandFarmLog
        {
            get { return _expandFarmLog; }
            set { _expandFarmLog = value; }
        }
        [AMFArray("itemPurchaseLog", typeof(string))]
        private List<string> _itemPurchaseLog;

        public List<string> ItemPurchaseLog
        {
            get { return _itemPurchaseLog; }
            set { _itemPurchaseLog = value; }
        }
        [AMFArray("fuelWeekData", typeof(string))]
        private List<string> _fuelWeekData;

        public List<string> FuelWeekData
        {
            get { return _fuelWeekData; }
            set { _fuelWeekData = value; }
        }
        [AMF("exchangeLastSeen")]
        private int? _exchangeLastSeen;

        public int? ExchangeLastSeen
        {
            get { return _exchangeLastSeen; }
            set { _exchangeLastSeen = value; }
        }
        [AMFDictionary("pastVelocityAmounts", typeof(double?))]
        private Dictionary<string, double?> _pastVelocityAmounts;

        public Dictionary<string, double?> PastVelocityAmounts
        {
            get { return _pastVelocityAmounts; }
            set { _pastVelocityAmounts = value; }
        }
        [AMF("className")]
        private string _className;

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        [AMF("unwither")]
        private int? _unwither;

        public int? Unwither
        {
            get { return _unwither; }
            set { _unwither = value; }
        }
        [AMF("isFarmvilleFan")]
        private string _isFarmvilleFan;

        public string IsFarmvilleFan
        {
            get { return _isFarmvilleFan; }
            set { _isFarmvilleFan = value; }
        }

        public Dictionary<string, int> MasteryCounters = new Dictionary<string, int>();
        public int CountToMastery(string seedName)
        {
            if (!Game.Settings.SeedSetting.SeedSettings[seedName].Mastery)
                return 0;
            if (!MasteryCounters.ContainsKey(seedName))
                return Game.Settings.SeedSetting.SeedSettings[seedName].Mastery2Count;

            int count = Game.Settings.SeedSetting.SeedSettings[seedName].Mastery2Count;

            count -= (int)_masteryCounters[Game.Settings.SeedSetting.SeedSettings[seedName].Code];

            return count;
        }
        

    }
}
