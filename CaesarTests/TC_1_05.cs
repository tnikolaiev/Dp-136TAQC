using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using CaesarLib;
using NUnit.Framework.Internal;

namespace CaesarTests
{
 
    [TestFixture]
    public class TC_1_05
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        LocationWindow locationWindowInstance;
        CenterContainer groupLocationInstance;
        TopMenu topMenuInstance;
        MainPage mainPageInstance;
        List<String> listOfCity;

        [OneTimeSetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
           
        }

        static IEnumerable<object[]> GetLocationLists = Instruments.ReadXML("LocationLists.xml", "testData", "exeptedResult", "city");

        [Test, TestCaseSource("GetLocationLists")]
        public void TestLocationList(string exeptedResult, string city)
        {
            topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.LocationsItem);
            locationWindowInstance = new LocationWindow(driver);
            string[] parts = city.Split(',');
            List<string> listOfCity = new List<string> (parts);
            IList<IWebElement> nonActiveCity = locationWindowInstance.GetLocationNonActiveWebElements();
            locationWindowInstance.ClickNonActiveButtonNames(nonActiveCity, listOfCity);
            Acts.Click(locationWindowInstance.ConfurmButton);
            CenterContainer groupLocationInstance = mainPageInstance.MoveToCenterContainer();
            groupLocationInstance = new CenterContainer(driver);
            wait.Until(groupLocationInstance.IsHintVisible());
            Console.WriteLine(groupLocationInstance.LocationHint.Text);
            string exeptedResultTitle = exeptedResult;
            Assert.AreEqual(exeptedResultTitle, groupLocationInstance.LocationHint.Text);

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
        
    }
}