using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Classes
{
    class User
    {
        [AMF("id")]
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [AMF("session_id")]
        private float? _sessionId;

        public float? SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }

        [AMF("session_key")]
        private string _sessionKey;

        public string SessionKey
        {
            get { return _sessionKey; }
            set { _sessionKey = value; }
        }

        [AMF("is_new")]
        private bool? _isBool;

        public bool? IsBool
        {
            get { return _isBool; }
            set { _isBool = value; }
        }

        [AMF("lock_count")]
        private int? _lockCount;

        public int? LockCount
        {
            get { return _lockCount; }
            set { _lockCount = value; }
        }
        [AMF("worldName")]
        private string _worldName;

        public string WorldName
        {
            get { return _worldName; }
            set { _worldName = value; }
        }
        [AMF("dailyLotteryTimestamp")]
        private float? _dailyLotteryTimestamp;

        public float? DailyLotteryTimestamp
        {
            get { return _dailyLotteryTimestamp; }
            set { _dailyLotteryTimestamp = value; }
        }

        [AMF("dailyLotteryAmount")]
        private int? _dailyLotteryAmount;

        public int? DailyLotteryAmount
        {
            get { return _dailyLotteryAmount; }
            set { _dailyLotteryAmount = value; }
        }

        [AMF("dailyTS")]
        private float? _dailyTimestamp;

        public float? DailyTimestamp
        {
            get { return _dailyTimestamp; }
            set { _dailyTimestamp = value; }
        }

        [AMF("lastDemographicTimestamp")]
        private float? _lastDemographicTimestamp;

        public float? LastDemographicTimestamp
        {
            get { return _lastDemographicTimestamp; }
            set { _lastDemographicTimestamp = value; }
        }

        [AMF("bookmarkReward")]
        private int? _bookmarkReward;

        public int? BookmarkReward
        {
            get { return _bookmarkReward; }
            set { _bookmarkReward = value; }
        }

        [AMF("sequence")]
        private int? _sequence;

        public int? Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        [AMF("lastSaveTime")]
        private float? _lastSaveTime;

        public float? LastSaveTime
        {
            get { return _lastSaveTime; }
            set { _lastSaveTime = value; }
        }

        [AMF("firstDayTimestamp")]
        private float? _firstDayTimestamp;

        public float? FirstDayTimestamp
        {
            get { return _firstDayTimestamp; }
            set { _firstDayTimestamp = value; }
        }

        [AMF("bumperCropDayTimestamp")]
        private float? _bumperCropDayTimestamp;

        public float? BumperCropDayTimestamp
        {
            get { return _bumperCropDayTimestamp; }
            set { _bumperCropDayTimestamp = value; }
        }

        [AMF("isBumperCropDay")]
        private bool? _isBumperCropDay;

        public bool? IsBumperCropDay
        {
            get { return _isBumperCropDay; }
            set { _isBumperCropDay = value; }
        }

        [AMF("giftIdleMissionTimestamp")]
        private float? _giftIdleMissionTimestamp;

        public float? GiftIdleMissionTimestamp
        {
            get { return _giftIdleMissionTimestamp; }
            set { _giftIdleMissionTimestamp = value; }
        }

        [AMF("gotFreeCash")]
        private bool? _gotFreeCash;

        public bool? GotFreeCash
        {
            get { return _gotFreeCash; }
            set { _gotFreeCash = value; }
        }

        [AMF("bookmarkCount")]
        private int? _bookmarkCount;

        public int? BookmarkCount
        {
            get { return _bookmarkCount; }
            set { _bookmarkCount = value; }
        }

        [AMF("hasVisitedDotCom")]
        private bool? _hasVisitedDotCom;

        public bool? HasVisitedDotCom
        {
            get { return _hasVisitedDotCom; }
            set { _hasVisitedDotCom = value; }
        }

        [AMF("bookmarkedDotCom")]
        private bool? _bookmarkedDotCom;

        public bool? BookmarkedDotCom
        {
            get { return _bookmarkedDotCom; }
            set { _bookmarkedDotCom = value; }
        }

        [AMF("lastBookmarkPopupTime")]
        private float? _lastBookmarkPopupTime;

        public float? LastBookmarkPopupTime
        {
            get { return _lastBookmarkPopupTime; }
            set { _lastBookmarkPopupTime = value; }
        }

        [AMF("hasEnteredThroughFeed")]
        private bool? _hasEnteredThroughFeed;

        public bool? HasEnteredThroughFeed
        {
            get { return _hasEnteredThroughFeed; }
            set { _hasEnteredThroughFeed = value; }
        }

        [AMF("hasEnteredThroughBookmark")]
        private bool? _hasEnteredThroughBookmark;

        public bool? HasEnteredThroughBookmark
        {
            get { return _hasEnteredThroughBookmark; }
            set { _hasEnteredThroughBookmark = value; }
        }

        [AMF("hasEarnedBookmarkReward")]
        private bool? _hasEarnedBookmarkReward;

        public bool? HasEarnedBookmarkReward
        {
            get { return _hasEarnedBookmarkReward; }
            set { _hasEarnedBookmarkReward = value; }
        }

        [AMF("nybLevel")]
        private bool? _nybLevel;

        public bool? NybLevel
        {
            get { return _nybLevel; }
            set { _nybLevel = value; }
        }

        [AMF("lastExploitCheckTimestamp")]
        private float? _lastExploitTimestamp;

        public float? LastExploitTimestamp
        {
            get { return _lastExploitTimestamp; }
            set { _lastExploitTimestamp = value; }
        }

        [AMF("transfer")]
        private bool _transfer;

        public bool Transfer
        {
            get { return _transfer; }
            set { _transfer = value; }
        }

        [AMF("flashSessionKey")]
        private string _flashSessionKey;

        public string FlashSessionKey
        {
            get { return _flashSessionKey; }
            set { _flashSessionKey = value; }
        }

        public class Attributes
        {
            [AMF("lastSeenSigTime")]
            private float _lastSeenSigTime;

            public float LastSeenSigTime
            {
                get { return _lastSeenSigTime; }
                set { _lastSeenSigTime = value; }
            }

            [AMF("name")]
            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
        }

        [AMFSubObject("attr", typeof(Attributes))]
        private Attributes _attributes;

        public Attributes Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        [AMFArray("iconCodes", typeof(string))]
        private System.Collections.Generic.List<string> _iconCodes;

        public System.Collections.Generic.List<string> IconCodes
        {
            get { return _iconCodes; }
            set { _iconCodes = value; }
        }
        
    }
}
