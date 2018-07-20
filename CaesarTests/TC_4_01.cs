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
    public class TC_4_01
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        AdminPage adminPageInstance;
        WebDriverWait wait;
        RandomText random = new RandomText();
        List<string> tableElements;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("Dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            driver.Url = @"http://localhost:3000/admin";
            adminPageInstance = new AdminPage(driver);
        }

        [Test]
        public void Test_CreateUserFormIsDisplayed()
        {
            Acts.Click(adminPageInstance.AddButton);
            IWebElement name = driver.FindElement(By.Name("firstName"));
            IWebElement sName = driver.FindElement(By.Name("lastName"));
            IWebElement login = driver.FindElement(By.Name("login"));
            IWebElement password = driver.FindElement(By.Name("password"));
            IWebElement role = driver.FindElement(By.Name("role"));
            IWebElement location = driver.FindElement(By.Name("location"));

            Assert.IsEmpty(name.Text, sName.Text, login.Text, password.Text, role.Text, location.Text);

        }

        [Test]
        public void Test_FirstNameField()
        {
            string expectedResult = "Olga";
            Acts.Click(adminPageInstance.AddButton);

            wait.Until((d) => AdminPage.IsCreateForm(driver));

            IWebElement name = driver.FindElement(By.Name("firstName"));
            Acts.InputValue(name, "Olga");
            Assert.AreEqual(expectedResult, name.Text);
        }

        [Test]
        public void Test_CreateUser_Valid()
        {
            Acts.Click(adminPageInstance.AddButton);
            wait.Until((d) => AdminPage.IsCreateForm(driver));
            var name = driver.FindElement(By.Name("firstName"));
            Acts.InputValue(name, random.RandomName(20));

            var sName = driver.FindElement(By.Name("lastName"));
            Acts.InputValue(sName, random.RandomName(20));

            var login = driver.FindElement(By.Name("login"));
            login.SendKeys(random.RandomName(20));

            var password = driver.FindElement(By.Name("password"));
            Acts.InputValue(password, random.RandomName(2) + random.RandomNumber(0, 100) + random.RandomName(5));

            var submit = driver.FindElement(By.ClassName("btn-primary"));

            Acts.Click(submit);

            tableElements = adminPageInstance.GetTableElements("td");
            CollectionAssert.Contains(tableElements, Acts.GetAttribute(login, "loginName"));
            
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
