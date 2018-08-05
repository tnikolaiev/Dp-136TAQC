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
    class TC_2_03_7 : BaseTest
    {
        [Test]

        public void CheckCreatingEventInScheduleditor()
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
            MainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-092-NET").Click();

            //Open ScheduleEditor
            MainPageInstance.CenterContainer.ScheduleContent.ScheduleCogwheell.Click();
            wait.Until((d) => MainPageInstance.ModalWindow.EditScheduleWindow.IsScheduleEditorDisplayed(driver));

            //Choose options for event

            Acts.SelectOptionFromDDL(MainPageInstance.ModalWindow.EditScheduleWindow.RoomControl, "740");
            Acts.SelectOptionFromDDL(MainPageInstance.ModalWindow.EditScheduleWindow.EventControl, "Special");        
            MainPageInstance.ModalWindow.EditScheduleWindow.WeeklyReportEvent.Click();

            // Put event in cell
            MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.GetCell("9:00", "Wednesday\r\n08/08").Click();
            wait.Until((d) => MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.IsActivityExists(MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.GetCell("9:00", "Wednesday\r\n08/08")));

            // Click Save
            MainPageInstance.ModalWindow.EditScheduleWindow.SaveButton.Click();
            wait.Until((d) => MainPageInstance.CenterContainer.ScheduleContent.IsOpened(driver));

            //Open ScheduleEditor  again           
            MainPageInstance.CenterContainer.ScheduleContent.ScheduleCogwheell.Click();
            wait.Until((d) => MainPageInstance.ModalWindow.EditScheduleWindow.IsScheduleEditorDisplayed(driver));

            //Assert that event is in cell
            IWebElement cell = MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.GetCell("9:00", "Wednesday\r\n08/08");
            Assert.True(MainPageInstance.ModalWindow.EditScheduleWindow.ScheduleEditWeekTable.IsActivityCorrect(cell, "Weekly report\r\nO. Reuta\r\n740"));

            //Close ScheduleEditor
            MainPageInstance.ModalWindow.EditScheduleWindow.CancelButton.Click();
        }

    }

}

