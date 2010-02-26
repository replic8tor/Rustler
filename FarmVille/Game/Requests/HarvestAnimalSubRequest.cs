using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class HarvestAnimalSubRequest
        : RequestObject
    {
        private Game.Objects.AnimalObject _animal;

        internal Game.Objects.AnimalObject Animal
        {
            get { return _animal; }
            set { _animal = value; }
        }

        public override object[] GetParameterArray()
        {
            FluorineFx.ASObject parameter1 = new FluorineFx.ASObject();
            parameter1.Add("id", Animal.Id);
            parameter1.Add("deleted", false);
            parameter1.Add("state", "ripe");
            parameter1.Add("tempId", float.NaN);
            parameter1.Add("direction", 0);
            parameter1.Add("position", Animal.Position.ToObject());
            parameter1.Add("itemName", Animal.ItemName);
            parameter1.Add("plantTime", Animal.PlantTime);
            parameter1.Add("className", Animal.ClassName);         

            FluorineFx.ASObject parameter2 = new FluorineFx.ASObject();
            parameter2.Add("energyCost", 0);
            return new object[] { "harvest", parameter1, new object[] { parameter2 } };
        }


        public HarvestAnimalSubRequest(int sequence)
            : base(sequence, "WorldService.performAction")
        {


        }
        public HarvestAnimalSubRequest(int sequence, Game.Objects.AnimalObject animal)
            : base(sequence, "WorldService.performAction")
        {
            _animal = animal;

        }


    }
}
