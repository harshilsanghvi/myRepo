/*
 * Created by Ranorex
 * User: hsanghvi
 * Date: 6/22/2017
 * Time: 10:35 AM
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

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Core.Reporting;

namespace RanorexDemo.Library.Utilities
{
	/// <summary>
	/// Description of ReportClass.
	/// </summary>
	[TestModule("EB786754-B7DC-414F-984A-354CFDFEA27F", ModuleType.UserCode, 1)]
	public class ReportClass : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public ReportClass()
		{
			// Do not delete - a parameterless constructor is required!
		}
		public static String Reportfilelocation = "";
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
		
		public static void ReportSetup(string strApplicationName, string strResultFolderPath, string timestamp)
		{
			try
			{
				string ExecutionInstanceName = strApplicationName+"_"+timestamp;
				System.IO.Directory.CreateDirectory(strResultFolderPath+ExecutionInstanceName);
				ReportClass.Reportfilelocation = strResultFolderPath+ExecutionInstanceName;
				String Reportfile = strResultFolderPath+ExecutionInstanceName+"\\"+ExecutionInstanceName+".html";
				TestReport.Setup(ReportLevel.Info,Reportfile,true);
//				TestReport.ReportEnvironment.UseScreenshotFolder = true;
			}
			catch(Exception ex)
			{
				Report.Failure("Exception Occurred "+ex.Message);
			}
		}
	}
}
