/*
 * Created by Ranorex
 * User: ny102891
 * Date: 6/9/2017
 * Time: 3:49 AM
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
using System.Diagnostics;
using System.IO;
using System.Data.OleDb;
using System.Data;


using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Core.Reporting;
//using BindingAuthority_Automation.Library.Utilities;
//using BindingAuthority_Automation.Scripts;
//using BindingAuthority_Automation.Library.IOAccess;
using RanorexDemo.Library.Utilities;
using RanorexDemo.TestScript;

namespace BindingAuthority_Automation
{
	/// <summary>
	/// Description of Driver.
	/// </summary>
	[TestModule("04E8077C-03F5-4ACB-AB17-BEF50E6C7705", ModuleType.UserCode, 1)]
	public class Driver : ITestModule
	{
		//Path variables
		public string TestPath {get;set;}
		public static bool reRun = false;
		public static string TestDataFilePath = "";
		public  readonly static string   TodayDate;
		string ResultFolderPath = "";
		//Set Dictionary drDriverData to get Test data for Policy Type
		public static string filePath = @Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public Driver()
		{
			// Do not delete - a parameterless constructor is required!
			ResultFolderPath =  filePath+"\\Reports\\";
			
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
			try
			{
				
				
				String _strPathRegionalWorkFlowSheet= filePath+"\\TestData\\BindingAuthority_Testdata.xlsx";
				//Ranorex.Controls.ProgressForm.Hide();
				//Read Driver Data sheet
				
				//TestDataFilePath = GetSheetPath(strEntityName);
				string timestamp = System.DateTime.Now.ToString("yyyyMMdd") +"_"+ System.DateTime.Now.ToString("hhmmss");
				
				//Report Enviornment Setup
				ReportClass.ReportSetup("BindingAuthority",ResultFolderPath, timestamp);

				//Read Driver Data sheet
				DataTable dt= DataReader.ReadMyExcel(_strPathRegionalWorkFlowSheet,"Driver_BindingAuthority");
				//DataReader.EmptyReRun(TestDataFilePath,"Driver_BindingAuth$");
				
				//Read each Row in Driver_Project
				foreach (DataRow dr in dt.Rows)
				{
					Dictionary<string,string> drDriverData= DataReader.LoadData(dr);
					
					//Read Row needs to be executed TestCaseName
					if(drDriverData["Execute"]=="Y")
					{
						//--------------------------Test Suite Start
						TestReport.BeginTestSuite("Binding Authority","Comment");
						//--------------------------Start Test Case
//						TestReport.BeginTestCase(drDriverData["TestCaseNo"] +" - " + drDriverData["TestCaseName"]);
						DataTable dtTestData = new DataTable();
						
						//Read Rows from Test Data Sheet and get Test Script row count
						dtTestData= DataReader.ReadMyExcel(_strPathRegionalWorkFlowSheet,drDriverData["DataSheet"]);
						DataRow[] filteredRows =  dtTestData.Select("TestCaseName='"+drDriverData["TestCaseName"]+"'");
						foreach (DataRow testdatadr in filteredRows)
						{
							Dictionary<string,string> drTestData = DataReader.LoadData(testdatadr);
							int iterationcount = Int32.Parse(drTestData["Iteration"]);
							int itcount=1;
							if(iterationcount>1)
							{
//								String test="Testing";
//								ActivityExecType act= ActivityExecType.RunIteration;
//								TestEntryActivityType tactivty=TestEntryActivityType.TestCase;
								for(int j = 1; j<=iterationcount;j++)
								{
									//ReportClass.errorValue = 0;
									
//									TestReport.BeginTestContainer(itcount,test,act,tactivty);
//									TestReport.BeginTestCase(drTestData["TestCaseName"],test);
									//TestReport.
									TestReport.BeginTestModule(drDriverData["TestCaseName"]);
									TestModuleRunner.Run(callTestCase(drDriverData["TestCaseName"],drTestData));
									TestReport.EndTestModule();
									//	TestReport.EndTestEntryContainer();
//									TestReport.EndTestCase();
									itcount++;
								}
							}
							else
							{
								if(iterationcount==1)
								{
									//ReportClass.errorValue = 0;
									TestReport.BeginTestModule(drDriverData["TestCaseName"]);
									TestModuleRunner.Run(callTestCase(drDriverData["TestCaseName"],drTestData));
//									TestReport.EndTestModule();
//									if(reRun)
//										DataReader.UpdateExcelFile(TestDataFilePath,"Driver_RegionalWorkflow","TestCaseName",drDriverData["TestCaseName"],"ReRun","Y");
								}
							}
						}
						//End Test Case
//						TestReport.EndTestCase();
					}
				}
				
//				//Read each Row in Driver_Project
//				foreach (DataRow dr in dt.Rows)
//				{
//					Dictionary<string,string> drDriverData= DataReader.LoadData(dr);
//
//					//Read Row needs to be executed TestCaseName
//					if(drDriverData["ReRun"]=="Y")
//					{
//						//--------------------------Test Suite Start
//						TestReport.BeginTestSuite("Regional WorkFlow","Comment");
//						//--------------------------Start Test Case
//						TestReport.BeginTestCase(drDriverData["TestCaseName"]);
//						DataTable dtTestData = new DataTable();
//
//						//Read Rows from Test Data Sheet and get Test Script row count
//						dtTestData= DataReader.ReadMyExcel(_strPathRegionalWorkFlowSheet,drDriverData["DataSheet"]);
//						DataRow[] filteredRows =  dtTestData.Select("TestCaseName='"+drDriverData["TestCaseName"]+"'");
//						foreach (DataRow testdatadr in filteredRows)
//						{
//							Dictionary<string,string> drTestData = DataReader.LoadData(testdatadr);
//							TestReport.BeginTestModule(drDriverData["TestCaseName"]);
//							TestModuleRunner.Run(ApplicationFunctions.callTestCase(drDriverData["TestCaseName"],drTestData));
//							TestReport.EndTestModule();
//						}
//						//End Test Case
//						TestReport.EndTestCase();
//					}
//				}
				
				
			}
			catch(Exception ex)
			{
				Report.Failure("Exception Occured :",ex.Message);
			}
			
		}
		public static Ranorex.Core.Testing.ITestModule callTestCase(string strTestCaseName,Dictionary<string,string> drTestData)
		{

			//Create objects for Test Scripts
			switch (strTestCaseName)
			{
				case "DemoScenario":
					LoginDemoaut_CM loginscript = new LoginDemoaut_CM(drTestData);
					return loginscript;
				default:
					LoginDemoaut_CM def = new LoginDemoaut_CM(drTestData);
					return def;
					//break;
			}
		}
		
//		public static Ranorex.Core.Testing.ITestModule callTestCase(string strTestCaseName,Dictionary<string,string> drTestData)
//		{
//
//			//Create objects for Test Scripts
//			switch (strTestCaseName)
//			{
//		/*		case "Login":
//					Login VT = new Login(drTestData);
//					return VT;
//				case "KC_91111_IN_Production_Contractors":
//					KC_91111_IN_Production_Contractors TC1 = new KC_91111_IN_Production_Contractors(drTestData);
//					return TC1;
//				case "KC_91127_SC_Production2_Contractors":
//					KC_91127_SC_Production2_Contractors TC2 = new KC_91127_SC_Production2_Contractors(drTestData);
//					return TC2; */
//				case "KC_92478_VT_Production_Contractors":
//					KC_92478_VT_Production_Contractors VT = new KC_92478_VT_Production_Contractors(drTestData);
//					return VT;
//
//				default:
//					Login ba = new Login(drTestData);
//					return ba;
//			}
//		}
	}
}

