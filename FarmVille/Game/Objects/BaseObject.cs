using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmVille.Game.Classes;

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

    public class ObjectPosition
     : AMFObject {
        [AMF("x")]
        private int? _x;

        public int? X
        {
            get { return _x; }
            set { _x = value; }
        }
        [AMF("y")]
        private int? _y;

        public int? Y
        {
            get { return _y; }
            set { _y = value; }
        }
        [AMF("z")]
        private int? _z;

        public int? Z
        {
            get { return _z; }
            set { _z = value; }
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
        : AMFObject
    {
        [AMF("usesAltGraphic")]
        private bool? _usesAltGraphic;

        public bool? UsesAltGraphic
        {
            get { return _usesAltGraphic; }
            set { _usesAltGraphic = value; }
        }
        private string _state;
        [AMF("state")]
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        [AMF("itemName")]
        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        
        [AMFObject("position",typeof(ObjectPosition))]
        private ObjectPosition _position;

        public ObjectPosition Position
        {
            get { return _position; }
            set { _position = value; }
        }
        [AMF("id")]
        private int? _id;

        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [AMF("className")]
        private string _className;

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        [AMF("id")]
        private bool? _giftable = false;

        public bool? Giftable
        {
            get { return _giftable; }
            set { _giftable = value; }
        }

        [AMF("giftSenderId")]
        private string _giftSenderId;

        public string GiftSenderId
        {
            get { return _giftSenderId; }
            set { _giftSenderId = value; }
        }
        
        

        public override void  FromAMF(FluorineFx.ASObject obj)
        {
 	        base.FromAMF(obj);
            if (obj.ContainsKey("giftSenderId"))
                _giftable = true;
        }
        

    }
}
