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
    public class TC_4_01_Create
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;

        Table table;
        string expectedResult = "someText";

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
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            usersForm = new CreateEditUsersForm(driver);
            usersForm.AddButton.Click();
            wait.Until((d) => CreateEditUsersForm.IsCreateFormOpened(d));

        }

        [Test]
        public void Test_CreateUserFormIsDisplayed()
        {
            Assert.IsEmpty(Acts.GetAttribute(usersForm.FirstNameField, "value"),
            usersForm.LastNameField.GetAttribute("value"),
            usersForm.Photo.GetAttribute("value"),
            usersForm.Login.GetAttribute("value"),
            usersForm.Password.GetAttribute("value"));

            Assert.AreEqual(usersForm.Location.GetAttribute("value"), "Chernivtsy");
            Assert.AreEqual(usersForm.Role.GetAttribute("value"), "Teacher");
        }

        [Test]
        public void Test_FirstNameFieldDisplayedText()
        {            
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.FirstNameField.SendKeys("someText");
            Assert.AreEqual(expectedResult, usersForm.FirstNameField.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_LastNameFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.LastNameField.SendKeys("someText");
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
            usersForm.Photo.SendKeys("someText");
            Assert.AreEqual(expectedResult, usersForm.Photo.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_LoginFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.Login.SendKeys("someText");
            Assert.AreEqual(expectedResult, usersForm.Login.GetAttribute("value"));
            usersForm.Close.Click();
        }

        [Test]
        public void Test_PasswordFieldDisplayedText()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.Password.SendKeys("someText");
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

            String expectedResult = usersForm.Login.GetAttribute("value");
            usersForm.SubmitButton.Click();

            table = new Table(usersForm.GetTable, driver);
            
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
