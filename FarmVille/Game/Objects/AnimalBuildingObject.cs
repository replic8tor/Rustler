using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmVille.Game.Classes;

namespace FarmVille.Game.Objects
{
    
    public class AnimalBuildingObject
        : StorageBuilding
    {
        [AMF("friendFedAnimalBuildingCount")]
        private int? _friendFedAnimalBuildingCount;
    }
}
