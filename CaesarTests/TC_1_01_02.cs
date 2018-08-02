using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_01_02
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
            //driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
        }

        static Object[] LoginCredentials =
        {
            new String[] { "admin", "1234" },
            new String[] { "sasha", "1234" },
            new String[] { "dmytro", "1234" }
        };

        [Test, TestCaseSource("LoginCredentials")]
        public void ExecuteTest_LoginWithValidLoginCredentials(String login, String password)
        {
            loginPageInstance.LogIn(login, password, wait);
            Assert.IsTrue(wait.Until((d) => MainPage.IsMainPageOpened(d)));
        }

        [OneTimeTearDown]
        public void LastCleanUp()
        {
            driver.Quit();
        }
    }
}
