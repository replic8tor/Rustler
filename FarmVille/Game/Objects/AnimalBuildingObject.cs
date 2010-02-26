using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Objects
{
    
    public class AnimalBuildingObject
        : StorageBuilding
    {
        private int _friendFedAnimalBuildingCount;
        

        public override void FromObject(FluorineFx.ASObject obj)
        {
            _friendFedAnimalBuildingCount = (int)obj["friendFedAnimalBuildingCount"];
            
            base.FromObject(obj);
        }
    }
}
