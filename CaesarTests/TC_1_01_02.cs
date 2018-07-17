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
    class TC_1_01_02
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        List<String> logins;
        List<String> passwords;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            logins = new List<String> { "admin", "sasha", "dmytro" };
            passwords = new List<String> { "1234", "1234", "1234" };
        }

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
        }

        [Test]
        public void ExecuteTest_LoginWithValidLoginCredentials()
        {
            int i = 0;
            while (i < logins.Count)
            {
                driver.Url = @"http://localhost:3000/logout";
                loginPageInstance = new LoginPage(driver);
                wait.Until((d) => LoginPage.IsLoginPage(d));

                loginPageInstance.LogIn(logins[i], passwords[i]);
                Assert.IsTrue(wait.Until((d) => MainPage.IsMainPage(d)));
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
