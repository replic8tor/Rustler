using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace FarmVille.Bot.Scripts
{
    public class ScriptManager
    {
        public static ScriptManager Instance = null;
      
        public static void InitializeClass() {
            Instance = new ScriptManager();
            Instance.Initialize();
        }

        private List<Script> _scripts = new List<Script>();

        internal List<Script> Scripts
        {
            get { return _scripts; }
            set { _scripts = value; }
        }
        private MainScript _main = null;

        public MainScript Main
        {
            get { return _main; }
            set { _main = value; }
        }

        public void Initialize(){
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(System.Reflection.Assembly.GetEntryAssembly());
            List<Assembly> dynamicAssemblues = Everworld.ScriptCompiler.Assemblies.ToList();
            foreach (Assembly asm in dynamicAssemblues)
                assemblies.Add(asm);

            Console.WriteLine("Building table of scripts...");
            Type defaultMain = null;
            SeedPicker picker = new Scripts.DefaultSeedPicker();
            foreach (Assembly asm in assemblies)
            {
                Type[] types = asm.GetTypes();
                
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(Script)) && (!type.IsSubclassOf(typeof(MainScript)) && type.Name != "MainScript"))
                    {
                        Script instance = type.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, null, null) as Script;
                        _scripts.Add(instance);
                         Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info,"Script Manager","Loaded script {0} ({1}) by {2}",instance.GetName(), instance.GetVersion(), instance.GetAuthor());
                    }
                    if (type.Name == "MainScript" && defaultMain == null)
                        defaultMain = type;
                    else if (type.IsSubclassOf(typeof(MainScript)))
                        defaultMain = type;

                    if (type.Name == Program.Instance.Config.Farm.SeedPicker)
                    {
                        picker = type.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, null, null) as SeedPicker;
                    }
                }
               
            }
           
            _main = defaultMain.InvokeMember("",System.Reflection.BindingFlags.CreateInstance,null,null,null) as MainScript;
            _main.SeedPicker = picker;

            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info,"Script Manager", "Using {0} ({1}) by {2} as main script.", _main.GetName(), _main.GetVersion(), _main.GetAuthor());

            _main.GlobalStartup();
            foreach( Script script in _scripts )
                script.GlobalStartup();
        }

        private bool _errorRaised = false;

        public void RaiseSessionError(int errorType, string errorData) {
            _errorRaised = true;
            _main.OnSessionError(Program.Instance.GameSession, errorType, errorData);
            foreach (Script script in _scripts)
                script.OnSessionError(Program.Instance.GameSession, errorType, errorData);
            
        }

        public bool _nextRunSet = false;
        private DateTime _nextRun = DateTime.Now;
        public void SetNextUpdate(DateTime nextTime)
        { 
            if ( !_nextRunSet )
            {
                _nextRunSet = true;
                _nextRun = nextTime;
                return;
            } else {
                // time is already set
                if ( nextTime < _nextRun )  // see if the new time is sooner then the current set time
                    _nextRun = nextTime;    // if it is interrupt then instead.
            }

        }

        public bool Run() {

             while (true)
             {
                try
                {
               
                    _main.SessionStartup(null);


                    Program.Instance.GameSession.ServerSession.OnInvalidToken += delegate(int error, string data, Bot.Server.ServerSession.BlockingCallback call) {
                        _errorRaised = true;
                    };
                    Program.Instance.GameSession.ServerSession.OnNewVersion += delegate(int error, string data, Bot.Server.ServerSession.BlockingCallback call)
                    {
                        _errorRaised = true;
                    };

                    _main.OnBeforeFarmLoad(Program.Instance.GameSession);

                    while (!_errorRaised)
                    {
                        _nextRunSet = false; _nextRun = DateTime.Now;
                        if (!_main.OnBeforeFarmWork(Program.Instance.GameSession) ||
                            !_main.OnBeforeHarvest(Program.Instance.GameSession) ||
                            !_main.OnBeforeHarvestAnimals(Program.Instance.GameSession) ||
                            !_main.OnBeforeHarvestTrees(Program.Instance.GameSession) ||
                            !_main.OnBeforePlow(Program.Instance.GameSession) ||
                            !_main.OnBeforePlanting(Program.Instance.GameSession))
                        {
                            _errorRaised = true;
                        }

                        Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "Next cycle @ {0}.", _nextRun.ToString());

                        while (DateTime.Now < _nextRun)
                            System.Threading.Thread.Sleep(10000);                        
                    }

                    _errorRaised = false;

                    _main.SessionShutdown(Program.Instance.GameSession);

                }
                catch (Exception ex)
                {
                    Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "Script Manager", "Unhandled script error {0}\n{1}", ex.Message, ex.StackTrace);

                    ex = ex.InnerException;
                    while (ex != null )
                    {
                        Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Error, "Script Manager", "Unhandled script error {0}\n{1}", ex.Message, ex.StackTrace);
                        ex = ex.InnerException;
                    }
                    break;
                }
            }
             _main.GlobalShutdown();
             foreach (Script script in _scripts)
                 script.GlobalShutdown();
             return true;

            
        }

        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            _main.GlobalShutdown();
            foreach (Script script in _scripts)
                _main.GlobalShutdown();
        }
    }
}
