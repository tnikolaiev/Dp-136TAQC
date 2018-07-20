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
    class TC_1_01_03
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

        [Test]
        public void ExecuteTest_EscKey_EmptyFields()
        {
            Acts.InputValue(loginPageInstance.LoginField, "dmytro");
            Acts.InputValue(loginPageInstance.PasswordField, "1234");
            loginPageInstance.PasswordField.SendKeys(Keys.Escape);
            bool loginFieldEmpty = String.Empty.Equals(loginPageInstance.LoginField.GetAttribute("value"));
            bool passFieldEmpty = String.Empty.Equals(loginPageInstance.PasswordField.GetAttribute("value"));
            Assert.IsTrue(loginFieldEmpty & passFieldEmpty);
        }

        [Test]
        public void ExecuteTest_EnterKey_Login()
        {
            Acts.InputValue(loginPageInstance.LoginField, "Dmytro");
            Acts.InputValue(loginPageInstance.PasswordField, "1234");
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            Assert.IsTrue(wait.Until(d => MainPage.IsMainPageOpened(d)));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
