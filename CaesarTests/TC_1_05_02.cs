using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_05_02
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        LocationPage locationPageInstance;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPage(d));
        }
        [Test]
        public void ExecuteTest_LocationPageCloseWithoutChoosing()
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.ClassName("containerMainMenu"))).Build().Perform();
            driver.FindElement(By.XPath("//i[@class='fa fa-globe fa-2x']")).Click();
            locationPageInstance = new LocationPage(driver);
            Acts.Click(locationPageInstance.CancelButton);
            Assert.IsTrue(wait.Until((d) => MainPage.IsMainPage(d)));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
           driver.Close();
        }
    }
}

