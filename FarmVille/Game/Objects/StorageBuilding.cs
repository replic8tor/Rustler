using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Objects
{
    [FarmObjectClass("StorageBuilding")]
    public class StorageBuilding
        : BuildingObject
    {
        private Dictionary<string, int> _contents = new Dictionary<string, int>();
        public override void  FromObject(FluorineFx.ASObject obj)
        {
            object[] contents = obj["contents"] as object[];
            if (contents != null)
            {
                foreach (object o in contents)
                {
                    FluorineFx.ASObject contentObj = o as FluorineFx.ASObject;
                    _contents.Add((string)contentObj["itemCode"], (int)contentObj["numItem"]);
                }
            }
 	        base.FromObject(obj);
        }
       
    }
}
