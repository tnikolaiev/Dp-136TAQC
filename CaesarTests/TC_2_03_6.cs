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
    class TC_2_03_6 : BaseTest
    {     
        [Test]

        public void CheckQAEventsScheduleEditor()
        {
            {
                //Opening Caesar and Logging in
                driver.Url = baseURL;
                loginPageInstance = new LoginPage(driver);
                loginPageInstance.LogIn("qwerty", "1234", wait);

                //Opening Schedule Page
                MainPageInstance = new MainPage(driver);
                wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
                MainPageInstance.TopMenu.ScheduleItem.Click();

                //Selecting group
                MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();


                //Click on cogweel for ScheduleEditor opening
                MainPageInstance.CenterContainer.ScheduleContent.ClickCogwheel(wait);
             
                //Check if correct events present
                Assert.IsTrue(Acts.IsElementPresent(driver, By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Lecture')]")));
                Assert.IsTrue(Acts.IsElementPresent(driver, By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Weekly report')]")));
                Assert.IsTrue(Acts.IsElementPresent(driver, By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Consultation')]")));
                Assert.IsTrue(Acts.IsElementPresent(driver, By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Work with Expert')]")));

                //Close ScheduleEditor
                MainPageInstance.ModalWindow.EditScheduleWindow.CancelButton.Click();
            }
        }
        
    }

}

