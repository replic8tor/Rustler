using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Game.Objects
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FarmObjectClassAttribute : System.Attribute
    {
        private string p_mFarmObjectClass = "";
        public string FarmObject { get { return p_mFarmObjectClass; } }
        public FarmObjectClassAttribute() { }
        public FarmObjectClassAttribute(string pFarmObject) { p_mFarmObjectClass = pFarmObject; }
    }

    public class Position {
        private int _x;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        private int _y;

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        private int _z;

        public int Z
        {
            get { return _z; }
            set { _z = value; }
        }
        public virtual Position FromObject(FluorineFx.ASObject obj)
        {
            _x = (int) ( obj["x"] ?? 0 );
            _y = (int)(obj["y"] ?? 0 );
            _z = (int)(obj["z"] ?? 0 );
            return this;
        }

        public virtual FluorineFx.ASObject ToObject()
        {
            FluorineFx.ASObject pos = new FluorineFx.ASObject();
            pos.Add("x", _x);
            pos.Add("y", _y);
            pos.Add("z", _z);
            return pos;
        }
    }
    
    public class BaseObject
    {

        private bool _usesAltGraphic;

        public bool UsesAltGraphic
        {
            get { return _usesAltGraphic; }
            set { _usesAltGraphic = value; }
        }
        private string _state;

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        private Position _position;

        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _className;

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        private bool _giftable = false;

        public bool Giftable
        {
            get { return _giftable; }
            set { _giftable = value; }
        }
        private string _giftSenderId;

        public string GiftSenderId
        {
            get { return _giftSenderId; }
            set { _giftSenderId = value; }
        }
        
        

        public virtual void FromObject(FluorineFx.ASObject obj)
        {

            if (obj.ContainsKey("giftSenderId"))
            {
                _giftable = true;
                _giftSenderId = (string)obj["giftSenderId"];
            }
            _usesAltGraphic = (bool)obj["usesAltGraphic"];
            _state = (string)obj["state"];
            _itemName = (string)obj["itemName"];
            _position = new Position().FromObject((FluorineFx.ASObject)obj["position"]);
            _id = (int)obj["id"];
            _className = (string)obj["className"];
        }


    }
}
