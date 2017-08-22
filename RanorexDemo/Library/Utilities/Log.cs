/*
 * Created by Ranorex
 * User: vineet singh, jkumud, ashutosh kumar, harshil sanghvi
 * Date: 8/19/2015
 * Time: 1:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using System.IO;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace RanorexDemo.Library.Utilities
{
    /// <summary>
    /// Description of Log.
    /// </summary>
    [TestModule("E1D3FC3E-6018-4421-BBCC-2DBDA14D919C", ModuleType.UserCode, 1)]
    public class Log : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Log()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
        }
        public static StringBuilder sb;
        public static void StartLogger()
        {
        sb = new StringBuilder();
        sb.AppendLine("********************!!!!!!!!!!!!! Test Case Started  !!!!!!!!!!!!!!***********************************");
        }
        public static void WriteMessage(string Message)
        {
        //    sb.Append("              "+ DateTime.Now.ToLongTimeString() +" "+Message+ "\n");
                string text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"
                                      );
                sb.AppendLine("*****"+ text+"---"+ Message);
        }
        public static void StopLogger(string testCasename)
        {
            
            
         //using (StreamWriter outfile = new StreamWriter(DateTime.Now.ToLongTimeString() + @"\"+testCasename+".txt"))
         using (StreamWriter outfile = new StreamWriter(@"D:\Internal_POC\Internal_POC\Logs\TESTLOG.txt"))
        {
            outfile.Write(sb.ToString());
        }
        }
        

    }
}
