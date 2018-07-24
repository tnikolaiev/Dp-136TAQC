using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    public class TC_4_01_Edit
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        AdminPage adminPage;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;
        Table table;
        List<string> lastUser;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("Dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            driver.Url = @"http://localhost:3000/admin";
            adminPage = new AdminPage(driver);
            wait.Until((d) => AdminPage.IsAdminPageOpened(d));            
            usersForm = new CreateEditUsersForm(driver);

        }

        [Test]
        public void Test_EditUserFormIsDisplayed()
        {          
            
            table = new Table(adminPage.GetTable, driver);
            int size = table.GetRows().Count;
            Console.Write(size);
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));

        }

        [Test]
        public void Test_EditUserForm()
        {
            adminPage.getLastElement(adminPage.Edit).Click();
            Assert.IsTrue(wait.Until((d) => AdminPage.IsCreateFormOpened(d)));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
