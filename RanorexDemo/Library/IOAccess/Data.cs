/*
 * Created by Ranorex
 * User: vineet singh, jkumud, ashutosh kumar, harshil sanghvi
 * Date: 4/9/2015
 * Time: 2:37 PM
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
using System.Reflection; 
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using System.Data;
using RanorexDemo.Library.Utilities;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel; 

namespace RanorexDemo.Library.IOAccess
{
    /// <summary>
    /// Description of Data.
    /// </summary>
    [TestModule("7AAB040A-B562-45BE-9F0C-AD3AD05BD595", ModuleType.UserCode, 1)]
    public class Data : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Data()
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
        public static void writedata()
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection ;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                string sql = null;
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\\Users\\hsanghvi\\Desktop\\Checkwrite.xls';Extended Properties=Excel 8.0;");
                MyConnection.Open();
                myCommand.Connection = MyConnection;
                int strid = 23;
//                string strdata = "check";
                sql = "Insert into [Sheet1$] ('id') values("+strid+")";
                myCommand.CommandText = sql;
                myCommand.ExecuteNonQuery();
                MyConnection.Close();
            }
            catch (Exception ex)
            {
                Report.Failure("Failuer in write"+ex.Message);
            }
        }
        
        public static DataTable GetValue(string strfileName, string strSheetName)
        {
                var rowTable = new DataTable();
                string strPath = CommonUtilities.getAppValue("directory") + "TestData\\" + strfileName + ".xlsx";
                // strPath = @"..\..\TestData\" + strfileName + ".xlsx";
                // strPath = System.Environment.CurrentDirectory + @"\TestData\" + strfileName + ".xlsx";
                
                try
                {
                	// The connector won´t throw an exception if it´s created using a filepath that is incorrect but loading the data will fail, therefore I check, if the excel file exists first
                  	if(System.IO.File.Exists(strPath))
          			{
                		// Now we create the connector itself
                		// Note that "Tabelle1" is the name of the worksheet. On a english os, this probably has to be renamed to Worksheet1
                		// A:A defines the column range. It is also possible to get those automatically by setting this to "Auto-Range"
                  	    // Ranorex.Core.Data.ExcelDataConnector excelConnector = new Ranorex.Core.Data.ExcelDataConnector("excelConnector",@"..\..\TestData\" + strfileName + ".xlsx", strSheetName,"Auto-Range",System.Windows.Forms.CheckState.Unchecked);
                        Ranorex.Core.Data.ExcelDataConnector excelConnector = new Ranorex.Core.Data.ExcelDataConnector("excelConnector", strPath, strSheetName,"Auto-Range", System.Windows.Forms.CheckState.Unchecked);                      
                		
                        // Load all Data from the Excel file
                  		Ranorex.Core.Data.ColumnCollection columnCollection;
                  		Ranorex.Core.Data.RowCollection rowCollection;
                  		excelConnector.LoadData(out columnCollection, out rowCollection);
		                // excelConnector.StoreData(1,100);                       
                                                
					    // We can now use the data - e.g. add all Texts of the first Excel column to a List of Strings                       
	                    foreach(var item in columnCollection)
		                {
		                   rowTable.Columns.Add(new DataColumn(item.Name,typeof(string)));
		                }
                                                
	                    foreach(var item in rowCollection)
	                    {
	                        var dataRow = rowTable.NewRow();
	                        dataRow.ItemArray = item.Values;
	                        rowTable.Rows.Add(dataRow);
	                    }  
                  	}
                }
                catch(Exception ex)
                {
                   Report.Failure("Fail to GetValue from WB : " + strfileName + " and WC : " + strSheetName, ex.Message);
                } 
                return rowTable;
        }
        
        /// <summary>
        /// Creates a data set from CSV file
        /// </summary>
        /// <param name="filename">Name of csv file</param>
        /// <param name="delimiter">type of delimiter</param>
        /// <returns>object of dataset</returns>
        public static DataSet GetDataSet(string filename, char delimiter = ',')
        {
            var dataset = new DataSet();
            char delimited = ',';
            if (delimiter == '|')
            {
                delimited = delimiter;
            }
            else
            {
                delimited = ',';
            }

            try
            {
                if (filename.Length > 0 && IsFileNameValid(Path.GetFileName(filename)))
                {
                    var rowTable = new DataTable();
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        string singleRow = default(string);
                        bool isHeader = true;
                        while ((singleRow = reader.ReadLine()) != null)
                        {
                            var rowValues = singleRow.Split(delimited);
                            if (isHeader)
                            {
                                isHeader = false;
                                for (int i = 0; i < rowValues.Length; i++)
                                {
                                    rowTable.Columns.Add(new DataColumn(rowValues[i].Replace(@"""", string.Empty), typeof(string)));
                                }
                            }
                            else
                            {
                                var csvdataRow = rowTable.NewRow();
                                //csvdataRow.ItemArray = rowValues.ToArray<object>();
                                rowTable.Rows.Add(csvdataRow);
                            }
                        }
                    }

                    dataset.Tables.Add(rowTable);
                }
            }
            catch (Exception ex)
            {
                Report.Failure("Fail to GetDataSetFromCsv for file " + filename, ex.Message);
                throw ex;
            }

            return dataset;
        }
        
        /// <summary>
        /// Check for a valid file name
        /// </summary>
        /// <param name="fileName">Filename to be validated</param>
        /// <returns>True if file name is valid and False if not valid</returns>
        private static bool IsFileNameValid(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (fileName.IndexOf(c) > 0)
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Function to SetValue to csv file by passing field data
        /// </summary>
        /// <param name="strFilePath">strFilePath</param>
        /// <param name="strFielddata">strFielddata</param>
        
        
        public static void SetData(string strFilePath)
        {
        	FileStream fs = null;
        	try
            {
                FileInfo finfo = new FileInfo(strFilePath);
                using (fs = finfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                	// Initialise stream object with file
                    using (var wr = new StreamWriter(fs))
                    {
                    
                    }
                }
        	}
        	catch(Exception e)
        	{
        		Report.Failure(e.Message);
        	}
        }
    
    }
}
