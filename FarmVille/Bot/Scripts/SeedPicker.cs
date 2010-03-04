using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Bot.Scripts
{
    public abstract class SeedPicker
    {
        public abstract string PickSeed(Game.Objects.PlotObject plot);
    }

    public class DefaultSeedPicker
        : SeedPicker
    {
        public override string PickSeed(Game.Objects.PlotObject plot)
        {
            return Program.Instance.Config.Farm.PlantSeed;
        }
    }

    public class MasterySeedPicker :
        SeedPicker
    {
        public override string PickSeed(Game.Objects.PlotObject plot)
        {
            foreach (Game.Settings.SeedSetting seed in Game.Settings.SeedSetting.SeedSettings.Values)
            { 
                int masteryLeft = Program.Instance.GameSession.Player.CountToMastery(seed.Name);
                masteryLeft -= Program.Instance.GameSession.World.GetPlantedCount(seed.Name);
                if ( seed.Usable && seed.Buyable && seed.Mastery && seed.RequiredLevel <= Program.Instance.GameSession.Player.Level && ( masteryLeft > 0 ))
                    return seed.Name;
            }

            return Program.Instance.Config.Farm.PlantSeed;
        }
    }
    public class RandomSeedPicker :
        SeedPicker
    {
        private static System.Random s_random = new Random();
        public override string PickSeed(Game.Objects.PlotObject plot)
        {
            var names = from x in Game.Settings.SeedSetting.SeedSettings.Values
                        where x.Usable == true && x.Buyable == true && x.RequiredLevel <= Program.Instance.GameSession.Player.Level
                        select x.Name;
            int index = s_random.Next(0, names.Count());
            return names.ElementAt(index);

        }
    }
}
