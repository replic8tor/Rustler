﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Everworld;
using System.Net;
using System.IO;



namespace FarmVille
{
    public class Program
        : Everworld.EverworldApplication 
    {
        public readonly static Program Instance = new Program();

        private FarmVille.Bot.GameSession _gameSession;

        public FarmVille.Bot.GameSession GameSession
        {
            get { return _gameSession; }
            set { _gameSession = value; }
        }

        private Bot.Config _config = null;

        public Bot.Config Config
        {
            get { return _config; }
            set { _config = value; }
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 25);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            string configFile = "config.xml";
            if (!System.IO.File.Exists(configFile))
            {
                Program.Instance.Config = new Bot.Config();
                Bot.Config.Save(configFile, Program.Instance.Config);
                Console.WriteLine("Bot configuration doesn't exist, creating. Please fill in user information in config.xml and restart the bot.");
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
                return;
            }
            else {
                Program.Instance.Config = Bot.Config.Load(configFile);
                if (Program.Instance.Config.User.Username.Length == 0 ||
                    Program.Instance.Config.User.Password.Length == 0)
                {
                    Console.WriteLine("User information is not set in config.xml. Fill in your login information and restart the bot.");
                    Console.WriteLine("Press enter to exit.");
                    Console.ReadLine();
                    return;
                }
            }

            
            Program.Instance.Logger.Log(Everworld.Logging.Logger.LogLevel.Info, "Main", "Application started.");
            Program.Instance.Run();
            Console.ReadLine();
            
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            Console.WriteLine("Unhandled exception occured.");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
        
        public void Run()
        {
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            Initialize();
            Bot.Scripts.ScriptManager.Instance.Run();
            

                    // Build SuperPlot
                    /*
                    for (int x = 0; x < (4 * 4); x += 4)
                    {
                        for (int y = 0; y < (4 * 2); y += 4)
                        {
                            for (int z = 0; z < 500; z++)
                            {
                                Game.Objects.PlotObject plot = new Game.Objects.PlotObject();
                                plot.ClassName = "Plot";
                                plot.Giftable = false;
                                plot.GiftSenderId = "";
                                plot.HasGiftRemaining = false;
                                plot.Id = 0;
                                plot.IsBigPlot = false;
                                plot.IsJumbo = false;
                                plot.IsProduceItem = false;
                                plot.ItemName = "";
                                plot.State = "plowed";
                                plot.UsesAltGraphic = false;
                                plot.PlantTime = float.NaN;
                                plot.Position = new Game.Objects.Position() { X = x, Y = y, Z = 0 };
                                plot.Plow();
                            }            
                        }
                    }
                    */



              

        }

     
        private static void GenerateFormData(System.Xml.XmlNode ele, Dictionary<string,string> output)
        {
            List<System.Xml.XmlNode> checkedNodes = new List<System.Xml.XmlNode>();
            LinkedList<System.Xml.XmlNode> uncheckedNodes = new LinkedList<System.Xml.XmlNode>();

            uncheckedNodes.AddLast(ele);
            while(uncheckedNodes.Count > 0 )
            {
                System.Xml.XmlNode aNode = uncheckedNodes.First.Value;
                uncheckedNodes.RemoveFirst();
                checkedNodes.Add(aNode);
                foreach (System.Xml.XmlNode subNode in aNode.ChildNodes)
                    uncheckedNodes.AddLast(subNode);

      
            }

            foreach (System.Xml.XmlNode node in checkedNodes)
            {
                if (node.Name == "div" || node.Name == "#text" || node.Name == "a" || node.Name == "label" || node.Name == "td" || node.Name == "tr" || node.Name == "image" || node.Name == "p" || node.Name == "table")
                    continue;
                if (node.Name == "input")
                {
                    if (!output.ContainsKey(node.Attributes["name"].Value))
                        output.Add(node.Attributes["name"].Value, System.Web.HttpUtility.UrlEncode(node.Attributes["value"].Value));
                }
            }

        }
    }
}