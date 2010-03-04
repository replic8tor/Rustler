using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmVille.Game.Classes;

namespace FarmVille.Game.Objects
{
    [FarmObjectClass("StorageBuilding")]
    public class StorageBuilding
        : BuildingObject
    {
        [AMFDictionary("contents", typeof(int?))]
        private Dictionary<string, int?> _contents;       
    }
}
