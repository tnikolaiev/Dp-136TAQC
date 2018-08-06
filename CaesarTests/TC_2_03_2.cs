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
    class TC_2_03_2 : BaseTest
    {
        [Test]

        public void IsScheduleEditorUnavailableWhenFewGroupsSelected()
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
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();

            //Click on cogweel for ScheduleEditor opening
            MainPageInstance.CenterContainer.ScheduleContent.ScheduleCogwheell.Click();

            //Assert modal window for groups multiselecting is displayed
            Assert.IsTrue(wait.Until((d) => MainPageInstance.ModalWindow.SelectGroupWindow.IsSelectGroupWindowOpened(driver)));

            //Close SelectGroupWindow
            MainPageInstance.ModalWindow.SelectGroupWindow.CancelButton.Click();
        }

    }

}