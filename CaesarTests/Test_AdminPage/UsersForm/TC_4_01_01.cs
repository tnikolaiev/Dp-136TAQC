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
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            usersForm = new CreateEditUsersForm(driver);
            usersForm.addUsers();
            usersForm.IsOpened(wait);
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
            usersForm.FirstNameField.SendKeys("someText");
            string actualResult = usersForm.FirstNameField.GetAttribute("value");
            Assert.AreEqual("someText", actualResult);
        }

        [Test]
        public void Test_LastNameFieldDisplayedText()
        {
            usersForm.LastNameField.SendKeys("someText");
            string actualResult = usersForm.LastNameField.GetAttribute("value");
            Assert.AreEqual("someText", actualResult);
        }

        [Test]
        public void Test_RoleIsDisplayedText()
        {
            usersForm.selectRole(1);
            string actualResult = usersForm.RoleDDL.GetAttribute("value");
            Assert.AreEqual("Coordinator", actualResult);
        }

        [Test]
        public void Test_LocationIsDisplayedText()
        {
            usersForm.selectLocation(1);
            string actualResult = usersForm.LocationDDL.GetAttribute("value");
            Assert.AreEqual("Dnipro", actualResult);
        }

        [Test]
        public void Test_PhotoFieldDisplayedText()
        {
            usersForm.Photo.SendKeys("someText");
            string actualResult = usersForm.Photo.GetAttribute("value");
            Assert.AreEqual("someText", actualResult);
           
        }

        [Test]
        public void Test_LoginFieldDisplayedText()
        {
            usersForm.Login.SendKeys("someText");
            string actualResult = usersForm.Login.GetAttribute("value");
            Assert.AreEqual("someText", actualResult);

        }

        [Test]
        public void Test_PasswordFieldDisplayedText()
        {
            usersForm.Password.SendKeys("someText");
            string actualResult = usersForm.Password.GetAttribute("value");
            Assert.AreEqual("someText", actualResult);
        }
       
        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }

    }
}