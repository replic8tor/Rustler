using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Objects
{
    [FarmObjectClass("Building")]
    public class BuildingObject
        : PlantableObject
    {
        private float _buildTime;

        public override void FromObject(FluorineFx.ASObject obj)
        {
            if (obj["buildTime"] is float)
                _buildTime = (float)obj["buildTime"];
            else if (obj["buildTime"] == null)
                _buildTime = 0;
            else 
                _buildTime = (float)(int)obj["buildTime"];
            base.FromObject(obj);
        }
    }
}
