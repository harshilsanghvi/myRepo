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
using System.Data;
using System.Data.OleDb;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using System.Diagnostics;
using System.IO;
namespace RanorexDemo.Library.Utilities
{
    /// <summary>
    /// Description of DataReader.
    /// </summary>
    [TestModule("D47FEB9E-4DF5-49DB-990E-147CE0F53BCA", ModuleType.UserCode, 1)]
    public class DataReader : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public DataReader()
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
        public static DataTable ReadMyExcel(string filePath, string sheetNames)
        {
        	DataTable table = new DataTable();
        	try
        	{
				String connString = GetConnectionString(filePath);
				string sheet = sheetNames + "$";
	            using (OleDbConnection dbConnection = new OleDbConnection(connString))
    	        {
        	        OleDbDataAdapter dbAdapter = new OleDbDataAdapter("SELECT * FROM [" + sheet + "]", dbConnection);
                	dbAdapter.Fill(table);
            	}
        	}
        	catch(Exception ex)
            {
        		StackTrace sf = new StackTrace();
            	Report.Failure("Exception Occurred in "+sf.GetFrame(1).GetMethod().Name +"Error"+ex.Message);
            	throw ex;
            }
        	return table;
        }
        
        private static string GetConnectionString(string filePath)
        {
        	string excelConnectionString = "";
        	try
        	{
	            if (Path.GetExtension(filePath) == ".xlsx")
	            {
	                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
	                                                    ";Mode=ReadWrite;Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";
	            }
	            // xls extension file
	            else if (Path.GetExtension(filePath) == ".xls")
	            {
	                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
	                                                   ";Mode=ReadWrite;Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";
	            }
	            else
	            {
	                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
	                    "Data Source=" + filePath + ";Extended Properties=Excel 8.0;";
	            }
        	}
        	catch(Exception ex)
            {
            	StackTrace sf = new StackTrace();
            	Report.Failure("Exception Occurred in "+sf.GetFrame(1).GetMethod().Name +"Error"+ex.Message);
            	throw ex;
            }
            return excelConnectionString;
        }
        
        public static DataRow GetUser(DataTable dt, string ColumnName, string ColValue)
        {
        	DataRow dr= null;
        	DataRow[] result = null;
        	try
        	{
	        	result = dt.Select(ColumnName +" = '" + ColValue + "'");
		        //DataRow[] result = dt.Select("TestID = '" + TestCaseId + "'");
		        if(result.Length == 1  )
		        {
			        dr= result[0];
			        dr.ToString();
		        }
        	}
        	catch(Exception ex)
            {
            	StackTrace sf = new StackTrace();
            	Report.Failure("Exception Occurred in "+sf.GetFrame(1).GetMethod().Name +"Error"+ex.Message);
            	throw ex;
            }
	        return dr;
        }
        
        public static DataRow GetUser(DataTable dt, string ColumnName, string ColValue,string ColumnName1, string ColValue1)
        {
        	DataRow dr= null;
        	DataRow[] result = null;
        	try
        	{
	        	result = dt.Select(ColumnName +" = '" + ColValue + "' AND "+ ColumnName1 +" = '" + ColValue1 + "'");
		        //DataRow[] result = dt.Select("TestID = '" + TestCaseId + "'");
		        if(result.Length == 1  )
		        {
			        dr= result[0];
			        dr.ToString();
		        }
        	}
        	catch(Exception ex)
            {
            	StackTrace sf = new StackTrace();
            	Report.Failure("Exception Occurred in "+sf.GetFrame(1).GetMethod().Name +"Error"+ex.Message);
            	throw ex;
            }
	        return dr;
        }
        
        public static Dictionary<string, string> LoadData(DataRow dr)
        {
        	Dictionary<string , string> dic= new Dictionary<string, string>();
        	try
        	{
	        	for(int i=0; i <= dr.Table.Columns.Count-1; i++ )
	        	{
	        		dic.Add(dr.Table.Columns[i].ColumnName, dr[i].ToString() );
	        	}         	
        	}
        	catch(Exception ex)
            {
            	StackTrace sf = new StackTrace();
            	Report.Failure("Exception Occurred in "+sf.GetFrame(1).GetMethod().Name +"Error"+ex.Message);
            }
         	return dic;      	
        }
    	
        public static void PrintDictionary(Dictionary<string, string> dictionarydata)
        {
        	string arr = "Current Variable Values: ";
        	try
        	{
	        	foreach(KeyValuePair<string, string> kvp in dictionarydata)
				{
					arr += string.Format("${0} = '{1}'  ",kvp.Key,kvp.Value);
				}
        	}
        	catch(Exception ex)
            {
            	Report.Failure("Exception Occurred "+ex.Message);
            }
			Report.Info(arr);
        }
      
