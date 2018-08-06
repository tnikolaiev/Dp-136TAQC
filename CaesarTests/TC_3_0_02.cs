using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CaesarLib;

namespace CaesarTests
{
    
    [TestFixture]
    public class TC_3_0_02
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
        public void EditingCurrentTeacherExpert()
        {
            string expected1 = "D. Petin";
            string expected2 = "A. Koval";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-092-NET").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.EditTeacher("D. Petin");
            mainPageInstance.ModalWindow.GroupCreateWindow.EditExpert("A. Koval");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-092-NET").Click();

            string actual1 = mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupCoordination.GetValueFromCell(2, 2);
            string actual2 = mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupCoordination.GetValueFromCell(3, 2);

            Assert.IsTrue((expected1 == actual1) & (expected2 == actual2));

            mainPageInstance1.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance1.ModalWindow.GroupCreateWindow.EditTeacher("O. Reuta");
            mainPageInstance1.ModalWindow.GroupCreateWindow.EditExpert("V. Koldovskyy");
            mainPageInstance1.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
        }

        [Test]
        public void AddingNewTeacherExpert()
        {
            string expected1 = "D. Petin";
            string expected2 = "A. Koval";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-092-NET").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.OneMoreTeacher.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveSecondTeacher.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.AddSecondExpert("A. Koval");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-092-NET").Click();

            string actual1 = mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupCoordination.GetValueFromCell(3, 2);
            string actual2 = mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupCoordination.GetValueFromCell(5, 2);

            Assert.IsTrue((expected1 == actual1) & (expected2 == actual2));

            mainPageInstance1.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance1.ModalWindow.GroupCreateWindow.DeleteSecondTeacher.Click();
            mainPageInstance1.ModalWindow.GroupCreateWindow.DeleteSecondExpert.Click();
            mainPageInstance1.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();

        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
