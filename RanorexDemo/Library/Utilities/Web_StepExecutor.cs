/*
 * Created by Ranorex
 * User: vineet singh, jkumud, ashutosh kumar, harshil sanghvi
 * Date: 9/22/2015
 * Time: 3:52 PM
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

namespace RanorexDemo.Library.Utilities
{
    /// <summary>
    /// Description of Windows_StepExecutor.
    /// </summary>
    [TestModule("E731BCFE-374C-4BD0-AFEE-E0D62110A48C", ModuleType.UserCode, 1)]
    public class Web_StepExecutor : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Web_StepExecutor()
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
        /// Select the item from the OptionTag / Drop List
        /// </summary>
        /// <param name="DropButton">DropButton Object</param>
        /// <param name="selectBox">Select Tag / List</param>
        /// <param name="itemName">Option Tag / List Item</param>
        public static void SelectOptionforWeb(ButtonTag DropButton, SelectTag selectBox, string itemName)  
        {
        	try
        	{
	        	Delay.Milliseconds(200);
	        	DropButton.PressKeys("{Enter}");
	        	Delay.Milliseconds(200);
	        	Report.Info("Selecting item '" + itemName + "' in " + selectBox);  
	            // ignore case regex  
	            string itemRegex = "^(?i)" + Regex.Escape(itemName) + "$";  
	            selectBox.EnsureVisible();     
	            OptionTag option = selectBox.FindSingle<OptionTag>("option[@InnerText~'" + itemName + "']");  
	            option.Selected = true;
	            Delay.Milliseconds(200);
	            option.PressKeys("{Enter}");
        	}
        	catch(Exception ex)
        	{
        		Report.Info("INFO", "Fail SelectOption : " + ex.Message);
        	}
        }
        
        /// This method used for click the element
        /// </summary>
        /// <param name="element">element xpath</param>
        /// <param name="strControlName">Logical name of control name</param>
        public static void ClickforWeb(Element element, string strControlName)
        {
        	try
        	{
        		element.EnsureVisible();
        		element.Focus();
        		Mouse.MoveTo(element);
        		Mouse.Click(element,System.Windows.Forms.MouseButtons.Left,Location.Center);
        		Report.Success(strControlName +" is Clicked");
        		Delay.Seconds(1);
        	}
        	catch(Exception ex)
        	{
        		Report.Failure(strControlName +" is not Clicked"+ ex.Message);
        	}
        }
       
     
        /// <summary>
        /// Select Combo Box
        /// </summary>
        /// <param name="combobox">Element xpath</param>
        /// <param name="Option">Value to Select</param>
        /// <param name="ControlName">logical name of control name</param>
        public static void selectComboBoxOptionforWeb(Ranorex.ComboBox combobox,string Option,string ControlName)
        {
        	int i =0;
        	Boolean selected =false;
        try
        	{
        		combobox.Focus();
        		Mouse.Click(combobox);
        		Delay.Milliseconds(1);
        		while(selected==false)
        		{
        			i=i+1;
        			combobox.PressKeys(Option);
        			if (combobox.Text.Equals(Option))
	        			{
	        				selected=true;
	        				Keyboard.Press("{Enter}");
	        				Report.Info(ControlName+" is Selected: "+ Option);
	        			}
        			else
        				{
        				if(i==10)
	        				{
        					Report.Failure("Given Value is Not Present");
        					break;
	        				}
        				Mouse.Click(combobox);
        				Delay.Milliseconds(5);
        				}
        		}
        	}
        catch(Exception ex)
	        {
	        	Report.Failure("Exception Occurred"+ ex.Message);
	        }
        }
        
        /// <summary>
        /// Select Combo Box on Web page
        /// </summary>
        /// <param name="combobox">Element xpath</param>
        /// <param name="Option">Value to Select</param>
        /// <param name="ControlName">logical name of control name</param>
        public static void selectComboBoxOptionforWeb(InputTag combobox,string Option,string ControlName)
        {
        	for(;;)
			{
        		combobox.Focus();
				combobox.Value = "";
				combobox.PressKeys(Option);
				Delay.Seconds(2);
				Keyboard.Press("{TAB}");
				if(combobox.TagValue.Equals(Option))
				   break;
			}
        }
        
    	/// <summary>
    	/// Used for enter text in text box
    	/// </summary>
    	/// <param name="element">element xpath</param>
    	/// <param name="strText">Valut to enter</param>
    	/// <param name="ControlName">logical name of control name</param>
        public static void enterTextforWeb(Element element, string strText,string ControlName)
        {
        	try
        	{
        		element.EnsureVisible();
        		element.Focus();
        		Keyboard.Press(strText);
        		Report.Info(strText+" is entered in "+ ControlName);
        		Delay.Milliseconds(300);
        	}
        	
        	catch(Exception ex)
        	{
        		Report.Failure(element+" strText is not entered "+ ex.Message);
        	}
    	}
        
       /// <summary>
       /// Used for click on check box
       /// </summary>
       /// <param name="element">element xpath</param>
       /// <param name="state">for select=check and for diselect=uncheck</param>
        public static void checkboxforWeb(Ranorex.CheckBox element,string state)  
        {       Boolean beforeclick,afterclick; 
            //create the checkbox from the repository 
            try
            {
            Ranorex.CheckBox checkbox = element;
            if(checkbox.Checked)
            {
              if(state=="check")
              {
              	
              }
              else
              {
                     for(;;)
                     {
                     beforeclick =checkbox.Checked;
                     
                     checkbox.Click();
                     afterclick =checkbox.Checked;
                     if(beforeclick ==afterclick)
                     {
                     	
                     }
                     else
                     {
                           break;
                     }
                     }
                    
              }
            }
            else
            {
              if(state=="uncheck")
              {
                     
              }
              else
              {
                     for(;;)
                     {
                     beforeclick =checkbox.Checked;
                     
                     checkbox.Click();
                     afterclick =checkbox.Checked;
                     if(beforeclick ==afterclick)
                     {
                     }
                     else
                     {
                           break;
                     }
                     }
              }
            }
            }
            catch(Exception e)
            {
            	Report.Failure("Exception occured"+e.Message);
            }
   
         }
    
        public static void click(Element element, string strControlName)
        {
        	try
        	{
        		element.EnsureVisible();
        		element.Focus();
        		Mouse.MoveTo(element);
        		Delay.Seconds(1);
        		Mouse.Click(element,System.Windows.Forms.MouseButtons.Left,Location.Center);
        		Report.Info(strControlName+" is Clicked.");
        		Delay.Seconds(1);
        	}
        	
        	catch(Exception ex)
        	{
        		Report.Failure(strControlName+ " is not Clicked "+ex.Message);
        	}
        }
    	
        public static void Click(Element element, string strControlName)
        {
        	try
        	{
        		element.EnsureVisible();
        		element.Focus();
        		Mouse.MoveTo(element);
        		Delay.Seconds(1);
        		Mouse.Click(element,System.Windows.Forms.MouseButtons.Left,Location.Center);
        		Report.Success(strControlName +" is Clicked");
        		Delay.Seconds(1);
        	}
        	catch(Exception ex)
        	{
        		Report.Failure(strControlName +" is not Clicked"+ ex.Message);
        	}
        }
        
        public static void enterValue(Ranorex.InputTag element, string strText, string strContrlName)
        {
        	try
        	{
        		element.EnsureVisible();
        		element.Focus();
        		element.TagValue = strText;
        		Delay.Seconds(0.5);
        		Report.Info(strText+" is entered in "+strContrlName);
        	}
        	
        	catch(Exception ex)
        	{
        		Report.Failure(strText+" is not entered in "+strContrlName+ex.Message);
        	}
        }
        
        public static void enterText(Element element, string strText, string strContrlName)
        {
        	try
        	{
        		element.EnsureVisible();
        		element.Focus();
        		Keyboard.Press("{END}{SHIFT DOWN}{HOME}{SHIFT UP}{DELETE}");
        		Keyboard.Press(strText);
        		Report.Info(strText+" is entered in "+strContrlName);
        		Delay.Seconds(1);
        	}
        	
        	catch(Exception ex)
        	{
        		Report.Failure(strText+" is not entered in "+strContrlName+ex.Message);
        	}
        }
        
        public static void selectComboBox (Element element, string strValue, string strControlName)
        {
        	try
        	{
        		element.EnsureVisible();
        		element.Focus();
        		Keyboard.Press(strValue);
        		Keyboard.Press("{ENTER}");
        		Report.Info(strValue+" is selected in "+strControlName);
        	}
        	
        	catch(Exception ex)
        	{
        		Report.Failure(strValue+ " is not seleted in "+strControlName+ex.Message);
        	}
        }
    	
        public static void SelectOptionsTagsInTable(SelectTag ObjSelectTag, string strValue, string strControlName)
        {
        	try
        	{
        		OptionTag opt1 = ObjSelectTag.FindSingle(".//option[@text='"+strValue+"']");
        		opt1.EnsureVisible();
        		opt1.Focus();
        		opt1.Selected = true;
        		opt1.PerformClick();
        		Report.Info(strValue+" is selected in "+strControlName);
        	}
        	catch(Exception ex)
        	{
        		Report.Failure("Exception Occurred "+ex.Message);
        	}
        }
        
        public static void selectRadiobutton(Ranorex.InputTag element,string strValue,string ControlName)
        {
        	try
        	{
    			element.EnsureVisible();
        		element.Focus();
        		Mouse.MoveTo(element);
        		Delay.Seconds(1);
    			element.Click();
        		Report.Info(strValue+" is selected in "+ ControlName);
		    	Delay.Milliseconds(300);
        	}
        	catch(Exception ex)
        	{
        		Report.Screenshot();
        		Report.Failure(strValue+" is not selected due to "+ ex.Message);
        	}
        }
        
        public static void selectValue(Ranorex.SelectTag element,string strValue,string ControlName)
        {
        	try
        	{
        		for(int i =0;i<3;i++)
        		{
		        	OptionTag optTag = element.FindSingle(".//option[@innertext='"+strValue+"']");
//		        	optTag.Focus();
//		        	optTag.EnsureVisible();
//		        	element.Click();
//		        	Delay.Seconds(1);
//		        	optTag.Click();
					optTag.Selected = true; 
					
//					if(element.Options.IndexOf(strValue).ToString().Equals(element.InnerText.ToString()))
//						break;
        		}
        		Report.Info(strValue+" is selected in "+ ControlName);
		    	Delay.Milliseconds(300);
        	}
        	catch(Exception ex)
        	{
        		Report.Screenshot();
        		Report.Failure(strValue+" is not selected due to "+ ex.Message);
        	}

    	}
    
    }
}
