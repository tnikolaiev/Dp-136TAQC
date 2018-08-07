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
    public class TC_4_01_02_00
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;
        Table table;
        string index;
        List<string> expectedResult;
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

        [OneTimeSetUp]
        public void OpenAdminPage()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("Dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            driver.Url = @"http://localhost:3000/admin";
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
                        
            
        }
        [SetUp]
        public void AddUserClick()
        {
            usersForm = new CreateEditUsersForm(driver);
            table = new Table(usersForm.GetTable);
            usersForm.addUsers();
            usersForm.IsOpened(wait);
        }
        
        [Test]
        public void Test_CreateUserWithEmptyFields()
        {
            expectedResult = usersForm.RememberUser();
            expectedResult.Add("EditDelete");
            usersForm.SubmitButton.Click();
            index = usersForm.Login.GetAttribute("value");
            Thread.Sleep(1000);            
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("UsersName")]
        public void Test_CreateUser_FirstNameIsInvalid(string name, string sername, string login, string password)
        {
            usersForm.setFirstName(name)
                .setLastName(sername)
                .setLogin(login)
                .setPassword(password)
                .SubmitButton.Click();

            index = usersForm.Login.GetAttribute("value");
            expectedResult = usersForm.RememberUser();
            expectedResult.Add("EditDelete");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("UsersName")]
        public void Test_CreateUser_LastNameIsInvalid(string sername, string name, string login, string password)
        {
            usersForm.setFirstName(name)
                .setLastName(sername)
                .setLogin(login)
                .setPassword(password)
                .SubmitButton.Click();

            index = usersForm.Login.GetAttribute("value");
            expectedResult = usersForm.RememberUser();
            expectedResult.Add("EditDelete");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }
        [Test, TestCaseSource("UsersLogin")]
        public void Test_CreateUser_LoginIsInvalid(string name, string sername, string login, string password)
        {
            usersForm.setFirstName(name)
                .setLastName(sername)
                .setLogin(login)
                .setPassword(password)
                .SubmitButton.Click();

            index = usersForm.Login.GetAttribute("value");
            expectedResult = usersForm.RememberUser();
            expectedResult.Add("EditDelete");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("UsersPassword")]
        public void Test_CreateUser_PasswordIsInvalid(string name, string sername, string login, string password)
        {
            usersForm.setFirstName(name)
                .setLastName(sername)
                .setLogin(login)
                .setPassword(password)
                .SubmitButton.Click();

            index = usersForm.Login.GetAttribute("value");
            expectedResult = usersForm.RememberUser();
            expectedResult.Add("EditDelete");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [TearDown]
        public void Delete()
        {
            if (table.FindRowInTable(expectedResult))
            {
                usersForm.DeleteUser(table.GetRowNumberByValueInCell(index, 5));
            }
        }

        [OneTimeTearDown]
        public void CleanUp()
        {           
            driver.Close();
            driver.Quit();
        }
    }
}