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
    class TC_1_08_01
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        TopMenu topMenuInstance;
        MainPage mainPageInstance;
        About aboutInstance;

        [SetUp]
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

        static Object[] GroupsOfContributors =
        {
            new String[] { "Development & Research,Quality Assurance,Management and Mentoring,Additional Thanks" },
        };

        [Test, TestCaseSource("GroupsOfContributors")]
        public void ExecuteTest_CheckButtonAbout(string expectedResult)
        {
            topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.AboutItem);
            //Thread.Sleep(6000);
            aboutInstance = new About(driver);
            List<String> actualResult = aboutInstance.GetButtonsName(wait);
            // actualResult = aboutInstance.GetButtonsName(wait);
            string lineActualResult = (string.Join(",", actualResult.ToArray()));
            //string actualRes = lineActualResult.Replace("\r\n", ',');
            Console.WriteLine(lineActualResult);
            //Console.WriteLine(res);
            string[] listExpRes = expectedResult.Split(',');
            List<string> expectedRes = new List<string>(listExpRes);
            // Console.WriteLine(actualResult);
            // Console.WriteLine(expectedResult);
            // string expectedRes = res;
            // string expectedResult = "Development & Research,Quality Assurance,Anagement and Mentoring,AdditionalThanks";
            foreach (var item in expectedRes)
            {
                Console.WriteLine(item.Length);
            }
            // string[] expectedResult = res.Split(',');
            CollectionAssert.AreEqual(expectedRes, actualResult);



                //string exeptualResultTitle = "Development & Research";
                //Console.WriteLine(groupLocationInstance.About.GetButtonsName());
                //Assert.AreEqual(exeptualResultTitle, groupLocationInstance.About.DevelopmentResearch.Text);
                //groupLocationInstance = new CenterContainer(driver);

                //Assert.AreSame(string[],mainPageInstance.)
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
           driver.Quit();
        }
    }
}

