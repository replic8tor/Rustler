using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmVille.Game.Classes;

namespace FarmVille.Game.Objects
{
    [AMFConstructableObject("StorageBuilding")]
    public class StorageBuilding
        : BuildingObject
    {
        public class StorageContents
            : AMFObject {
            [AMF("itemCode")]
            private string _itemCode;
            [AMF("numItem")]
            private int? _numItem;

        }
        [AMFArray("contents", typeof(StorageContents))]
        private List<StorageContents> _contents;       
    }
}
