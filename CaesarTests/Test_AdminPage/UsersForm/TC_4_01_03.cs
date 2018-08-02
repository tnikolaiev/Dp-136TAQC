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
    public class TC_4_01_03
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditUsersForm usersForm;
        CreateEditUsersForm usersFormEdit;
        WebDriverWait wait;
        Table table;
        string index;
        List<string> user;

        //[OneTimeSetUp]
        //public void Initialize()
        //{
        //    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //    driver.Url = @"http://localhost:3000/logout";
        //    loginPageInstance = new LoginPage(driver);
        //    wait.Until((d) => LoginPage.IsLoginPageOpened(d));
        //    loginPageInstance.LogIn("Dmytro", "1234");
        //    wait.Until((d) => MainPage.IsMainPageOpened(d));
        //    driver.Url = @"http://localhost:3000/admin";
        //    wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
        //    usersForm = new CreateEditUsersForm(driver);
        //}

        [SetUp]
        public void EditUser()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("Dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            driver.Url = @"http://localhost:3000/admin";
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            usersForm = new CreateEditUsersForm(driver);
            
            usersForm.Test_UserCreate(wait);
            //usersForm.addUsers();

            //usersForm.IsOpened(wait);
            //usersForm.setFirstName("Olga")
            //    .setLastName("Ivanova")
            //    .selectRole(0)
            //    .selectLocation(0)
            //    .setLogin("IvanovaO@")
            //    .setPassword("qwerty12#")
            //    .SubmitButton.Click();
           
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            table = new Table(usersForm.GetTable, driver);            
        }

        [Test]
        public void Test_EditUserFormIsDisplayed()
        {
            usersFormEdit = new CreateEditUsersForm(driver);
            index = usersFormEdit.Login.GetAttribute("value");
            List<string> expectedResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            expectedResult.RemoveAt(7);
            //user = usersForm.RememberUser();
            usersFormEdit.EditClick(table.GetRowsWithColumns(), table.GetRowNumberByValueInCell(index, 5));            
           
            List<string> actualResult = usersFormEdit.RememberUser();
            usersFormEdit.Close.Click();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [TearDown]
        public void Delete()
        {
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            Thread.Sleep(1000);
            usersFormEdit.DeleteUser(table.GetRowNumberByValueInCell(index, 5));
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {           
            driver.Close();
            driver.Quit();
        }
    }
}
