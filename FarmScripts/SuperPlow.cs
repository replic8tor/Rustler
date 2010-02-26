using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Bot.Scripts
{
    public class SuperPlow
        : Script
    {

        public class PlotDefinition {
            public int X = 0;
            public int Y = 0;
            public int PlotCount = 0;
        }

        private List<PlotDefinition> _plotDefinitions = new List<PlotDefinition>();

        private List<PlotDefinition> PlotSettingsFromString(string str)
        {
            List<PlotDefinition> list = new List<PlotDefinition>();
            string[] plotsetting = str.Split(',');
            foreach (string setting in plotsetting)
            {
                string[] strparams = setting.Split(':');                
                list.Add(new PlotDefinition() { X = Convert.ToInt32(strparams[0]), Y = Convert.ToInt32(strparams[1]), PlotCount = Convert.ToInt32(strparams[2]) });
            }
            return list;
        }

        private string StringFromPlotSettings(List<PlotDefinition> defs)
        {
            string retval = "";
            bool first = true;
            foreach (PlotDefinition plot in defs)
            {
                if (!first)
                    retval += ",";
                retval += plot.X + ":";
                retval += plot.Y + ":";
                retval += plot.PlotCount;
                first = false;
            }
            return retval;

        }
         public override bool GlobalStartup()
        {
            if (Program.Instance.Config.ReadCustomInt("superplow", "configversion", 0) < 1)
            {
                Program.Instance.Config.WriteCustomInt("superplow", "configversion", 1);
                Program.Instance.Config.WriteCustomBool("superplow", "maintainplots", false);
                Program.Instance.Config.WriteCustomString("superplow", "superplots", "0:0:0");
            }
            else {
                _plotDefinitions = PlotSettingsFromString( Program.Instance.Config.ReadCustomString("superplow", "superplots", "0:0:0"));
            }
            return true;
        }
        public override string GetName()
        {
            return "SuperPlow";
        }
        public override bool OnFarmWork(GameSession session)
        {
            if (!Program.Instance.Config.ReadCustomBool("superplow", "maintainplots", false))
                return true;

            List<PlotDefinition> toPlot = new List<PlotDefinition>();
            foreach (PlotDefinition plotSetting in _plotDefinitions)
            { 
                string hashKey = plotSetting.X.ToString() + "," + plotSetting.Y.ToString();
                if (session.World.SuperPlots.ContainsKey(hashKey))
                {
                    int leftOver = plotSetting.PlotCount - session.World.SuperPlots[hashKey].Count;
                    if ( leftOver > 0 )
                        toPlot.Add(new PlotDefinition() { PlotCount = leftOver, X = plotSetting.X, Y = plotSetting.Y });
                }
            }

            if ( toPlot.Count == 0 )
                return true;

            foreach ( PlotDefinition plot in toPlot )
            {
                int plotCount = plot.PlotCount;
                string hashKey = plot.X.ToString() + "," + plot.Y.ToString();
                for ( int  x = 0; x< plotCount; x++ )
                {

                    Game.Objects.PlotObject newplot = new Game.Objects.PlotObject();
                    newplot.ClassName = "Plot";
                    newplot.Giftable = false;
                    newplot.GiftSenderId = "";
                    newplot.HasGiftRemaining = false;
                    newplot.Id = 0;
                    newplot.IsBigPlot = false;
                    newplot.IsJumbo = false;
                    newplot.IsProduceItem = false;
                    newplot.ItemName = "";
                    newplot.State = "plowed";
                    newplot.UsesAltGraphic = false;
                    newplot.PlantTime = float.NaN;
                    newplot.Position = new Game.Objects.Position() { X = plot.X, Y = plot.Y, Z = 0 };
                    if ( !newplot.Plow() )
                        return false;
                    session.World.FarmObjects.Add(newplot);
                    if ( !session.World.SuperPlots.ContainsKey( hashKey ) )
                        session.World.SuperPlots.Add( hashKey, new List<Game.Objects.PlotObject>() );
                    session.World.SuperPlots[hashKey].Add(newplot);   
                }
            }
            return true;
        }
    }
}
