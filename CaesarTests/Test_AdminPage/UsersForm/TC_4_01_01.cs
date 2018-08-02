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
    public class TC_4_01_01
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;
       
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
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            usersForm = new CreateEditUsersForm(driver);
            usersForm.addUsers();
        }

        [Test]
        public void Test_CreateUserFormIsDisplayed()
        {
            List<string> expectedResult = new List<string> { "", "", "Teacher", "Chernivtsy", "", "", "" };
            List<string> actualResult = usersForm.RememberUser();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_FirstNameFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.FirstNameField.SendKeys("someText");
            Assert.AreEqual("someText", usersForm.FirstNameField.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_LastNameFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.LastNameField.SendKeys("someText");
            Assert.AreEqual("someText", usersForm.LastNameField.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_RoleIsDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.selectRole(1);
            Assert.AreEqual("Coordinator", usersForm.RoleDDL.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_LocationIsDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.selectLocation(1);
            Assert.AreEqual("Dnipro", usersForm.LocationDDL.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_PhotoFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.Photo.SendKeys("someText");
            Assert.AreEqual("someText", usersForm.Photo.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_LoginFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.Login.SendKeys("someText");
            Assert.AreEqual("someText", usersForm.Login.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_PasswordFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.Password.SendKeys("someText");
            Assert.AreEqual("someText", usersForm.Password.GetAttribute("value"));
            usersForm.Close.Click();
        }
       
        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }

    }
}