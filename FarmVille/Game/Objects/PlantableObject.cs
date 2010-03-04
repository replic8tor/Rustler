using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmVille.Game.Classes;

namespace FarmVille.Game.Objects
{
    public class PlantableObject
        : BaseObject
    {
        [AMF("plantTime")]
        private double? _plantTime;

        public double? PlantTime
        {
            get { return _plantTime; }
            set { _plantTime = value; }
        }
    }
}
