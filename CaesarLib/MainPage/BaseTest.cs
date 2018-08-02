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