/*
 * Created by Ranorex
 * User: hsanghvi
 * Date: 6/22/2017
 * Time: 10:03 AM
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
using RanorexDemo.TestScript;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using RanorexDemo.Library.Utilities;
namespace RanorexDemo.Library.Application
{
	/// <summary>
	/// Description of DemoAutFunction.
	/// </summary>
	[TestModule("2A2B5436-AD54-4509-A074-2AEB95379B53", ModuleType.UserCode, 1)]
	public class DemoAutFunction : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public DemoAutFunction()
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
		
		public static void Login()
		{
			RanorexDemoRepository repo = new RanorexDemoRepository();
			
			Web_StepExecutor.enterText(repo.Mercury.Home.UserName,"mercury","Username field");
			
			Web_StepExecutor.enterText(repo.Mercury.Home.Password,"mercury","Password field");
			
			Web_StepExecutor.click(repo.Mercury.Home.Login,"Sign in button");
			
			repo.MercuryFlight.FlightConfirmation.Textfield_FlightFinderInfo.WaitForExists(30000);
			Report.Success("User logged in Successfully");
			
			Report.Screenshot();
		}
		
		public static void EnterDetails()
		{
			RanorexDemoRepository repo = new RanorexDemoRepository();
			//Flight Details Page
			Web_StepExecutor.click(repo.MercuryFlight.BookAFlight.TripType,"One way radio button");
			
			Web_StepExecutor.enterText(repo.MercuryFlight.BookAFlight.PassCount,"2","Passenger count field");
			
			Web_StepExecutor.enterText(repo.MercuryFlight.BookAFlight.ToPort,"Frankfurt","Arriving in field");
					
			Web_StepExecutor.click(repo.MercuryFlight.BookAFlight.BusinessClass,"Business class radio button");
			
			Web_StepExecutor.click(repo.MercuryFlight.FlightConfirmation.FindFlights,"Continue button");
						
			repo.MercuryFlight.OutFlightInfo.WaitForExists(20000);
			Report.Success("Page Navigated to Select Flight page");
			Report.Screenshot();
			
			//Select Flights Page
			Web_StepExecutor.click(repo.MercuryFlight.OutFlight,"Flight radio button");
			
			Web_StepExecutor.click(repo.MercuryFlight.FlightConfirmation.ReserveFlights,"Continue button");
						
			//Book a Flight Page
			repo.MercuryFlight.FlightConfirmation.PassFirst0Info.WaitForExists(20000);
			Report.Success("Page navigated to Book a Flight page");
			
		}
		
		public static void PurchaseFlight(string strFName, string strLName)
		{
			string strFlightconfirmationNo = "";
			RanorexDemoRepository repo = new RanorexDemoRepository();
			
			//Entering Values in Passenger Details text boxes
			Web_StepExecutor.enterText(repo.MercuryFlight.FlightConfirmation.PassFirst0,strFName,"First name field");
			
			Web_StepExecutor.enterText(repo.MercuryFlight.FlightConfirmation.PassLast0,strLName,"Last name field");
			
			Web_StepExecutor.enterText(repo.MercuryFlight.FlightConfirmation.PassFirst1,"Harsh","First name field");
			
			Web_StepExecutor.enterText(repo.MercuryFlight.FlightConfirmation.PassLast1,"Shah","Last name field");
			
			Web_StepExecutor.enterText(repo.MercuryFlight.FlightConfirmation.Creditnumber,"123456789","Credit Card Number field");
			
			Web_StepExecutor.click(repo.MercuryFlight.FlightConfirmation.SecurePurachse,"Secure Purchase button");
			
			repo.MercuryFlight.FlightConfirmation.Text_FlightNoInfo.WaitForExists(20000);
			Report.Success("Page Navigated to Flight Confirmation screen");
			Report.Screenshot();
			
			strFlightconfirmationNo = repo.MercuryFlight.FlightConfirmation.Text_FlightNo.InnerText.ToString();
			Report.Info(strFlightconfirmationNo);
			
			repo.MercuryFlight.FlightConfirmation.TotalPriceIncludin.EnsureVisible();
			Report.Screenshot();
			
			Web_StepExecutor.click(repo.MercuryFlight.FlightConfirmation.Btn_Logout,"Logoutbutton");
			
			repo.MercuryFlight.FlightConfirmation.Textfield_SignOnInfo.WaitForExists(20000);
			Report.Success("User Logged out successfully");
			Report.Screenshot();
		}
		
	}
}
