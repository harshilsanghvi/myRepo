/*
 * Created by Ranorex
 * User: hsanghvi
 * Date: 6/22/2017
 * Time: 8:53 AM
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
using RanorexDemo.Library.Application;
using RanorexDemo.Library.Utilities;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace RanorexDemo.TestScript
{
	/// <summary>
	/// Description of LoginDemoaut_CM.
	/// </summary>
	[TestModule("7C3300B1-0D3E-4D8E-8A47-662F69D5258D", ModuleType.UserCode, 1)]
	public class LoginDemoaut_CM : ITestModule
	{
		Dictionary<string,string> drTestData = new Dictionary<string, string>();
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public LoginDemoaut_CM()
		{
			// Do not delete - a parameterless constructor is required!
		}
		public LoginDemoaut_CM(Dictionary<string,string> testData)
		{
			drTestData = testData;
			
		}
		string _strFirstName = "";
		[TestVariable("e576c71c-49f2-4cbb-9857-0b86a268b332")]
		public string strFirstName
		{
			get { return _strFirstName; }
			set { _strFirstName = value; }
		}

		string _strLastName = "";
		[TestVariable("90c00b0f-5010-47d0-9575-50f278680ff5")]
		public string strLastName
		{
			get { return _strLastName; }
			set { _strLastName = value; }
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
			RanorexDemoRepository repo = new RanorexDemoRepository();
			
			//Launch Browser
			Browser.LaunchAndNavigate(drTestData["URL"],"ie");
			
			DemoAutFunction.Login();

			DemoAutFunction.EnterDetails();
			
			DemoAutFunction.PurchaseFlight(drTestData["FirstName"],drTestData["LastName"]);
			
			//Close Browser
			Browser.closeAllOpenBrowsers();
			
		}
	}
}
