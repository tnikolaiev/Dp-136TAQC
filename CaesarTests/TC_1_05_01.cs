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
    class TC_1_05_01
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        LocationWindow locationPageInstance;
        CenterContainer groupLocationInstance;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
           // driver.FindElement(By.XPath("//*[@id='top-menu']/div[1]/div[1]/p")).Click();
        }
        [Test]
        public void ExecuteTest_ChooseLocation_LocationPageOpened()
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.ClassName("containerMainMenu"))).Build().Perform();
            driver.FindElement(By.XPath("//i[@class='fa fa-globe fa-2x']")).Click();
            locationPageInstance = new LocationWindow(driver);
            Acts.Click(locationPageInstance.CityChernivtsy);
            Acts.Click(locationPageInstance.ConfurmButton);
            string exeptualResultTitle = "Chernivtsy";
            groupLocationInstance = new CenterContainer(driver);
            Console.WriteLine(groupLocationInstance.GroupLocation.Text);
            Assert.AreEqual(exeptualResultTitle, groupLocationInstance.GroupLocation.Text);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}

