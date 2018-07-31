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
    public class TC_4_03_CreateStudent_Success
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditStudentsForm studentForm;
        WebDriverWait wait;

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
            wait.Until((d) => CreateEditStudentsForm.IsAdminPageOpened(d));
            studentForm = new CreateEditStudentsForm(driver);
            studentForm.AddButton.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("add-new-user")));
            studentForm.GoToStudents.Click();
            wait.Until((d) => CreateEditStudentsForm.IsCreateEditFormOpened(d));
        }

        [Test]
        public void Test_CreateStudentFormIsDisplayed()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Assert.IsEmpty(studentForm.GroupIdField.GetAttribute("value"),
            studentForm.NameField.GetAttribute("value"),
            studentForm.LastNameField.GetAttribute("value"),
            studentForm.CvUrlField.GetAttribute("value"),
            studentForm.ImageUrlField.GetAttribute("value"),
            studentForm.EntryScoreField.GetAttribute("value"),
            studentForm.ApprovedByField.GetAttribute("value"));

            Assert.AreEqual(studentForm.EnglishLevelDDL.GetAttribute("value"), "Elementary");
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
