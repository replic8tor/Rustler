using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public abstract class BaseRequest
    {
        private string _target;

        public string Target
        {
            get { return _target; }
            set { _target = value; }
        }

        private string _response;

        public string Response
        {
            get { return _response; }
            set { _response = value; }
        }

        public abstract object[] BuildContent(int baseSequence, out int finalSequence);

        public BaseRequest(string target, string response) {
            _target = target;
            _response = response;
        }
    }
}
