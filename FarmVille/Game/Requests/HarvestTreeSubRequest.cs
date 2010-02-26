using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class HarvestTreeSubRequest
        : RequestObject
    {
        private Game.Objects.TreeObject _tree;

        internal Game.Objects.TreeObject Tree
        {
            get { return _tree; }
            set { _tree = value; }
        }

        public override object[] GetParameterArray()
        {
            FluorineFx.ASObject parameter1 = new FluorineFx.ASObject();
            parameter1.Add("id", Tree.Id);
            parameter1.Add("deleted", false);
            parameter1.Add("state", "ripe");
            parameter1.Add("tempId", float.NaN);
            parameter1.Add("direction", 0);
            parameter1.Add("position", Tree.Position.ToObject());
            parameter1.Add("itemName", Tree.ItemName);
            parameter1.Add("plantTime", Tree.PlantTime);
            parameter1.Add("className", Tree.ClassName);      

            FluorineFx.ASObject parameter2 = new FluorineFx.ASObject();
            parameter2.Add("energyCost", 0);
            return new object[] { "harvest", parameter1, new object[] { parameter2 } };
        }


        public HarvestTreeSubRequest(int sequence)
            : base(sequence, "WorldService.performAction")
        {


        }
        public HarvestTreeSubRequest(int sequence, Game.Objects.TreeObject tree)
            : base(sequence, "WorldService.performAction")
        {
            _tree = tree;

        }


    }
}
