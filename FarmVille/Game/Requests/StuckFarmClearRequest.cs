using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class StuckFarmClearRequest
        : RequestObject
    {
        private Game.Objects.PlotObject _plot;

        internal Game.Objects.PlotObject Plot
        {
            get { return _plot; }
            set { _plot = value; }
        }

        private int _id;

        public override object[] GetParameterArray()
        {

            
   
   
   
   

            FluorineFx.ASObject parameter1 = new FluorineFx.ASObject();
            parameter1.Add("id",_id);
            parameter1.Add("tempId", float.NaN);
            parameter1.Add("state", "plowed");
            parameter1.Add("itemName", "");
            parameter1.Add("plantTime", float.NaN);
            parameter1.Add("direction", 0);
            parameter1.Add("position", new Game.Objects.Position { X=0, Y=0,Z=0 });
            parameter1.Add("className", "Plot");            

            return new object[] { "clear", parameter1, new object[] { } };
        }


        public StuckFarmClearRequest(int sequence)
            : base(sequence, "WorldService.performAction")
        {


        }
        public StuckFarmClearRequest(int sequence, int id)
            : base(sequence, "WorldService.performAction")
        {
            _id = id;
        }


    }
}

