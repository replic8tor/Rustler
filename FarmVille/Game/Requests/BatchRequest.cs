using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class BatchRequest
        : BaseRequest

    {
        private BasicSessionInfo _sessionInfo;

        public BasicSessionInfo SessionInfo
        {
            get { return _sessionInfo; }
            set { _sessionInfo = value; }
        }
        private System.Collections.Generic.List<RequestObject> _batchedRequests = new List<RequestObject>();

        public System.Collections.Generic.List<RequestObject> BatchedRequests
        {
            get { return _batchedRequests; }
            set { _batchedRequests = value; }
        }
        private float _field2 = 0;

        public float Field2
        {
            get { return _field2; }
            set { _field2 = value; }
        }
        public BatchRequest(string response) : base("BaseService.dispatchBatch", response) { }

        public override object[] BuildContent( int baseSequence, out int finalSequence)
        {
            int sequence = baseSequence;
            System.Collections.ArrayList batchObjects = new System.Collections.ArrayList();
            foreach (RequestObject req in _batchedRequests)
            {
                req.Sequence = sequence++;
                batchObjects.Add(req.ToRequestObject());
            }
            finalSequence = sequence;
            return new object[] { _sessionInfo.ToRequestObject(), batchObjects.ToArray(), _field2  };
        }
    }
}
