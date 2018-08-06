using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CaesarLib;
using System.Threading;

namespace CaesarTests
{
    
    [TestFixture]
    public class TC_3_0_07
    {
        IWebDriver driver;
        LoginPage loginPageInstance;
        WebDriverWait wait;
        MainPage mainPageInstance;
        MainPage mainPageInstance1;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
            mainPageInstance1 = new MainPage(driver);

        }

        [Test]
        public void EdtingDirection()
        {
            List<int> expected = new List<int>();
            expected.Add(12);
            expected.Add(9);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.SetDirection(".Net");
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("01/01/2018");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();


            List<int> actual = new List<int>();
            List<DateTime> time1 = new List<DateTime>();
            foreach (var item in mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupInfo.GetRows())
            {
                if (item.Text != "")
                {
                    string[] words = item.Text.Split(new char[] { ' ' });
                    time1.Add(DateTime.ParseExact(words[1], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture));
                }

            }
            
            TimeSpan timeSpan1 = time1[1] - time1[0];
            actual.Add(timeSpan1.Days/7);

            mainPageInstance1.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance1.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance1.ModalWindow.GroupCreateWindow.SetDirection("MQC");
            mainPageInstance1.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance1.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("01/01/2018");
            mainPageInstance1.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();


            foreach (var item in mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupInfo.GetRows())
            {
                if (item.Text != "")
                {
                    string[] words = item.Text.Split(new char[] { ' ' });
                    time1.Add(DateTime.ParseExact(words[1], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture));
                }

            }

            TimeSpan timeSpan2 = time1[4] - time1[3];
            actual.Add(timeSpan2.Days / 7);
            CollectionAssert.AreEqual(expected, actual);

        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
