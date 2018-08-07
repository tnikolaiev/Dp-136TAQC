using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_05_01
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        CenterContainer groupLocationInstance;
        TopMenu topMenuInstance;
        MainPage mainPageInstance;
        GroupsInLocation groupsInLocationInstance;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

        }
        [Test]
        public void ExecuteTest_ChooseLocationSofia_UsingDoubleClick_LocationPageOpened()
        {
            topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.LocationsItem);
            mainPageInstance.DoubleClick(mainPageInstance.ModalWindow.LocationWindow.CitySofia);
            string exeptualResultTitle = "Sofia";
            groupLocationInstance = new CenterContainer(driver);
            Console.WriteLine(groupLocationInstance.GroupLocation.Text);
            Assert.AreEqual(exeptualResultTitle, groupLocationInstance.GroupLocation.Text);
        }

        [Test]
        public void ExecuteTest_ChooseLocationSofia_UsingDoubleClick_CheckGroupsName()
        {
            topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.LocationsItem);
            mainPageInstance.DoubleClick(mainPageInstance.ModalWindow.LocationWindow.CitySofia);
            string exeptualResultTitle = "Sf-089-MQC";
            Console.WriteLine(mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("Sf-089-MQC").Text);
            Assert.AreEqual(exeptualResultTitle, mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("Sf-089-MQC").Text);
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

