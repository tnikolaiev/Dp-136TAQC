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
    public class TC_4_01_CreateUser_Success
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;

        Table table;

        static object[] UsersInfo =
        {
            new object[] {"Olga", "Ivanova", 0, 0, "IvanovaO@", "qwerty12#" },
            new object[] {"Hanna", "Lavrova", 1, 1, "IvanovaH@", "qwerty12#" },
            new object[] {"Thor", "Thorov", 2, 2, "Thor@", "qwerty12#" },
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
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            usersForm = new CreateEditUsersForm(driver);
            usersForm.AddButton.Click();

        }

        [Test]
        public void Test_CreateUserFormIsDisplayed()
        {
            Assert.IsEmpty(Acts.GetAttribute(usersForm.FirstNameField, "value"),
            usersForm.LastNameField.GetAttribute("value"),
            usersForm.Photo.GetAttribute("value"),
            usersForm.Login.GetAttribute("value"),
            usersForm.Password.GetAttribute("value"));

            Assert.AreEqual(usersForm.LocationDDL.GetAttribute("value"), "Chernivtsy");
            Assert.AreEqual(usersForm.RoleDDL.GetAttribute("value"), "Teacher");
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

        [Test, TestCaseSource("UsersInfo")]
        public void Test_CreateUser_Valid(string name, string sername, int role, int location, string login, string password)
        {
            usersForm.IsOpened(wait);
            usersForm.setFirstName(name)
                .setLastName(sername)
                .selectRole(role)
                .selectLocation(location)
                .setLogin(login)
                .setPassword(password)
                .SubmitButton.Click();
            
            String expectedResult = usersForm.Login.GetAttribute("value");

            table = new Table(usersForm.GetTable, driver);
            table.getRowWithColumns(1);

            CollectionAssert.Contains(table.getRowWithColumns(0), expectedResult);

            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            Thread.Sleep(1000);
            usersForm.getLastElement(usersForm.Delete, 0).Click();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }

    }
}