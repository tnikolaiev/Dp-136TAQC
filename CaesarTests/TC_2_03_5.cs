using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_2_03_5 : BaseTest
    {
        [SetUp]

        public void SetUp()
        {
            //Opening Caesar and Logging in
            driver.Url = baseURL;
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("qwerty", "1234", wait);

            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            MainPageInstance.OpenScheduleContent(wait);
        }

        [Test]

        public void CheckQAEventsScheduleEditor()
        {                     

                //Selecting group
                MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

                //Open ScheduleEditor
                MainPageInstance.CenterContainer.ScheduleContent.ClickCogwheel(wait);

                //Check if correct events present
                MainPageInstance.ModalWindow.EditScheduleWindow.AreQAEventsExist(driver);

                //Close ScheduleEditor
                MainPageInstance.ModalWindow.EditScheduleWindow.CancelButton.Click();
         }

        [Test]

        public void CheckDevEventsScheduleEditor()
        {

            //Selecting group
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Open ScheduleEditor
            MainPageInstance.CenterContainer.ScheduleContent.ClickCogwheel(wait);

            //Check if correct events present
            MainPageInstance.ModalWindow.EditScheduleWindow.AreDevEventsExist(driver);

            //Close ScheduleEditor
            MainPageInstance.ModalWindow.EditScheduleWindow.CancelButton.Click();
        }


    }

}

