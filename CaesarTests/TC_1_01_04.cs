using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_01_04
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
        }

        private static IEnumerable<object[]> TestData()
        {
            var doc = XDocument.Load(@"C:\Users\Nikita\source\repos\Dp-136TAQC\CaesarTests\TestData\LoginPageInvalidData.xml");
            return from vars in doc.Descendants("testData")
                   let login = vars.Attribute("login").Value
                   let password = vars.Attribute("password").Value
                   select new String[] { login.ToString(), password.ToString() };
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

        [Test, TestCaseSource("TestData")]
        public void ExecuteTest_InvalidValues_ErrorMessage(String login, String password)
        {
            List<String> logins = new List<String>() { "login1", "#$%^#${}", "4f4&4]3", "login", "1234", "12" };
            List<String> passwords = new List<String>() { "$%^&#*", "pass2", "#@#@#@", "pa$$word", "12", "1234" };
            //int i = 0;
            //while (i < logins.Count)
            //{
                loginPageInstance.LogIn(login, password);

                Assert.AreEqual(String.Empty, loginPageInstance.PasswordField.GetAttribute("value"));
                Assert.AreEqual(login, loginPageInstance.LoginField.GetAttribute("value"));
                String expectedMessage = "Incorrect login or password. Please, try again";
                Assert.AreEqual(expectedMessage, loginPageInstance.MessageField.Text);

                loginPageInstance.LoginField.SendKeys(Keys.Escape);
                //i++;
            //}
        }

        [Test]
        public void ExecuteTest_15lettersLoginField_Contains10()
        {
            Acts.InputValue(loginPageInstance.LoginField, "abcdefghijklmno");
            Assert.AreEqual("abcdefghij", loginPageInstance.LoginField.GetAttribute("value"));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
