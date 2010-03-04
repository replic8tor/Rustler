using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Classes
{
    class InitUserResponseData
        : AMFObject
    {
        [AMFObject("userInfo", typeof(User))]
        private User _userInfo;

        internal User UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }

        public class NeighborInfo
            : AMFObject {
            [AMF("uid")]
            private string _uid;
            [AMF("level")]
            private double? _level;
            [AMF("xp")]
            private double? _xp;
            [AMF("gold")]
            private double? _gold;
            [AMF("valentinesReceived")]
            private int? _valentinesReceived;
            //[AMFObject("avatar", typeof(User.UserAvatar))]
            //private User.UserAvatar _avatar;
        }
        [AMFArray("neighbors", typeof(NeighborInfo))]
        private List<NeighborInfo> _neighbors;
    }
}
