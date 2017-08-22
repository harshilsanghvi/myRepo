/*
 * Created by Ranorex
 * User: vineet singh, jkumud, ashutosh kumar, harshil sanghvi
 * Date: 4/9/2015
 * Time: 4:02 PM
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
    /// Description of ObjControl.
    /// </summary>
    [TestModule("1D924280-9B9A-49AE-8CEF-0E381CAC9139", ModuleType.UserCode, 1)]
    public class Windows_StepExecutor : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Windows_StepExecutor()
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
        public static void SelectOption(ButtonTag DropButton, SelectTag selectBox, string itemName)  
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
        public static void Click(Element element, string strControlName)
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
        public static void selectComboBoxOption(Ranorex.ComboBox combobox,string Option,string ControlName)
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
    	/// Used for enter text in text box
    	/// </summary>
    	/// <param name="element">element xpath</param>
    	/// <param name="strText">Valut to enter</param>
    	/// <param name="ControlName">logical name of control name</param>
        public static void enterText(Element element, string strText,string ControlName)
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
        public static void checkbox(Ranorex.CheckBox element,string state)  
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
        
     
    }
}
