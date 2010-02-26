using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class PlowPlotSubRequest
        : RequestObject
    {
        private Game.Objects.PlotObject _plot;

        internal Game.Objects.PlotObject Plot
        {
            get { return _plot; }
            set { _plot = value; }
        }

        public override object[] GetParameterArray()
        {
            FluorineFx.ASObject parameter1 = new FluorineFx.ASObject();
            parameter1.Add("id", Plot.Id);
            parameter1.Add("tempId", float.NaN);
            parameter1.Add("state", "plowed");
            parameter1.Add("itemName", Plot.ItemName);
            parameter1.Add("plantTime", Plot.PlantTime);
            parameter1.Add("direction",0);
            parameter1.Add("isJumbo", Plot.IsJumbo);
            parameter1.Add("position", Plot.Position.ToObject());
            parameter1.Add("isBigPlot", Plot.IsBigPlot);
            parameter1.Add("className", Plot.ClassName);
            parameter1.Add("deleted", false);
            parameter1.Add("isProduceItem", Plot.IsProduceItem);

            FluorineFx.ASObject parameter2 = new FluorineFx.ASObject();
            parameter2.Add("energyCost", 0);
            return new object[]{ "plow", parameter1, new object[] { parameter2 } };
        }


        public PlowPlotSubRequest(int sequence)
            : base(sequence, "WorldService.performAction")
        {
            

        }
        public PlowPlotSubRequest(int sequence, Game.Objects.PlotObject plot)
            : base(sequence, "WorldService.performAction")
        {
            _plot = plot;
            
        }


    }
}
