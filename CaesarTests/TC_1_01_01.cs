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
    public class TC_1_01_01
    {
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;

        [SetUp]
        public void Initialization()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
        }

        static List<String> Links = new List<String> { @"http://localhost:3000", @"http://localhost:3000/Groups/Dnipro", @"http://localhost:3000/admin" };

        [Test, TestCaseSource("Links")]
        public void Test_NavigateToLinks_LoginPageOpened(String link)
        {
            driver.Url = link;
            Assert.IsTrue(wait.Until((d) => LoginPage.IsLoginPageOpened(driver)));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
            //driver.Dispose();
        }
    }
}