using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Classes
{
    class Player
    {
        [AMF("gold")]
        private float? _gold;
        [AMF("cash")]
        private float? _cash;
        [AMF("cashEarned")]
        private float? _cashEarned;
        [AMF("cashPurchased")]
        private float? _cashPurchased;
        [AMF("cashPurchasedCPA")]
        private float? _cashPurchasedCPA;
        [AMF("averagePurchasedCashPrice")]
        private float? _averagePurchasedCashPrice;
        [AMF("averageCPACashPrice")]
        private float? _averageCPACashPrice;
        [AMF("level")]
        private int? _level;
        [AMF("xp")]
        private float? xp;
        [AMF("energyMax")]
        private int? _energyMax;
        [AMF("energy")]
        private float? _energy;
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
        private float? _lastEnergyCheck;
        [AMF("fertilizer")]
        private int? _fertilizer;
        [AMF("animalFood")]
        private int? _animalFood;
        [AMF("hadUnlimitedLights")]
        private bool? _hasUnlimitedLights;
        [AMF("numOpenedPresents")]
        private int _numOpenedPresents;
        [AMF("lastPlotCountCheck")]
        private int? _lastPlotCountCheck;
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
        [AMFDictionary("sentGifts", typeof(float?))]
        private Dictionary<string, float?> _sentGifts;
        [AMFArray("pendingNeighbors", typeof(string))]
        private System.Collections.Generic.List<string> _pendingNeighbors;
        [AMFArray("upgrades", typeof(string))]
        private System.Collections.Generic.List<string> _upgrades;
        [AMF("lastLonelyAnimalCheck")]
        private float? _lastLonelyAnimalCheck;
        [AMFArray("lootItemHistory", typeof(string))]
        private List<string> _lootItemHistroy;
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
        [AMFDictionary("externalLevels", typeof(int?))]
        private Dictionary<string, int?> _externalLevels;
        [AMF("unwitherPerson")]
        private int? _unwitherPerson;
        [AMF("witherOn")]
        private bool? _witherOn;



        




    }
}
