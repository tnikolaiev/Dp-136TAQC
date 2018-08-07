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
        static IEnumerable<object[]> LoginUnderDifferentRoles = Instruments.ReadXML("LoginUnderDifferentRoles.xml", "testData", "login", "password");
        [Test, TestCaseSource("LoginUnderDifferentRoles")]

        public void IsScheduleEditorAvailable(String login, String password)
        {
            //Opening Caesar and Logging in
            driver.Url = baseURL;
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password, wait);

            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            MainPageInstance.OpenScheduleContent(wait);

            //Selecting group
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Click on cogweel for ScheduleEditor opening
            MainPageInstance.CenterContainer.ScheduleContent.ScheduleCogwheell.Click();

            //Assert ScheduleEditor is displayed
            Assert.IsTrue(wait.Until((d) => MainPageInstance.ModalWindow.EditScheduleWindow.IsScheduleEditorDisplayed(driver)));

            //Close ScheduleEditor
            MainPageInstance.ModalWindow.EditScheduleWindow.CancelButton.Click();
        }
      
    }

}

