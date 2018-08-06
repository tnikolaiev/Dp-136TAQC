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

        static IEnumerable<object[]> InvalidLoginCredentials = Instruments.ReadXML("InvalidLoginCredentials.xml", "testData", "login", "password");

        [Test, TestCaseSource("InvalidLoginCredentials")]
        public void Test_LoginWithInvalidLoginCredentials(String login, String password)
        {
            loginPageInstance.LogIn(login, password);
            bool firstCondition = login.Equals(loginPageInstance.LoginField.GetAttribute("value"));
            bool secondCondition = String.Empty.Equals(loginPageInstance.PasswordField.GetAttribute("value"));
            String expectedMessage = "Incorrect login or password. Please, try again";
            bool thirdCondition = expectedMessage.Equals(loginPageInstance.MessageField.Text);

            Assert.IsTrue(firstCondition && secondCondition && thirdCondition);
            loginPageInstance.LoginField.SendKeys(Keys.Escape);
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
