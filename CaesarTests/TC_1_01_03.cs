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
    class TC_1_01_03
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            loginPageInstance = new LoginPage(driver);
        }

        static object[] loginCredentials = { new String[] { "dmytro", "1234" } };

        [Test, TestCaseSource("loginCredentials")]
        public void ExecuteTest_EscKey_EmptyFields(String login, String password)
        {
            Acts.InputValue(loginPageInstance.LoginField, login);
            Acts.InputValue(loginPageInstance.PasswordField, password);
            loginPageInstance.PasswordField.SendKeys(Keys.Escape);
            bool loginFieldEmpty = String.Empty.Equals(loginPageInstance.LoginField.GetAttribute("value"));
            bool passFieldEmpty = String.Empty.Equals(loginPageInstance.PasswordField.GetAttribute("value"));
            Assert.IsTrue(loginFieldEmpty & passFieldEmpty);
        }

        [Test, TestCaseSource("loginCredentials")]
        public void ExecuteTest_EnterKey_Login(String login, String password)
        {
            Acts.InputValue(loginPageInstance.LoginField, login);
            Acts.InputValue(loginPageInstance.PasswordField, password);
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            Assert.IsTrue(wait.Until(d => MainPage.IsMainPageOpened(d)));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
