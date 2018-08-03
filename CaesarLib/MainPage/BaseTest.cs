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
    public class BaseTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected LoginPage loginPageInstance;
        protected MainPage MainPageInstance;

        [SetUp]

        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.Manage().Window.Maximize();

            //Navigating to Caesar app
            string baseURL = "localhost:3000";
            driver.Url = baseURL;

            //Logging in
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((driver) => MainPage.IsMainPageOpened(driver));

            BeforeTest();
        }       

        [TearDown]
        public void TearDown()
        {            
            driver.Close();
            driver.Quit();
            AfterTest();
        }

        protected virtual void BeforeTest() { }
        protected virtual void AfterTest() {}
    }
}