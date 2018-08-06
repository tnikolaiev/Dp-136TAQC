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
    public class TC_4_02_02_00
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditGroupsForm groupsForm;
        WebDriverWait wait;
        Table table;
        string index;
        [OneTimeSetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("Dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            driver.Url = @"http://localhost:3000/admin";
            wait.Until((d) => CreateEditGroupsForm.IsAdminPageOpened(d));
            groupsForm = new CreateEditGroupsForm(driver);
        }

        [SetUp]
        public void AddUserClick()
        {
            table = new Table(groupsForm.GetTable, driver);
            groupsForm.GoToGroups.Click();
            groupsForm.addGroups();
            groupsForm.IsOpened(wait);
        }
    }
}
