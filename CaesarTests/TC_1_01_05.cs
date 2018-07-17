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
    class TC_1_01_05
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        List<String> logins;
        List<String> passwords;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            logins = new List<String> { "odmin", "sashq", "jtytro" };
            passwords = new List<String> { "9234", "5234", "1734" };
        }

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance = new LoginPage(driver);
        }

        [Test]
        public void ExecuteTest_LoginWithInvalidLoginCredentials()
        {
            int i = 0;
            while (i < logins.Count)
            {
                loginPageInstance.LogIn(logins[i], passwords[i]);
                bool firstCondition = logins[i].Equals(Acts.GetAttribute(loginPageInstance.LoginField, "value"));
                bool secondCondition = String.Empty.Equals(Acts.GetAttribute(loginPageInstance.PasswordField, "value"));
                String expectedMessage = "Incorrect login or password. Please, try again";
                bool thirdCondition = expectedMessage.Equals(loginPageInstance.MessageField.Text);
                Assert.IsTrue(firstCondition && secondCondition && thirdCondition);
                loginPageInstance.LoginField.SendKeys(Keys.Escape);
                i++;
            }
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
