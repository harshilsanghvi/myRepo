﻿///////////////////////////////////////////////////////////////////////////////
//
// This file was automatically generated by RANOREX.
// DO NOT MODIFY THIS FILE! It is regenerated by the designer.
// All your modifications will be lost!
// http://www.ranorex.com
//
///////////////////////////////////////////////////////////////////////////////

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
using Ranorex.Core.Repository;

namespace RanorexDemo.TestScript
{
#pragma warning disable 0436 //(CS0436) The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'. Using the type defined in 'assembly'.
    /// <summary>
    ///The PurchaseFilght recording.
    /// </summary>
    [TestModule("87bf38e3-0fd4-4674-97d0-edab1dadf9b1", ModuleType.Recording, 1)]
    public partial class PurchaseFilght : ITestModule
    {
        /// <summary>
        /// Holds an instance of the RanorexDemo.RanorexDemoRepository repository.
        /// </summary>
        public static RanorexDemo.RanorexDemoRepository repo = RanorexDemo.RanorexDemoRepository.Instance;

        static PurchaseFilght instance = new PurchaseFilght();

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public PurchaseFilght()
        {
            strUserName = "";
            strConfirmationNo = "";
            strLName = "Sanghvi";
            strFName = "Harshil";
        }

        /// <summary>
        /// Gets a static instance of this recording.
        /// </summary>
        public static PurchaseFilght Instance
        {
            get { return instance; }
        }

#region Variables

        string _strUserName;

        /// <summary>
        /// Gets or sets the value of variable strUserName.
        /// </summary>
        [TestVariable("21a308ec-c2bd-4946-8665-59ca3f1f4c40")]
        public string strUserName
        {
            get { return _strUserName; }
            set { _strUserName = value; }
        }

        string _strConfirmationNo;

        /// <summary>
        /// Gets or sets the value of variable strConfirmationNo.
        /// </summary>
        [TestVariable("71ebc46e-7b33-4133-8ba9-88ac2aa0eaca")]
        public string strConfirmationNo
        {
            get { return _strConfirmationNo; }
            set { _strConfirmationNo = value; }
        }

        string _strLName;

        /// <summary>
        /// Gets or sets the value of variable strLName.
        /// </summary>
        [TestVariable("13069404-805a-49c0-a1aa-ea1e1d99d360")]
        public string strLName
        {
            get { return _strLName; }
            set { _strLName = value; }
        }

        string _strFName;

        /// <summary>
        /// Gets or sets the value of variable strFName.
        /// </summary>
        [TestVariable("bd9c9809-b9ad-4ac1-90ce-ef1c8a0f84de")]
        public string strFName
        {
            get { return _strFName; }
            set { _strFName = value; }
        }

#endregion

        /// <summary>
        /// Starts the replay of the static recording <see cref="Instance"/>.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCode("Ranorex", "7.0")]
        public static void Start()
        {
            TestModuleRunner.Run(Instance);
        }

        /// <summary>
        /// Performs the playback of actions in this recording.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        [System.CodeDom.Compiler.GeneratedCode("Ranorex", "7.0")]
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.00;

            Init();

            Report.Log(ReportLevel.Info, "Mouse", "Mouse Left Click item 'MercuryFlight.OutFlight' at Center.", repo.MercuryFlight.OutFlightInfo, new RecordItemIndex(0));
            repo.MercuryFlight.OutFlight.Click();
            Delay.Milliseconds(200);
            
            Report.Log(ReportLevel.Info, "Mouse", "Mouse Left Click item 'MercuryFlight.FlightConfirmation.ReserveFlights' at 46;6.", repo.MercuryFlight.FlightConfirmation.ReserveFlightsInfo, new RecordItemIndex(1));
            repo.MercuryFlight.FlightConfirmation.ReserveFlights.Click("46;6");
            Delay.Milliseconds(200);
            
            Report.Log(ReportLevel.Info, "Keyboard", "Key sequence from variable '$strFName' with focus on 'MercuryFlight.FlightConfirmation.PassFirst0'.", repo.MercuryFlight.FlightConfirmation.PassFirst0Info, new RecordItemIndex(2));
            repo.MercuryFlight.FlightConfirmation.PassFirst0.PressKeys(strFName);
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Keyboard", "Key sequence from variable '$strLName' with focus on 'MercuryFlight.FlightConfirmation.PassLast0'.", repo.MercuryFlight.FlightConfirmation.PassLast0Info, new RecordItemIndex(3));
            repo.MercuryFlight.FlightConfirmation.PassLast0.PressKeys(strLName);
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Keyboard", "Key sequence from variable '$strUserName' with focus on 'MercuryFlight.FlightConfirmation.PassFirst1'.", repo.MercuryFlight.FlightConfirmation.PassFirst1Info, new RecordItemIndex(4));
            repo.MercuryFlight.FlightConfirmation.PassFirst1.PressKeys(strUserName);
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Keyboard", "Key sequence 'Shah' with focus on 'MercuryFlight.FlightConfirmation.PassLast1'.", repo.MercuryFlight.FlightConfirmation.PassLast1Info, new RecordItemIndex(5));
            repo.MercuryFlight.FlightConfirmation.PassLast1.PressKeys("Shah");
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Set Value", "Setting attribute TagValue to 'IK' on item 'MercuryFlight.FlightConfirmation.CreditCard'.", repo.MercuryFlight.FlightConfirmation.CreditCardInfo, new RecordItemIndex(6));
            repo.MercuryFlight.FlightConfirmation.CreditCard.Element.SetAttributeValue("TagValue", "IK");
            Delay.Milliseconds(100);
            
