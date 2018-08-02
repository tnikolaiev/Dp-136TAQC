using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarTests
{
    [TestFixture]
    class TC_2_03_1 : BaseTest
    {
        static IEnumerable<object[]> StartFinishDateData = Instruments.ReadXML("LoginUnderDifferentRoles.xml", "testData", "login", "password");

        [Test, TestCaseSource("LoginUnderDifferentRoles")]

        public void IsScheduleEditorAvailable(string login, string password)
        {
            //Logging in
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password);
            wait.Until((driver) => MainPage.IsMainPageOpened(driver));

            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();
          
            //Select group from LeftContainer
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Click on cogweel for ScheduleEditor opening
            MainPageInstance.CenterContainer.ScheduleContent.ScheduleCogwheell.Click();

            //Assert ScheduleEditor is displayed
            Assert.IsTrue(wait.Until((d) => MainPageInstance.ModalWindow.EditScheduleWindow.IsScheduleEditorDisplayed(driver)));          
        }
      
    }

}

