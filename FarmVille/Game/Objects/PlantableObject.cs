using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Objects
{
    public class PlantableObject
        : BaseObject
    {
        private double _plantTime;

        public double PlantTime
        {
            get { return _plantTime; }
            set { _plantTime = value; }
        }

        public override void FromObject(FluorineFx.ASObject obj)
        {
            if (obj["plantTime"] is double)
                _plantTime = ((double)(obj["plantTime"] ?? 0.0) / 1000);
            else if (obj["plantTime"] is int)
                _plantTime = (float)((int)(obj["plantTime"] ?? 0) / 1000);

            base.FromObject(obj);
        }
    }
}
