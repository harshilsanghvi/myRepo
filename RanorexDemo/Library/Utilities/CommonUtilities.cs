/*
 * Created by Ranorex
 * User: vineet singh, jkumud, ashutosh kumar, harshil sanghvi
 * Date: 9/16/2015
 * Time: 4:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
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
    /// Description of Utilities.
    /// </summary>
    [TestModule("17DE8FFF-B5C3-4A8C-A36C-4394DB8BDCAE", ModuleType.UserCode, 1)]
    public class CommonUtilities : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CommonUtilities()
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
          /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        /// 
        public static string getAppValue (string key)
        {
        	string value = "3";
//        		System.Configuration.ConfigurationSettings.AppSettings[key].ToString();
        	return value;
        }
       
        /// <summary>
        /// create 6 digit unique number
        /// </summary>
        /// <returns>6 digit numbar</returns>
        public static string generateUniqueNumber()
        {
	        try
	        {
	        	Random r = new Random();
	        	int x= r.Next(0,1000000);
	        	string sixdigituniquenumber=x.ToString("D6");
	        	return sixdigituniquenumber;
	        }
        
	        catch(Exception ex)
	        {
	        	Report.Failure("Exception Occurred"+ex.Message);
	        	throw;
	        }
        }
       /// <summary>
       /// Copy file from old path to new path
       /// </summary>
       /// <param name="OldPath">old path of file</param>
       /// <param name="NewPath">new path of file</param>
       
        public static void copyFile(string OldPath,string NewPath)
        {
	        try
	        {
		        File.Copy(OldPath, NewPath);
	        }
        
	        catch(Exception ex)
	        {
	        	Report.Failure("Exception Occurred"+ex.Message);     
	        }
        }
        /// <summary>
          /// This Method used for executing the vBS file
          /// </summary>
          /// <param name="filePath">Path of VBS file</param>
        public void executeVBSFile(string filePath)
          {
          	try
          	{
          		System.Diagnostics.Process.Start(filePath);		              	
          	}
          	catch(Exception e)
          	{
          		Report.Failure("Exception Occured-"+e.Message);
          	}
          }
    
    }
}
