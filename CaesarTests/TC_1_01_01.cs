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
        List<string> links;
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;

        [OneTimeSetUp]
        public void FirstInitialization()
        {
            links = new List<string>
            {
                @"http://localhost:3000", @"http://localhost:3000/Groups/Dnipro", @"http://localhost:3000/admin"
            };
        }

        [SetUp]
        public void Initialization()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
        }

        [Test]
        public void Test_NavigateToLinks_LoginPageOpened()
        {
            foreach (var link in links)
            {
                driver.Url = link;
                Assert.IsTrue(wait.Until((d) => LoginPage.IsLoginPage(driver)));
            }
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}