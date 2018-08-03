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
    class TC_2_01_3 : BaseTest
    {

        static IEnumerable<object[]> LoginUnderDifferentRoles = Instruments.ReadXML("LoginUnderDifferentRoles.xml", "testData", "login", "password");


        [Test, TestCaseSource("LoginUnderDifferentRoles")]

        public void IsMonthTabAvailable(string login, string password)
        {
            //Opening Caesar and Logging in
            driver.Url = baseURL;
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password, wait);

            //Opening Schedule Page

            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();

            //Select group from LeftContainer

            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();


            //Assert MonthView tab is displayed
            Assert.IsTrue(MainPageInstance.CenterContainer
                .ScheduleContent
                .MonthTabInstance
                .IsMonthTabDisplayed(driver));          
        }

        [Test, TestCaseSource("LoginUnderDifferentRoles")]

        public void IsWeekTabAvailable(string login, string password)
        {

            //Opening Caesar and Logging in
            driver.Url = baseURL;
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("qwerty", "1234", wait);

            //Opening Schedule Page

            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();

            //Select group from LeftContainer

            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Click on WeekView button

            MainPageInstance.CenterContainer.ScheduleContent.WeekButton.Click();


            //Assert Week tab is displayed
            Assert.IsTrue(MainPageInstance
                .CenterContainer
                .ScheduleContent
                .WeekTabInstance
                .IsWeekTabDisplayed(driver));
        }

        [Test, TestCaseSource("LoginUnderDifferentRoles")]

        public void IsKeyDatesTabAvailable(string login, string password)
        {        

            //Opening Caesar and Logging in
             driver.Url = baseURL;
             loginPageInstance = new LoginPage(driver);
             loginPageInstance.LogIn("qwerty", "1234", wait);

            //Opening Schedule Page

            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();

            //Select group from LeftContainer

            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

           //Click on KeyDates button

             MainPageInstance.CenterContainer.ScheduleContent.KeyDatesButton.Click();
            
            //Assert KeyDates tab is displayed
            Assert.IsTrue(MainPageInstance
                .CenterContainer
                .ScheduleContent
                .KeyDatesTabInstance
                .IsKeyDatesDisplayed(driver));
        }
      
    }

}

