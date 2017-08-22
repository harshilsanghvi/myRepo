/*
 * Created by Ranorex
 * User: vineet singh, jkumud, ashutosh kumar, harshil sanghvi
 * Date: 4/9/2015
 * Time: 2:44 PM
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
using System.Diagnostics;
using RanorexDemo.Library.Utilities;

namespace RanorexDemo.Library.Utilities
{
	/// <summary>
	/// Description of Browser.
	/// </summary>
	[TestModule("78BB579E-F1F0-49AD-B14E-0D2C895A0A2E", ModuleType.UserCode, 1)]
	public class Browser : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public Browser()
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
		/// funtion to launch the application
		/// </summary>
		/// <param name="strURL">strURL</param>
		
		/// <summary>
		/// Kills the specific process from task manager
		/// </summary>
		/// <param name="processName">processName</param>
		public static void CleanProcesses(string processName)
		{
			try
			{
				Process[] processNames = Process.GetProcessesByName(processName);
				foreach (Process item in processNames)
				{
					item.Kill();
					Report.Info(processName+" has been Killed");
				}
			}
			catch(Exception ex)
			{
				Report.Failure("Fail to KillProcesses ", ex.Message);
			}
		}
		/// <summary>
		/// Closes all the open browsers
		/// </summary>
		public static void CloseBrowsers()
		{
			try
			{
				IList<Ranorex.WebDocument> AllDoms = Host.Local.FindChildren<Ranorex.WebDocument>();
				if (AllDoms.Count >=1)
				{
					foreach (WebDocument myDom in AllDoms)
					{
						myDom.Close();
					}
				}
			}
			catch(Exception ex)
			{
				Report.Failure("Fail to Close Browser ", ex.Message);
			}
		}
		
		public static void LaunchAndNavigate(string strURL,string strBrowser)
		{
			try
			{
				Host.Local.OpenBrowser(strURL,strBrowser,"",false,true);
				//Ranorex.WebDocument webdoc = "/dom[1]";
				//webdoc.FullScreen = true;
			}
			catch(Exception ex)
			{
				Report.Failure("Fail to Launch the Application : ", ex.Message);
			}
		}
		
		public static void closeAllOpenBrowsers()
		{
			try
			{
				IList <Ranorex.WebDocument> AllDoms = Host.Local.FindChildren<Ranorex.WebDocument>();
				if (AllDoms.Count >=1)
				{
					foreach (WebDocument myDom in AllDoms)
					{
						if(!myDom.Page.ToString().Equals("blank") || !myDom.Page.ToString().Equals(".rxlog") || !myDom.Page.ToString().Equals("DOM"))
						{
							myDom.Close();
							
						}
					}
					Report.Info("All Browsers Closed");
				}
			}
			catch(Exception ex)
			{
				//        		Report.Failure("Failed to Close browser Error: "+ex.Message);
			}
		}
		
		public static void closeBrowser()
		{
			try
			{
				Process[] processNames = Process.GetProcessesByName("iexplore");
				foreach(Process item in processNames)
				{
					item.Kill();
					Report.Info("Browser Closed");
				}
			}
			
			catch(Exception ex)
			{
				if(!ex.Message.ToString().Contains("denied"))
					Report.Failure("Browser not Closed "+ex.Message);
			}
		}
		
	}
}
