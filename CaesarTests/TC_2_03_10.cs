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
    class TC_2_03_10 : BaseTest
    {
        [Test]

        public void CheckGroupNameInScheduleEditorHeader()
        {
            //Opening Caesar and Logging in
            driver.Url = baseURL;
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("Dmytro", "1234", wait);

            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            MainPageInstance.OpenScheduleContent(wait);

            //Selecting group
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Open ScheduleEditor
            MainPageInstance.CenterContainer.ScheduleContent.ClickCogwheel(wait);

            //Assert GroupName is correct
            string GroupName = MainPageInstance.ModalWindow.EditScheduleWindow.GroupName.Text;
            Assert.AreEqual("DP-094-MQC", GroupName);

            //Close ScheduleEditor
            MainPageInstance.ModalWindow.EditScheduleWindow.CancelButton.Click();
        }

    }

}

