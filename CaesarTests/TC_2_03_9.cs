using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    class TC_2_03_9 : BaseTest
    {
        [SetUp]

        public void SetUp()
        {
            //Opening Caesar and Logging in
            driver.Url = baseURL;
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("Dmytro", "1234", wait);

            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();

            //Selecting group
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Open ScheduleEditor
            MainPageInstance.CenterContainer.ScheduleContent.ScheduleCogwheell.Click();
            wait.Until((d) => MainPageInstance.ModalWindow.EditScheduleWindow.IsScheduleEditorDisplayed(driver));

        }

            [Test]

            public void CheckPrevWeekToggles()
            {

            //Remember header names
            List<String> currentWeekHeaders = MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.GetHeadingsText();
           
            //Click on prevWeek toggle
            MainPageInstance.ModalWindow.EditScheduleWindow.PrevWeek.Click();

            //Remember header names
            List<String> prevWeekHeaders = MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.GetHeadingsText();

            //Assert headers in WeekTable changed
            CollectionAssert.AreNotEqual(currentWeekHeaders, prevWeekHeaders);

            //Close ScheduleEditor
            MainPageInstance.ModalWindow.EditScheduleWindow.CancelButton.Click();
        }

        [Test]

        public void CheckNextWeekToggles()
        {

            //Remember header names
            List<String> currentWeekHeaders = MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.GetHeadingsText();

            //Click on nextWeek toggle
            MainPageInstance.ModalWindow.EditScheduleWindow.NextWeek.Click();          

            //Remember header names
            List<String> nextWeekHeaders = MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.GetHeadingsText();

            //Assert headers in WeekTable changed
            CollectionAssert.AreNotEqual(currentWeekHeaders, nextWeekHeaders);

            //Close ScheduleEditor
            MainPageInstance.ModalWindow.EditScheduleWindow.CancelButton.Click();
        }

    }

}

