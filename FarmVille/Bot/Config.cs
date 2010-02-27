using System;
using System.IO;
using System.Xml.Serialization;
using Everworld.Utility;

namespace FarmVille.Bot
{
    public class Config
    {
        public static XmlSerializer sXMLSerializer;
        public UserConfig User = new UserConfig();
        public FarmConfig Farm = new FarmConfig();
        public SerializableDictionary< string, SerializableDictionary<string, object> > CustomSettings = new SerializableDictionary<string,SerializableDictionary<string,object>>();

        public object ReadCustomObject(string script, string setting, object defaultValue)
        {
            try
            {
                if (!CustomSettings.ContainsKey(script))
                    return defaultValue;
                SerializableDictionary<string, object> scriptSettings = CustomSettings[script];
                if (!scriptSettings.ContainsKey(setting))
                    return defaultValue;
                return scriptSettings[setting];
            }
            catch (System.Exception ex)
            {
                return defaultValue;
            }
        }
        public bool ReadCustomBool(string script, string setting, bool defaultValue) 
        {
            try{
                if (!CustomSettings.ContainsKey(script))
                    return defaultValue;
                SerializableDictionary<string, object> scriptSettings = CustomSettings[script];
                if (!scriptSettings.ContainsKey(setting))
                    return defaultValue;
                return (bool)scriptSettings[setting];
            }
            catch ( System.Exception ex ){
                return defaultValue;
            }
        }
        public int ReadCustomInt(string script, string setting, int defaultValue) {
            try
            {
                if (!CustomSettings.ContainsKey(script))
                    return defaultValue;
                SerializableDictionary<string, object> scriptSettings = CustomSettings[script];
                if (!scriptSettings.ContainsKey(setting))
                    return defaultValue;
                return (int)scriptSettings[setting];
            }
            catch (System.Exception ex)
            {
                return defaultValue;
            }
        }
        public string ReadCustomString(string script, string setting, string defaultValue) {
            try
            {
                if (!CustomSettings.ContainsKey(script))
                    return defaultValue;
                SerializableDictionary<string, object> scriptSettings = CustomSettings[script];
                if (!scriptSettings.ContainsKey(setting))
                    return defaultValue;
                return (string)scriptSettings[setting];
            }
            catch (System.Exception ex)
            {
                return defaultValue;
            }
        }

        public void WriteCustomBool(string script, string setting, bool value) {
            try
            {
                SerializableDictionary<string, object> scriptSettings;
                if (!CustomSettings.ContainsKey(script))
                    CustomSettings.Add(script, new SerializableDictionary<string, object>());
                scriptSettings = CustomSettings[script];
                if (scriptSettings.ContainsKey(setting))
                    scriptSettings.Remove(setting);
                scriptSettings.Add(setting, value);
                Save("config.xml", this);
            }
            catch (System.Exception ex)
            {
                return;
            }
        }
        public void WriteCustomInt(string script, string setting, int value) {
            try
            {
                SerializableDictionary<string, object> scriptSettings;
                if (!CustomSettings.ContainsKey(script))
                    CustomSettings.Add(script, new SerializableDictionary<string, object>());
                scriptSettings = CustomSettings[script];
                if (scriptSettings.ContainsKey(setting))
                    scriptSettings.Remove(setting);
                scriptSettings.Add(setting, value);
                Save("config.xml", this);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void WriteCustomString(string script, string setting, string value) {
            try
            {
                SerializableDictionary<string, object> scriptSettings;
                if (!CustomSettings.ContainsKey(script))
                    CustomSettings.Add(script, new SerializableDictionary<string, object>());
                scriptSettings = CustomSettings[script];
                if (scriptSettings.ContainsKey(setting))
                    scriptSettings.Remove(setting);
                scriptSettings.Add(setting, value);
                Save("config.xml", this);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void WriteCustomObject(string script, string setting, object value) {
            SerializableDictionary<string, object> scriptSettings;
            if (!CustomSettings.ContainsKey(script))
                CustomSettings.Add(script, new SerializableDictionary<string, object>());
            scriptSettings = CustomSettings[script];
            if (scriptSettings.ContainsKey(setting))
                scriptSettings.Remove(setting);
            scriptSettings.Add(setting, value);
            Save("config.xml", this);
        }

        static Config()
        {
            sXMLSerializer = new XmlSerializer(typeof(Config));
        }

        public static Config Load(string configLocation)
        {
            try
            {
                using ( StreamReader sw = new StreamReader(configLocation) )
                    return (Config)sXMLSerializer.Deserialize(sw);
            }
            catch (Exception ex)
            {
                return new Config();
            }
        }

        public static void Save(string configLocation, Config config)
        {
            using (StreamWriter sw = new StreamWriter(configLocation))
            {
                sXMLSerializer.Serialize(sw, config);
                sw.Flush();
            }
            
        }

        #region Nested type: UserConfig

        public class UserConfig
        {
            public string Username = "";
            public string Password = "";
            public bool LogToFile = false;
        }

        #endregion

        #region Nested type: FarmConfig

        public class FarmConfig
        {
            public string SeedPicker = "DefaultSeedPicker";
            public string PlantSeed = "strawberry";
            public bool PlowWitheredPlots = true;
            public bool OnlyWorkSuperPlots = false;
        }

        

        #endregion
    }
}