using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class InitUserSubRequest
        : RequestObject
    {
        public class InitUserSubRequestParameters {
            public string Field0 = "Nicole";
            public int Field1 = 480;
            public object[] ToArray() { 
                return new Object[] { Field0, Field1, true };
            }
        }

        private InitUserSubRequestParameters _parameters;

        private InitUserSubRequestParameters Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        public override object[] GetParameterArray()
        {
            return _parameters.ToArray();
        }
       

        public InitUserSubRequest(int sequence) : base(sequence, "UserService.initUser") {
            _parameters = new InitUserSubRequestParameters();

        }
        public InitUserSubRequest(int sequence, InitUserSubRequestParameters parameters) 
            : base(sequence,"UserService.initUser")
        {
            
            _parameters = parameters;
        }

       
    }
}
