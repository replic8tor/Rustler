using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Classes
{
    class User
        : AMFObject
    {
        [AMF("id")]
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [AMF("session_id")]
        private double? _sessionId;

        public double? SessionId
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
        private double? _dailyLotteryTimestamp;

        public double? DailyLotteryTimestamp
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
        private double? _dailyTimestamp;

        public double? DailyTimestamp
        {
            get { return _dailyTimestamp; }
            set { _dailyTimestamp = value; }
        }

        [AMF("lastDemographicTimestamp")]
        private double? _lastDemographicTimestamp;

        public double? LastDemographicTimestamp
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
        private double? _lastSaveTime;

        public double? LastSaveTime
        {
            get { return _lastSaveTime; }
            set { _lastSaveTime = value; }
        }

        [AMF("firstDayTimestamp")]
        private double? _firstDayTimestamp;

        public double? FirstDayTimestamp
        {
            get { return _firstDayTimestamp; }
            set { _firstDayTimestamp = value; }
        }

        [AMF("bumperCropDayTimestamp")]
        private double? _bumperCropDayTimestamp;

        public double? BumperCropDayTimestamp
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
        private double? _giftIdleMissionTimestamp;

        public double? GiftIdleMissionTimestamp
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
        private double? _lastBookmarkPopupTime;

        public double? LastBookmarkPopupTime
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
        private double? _lastExploitTimestamp;

        public double? LastExploitTimestamp
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
        [AMFObject("avatar", typeof(UserAvatar))]
        private UserAvatar _avatar;

        public UserAvatar Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        public class UserAvatar
            : AMFObject
        {
            public class p0
                : AMFObject {
                [AMF("c")]
                private string _c;

                public string C
                {
                    get { return _c; }
                    set { _c = value; }
                }
                [AMF("f")]
                private string _f;

                public string F
                {
                    get { return _f; }
                    set { _f = value; }
                }
                [AMF("i")]
                private string _i;

                public string I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p0", typeof(p0))]
            private p0 _p0;

            public p0 P0
            {
                get { return _p0; }
                set { _p0 = value; }
            }

            public class p4
                : AMFObject
            {
                [AMF("m")]
                private string _m;

                public string M
                {
                    get { return _m; }
                    set { _m = value; }
                }
                [AMF("f")]
                private string _f;

                public string F
                {
                    get { return _f; }
                    set { _f = value; }
                }
                [AMF("i")]
                private string _i;

                public string I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p4", typeof(p4))]
            private p4 _p4;

            public p4 P4
            {
                get { return _p4; }
                set { _p4 = value; }
            }

            public class p5
                : AMFObject
            {
                [AMF("m")]
                private string _m;

                public string M
                {
                    get { return _m; }
                    set { _m = value; }
                }
                [AMF("f")]
                private string _f;

                public string F
                {
                    get { return _f; }
                    set { _f = value; }
                }
                [AMF("i")]
                private string _i;

                public string I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p5", typeof(p5))]
            private p5 _p5;

            public p5 P5
            {
                get { return _p5; }
                set { _p5 = value; }
            }

            [AMF("v")]
            private string _v;

            public string V
            {
                get { return _v; }
                set { _v = value; }
            }

            public class p8
                : AMFObject
            {
                [AMF("m")]
                private string _m;

                public string M
                {
                    get { return _m; }
                    set { _m = value; }
                }
                [AMF("c")]
                private string _c;

                public string C
                {
                    get { return _c; }
                    set { _c = value; }
                }
                [AMF("f")]
                private string _f;

                public string F
                {
                    get { return _f; }
                    set { _f = value; }
                }
                [AMF("i")]
                private string _i;

                public string I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p8", typeof(p8))]
            private p8 _p8;

            public p8 P8
            {
                get { return _p8; }
                set { _p8 = value; }
            }

            [AMF("g")]
            private string _g;

            public string G
            {
                get { return _g; }
                set { _g = value; }
            }

            public class p7
                : AMFObject
            {
                
                [AMF("c")]
                private string _c;

                public string C
                {
                    get { return _c; }
                    set { _c = value; }
                }
                [AMF("f")]
                private string _f;

                public string F
                {
                    get { return _f; }
                    set { _f = value; }
                }
                [AMF("i")]
                private string _i;

                public string I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p7", typeof(p7))]
            private p7 _p7;

            public p7 P7
            {
                get { return _p7; }
                set { _p7 = value; }
            }

            public class p3
                : AMFObject
            {
                [AMF("i")]
                private double? _i;

                public double? I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p3", typeof(p3))]
            private p3 _p3;

            public p3 P3
            {
                get { return _p3; }
                set { _p3 = value; }
            }

            public class p6
              : AMFObject
            {
                [AMF("i")]
                private double? _i;

                public double? I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p6", typeof(p6))]
            private p6 _p6;

            public p6 P6
            {
                get { return _p6; }
                set { _p6 = value; }
            }

            public class p9
              : AMFObject
            {
                [AMF("i")]
                private double? _i;

                public double? I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p9", typeof(p9))]
            private p9 _p9;

            public p9 P9
            {
                get { return _p9; }
                set { _p9 = value; }
            }

            public class p2
              : AMFObject
            {
                [AMF("i")]
                private double? _i;

                public double? I
                {
                    get { return _i; }
                    set { _i = value; }
                }

            }

            [AMFObject("p2", typeof(p2))]
            private p2 _p2;

            public p2 P2
            {
                get { return _p2; }
                set { _p2 = value; }
            }




        }
        public class UserAttributes
            : AMFObject
        {
            public override void FromAMF(FluorineFx.ASObject obj)
            {
                base.FromAMF(obj);
            }

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

        [AMFObject("attr", typeof(UserAttributes))]
        private UserAttributes _attributes;

        internal UserAttributes Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        [AMFObject("player",typeof(Player))]
        private Player _player;

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }


        

        [AMFArray("iconCodes", typeof(string))]
        private System.Collections.Generic.List<string> _iconCodes;

        public System.Collections.Generic.List<string> IconCodes
        {
            get { return _iconCodes; }
            set { _iconCodes = value; }
        }

        [AMFObject("world", typeof(World))]
        private World _world;

        internal World World
        {
            get { return _world; }
            set { _world = value; }
        }
        
    }
}
