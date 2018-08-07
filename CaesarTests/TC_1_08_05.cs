using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_08_05
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        TopMenu topMenuInstance;
        MainPage mainPageInstance;
        About aboutInstance;

        [OneTimeSetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
        }

        static IEnumerable<object[]> GetNamesOfGroups = Instruments.ReadXML("NamesOfSecondGroups.xml", "testData", "name", "exeptedResult1", "exeptedResult2");

        [Test, TestCaseSource("GetNamesOfGroups")]
        public void ExecuteTest_CheckGroupNameAndCourseQualityAssurance(string name, string exeptedResult1, string exeptedResult2)
        {
            topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.AboutItem);
            aboutInstance = new About(driver);
            Acts.Click(aboutInstance.QualityAssurance);
            IList<IWebElement> listGetTitleGroup = aboutInstance.GetTitleGroups();
            aboutInstance.MoveToAboutCourse(listGetTitleGroup, name);
            wait.Until(aboutInstance.IsContentHeaderGroupNameHintVisible());
            bool firstCondition = (exeptedResult1==aboutInstance.ContentHeaderGroupNameHint.Text);
            bool secondCondition = (exeptedResult2==aboutInstance.ContentHeaderGroupNumberHint.Text);
            Console.WriteLine(aboutInstance.ContentHeaderGroupNameHint.Text);
            Console.WriteLine(aboutInstance.ContentHeaderGroupNumberHint.Text);
            Assert.IsTrue(firstCondition && secondCondition);
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

