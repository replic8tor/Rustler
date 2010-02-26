using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Requests
{
    public class PlantPlotSubRequest
        : RequestObject
    {
        private Game.Objects.PlotObject _plot;

        internal Game.Objects.PlotObject Plot
        {
            get { return _plot; }
            set { _plot = value; }
        }

        public string PlantRequest;
        public float SentPlantTime = 0;
        public override object[] GetParameterArray()
        {
            FluorineFx.ASObject parameter1 = new FluorineFx.ASObject();
            parameter1.Add("id", Plot.Id);
            parameter1.Add("tempId", float.NaN);
            parameter1.Add("state", "planted");
            parameter1.Add("itemName", PlantRequest);
            parameter1.Add("plantTime", (float)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000));
            SentPlantTime = (float)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);
            parameter1.Add("direction",0);
            parameter1.Add("isJumbo", Plot.IsJumbo);
            parameter1.Add("position", Plot.Position.ToObject());
            parameter1.Add("isBigPlot", Plot.IsBigPlot);
            parameter1.Add("className", Plot.ClassName);
            parameter1.Add("deleted", false);
            parameter1.Add("isProduceItem", Plot.IsProduceItem);

            FluorineFx.ASObject parameter2 = new FluorineFx.ASObject();
            parameter2.Add("energyCost", 0);
            parameter2.Add("isGift", false);
            return new object[]{ "place", parameter1, new object[] { parameter2 } };
        }


        public PlantPlotSubRequest(int sequence)
            : base(sequence, "WorldService.performAction")
        {
            

        }
        public PlantPlotSubRequest(int sequence, Game.Objects.PlotObject plot, string plantName )
            : base(sequence, "WorldService.performAction")
        {
            _plot = plot;
            PlantRequest = plantName;
        }


    }
}
