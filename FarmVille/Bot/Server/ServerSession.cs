using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Bot.Server
{
    public class ServerSession
    {
        private int _sequence = 1;

        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        private double _serverTimeOffset = 0;

        public double ServerTimeOffset
        {
            get { return _serverTimeOffset; }
            set { _serverTimeOffset = value; }
        }
        private FluorineFx.Net.NetConnection _netConnection;

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
        private string _fbId;

        public string FbId
        {
            get { return _fbId; }
            set { _fbId = value; }
        }

        public ServerSession(string token, string fbId, string flashRevision = "14446")
        {
            _token = token;
            _fbId = fbId;
            _flashRevision = flashRevision;
            _netConnection = new FluorineFx.Net.NetConnection();
            _netConnection.ObjectEncoding = FluorineFx.ObjectEncoding.AMF3;
            _netConnection.AddHeader("Accept-Encoding", true, "gzip, deflate");
            _netConnection.NetStatus += new FluorineFx.Net.NetStatusHandler(_netConnection_NetStatus);
            _netConnection.Connect("http://fb-0.farmville.com/flashservices/gateway.php");

        }

        private List<FluorineFx.Messaging.Api.Service.IPendingServiceCallback> _pendingCallbacks = new List<FluorineFx.Messaging.Api.Service.IPendingServiceCallback>();

        public List<FluorineFx.Messaging.Api.Service.IPendingServiceCallback> PendingCallbacks
        {
            get { return _pendingCallbacks; }
            set { _pendingCallbacks = value; }
        }
 
        public void MakeRequest(Game.Requests.BaseRequest baseRequest, FluorineFx.Messaging.Api.Service.IPendingServiceCallback callback)
        {
            
            lock (PendingCallbacks)
                _pendingCallbacks.Add(callback);

            int sequence = _sequence++;

            
            
            _netConnection.Call(baseRequest.Target, callback, "/" + sequence, baseRequest.BuildContent(sequence, out sequence));
            _sequence = sequence;
        }
        public class BlockingCallback
            : FluorineFx.Messaging.Api.Service.IPendingServiceCallback {
                private FarmVille.Game.Requests.BaseRequest _request;
                private ServerSession _session = null;
                private bool _sucecss = false;

                public bool Success
                {
                    get { return _sucecss; }
                    set { _sucecss = value; }
                }

                public FarmVille.Game.Requests.BaseRequest Request
                {
                    get { return _request; }
                    set { _request = value; }
                }
                private FluorineFx.Messaging.Api.Service.IPendingServiceCall _result;

                public FluorineFx.Messaging.Api.Service.IPendingServiceCall Result
                {
                    get { return _result; }
                    set { _result = value; }
                }
                private System.Threading.AutoResetEvent _waitEvent;

                public System.Threading.AutoResetEvent WaitEvent
                {
                    get { return _waitEvent; }
                    set { _waitEvent = value; }
                }

                public BlockingCallback(FarmVille.Game.Requests.BaseRequest request, ServerSession session) {
                    _waitEvent = new System.Threading.AutoResetEvent(false);
                    _session = session;
                    _request = request;
                }

           

            public void ResultReceived(FluorineFx.Messaging.Api.Service.IPendingServiceCall call) {
                lock (_session.PendingCallbacks)
                    _session.PendingCallbacks.Remove(this);
                _result = call;
                FluorineFx.ASObject res = call.Result as FluorineFx.ASObject;
                if ((int)res["errorType"] != 0)
                {
                    
                    _sucecss = false;
                    _waitEvent.Set();
                    _session.RaiseError((int)res["errorType"], (string)res["errorData"], this);
                    
                    
                }
                else {
                    _sucecss = true;
                    _waitEvent.Set();
                }

                
            }
            
        }

        public BlockingCallback MakeBlockingRequest(Game.Requests.BaseRequest baseRequest)
        {
            
            BlockingCallback blocking = new BlockingCallback(baseRequest, this);
            MakeRequest(baseRequest, blocking);
            blocking.WaitEvent.WaitOne();
            if (blocking.Result == null || blocking.Result.Result == null)
            {
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "ServerSession", "Result object was null.");
                return blocking;
            }
            double serverTime = (double)((blocking.Result.Result as FluorineFx.ASObject)["serverTime"]);
            double offset = serverTime - Everworld.Utility.Time.UnixTime();
            _serverTimeOffset = offset;
            return blocking;
        }

        void _netConnection_NetStatus(object sender, FluorineFx.Net.NetStatusEventArgs e)
        {
            FarmVille.Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "ServerSession", "{0} : {1}", e.Info["level"], e.Info["code"]);
            RaiseError(33, "Forcing invalid token on net connection failure.", null);
        }
        public delegate void ErrorHandler(int type, string message, BlockingCallback obj);
        public event ErrorHandler OnInvalidToken;
        public event ErrorHandler OnNewVersion;
        public virtual void RaiseError(int type, string message, BlockingCallback obj) {
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "ServerSession", "Error raised by server. {0}: {1}", type, message);
            if (type == 33)
            {
                ClearPendingCallbacks();
                if (OnInvalidToken != null)
                    OnInvalidToken(type, message, obj);
            }
            else if (type == 10) {
                ClearPendingCallbacks();
                if (OnNewVersion != null)
                    OnNewVersion(type, message, obj);
            }
        }

        protected virtual void ClearPendingCallbacks()
        {
            lock (PendingCallbacks)
            {
                foreach (BlockingCallback callback in PendingCallbacks)
                {
                    callback.Success = false;
                    callback.WaitEvent.Set();
                }
                PendingCallbacks.Clear();
            }
        }
    }
}