            Report.Log(ReportLevel.Info, "Keyboard", "Key sequence 'Pratik' with focus on 'MercuryFlight.FlightConfirmation.CcFrstName'.", repo.MercuryFlight.FlightConfirmation.CcFrstNameInfo, new RecordItemIndex(7));
            repo.MercuryFlight.FlightConfirmation.CcFrstName.PressKeys("Pratik");
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Keyboard", "Key sequence 'Nitish' with focus on 'MercuryFlight.FlightConfirmation.CcMidName'.", repo.MercuryFlight.FlightConfirmation.CcMidNameInfo, new RecordItemIndex(8));
            repo.MercuryFlight.FlightConfirmation.CcMidName.PressKeys("Nitish");
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Keyboard", "Key sequence 'Rai' with focus on 'MercuryFlight.FlightConfirmation.CcLastName'.", repo.MercuryFlight.FlightConfirmation.CcLastNameInfo, new RecordItemIndex(9));
            repo.MercuryFlight.FlightConfirmation.CcLastName.PressKeys("Rai");
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Keyboard", "Key sequence '123456789' with focus on 'MercuryFlight.FlightConfirmation.Creditnumber'.", repo.MercuryFlight.FlightConfirmation.CreditnumberInfo, new RecordItemIndex(10));
            repo.MercuryFlight.FlightConfirmation.Creditnumber.PressKeys("123456789");
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Mouse", "Mouse Left Click item 'MercuryFlight.FlightConfirmation.SecurePurachse' at Center.", repo.MercuryFlight.FlightConfirmation.SecurePurachseInfo, new RecordItemIndex(11));
            repo.MercuryFlight.FlightConfirmation.SecurePurachse.Click();
            Delay.Milliseconds(200);
            
            AppFunctions.ValidateConfOrder(repo.MercuryFlight.FlightConfirmation.Text_ConfirmationInfo);
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Get Value", "Getting attribute 'InnerText' from item 'MercuryFlight.FlightConfirmation.Text_FlightNo' and assigning its value to variable 'strConfirmationNo'.", repo.MercuryFlight.FlightConfirmation.Text_FlightNoInfo, new RecordItemIndex(13));
            strConfirmationNo = repo.MercuryFlight.FlightConfirmation.Text_FlightNo.Element.GetAttributeValueText("InnerText");
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "User", strConfirmationNo, new RecordItemIndex(14));
            
            Report.Log(ReportLevel.Info, "Invoke Action", "Invoking EnsureVisible() on item 'MercuryFlight.FlightConfirmation.TotalPriceIncludin'.", repo.MercuryFlight.FlightConfirmation.TotalPriceIncludinInfo, new RecordItemIndex(15));
            repo.MercuryFlight.FlightConfirmation.TotalPriceIncludin.EnsureVisible();
            Delay.Milliseconds(0);
            
            Report.Screenshot(ReportLevel.Info, "User", "", repo.MercuryFlight.Self, false, new RecordItemIndex(16));
            
            Report.Log(ReportLevel.Info, "Mouse", "Mouse Left Click item 'MercuryFlight.FlightConfirmation.Btn_Logout' at Center.", repo.MercuryFlight.FlightConfirmation.Btn_LogoutInfo, new RecordItemIndex(17));
            repo.MercuryFlight.FlightConfirmation.Btn_Logout.Click();
            Delay.Milliseconds(200);
            
            Report.Log(ReportLevel.Info, "Validation", "Validating Exists on item 'MercuryFlight.FlightConfirmation.Textfield_SignOn'.", repo.MercuryFlight.FlightConfirmation.Textfield_SignOnInfo, new RecordItemIndex(18));
            Validate.Exists(repo.MercuryFlight.FlightConfirmation.Textfield_SignOnInfo);
            Delay.Milliseconds(0);
            
            Report.Log(ReportLevel.Info, "Delay", "Waiting for 3s.", new RecordItemIndex(19));
            Delay.Duration(3000, false);
            
            Report.Log(ReportLevel.Info, "Application", "Killing application containing item 'MercuryFlight'.", repo.MercuryFlight.SelfInfo, new RecordItemIndex(20));
            Host.Current.KillApplication(repo.MercuryFlight.Self);
            Delay.Milliseconds(0);
            
        }

#region Image Feature Data
#endregion
    }
#pragma warning restore 0436
}