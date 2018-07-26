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
        CreateEditUsersForm usersForm;
        WebDriverWait wait;
        Table table;

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
            usersForm = new CreateEditUsersForm(driver);
            wait.Until((d) => AdminPage.IsAdminPageOpened(d));
           
        }

        [Test]
        public void Test_EditUserFormIsDisplayed()
        {
            usersForm.getLastElement(usersForm.Edit, 5).Click();
            wait.Until((d) => CreateEditUsersForm.IsCreateFormOpened(d));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            table = new Table(usersForm.GetTable, driver);
            List<string> expectedResult = table.getRowWithColumns(5);
            expectedResult.RemoveAt(7);
            List<string> actualResult = new List<string>();
            actualResult.Add(usersForm.FirstNameField.GetAttribute("value"));
            actualResult.Add(usersForm.LastNameField.GetAttribute("value"));
            actualResult.Add(usersForm.Role.GetAttribute("value"));
            actualResult.Add(usersForm.Location.GetAttribute("value"));
            actualResult.Add(usersForm.Photo.GetAttribute("value"));
            actualResult.Add(usersForm.Login.GetAttribute("value"));
            actualResult.Add(usersForm.Password.GetAttribute("value"));

            CollectionAssert.AreEqual(expectedResult, actualResult);

        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
