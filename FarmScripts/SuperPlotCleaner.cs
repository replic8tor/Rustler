using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Bot.Scripts
{
    public class SuperPlotCleaner
        : Script
    {
        public override bool GlobalStartup()
        {
            if (Program.Instance.Config.ReadCustomInt("superplotcleaner", "configversion", 0) < 1)
            {
                Program.Instance.Config.WriteCustomInt("superplotcleaner", "configversion", 1);
                Program.Instance.Config.WriteCustomBool("superplotcleaner", "active", false);
            }
            return true;
        }
        public override string GetName()
        {
            return "Super Plot Cleaner";
        }
        public override bool OnFarmLoad(GameSession session)
        {
            if ( !Program.Instance.Config.ReadCustomBool("superplotcleaner", "active", false ) )
                return true;

            List<Game.Objects.PlotObject> plotsToDelete = new List<Game.Objects.PlotObject>();
            foreach (List<Game.Objects.PlotObject> list in session.World.SuperPlots.Values)
                plotsToDelete.AddRange(list);

            for (int x = 0; x < plotsToDelete.Count ; x += 20)
            {
                if (!Game.Objects.PlotObject.MassRemove(plotsToDelete.GetRange(x, Math.Min(20, plotsToDelete.Count - x)).ToArray()))
                {
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info,"SuperCleaner","There was an error in our request to the server. Resetting..");
                    return false;
                }
                Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info,"SuperCleaner","{0} plots cleared.", x);
            }

            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info,"SuperCleaner","Plots deleted. Disabling cleaner and restarting.");
            Program.Instance.Config.WriteCustomBool("superplotcleaner", "active", false );
            return false;
        }
    }
}