        /// <summary>
        ///Method will return DataTable of the respective CSV file   
        ///</summary>
        public static DataTable CsvFileToTable(string FilePath)
        {
            //Create an datatable object and initialize it
            DataTable dt = new DataTable();
            try
            {
	            //Create an datarow object
	            DataRow row;
	
	            //Get the number of rows in the provided .CSV filepath
	            string[] csvRows = System.IO.File.ReadAllLines(FilePath);
	            //Get the total csvColumn of the first row in the .CSV file
	            string[] csvColumn = csvRows[0].Split(',');
	
	            //Add the first row data as the column of the datatable
	            for (int count = 0; count < csvColumn.Length; count++)
	            {
	                dt.Columns.Add(csvColumn[count].ToString().Trim());
	            }
	            //Add all the rows(exculding the first one) to the table below the respective column name
	            for (int count = 1; count < csvRows.Length; count++)
	            {
	                if (!string.IsNullOrEmpty(csvRows[count]))
	                {
	                    string[] csvValues = csvRows[count].Split(',');
	                    row = dt.NewRow();
	                    for (int f = 0; f < csvColumn.Length; f++)
	                    {
	                        row[f] = csvValues[f];
	                    }
	                    dt.Rows.Add(row);
	                }
	            }
            }
            catch(Exception ex)
            {
            	StackTrace sf = new StackTrace();
            	Report.Failure("Exception Occurred in "+sf.GetFrame(1).GetMethod().Name +"Error"+ex.Message);
            	throw ex;
            }
            return dt;
        }
    
        public static void UpdateExcelFile(string filePath, string sheetName, string forColumnName1, string forValue1, string forColumnName2, string forValue2, string insertColumn, string insertValue)
		{
		    DataSet ds = new DataSet();
		    String connString = GetConnectionString(filePath);
		    using (OleDbConnection conn = new OleDbConnection(connString))
		    {
		        conn.Open();
		        OleDbCommand cmd = new OleDbCommand();
		        cmd.Connection = conn;
		        cmd.CommandText = "UPDATE [" + sheetName + "] SET "+insertColumn+" = '"+insertValue+"' where "+forColumnName1+" = 'Mumbai' and Destination = 'Bangalore'";
		        cmd.ExecuteNonQuery();
		        cmd = null;
	        	conn.Close();
			 }
		}
    
    	public static void WriteIntoExcel(string filePath,string strTestCaseName, string strPolicyNo, string strClaimNo)
		{
        	try
        	{
	        	DataSet ds = new DataSet();
			    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+filePath+";Extended Properties=Excel 12.0;";
	        	using (OleDbConnection conn = new OleDbConnection(connectionString))
			    {
			        conn.Open();
			        OleDbCommand cmd = new OleDbCommand();
			        cmd.Connection = conn;
			       //  Get all Sheets in Excel File
			        DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
//			        //Loop through all Sheets to get data
//			        bool flag = false;
//			        foreach (DataRow dr in dtSheet.Rows)
//			        {
//			            string sheetN = dr["TABLE_NAME"].ToString();
//			            if(sheetN == "ClaimsOutput")
//			            {
//			        		flag = true;    	
//						}
//			        }
					
			        string currentDate = System.DateTime.Now.ToString();
			      	cmd.CommandText = "INSERT INTO [ClaimsOutput] (TestScript,PolicyNumber,ClaimNumber) VALUES('"+strTestCaseName+"','"+strPolicyNo+"','"+strClaimNo+"');";
					cmd.ExecuteNonQuery();
			        cmd = null;
		        	conn.Close();
			 	}
        	}
        	catch(Exception ex)
        	{
        		Report.Failure("Fail to Write into Excel ", ex.Message);
        	}
		}
    	
    	public static string CreateExcel(string strApplicationName, string strResultFolderPath, string timestamp)
    	{
    		String excelFile = "";
    		try
    		{
//    			string reportFileDirectory = Ranorex.Core.Reporting.TestReport.ReportEnvironment.ReportFileDirectory.ToString();
//	    		excelFile = filePath+strApplicationName+"_"+System.DateTime.Now.ToString("yyyymmdd")+"_"+ System.DateTime.Now.ToString("hhmmss")+".xlsx";
				string ExecutionInstanceName = strApplicationName+"_"+timestamp;
        		System.IO.Directory.CreateDirectory(strResultFolderPath+ExecutionInstanceName);
				excelFile = strResultFolderPath+ExecutionInstanceName+"\\"+ExecutionInstanceName+".xlsx";
				
				string TabName ="ClaimsOutput";
				string xConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+excelFile+";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
				var conn = new OleDbConnection(xConnStr);
				string ColumnName ="TestScript varchar(255), PolicyNumber varchar(255), ClaimNumber varchar(100)";
				conn.Open();
				var cmd = new OleDbCommand("CREATE TABLE [" + TabName + "] (" + ColumnName + ")", conn);
				cmd.ExecuteNonQuery();
				conn.Close();
    		}
    		catch(Exception ex)
    		{
    			StackTrace sf = new StackTrace();
            	Report.Failure("Exception Occurred in "+sf.GetFrame(1).GetMethod().Name +"Error"+ex.Message);
            	throw ex;
    		}
			return excelFile;
	       	
    	}
    }
}
