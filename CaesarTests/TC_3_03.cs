using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using CaesarLib;

namespace CaesarTests
{
  
    [TestFixture]
    public class TC_3_03
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        MainPage mainPageInstance;
        
        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

        }

        [Test]
        public void EditingStudent()
        {

            string expected1 = "100";
            string expected2 = "4";
            string expected3 = "N. Varenko";

            mainPageInstance.MoveToTopMenu().StudentsItem.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            mainPageInstance.ModalWindow.EditStudentListWindow.EditButtons[0].Click();
            mainPageInstance.ModalWindow.EditStudentWindow.IncomingTest.Clear();
            mainPageInstance.ModalWindow.EditStudentWindow.IncomingTest.SendKeys("100");
            mainPageInstance.ModalWindow.EditStudentWindow.EntryScore.Clear();
            mainPageInstance.ModalWindow.EditStudentWindow.EntryScore.SendKeys("4");
            Acts.SelectOptionFromDDL(mainPageInstance.ModalWindow.EditStudentWindow.ApprovedBy, 1);
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            mainPageInstance.ModalWindow.EditStudentListWindow.ExitFormButton.Click();
            
            string actual1 = mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetValueFromCell(1, 4);
            string actual2 = mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetValueFromCell(1, 5);
            string actual3 = mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetValueFromCell(1, 6);

            Assert.IsTrue((expected1 == actual1) & (expected2 == actual2) & (expected3 == actual3));
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
