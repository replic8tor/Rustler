using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class LoadWorldRequest
        : RequestObject
    {
        private string _neighbor;

        public string Neighbor
        {
          get { return _neighbor; }
          set { _neighbor = value; }
        }

        public override object[] GetParameterArray()
        {
            return new object[] { _neighbor };
        }


        public LoadWorldRequest(int sequence)
            : base(sequence, "WorldService.loadWorld")
        {


        }
        public LoadWorldRequest(int sequence, string neighbor)
            : base(sequence, "WorldService.loadWorld")
        {
            _neighbor = neighbor;
        }


    }
}

