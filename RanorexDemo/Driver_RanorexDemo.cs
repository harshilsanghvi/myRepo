/*
 * Created by Ranorex
 * User: hsanghvi
 * Date: 6/22/2017
 * Time: 10:21 AM
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
using System.Data;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Core.Reporting;
using RanorexDemo.Library.Utilities;
using RanorexDemo.Library.Application;
using RanorexDemo.TestScript;

namespace RanorexDemo
{
	/// <summary>
	/// Description of Driver_RanorexDemo.
	/// </summary>
	[TestModule("D4A74C00-4E6D-49A2-8540-708F120FE759", ModuleType.UserCode, 1)]
	public class Driver_RanorexDemo : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		
		public Driver_RanorexDemo()
		{
			// Do not delete - a parameterless constructor is required!
			TestDataFilePath = filePath+"\\TestData\\TestData.xlsx";
			ResultFolderPath =  filePath+"\\Reports\\";
		}
		string TestDataFilePath = "";
		string ResultFolderPath = "";
		string envDetails = "";
		string filePath = @Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
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
			try
			{
				string testCaseName;
				
				string timestamp = System.DateTime.Now.ToString("yyyyMMdd") +"_"+ System.DateTime.Now.ToString("hhmmss");
				
				//Read Driver Data sheet
				DataTable dt= DataReader.ReadMyExcel(TestDataFilePath,"Driver_RanorexDemo");
				DataTable dtTestData = new DataTable();
				//Report Envirnment Setup
				ReportClass.ReportSetup("RanorexDemo",ResultFolderPath, timestamp);
				Ranorex.Controls.ProgressForm.Show();
				if(envDetails=="")
				{
					//Read each Row in Driver_ClaimsCenter
					foreach (DataRow dr in dt.Rows)
					{
						Dictionary<string,string> drDriverData= DataReader.LoadData(dr);
						
						//Read Row needs to be executed
						if(drDriverData["Execute"]=="Y")
						{
							testCaseName = drDriverData["TestCaseName"];
							//--------------------------Test Suite Start
							TestReport.BeginTestSuite("Ranorex Demo","Comment");
							//--------------------------Start Test Case
							TestReport.BeginTestCaseContainer(testCaseName);
							int iterationcount = Int32.Parse(drDriverData["Iteration"]);
							int itcount=1;
							
							//Read Rows from Test Data Sheet and get Test Script row count
							dtTestData = DataReader.ReadMyExcel(TestDataFilePath,drDriverData["DataSheet"]);
							DataRow[] filteredRows =  dtTestData.Select("TestCaseNo='"+drDriverData["TestCaseNo"]+"'");
							Dictionary<string,string> drTestData = DataReader.LoadData(filteredRows[0]);
							//------------------------------Start individual Script iteration
							if(iterationcount>1)
							{
								String test="Testing";
								ActivityExecType act= ActivityExecType.RunIteration;
								TestEntryActivityType tactivty=TestEntryActivityType.TestCase;
								for(int j = 1; j<=iterationcount;j++)
								{
									
									TestReport.BeginTestEntryContainer(j,test,act,tactivty);
//										(drDriverData["TestCaseName"],j,test,act,tactivty);
									TestReport.BeginTestModule(testCaseName);
									TestModuleRunner.Run(callTestCase(drDriverData["TestCaseName"],drTestData));
									TestReport.EndTestModule();
									TestReport.EndTestEntryIterationContainer();
									itcount++;
								}
							}
							else
							{
								if(iterationcount==1)
								{
									TestReport.BeginTestModule(testCaseName);
									TestModuleRunner.Run(callTestCase(drDriverData["TestCaseName"],drTestData));
									TestReport.EndTestModule();
								}
							}
							//End Test Case
							TestReport.EndTestCaseContainer();
						}
					}
				}
			}
			catch(Exception ex)
			{
				Report.Failure(ex.Message);
			}
			
		}
		public static Ranorex.Core.Testing.ITestModule callTestCase(string strTestCaseName,Dictionary<string,string> drTestData)
		{

			//Create objects for Test Scripts
			switch (strTestCaseName)
			{
				case "LoginDemoaut_CM":
					LoginDemoaut_CM loginscript = new LoginDemoaut_CM(drTestData);
					return loginscript;
				default:
					LoginDemoaut_CM def = new LoginDemoaut_CM(drTestData);
					return def;
					//break;
			}
		}
		
	}
	
}

