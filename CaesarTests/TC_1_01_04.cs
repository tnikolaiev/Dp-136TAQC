using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_01_04
    {
        IWebDriver driver;
        LoginPage loginPageInstance;
        WebDriverWait wait;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            driver = new ChromeDriver();
        }

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
        }

        [Test]
        public void ExecuteTest_EmptyFields_LoginButtonClick_NoChanges()
        {
            loginPageInstance.LoginButton.Click();
            Assert.IsTrue(LoginPage.IsLoginPageOpened(driver));
        }

        [Test]
        public void ExecuteTest_EmptyFields_EnterPressed_NoChanges()
        {
            loginPageInstance.LoginField.SendKeys(Keys.Enter);
            Assert.IsTrue(LoginPage.IsLoginPageOpened(driver));
        }

        [Test]
        public void ExecuteTest_EmptyLoginField_LoginButtonUnactive()
        {
            Acts.InputValue(loginPageInstance.PasswordField, "pass");
            Assert.IsFalse(loginPageInstance.LoginButton.Enabled);
        }

        [Test]
        public void ExecuteTest_EmptyPassField_LoginButtonUnactive()
        {
            Acts.InputValue(loginPageInstance.LoginField, "log1");
            Assert.IsFalse(loginPageInstance.LoginButton.Enabled);
        }

        static IEnumerable<object[]> LoginInvalidData()
        {
            return Instruments.ReadXML("LoginPageInvalidData.xml", "testData", "login", "password");
        }

        [Test, TestCaseSource("LoginInvalidData")]
        public void ExecuteTest_InvalidValues_ErrorMessage(String login, String password)
        {
            loginPageInstance.LogIn(login, password);
            bool passFieldEmpty = String.Empty.Equals(loginPageInstance.PasswordField.GetAttribute("value"));
            bool loginFieldKeepsValue = login.Equals(loginPageInstance.LoginField.GetAttribute("value"));
            String expectedMessage = "Incorrect login or password. Please, try again";
            bool textMatches = expectedMessage.Equals(loginPageInstance.MessageField.Text);

            Assert.IsTrue(passFieldEmpty & loginFieldKeepsValue & textMatches);
            loginPageInstance.LoginField.SendKeys(Keys.Escape);
        }

        [Test]
        public void ExecuteTest_15lettersLoginField_Contains10()
        {
            Acts.InputValue(loginPageInstance.LoginField, "abcdefghijklmno");
            Assert.AreEqual("abcdefghij", loginPageInstance.LoginField.GetAttribute("value"));
        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }

        [OneTimeTearDown]
        public void FinalCleanUp()
        {            
            driver.Quit();
        }
    }
}
