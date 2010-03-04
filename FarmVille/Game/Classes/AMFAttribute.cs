using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluorineFx;
using System.Reflection;
namespace FarmVille.Game.Classes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AMFConstructableObjectAttribute 
        : System.Attribute
    {
        private string p_mAMFConstructableObject = "";
        public string FarmObject { get { return p_mAMFConstructableObject; } }
        public AMFConstructableObjectAttribute() { }
        public AMFConstructableObjectAttribute(string pFarmObject) { p_mAMFConstructableObject = pFarmObject; }
    }
    public class AMFTypedObjectArrayAttribute
        : System.Attribute
    {
        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private Type _baseType;

        public Type BaseType
        {
            get { return _baseType; }
            set { _baseType = value; }
        }

        public AMFTypedObjectArrayAttribute(string tag, Type basetype)
        {
            _tag = tag;
            _baseType = basetype;
        }
        
    }
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
    public class AMFDictionaryAttribute
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
        public AMFDictionaryAttribute(string tag, Type value)
        {
            _tag = tag;
            _valueType = value;
        }
    }
    public class AMFObjectAttribute
        : System.Attribute {
            private string _tag;

            public string Tag
            {
                get { return _tag; }
                set { _tag = value; }
            }

            private Type _type;

            public Type Type
            {
                get { return _type; }
                set { _type = value; }
            }
            public AMFObjectAttribute( string tag, Type type )
            {
                _tag = tag;
                _type = type;
            }
    }
    public class AMFObject 
    {
        FluorineFx.ASObject _fromObject;

        public FluorineFx.ASObject FromObject
        {
            get { return _fromObject; }
            set { _fromObject = value; }
        }
        private static Dictionary<FieldInfo, object[]> _attributeCache = new Dictionary<FieldInfo, object[]>();
        private static Dictionary<Type, List<FieldInfo>> _fieldCache = new Dictionary<Type, List<FieldInfo>>();
        public virtual void FromAMF(FluorineFx.ASObject obj)
        {
            _fromObject = obj;
            Type objType = this.GetType();
            
                
            List<FieldInfo> memberInfo = new List<FieldInfo>();
            Type t = objType;
            if (!_fieldCache.ContainsKey(objType))
            {
                while (t != typeof(AMFObject))
                {
                    memberInfo.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic));
                    t = t.BaseType;
                }
                _fieldCache.Add(objType,memberInfo);
            } else
                memberInfo = _fieldCache[objType];
            
            foreach (FieldInfo info in memberInfo)
            {
                object[] customAttributes;
                if (!_attributeCache.ContainsKey(info))
                {
                    customAttributes = info.GetCustomAttributes(true);
                    _attributeCache.Add(info, customAttributes);
                }
                else
                    customAttributes = _attributeCache[info];

                    try
                    {
                        foreach (object attr in customAttributes)
                        {
                            if (attr is AMFAttribute)
                            {
                                AMFAttribute attribute = attr as AMFAttribute;
                                if (!obj.ContainsKey(attribute.Tag))
                                    info.SetValue(this, null);
                                else if (info.FieldType == typeof(string))
                                {
                                    info.SetValue(this, (string)obj[attribute.Tag]);
                                }
                                else if (info.FieldType == typeof(int?))
                                {
                                    info.SetValue(this, (int?)obj[attribute.Tag]);
                                }
                                else if (info.FieldType == typeof(float?))
                                {
                                    info.SetValue(this, (float?)obj[attribute.Tag]);
                                }
                                else if (info.FieldType == typeof(double?))
                                {
                                    if (obj[attribute.Tag] is double)
                                        info.SetValue(this, (double?)obj[attribute.Tag]);
                                    else if (obj[attribute.Tag] is int)
                                        info.SetValue(this, (double?)(int?)obj[attribute.Tag]);
                                }
                                else if (info.FieldType == typeof(bool?))
                                {
                                    info.SetValue(this, (bool?)obj[attribute.Tag]);
                                }
                            }
                            else if (attr is AMFArrayAttribute)
                            {
                                AMFArrayAttribute attribute = attr as AMFArrayAttribute;
                                if (attribute.ArrayType == typeof(string))
                                {
                                    List<string> array = new List<string>();
                                    foreach (object aobj in (object[])obj[attribute.Tag])
                                        array.Add((string)aobj);
                                    info.SetValue(this, array);

                                }
                                else if (attribute.ArrayType.IsSubclassOf(typeof(AMFObject)))
                                {
                                    object list = info.FieldType.InvokeMember("", BindingFlags.CreateInstance, null, null, null);
                                    if (obj[attribute.Tag] != null)
                                    {
                                        foreach (object aobj in (object[])obj[attribute.Tag])
                                        {
                                            FluorineFx.ASObject asobj = aobj as FluorineFx.ASObject;
                                            AMFObject amfobj = (AMFObject)attribute.ArrayType.InvokeMember("", BindingFlags.CreateInstance, null, null, null);
                                            amfobj.FromAMF(asobj);
                                            list.GetType().InvokeMember("Add", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, list, new object[] { amfobj });
                                        }
                                    }
                                    info.SetValue(this, list);
                                }
                            }
                            else if (attr is AMFTypedObjectArrayAttribute)
                            {
                                AMFTypedObjectArrayAttribute attribute = attr as AMFTypedObjectArrayAttribute;
                                object list = info.FieldType.InvokeMember("", BindingFlags.CreateInstance, null, null, null);
                                foreach (object aobj in (object[])obj[attribute.Tag])
                                {

                                    FluorineFx.ASObject asobj = aobj as FluorineFx.ASObject;
                                    System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(asobj.TypeName, @"var.www.releases.release-([0-9]+)-([0-9]+)-([0-9]+).([0-9]+).includes.([A-Za-z0-9\-]+).class");
                                    AMFObject amfobj = ObjectBuilder.Instance.BuildObject(match.Groups[5].Value, asobj);

                                    //AMFObject amfobj = (AMFObject)attribute.BaseType.InvokeMember("", BindingFlags.CreateInstance, null, null, null);
                                    amfobj.FromAMF(asobj);
                                    list.GetType().InvokeMember("Add", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, list, new object[] { amfobj });
                                }
                                info.SetValue(this, list);
                            }
                            else if (attr is AMFObjectAttribute)
                            {
                                AMFObjectAttribute attribute = attr as AMFObjectAttribute;

                                AMFObject amfobj = (AMFObject)attribute.Type.InvokeMember("", BindingFlags.CreateInstance, null, null, null);
                                amfobj.FromAMF((FluorineFx.ASObject)obj[attribute.Tag]);
                                info.SetValue(this, amfobj);

                            }
                            else if (attr is AMFDictionaryAttribute)
                            {
                                AMFDictionaryAttribute attribute = attr as AMFDictionaryAttribute;
                                if (attribute.ValueType == typeof(int?))
                                {
                                    Dictionary<string, int?> dict = new Dictionary<string, int?>();
                                    FluorineFx.ASObject dictobj = (FluorineFx.ASObject)obj[attribute.Tag];
                                    foreach (string key in dictobj.Keys)
                                    {
                                        object val = dictobj[key];
                                        dict.Add(key, (int?)val);
                                    }
                                    info.SetValue(this, dict);
                                }
                                else if (attribute.ValueType == typeof(bool?))
                                {
                                    Dictionary<string, bool?> dict = new Dictionary<string, bool?>();
                                    FluorineFx.ASObject dictobj = (FluorineFx.ASObject)obj[attribute.Tag];
                                    foreach (string key in dictobj.Keys)
                                    {
                                        object val = dictobj[key];
                                        dict.Add(key, (bool?)val);
                                    }
                                    info.SetValue(this, dict);
                                }
                                else if (attribute.ValueType == typeof(double?))
                                {
                                    Dictionary<string, double?> dict = new Dictionary<string, double?>();
                                    FluorineFx.ASObject dictobj = (FluorineFx.ASObject)obj[attribute.Tag];
                                    foreach (string key in dictobj.Keys)
                                    {
                                        object val = dictobj[key];
                                        if (val is int)
                                            dict.Add(key, (double?)(int?)val);
                                        else
                                            dict.Add(key, (double?)val);
                                    }
                                    info.SetValue(this, dict);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error setting member: " + info.Name + " " + this.GetType());
                    }
            }
        }
    }

}
