using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public abstract class RequestObject
    {
        public abstract object[] GetParameterArray();

        public virtual FluorineFx.ASObject ToRequestObject() {
            FluorineFx.ASObject retVal = new FluorineFx.ASObject();
            retVal.Add("params", GetParameterArray());
            retVal.Add("sequence", _sequence);
            retVal.Add("functionName", _functionName);
            return retVal;
        }

        public RequestObject(int sequence, string functionName)
        {
            _sequence = sequence;
            _functionName = functionName;
        }

        private int _sequence;

        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }
        private string _functionName;

        public string FunctionName
        {
            get { return _functionName; }
            set { _functionName = value; }
        }
    }
}
