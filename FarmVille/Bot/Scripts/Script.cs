using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmVille.Bot.Scripts
{
    public class Script
    {
        public virtual string GetName() { return "Default Script"; }
        public virtual string GetAuthor() { return "Kolie"; }
        public virtual string GetVersion() { return "0.1"; }

        public virtual bool GlobalStartup() { return true; }
        public virtual bool GlobalShutdown() { return true; }

        public virtual bool SessionStartup(Bot.GameSession session) { return true; }
        public virtual bool OnSessionError(Bot.GameSession session, int errorType, string errorMessage) { return true; }
        public virtual bool SessionShutdown(Bot.GameSession session) { return true; }

        private static System.Threading.Mutex _scriptLock = new System.Threading.Mutex();

        public static System.Threading.Mutex ScriptLock
        {
            get { return Script._scriptLock; }
            set { Script._scriptLock = value; }
        }
        
        public virtual void AcquireScriptLock() {
            _scriptLock.WaitOne();
        }

        public virtual void ReleaseScriptLock() {
            _scriptLock.ReleaseMutex();
        }

        public virtual bool OnBeforeFarmLoad(Bot.GameSession session) { return true; }
        public virtual bool OnFarmLoad(Bot.GameSession session) { return true; }
        public virtual bool OnBeforeFarmWork(Bot.GameSession session) { return true; }
        public virtual bool OnBeforeHarvest(Bot.GameSession session) { return true; }
        public virtual bool OnHarvest(Bot.GameSession session) { return true; }
        public virtual bool OnBeforeHarvestAnimals(Bot.GameSession session) { return true; }
        public virtual bool OnHarvestAnimals(Bot.GameSession session) { return true; }
        public virtual bool OnBeforeHarvestTrees(Bot.GameSession session) { return true; }
        public virtual bool OnHarvestTrees(Bot.GameSession session) { return true; }
        public virtual bool OnBeforePlow(Bot.GameSession session) { return true; }
        public virtual bool OnPlow(Bot.GameSession session) { return true; }
        public virtual bool OnBeforePlanting(Bot.GameSession session) { return true; }
        public virtual bool OnPlanting(Bot.GameSession session) { return true; }
        public virtual bool OnFarmWork(Bot.GameSession session) { return true; }
    }
}
