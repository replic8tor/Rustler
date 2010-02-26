using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluorineFx;

namespace FarmVille.Game.Requests
{
    public class BasicSessionInfo
    {
        public FluorineFx.ASObject ToRequestObject() {
            FluorineFx.ASObject retVal = new ASObject();
            retVal.Add("sigTime", _sigTime);
            retVal.Add("token", _token);
            retVal.Add("flashRevision", _flashRevision);
            retVal.Add("userId", _userId);
            return retVal;
        }
        public BasicSessionInfo(float sigTime, string token, string flashRevision, string userId) {
            _sigTime = sigTime;
            _token = token;
            _flashRevision = flashRevision;
            _userId = userId;
        }

        public BasicSessionInfo() {
            _sigTime = float.NaN;
            _token = "";
            _flashRevision = "13624";
            _userId = "0";
        }

        private float _sigTime;

        public float SigTime
        {
            get { return _sigTime; }
            set { _sigTime = value; }
        }
        private string _token;

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string _flashRevision;

        public string FlashRevision
        {
            get { return _flashRevision; }
            set { _flashRevision = value; }
        }
        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

    }
}
