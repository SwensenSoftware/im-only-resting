/*
Copyright 2012 Stephen Swensen

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using System.Reflection;
using System.Drawing;

namespace Swensen.Ior.Forms
{
    static class Program
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log.Info("App starting");

            //set default form icon to project exe icon
            var defaultIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            typeof(Form).GetField("defaultIcon", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, defaultIcon);
            
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ApplicationExit += Application_ApplicationExit;
            //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        //private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
        //    log.Warn("Swallowing UnobseredTaskException: {0}", e.Exception);
        //    e.SetObserved();
        //}

        static void Application_ApplicationExit(object sender, EventArgs e) {
            log.Info("App shutting down");
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            log.FatalException(String.Format("Unhandled AppDomain exception, IsTerminating={0}", e.IsTerminating), (Exception) e.ExceptionObject);
        }


    }
}
