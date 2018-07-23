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
        AdminPage adminPage;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;
        List<string> tableElements;
        readonly string expectedResult = "someText";

        static object[] UsersInfo =
        {
            new object[] {"Olga", "Ivanova", 0, 0, "IvanovaO@", "qwerty12#" },
            new object[] {"Hanna", "Lavrova", 1, 1, "IvanovaH@", "qwerty12#" },
            new object[] {"Tor", "Torov", 2, 2, "Tor@", "qwerty12#" },
            new object[] {"Halk", "Halkov", 0, 3, "Halk@", "qwerty12#" },
            new object[] {"Iron", "Ironon", 1, 4, "Iron@", "qwerty12#" },
            new object[] {"Lady", "Ivanova", 1, 5, "Lady@", "qwerty12#" },
            new object[] {"Sima", "Kotova", 1, 5, "Sima@", "qwerty12#" },

        };

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
            adminPage = new AdminPage(driver);
            Acts.Click(adminPage.AddButton);
            wait.Until((d) => AdminPage.IsCreateForm(driver));
            usersForm = new CreateEditUsersForm(driver);
        }

        [Test]
        public void Test_CreateUserFormIsDisplayed()
        {
            Assert.IsEmpty(Acts.GetAttribute(usersForm.FirstNameField, "value"),
            Acts.GetAttribute(usersForm.LastNameField, "value"),
            Acts.GetAttribute(usersForm.Photo, "value"),
            Acts.GetAttribute(usersForm.Login, "value"),
            Acts.GetAttribute(usersForm.Password, "value"));

            Assert.AreEqual(usersForm.Location.GetAttribute("value"), "Chernivtsy");
            Assert.AreEqual(usersForm.Role.GetAttribute("value"), "Teacher");
        }

        [Test]
        public void Test_FirstNameFieldDisplayedText()
        {            
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.InputValue(usersForm.FirstNameField, "someText");
            Assert.AreEqual(expectedResult, usersForm.FirstNameField.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_LastNameFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.InputValue(usersForm.LastNameField, "someText");
            Assert.AreEqual(expectedResult, usersForm.LastNameField.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_RoleIsDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.SelectOptionFromDDL(usersForm.Role, "Coordinator");
            Assert.AreEqual("Coordinator", usersForm.Role.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_LocationIsDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.SelectOptionFromDDL(usersForm.Location, "Dnipro");
            Assert.AreEqual("Dnipro", usersForm.Location.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_PhotoFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.InputValue(usersForm.Photo, "someText");
            Assert.AreEqual(expectedResult, usersForm.Photo.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_LoginFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.InputValue(usersForm.Login, "someText");
            Assert.AreEqual(expectedResult, usersForm.Login.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_PasswordFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.InputValue(usersForm.Password, "someText");
            Assert.AreEqual(expectedResult, usersForm.Password.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test, TestCaseSource("UsersInfo")]
        public void Test_CreateUser_Valid(string name, string sername, int role, int location, string login, string password)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.FirstNameField.SendKeys(name);
            usersForm.LastNameField.SendKeys(sername);
            Acts.SelectElement(usersForm.Role, role);
            Acts.SelectElement(usersForm.Location, location);
            usersForm.Login.SendKeys(login);
            usersForm.Password.SendKeys(password);

            string expectedResult = usersForm.Login.GetAttribute("value");
            usersForm.SubmitButton.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("nav-tabs")));
            tableElements = adminPage.GetTableElements("td");

            CollectionAssert.Contains(tableElements, expectedResult);

            adminPage.getLastElement(adminPage.Delete).Click();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }

    }
}
