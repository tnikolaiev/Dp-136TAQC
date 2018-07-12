using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;
using CaesarLib;

namespace Tests
{
    [TestFixture]
    public class TC_1_01_01
    {
        List<string> links;
        IWebDriver driver = new ChromeDriver();
        LoginPage lp;

        [OneTimeSetUp]
        public void FirstInitialization()
        {
            links = new List<string>
            {
                @"http://localhost:3000", @"http://localhost:3000/Groups/Dnipro", @"http://localhost:3000/admin"
            };
            Console.WriteLine("Initialization complete");

        }

        [SetUp]
        public void Initialize()
        {
            lp = new LoginPage(driver);
        }

        [Test]
        public void ExecuteTest()
        {
            int count = 0;
            foreach (var link in links)
            {
                count++;
                driver.Url = link;
                Assert.IsTrue(LoginPage.IsLoginPage(driver));
                Console.WriteLine(String.Format("{0} passed", link));
            }
            Console.WriteLine("Execution complete");
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            //driver.Close();
            Console.WriteLine("Driver closed");
        }
    }
}