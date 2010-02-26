using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Settings
{
    /*
     * <item name="strawberry" type="seed" subtype="fruit" buyable="true" mastery="true" code="SB">
        <requiredLevel>1</requiredLevel>
        <cost>10</cost>
        <growTime>0.174</growTime>
        <plantXp>1</plantXp>
        <coinYield>35</coinYield>
        <sizeX>4</sizeX>
        <sizeY>4</sizeY>
        <masteryYield>1</masteryYield>
        <masteryLevel count="500" xp="25" coins="500"/>
        <masteryLevel count="1500" xp="75" coins="1500"/>
        <masteryLevel count="5250" xp="250" coins="5000" gift="masterstrawberries"/>
        <largeCropXp>1</largeCropXp>
        <largeCropChance>0.4</largeCropChance>
        </item>
     * */
    public class SeedSetting
    {
        public static Dictionary<string, SeedSetting> SeedSettings = new Dictionary<string, SeedSetting>();
        public static Dictionary<string, string> SeedByCode = new Dictionary<string, string>();

        public static SeedSetting FromXmlNode(System.Xml.XmlNode seedSetting)
        {
            SeedSetting retVal = new SeedSetting();
            retVal._name = seedSetting.Attributes["name"].Value;

            if (seedSetting.Attributes.GetNamedItem("license") != null)
                retVal.Usable = false;


            if (seedSetting.Attributes["subtype"] != null)
                retVal._subtype = seedSetting.Attributes["subtype"].Value;
            else
                retVal._subtype = "";

            retVal._buyable = seedSetting.Attributes["buyable"].Value == "true";
            
            foreach ( System.Xml.XmlNode x in seedSetting.ChildNodes )
            {
                if ( x.Name == "unlockCost" || x.Name == "limitedStart" )
                {
                    retVal.Usable = false;
                    break;
                }
            }
          
            
            if ( seedSetting.Attributes["mastery"] != null )
                retVal._mastery = seedSetting.Attributes["mastery"].Value == "true";
            int masterLevel = 0;
            foreach (System.Xml.XmlNode node in seedSetting.ChildNodes)
            {
                if (node.Name == "masteryLevel")
                {
                    if ( masterLevel == 0 )
                        retVal.Mastery0Count = Convert.ToInt32(node.Attributes["count"].Value);
                    else if ( masterLevel == 1) 
                        retVal.Mastery1Count = Convert.ToInt32(node.Attributes["count"].Value);
                    else if ( masterLevel == 2)
                        retVal.Mastery2Count = Convert.ToInt32(node.Attributes["count"].Value);
                    masterLevel++;
                }

            }

            retVal._code = seedSetting.Attributes["code"].Value;

            

            if (seedSetting.Attributes["requiredLevel"] != null)
                retVal._requiredLevel = int.Parse(seedSetting["requiredLevel"].Value);
            else
                retVal._requiredLevel = 0;

            if (seedSetting["cost"] != null)
                retVal._cost = int.Parse(seedSetting["cost"].InnerText);
            else
                retVal._cost = 0;

            retVal._growTime = float.Parse(seedSetting["growTime"].InnerText);
            retVal._plantXp = int.Parse(seedSetting["plantXp"].InnerText);
            retVal._coinYield = int.Parse(seedSetting["coinYield"].InnerText);
            retVal._sizeX = int.Parse(seedSetting["sizeX"].InnerText);
            retVal._sizeY = int.Parse(seedSetting["sizeY"].InnerText);
            if (seedSetting["largeCropXp"] != null)
                retVal._largeCropXp = int.Parse(seedSetting["largeCropXp"].InnerText);
            else
                retVal._largeCropXp = 0;

            if (seedSetting["largeCropChance"] != null)
                retVal._largeCropChance = float.Parse(seedSetting["largeCropChance"].InnerText);
            else
                retVal._largeCropChance = 0;

            SeedByCode.Add(retVal.Code, retVal.Name);

            return retVal;
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _subtype;

        public string Subtype
        {
            get { return _subtype; }
            set { _subtype = value; }
        }
        private bool _buyable;

        public float CropValue;

        public bool Buyable
        {
            get { return _buyable; }
            set { _buyable = value; }
        }
        private bool _mastery;

        public bool Mastery
        {
            get { return _mastery; }
            set { _mastery = value; }
        }
        private int _mastery0Count;

        public int Mastery0Count
        {
            get { return _mastery0Count; }
            set { _mastery0Count = value; }
        }
        private int _mastery1Count;

        public int Mastery1Count
        {
            get { return _mastery1Count; }
            set { _mastery1Count = value; }
        }
        private int _mastery2Count;

        public int Mastery2Count
        {
            get { return _mastery2Count; }
            set { _mastery2Count = value; }
        }
        
        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private int _requiredLevel;

        public int RequiredLevel
        {
            get { return _requiredLevel; }
            set { _requiredLevel = value; }
        }
        private int _cost;

        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        private float _growTime;

        public float GrowTime
        {
            get { return _growTime; }
            set { _growTime = value; }
        }

        public int GrowTimeInSeconds { get { return  (int)(((23.0 * GrowTime) * 60.0 * 60.0) + 1); } }

        private int _plantXp;

        public int PlantXp
        {
            get { return _plantXp; }
            set { _plantXp = value; }
        }
        private int _coinYield;

        public int CoinYield
        {
            get { return _coinYield; }
            set { _coinYield = value; }
        }
        private int _sizeX;

        public int SizeX
        {
            get { return _sizeX; }
            set { _sizeX = value; }
        }
        private int _sizeY;

        public bool Usable = true;

        public int SizeY
        {
            get { return _sizeY; }
            set { _sizeY = value; }
        }
        private int _largeCropXp;

        public int LargeCropXp
        {
            get { return _largeCropXp; }
            set { _largeCropXp = value; }
        }
        private float _largeCropChance;

        public float LargeCropChance
        {
            get { return _largeCropChance; }
            set { _largeCropChance = value; }
        }
    }
}
