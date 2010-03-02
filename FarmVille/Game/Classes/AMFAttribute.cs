using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluorineFx;
using System.Reflection;
namespace FarmVille.Game.Classes
{
    public class AMFArrayAttribute
        : System.Attribute
    {
        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private Type _arrayType;

        public Type ArrayType
        {
            get { return _arrayType; }
            set { _arrayType = value; }
        }

        public AMFArrayAttribute(string tag, Type type)
        {
            _tag = tag;
            _arrayType = type;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class AMFAttribute
        : System.Attribute
    {
        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        public AMFAttribute(string tag)
        {
            _tag = tag;
        }
    }
    public class AMFDictionary
        : System.Attribute
    {
        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private Type _valueType;

        public Type ValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
        }
        public AMFDictionary(string tag, Type value)
        {
            _tag = tag;
            _valueType = value;
        }
    }

    public class AMFObject :
        FluorineFx.ASObject
    {
        public virtual void FromAMF(FluorineFx.ASObject obj)
        { 
            Type objType = this.GetType();
            FieldInfo[] memberInfo = objType.GetFields();
            foreach (FieldInfo info in memberInfo)
            {
                object[] customAttributes = info.GetCustomAttributes(true);
                foreach (object attr in customAttributes)
                {
                    if (attr is AMFAttribute)
                    { 
                        AMFAttribute attribute = attr as AMFAttribute;
                        if ( !obj.ContainsKey(attribute.Tag) )
                            info.SetValue(this, null);
                        else if ( info.FieldType == typeof(string) ) {
                            info.SetValue(this, (string)obj[attribute.Tag]);
                        } else if ( info.FieldType == typeof(int?) ) {
                            info.SetValue(this, (int?)obj[attribute.Tag]);
                        } else if ( info.FieldType == typeof(float?) ) {
                            info.SetValue(this, (float?)obj[attribute.Tag]);
                        } else if ( info.FieldType == typeof(bool?) ) {
                            info.SetValue(this, (bool?)obj[attribute.Tag]);
                        }
                    }
                    if (attr is AMFArrayAttribute)
                    {
                        AMFArrayAttribute attribute = attr as AMFArrayAttribute;
                        if ( attribute.ArrayType == typeof(string) ) {
                            List<string> array = new List<string>();
                            
                        }
                    }
                }
            }
        }
    }

}
