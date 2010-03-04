using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace FarmVille.Game.Classes
{
    public class ObjectBuilder
    {
        public static ObjectBuilder Instance;
        
        public static void InitializeClass() {
            Instance = new ObjectBuilder();
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(System.Reflection.Assembly.GetEntryAssembly());
            List<Assembly> dynamicAssemblues = Everworld.ScriptCompiler.Assemblies.ToList();
            foreach (Assembly asm in dynamicAssemblues)
                assemblies.Add(asm);

            Console.WriteLine("Building table of constructable objects...");
            foreach (Assembly asm in assemblies)
            {
                Type[] types = asm.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(BaseObject)))
                    {
                        object[] attrs = type.GetCustomAttributes(typeof(AMFConstructableObjectAttribute), false);
                        if (attrs.Length > 0)
                        {
                            AMFConstructableObjectAttribute attr = attrs[0] as AMFConstructableObjectAttribute;
                            if (attr.FarmObject != "")
                            {
                                if (Instance._typeTable.ContainsKey(attr.FarmObject))
                                {
                                    Console.WriteLine(String.Format("Object {0} pre-empted by {1}", type.Name, asm.GetName()));
                                    Instance._typeTable.Remove(attr.FarmObject);
                                }
                                Console.WriteLine(type.Name);
                                Instance._typeTable.Add(attr.FarmObject, type);


                            }
                            else {
                                Console.WriteLine(String.Format("Object {0} is not constructable.", type.Name));
                            }
                        }
                        else {
                            Console.WriteLine(String.Format("Object {0} is not constructable.", type.Name));
                        }
                    }
                }
            }
        }

        private Dictionary<string, Type> _typeTable = new Dictionary<string, Type>();

        public virtual BaseObject BuildObject(string className, FluorineFx.ASObject obj)
        {
            BaseObject retVal = null;
            if (_typeTable.ContainsKey(className))
                retVal = _typeTable[className].InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, null, null) as BaseObject;
            else
                retVal = (typeof(BaseObject)).InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, null, null) as BaseObject;

            retVal.FromAMF(obj);
            return retVal;
        }
    }
}
