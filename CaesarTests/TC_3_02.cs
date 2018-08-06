using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
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
    public class TC_3_02
    {
        IWebDriver driver;
        LoginPage loginPageInstance;
        WebDriverWait wait;
        MainPage mainPageInstance;
        

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

        }

        [Test]
        public void CreatingNewStudent()
        {
            int expected = 1;
            string expected1 = "Ivanov Ivan";

            mainPageInstance.MoveToTopMenu().StudentsItem.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.FutureGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-095-JS").Click();
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            mainPageInstance.ModalWindow.EditStudentListWindow.CreateStudentButton.Click();
            mainPageInstance.ModalWindow.EditStudentWindow.FillForm("Ivan", "Ivanov", 5, "100", "4", 1);
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            mainPageInstance.ModalWindow.EditStudentListWindow.ExitFormButton.Click();

            int actual = 0;
            foreach (var el in mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetRows())
            {
                if (el.Text != "")
                    actual++;
            }

            string actual1 = mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetValueFromCell(1, 1);

            Assert.IsTrue((expected == actual) & (expected1 == actual1));
        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
            driver.Quit();
        }
    }
}
