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
    public class TC_4_03_01
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
            studentForm.GoToStudents.Click();
            studentForm.addStudents();
            wait.Until((d) => CreateEditStudentsForm.IsCreateEditFormOpened(d));
        }

        [Test]
        public void Test_CreateStudentFormIsDisplayed()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            List<string> expectedResult = new List<string> { "", "", "", "Elementary", "", "", "", "" };
            List<string> actualResult = studentForm.RememberStudent();

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void Test_GroupIdFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setGroupId("someText");
            Assert.AreEqual("someText", studentForm.GroupIdField.GetAttribute("value"));
            studentForm.Close.Click();
        }

        [Test]
        public void Test_NameFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setName("someText");
            Assert.AreEqual("someText", studentForm.NameField.GetAttribute("value"));
            studentForm.Close.Click();
        }

        [Test]
        public void Test_LastNameFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setLastName("someText");
            Assert.AreEqual("someText", studentForm.LastNameField.GetAttribute("value"));
            studentForm.Close.Click();
        }

        [Test]
        public void Test_EnglishLevelIsDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setEnglishLevelDDL(1);
            Assert.AreEqual("Pre-intermediate", studentForm.EnglishLevelDDL.GetAttribute("value"));
            studentForm.Close.Click();
        }

        [Test]
        public void Test_CvUrlIsDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setCvUrl("someText");
            Assert.AreEqual("someText", studentForm.CvUrlField.GetAttribute("value"));
            studentForm.Close.Click();
        }

        [Test]
        public void Test_ImageUrlFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setImageUrl("someText");
            Assert.AreEqual("someText", studentForm.ImageUrlField.GetAttribute("value"));
            studentForm.Close.Click();
        }

        [Test]
        public void Test_EntryScoreFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setEntryScore("someText");
            Assert.AreEqual("someText", studentForm.EntryScoreField.GetAttribute("value"));
            studentForm.Close.Click();
        }

        [Test]
        public void Test_ApprovedByFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setApprovedBy("someText");
            Assert.AreEqual("someText", studentForm.ApprovedByField.GetAttribute("value"));
            studentForm.Close.Click();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }

    }
}
