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
    public class TC_4_01_CreateUser_Fail
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;
        Table table;

        static object[] UsersName =
        {
            new object[] { "123456", "Ivanova", "IvanovaO@", "qwerty12#" },
            new object[] { "InputFirstNameCharacter", "Lavrova", "IvanovaH@", "qwerty12#" },
            new object[] { "!@#$%$^#@$", "Thorov",  "Thor@", "qwerty12#" }
        };

        static object[] UsersLogin =
        {
            new object[] { "Ivan", "Ivanova","123456", "qwerty12#" },
            new object[] { "Lora", "Lavrova", "InputFirstNameCharacter@", "qwerty12#" },
            new object[] { "Thor", "Thorov", "!@#$%$^#@$@", "qwerty12#" },
            new object[] { "Thor", "Thorov",  "Фьмвл12%к", "qwerty12#" }            
        };

        static object[] UsersPassword =
        {
            new object[] { "Ivan", "Ivanova", "IvanovaO@", "123456789" },
            new object[] { "Lora", "Lavrova", "IvanovaH@", "InputFirstName" },
            new object[] { "Thor", "Thorov", "Thor@", "qwe12#" },
            new object[] { "Thor", "Thorov", "Thor@", "!@#$%$^#@$@" },
                new object[] { "Lora", "Lavrova", "IvanovaH@", "Фьмвл12%к" }
        };

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
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            table = new Table(usersForm.GetTable, driver);
            usersForm.AddButton.Click();

        }
               
        [Test]
        public void Test_CreateUserWithEmptyFields()
        {
            //usersForm.IsOpened(wait);
            //List<string> expectedResult = new List<string>();
            //expectedResult = usersForm.RememberUser();
            //expectedResult.Add("EditDelete");
            //usersForm.SubmitButton.Click();
            //Thread.Sleep(1000);

           // Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("UsersName")]
        public void Test_CreateUser_FirstNameIsInvalid(string name, string sername, string login, string password)
        {
            //List<string> expectedResult = new List<string>();
            //usersForm.IsOpened(wait);
            //usersForm.setFirstName(name)
            //    .setLastName(sername)
            //    .setLogin(login)
            //    .setPassword(password)
            //    .SubmitButton.Click();

            //expectedResult = usersForm.RememberUser();
            //expectedResult.Add("EditDelete");
            //Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}