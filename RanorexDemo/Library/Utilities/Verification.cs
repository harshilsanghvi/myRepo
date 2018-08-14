/*
 * Created by Ranorex
 * User: vineet singh, jkumud, ashutosh kumar, harshil sanghvi
 * Date: 8/10/2015
 * Time: 1:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using System.Diagnostics;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace RanorexDemo.Library.Utilities
{
    /// <summary>
    /// Description of Verification.
    /// </summary>
    [TestModule("E1973FD2-ABED-47C6-A481-A4E99B776204", ModuleType.UserCode, 1)]
    public class Verification : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// 
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
        public Verification()
        {
            // Do not delete - a parameterless constructor is required!
        }

      
        
       /// <summary>
       /// validate the element to exist
       /// </summary>
      /// <param name="element">Elemnt name</param>
    
        public static void isElementExist(Element element)
        {
        	try
        	{
        		int i=1;
           		 for(;;)
	            {
	            	Boolean windowExist=element.Visible;
	            	if(windowExist)
	            		{
	            			break;
	            		}
	            	Delay.Seconds(5);
	            	i++;
	            	if(i==24)
	            	{
	            		
	            		throw new ElementNotFoundException();
	        			
	            	}
	            }
        	}
        	catch(Exception e)
        	{
        		throw e;
        	}
        }
        
        /// <summary>
        /// This is used for validated check box status
        /// </summary>
        /// <param name="element">elemnt xpath</param>
        /// <param name="checkboxstate">checkbox status to vaildate</param>
        /// <param name="controlName">logical contol name</param>
        
        public static void validateCheckboxstatus(Ranorex.CheckBox element,string checkboxstate, string controlName)  
        {       
        	
            Ranorex.CheckBox checkbox = element;
            if(checkbox.Checked)
            {
            	if(checkboxstate == "check")
            	{
            		Report.Success(controlName+ " is Checked");
            	}
            	else if(checkboxstate == "uncheck")
            	{
            		Report.Failure(controlName+" Check box is Checked");
            	}
            }
            else if(!checkbox.Checked)
            {
                if(checkboxstate == "check")
            	{
            		Report.Failure(controlName+ " is Unchecked");
            	}
            	else if(checkboxstate == "uncheck")
            	{
            		Report.Success(controlName+" Check box is Unchecked");
            	}
            }
   
         }

         /// <summary>
        /// Verify the records in Table
        /// </summary>
        /// <param name="ObjTable">TableTag Object</param>
        /// <param name="strCellData">Values To Verify in Table</param>
        /// <returns>Bool Value</returns>
        public static bool VerifyRecordInTable(TableTag ObjTable, params string[] strCellData)
        {
        	bool flag = false;
        	bool TestFlag = false;
        	string strCellValue = null;
           	
        	try
        	{
        		foreach(var item in strCellData)
        		{
        			foreach(TrTag tr in ObjTable.Find(".//tr"))
        			{
    					 foreach(TdTag td in tr.Find(".//td"))
						 {
					 		strCellValue = td.InnerText.ToString();
						 	if(strCellValue == item.ToString() || strCellValue.Contains(item.ToString()))
						 	{
						 		flag = true;
						 		TestFlag = true;
						 		break;
						 	}
						 }
						 if(TestFlag == true)
						 {
						 	break;
						 }
        			}
        			if(flag == false)
        			{
        				break;
        			}
        		}
        	}
        	catch(Exception ex)
        	{
        		Report.Info("Fail to VerifyrecordInTable ",ex.Message);
        	}
        	return flag;
        } 
        
    
    }
}
