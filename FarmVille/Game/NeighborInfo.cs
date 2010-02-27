using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game
{
    public class NeighborInfo
    {
        public static NeighborInfo FromASObject(FluorineFx.ASObject obj)
        {
            NeighborInfo info = new NeighborInfo();
            info._uid = obj["uid"] as string;
            if (obj["level"] is double)
                info._level = (double)obj["level"];
            else if (obj["level"] is int)
                info._level = (double)(int)obj["level"];

            if (obj["xp"] is double)
                info._level = (double)obj["xp"];
            else if (obj["xp"] is int)
                info._level = (double)(int)obj["xp"];
            if (obj["gold"] is double)
                info._level = (double)obj["gold"];
            else if (obj["gold"] is int)
                info._level = (double)(int)obj["gold"];

            if (obj.ContainsKey("valentinesReceived"))
                info._valentinesReceived = (int)obj["valentinesReceived"];
            return info;
        }
        private string _uid;

        public string Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private double _level;

        public double Level
        {
            get { return _level; }
            set { _level = value; }
        }
        private double _xp;

        public double Xp
        {
            get { return _xp; }
            set { _xp = value; }
        }
        private double _gold;

        public double Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        private int _valentinesReceived;

        public int ValentinesReceived
        {
            get { return _valentinesReceived; }
            set { _valentinesReceived = value; }
        }
    }
}
