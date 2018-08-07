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
    public class TC_4_01_03_00
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
            usersForm.setFirstName("Ivan")
                .setLastName("Ivanov")
                .selectRole(2)
                .selectLocation(3)
                .setLogin("IvanIvan")
                .setPassword("qwerty12~")
                .SubmitButton.Click();
            index = usersForm.Login.GetAttribute("value");
        }

        [SetUp]
        public void EditUser()
        {           
            usersForm = new CreateEditUsersForm(driver);
            table = new Table(usersForm.GetTable);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("add-new-user")));
            usersForm.EditUser(table.GetRowNumberByValueInCell(index, 5));
            usersForm.IsOpened(wait);
        }

        [Test, TestCaseSource("UsersName") ]
        public void Test_UserFormFirstNameIsNotEdit_Fail(string name, string sername, string login, string password)
        {
            usersForm.FirstNameField.Clear();
            usersForm.setFirstName(name)
                .SubmitButton.Click();
           expectedResult = usersForm.RememberUser();
            expectedResult.Add("EditDelete");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("UsersName")]
        public void Test_UserFormLastNameIsNotEdit_Fail(string name, string sername, string login, string password)
        {
            usersForm.LastNameField.Clear();
            usersForm.setLastName(name)
                .SubmitButton.Click();
            expectedResult = usersForm.RememberUser();
            expectedResult.Add("EditDelete");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }              
    
        [Test, TestCaseSource("UsersLogin")]
        public void Test_UserFormLoginIsNotEdit_Fail(string name, string sername, string login, string password)
        {
            usersForm.Login.Clear();

            usersForm.setLogin(login)
                .SubmitButton.Click();
            expectedResult = usersForm.RememberUser();
            index = usersForm.Login.GetAttribute("value");
            expectedResult.Add("EditDelete");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("UsersPassword")]
        public void Test_UserFormPasswordIsNotEdit_Fail(string name, string sername, string login, string password)
        {
            usersForm.Password.Clear();
            usersForm.setPassword(password)
                .SubmitButton.Click();
            expectedResult = usersForm.RememberUser();
            expectedResult.Add("EditDelete");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            if (table.FindRowInTable(expectedResult))
            {
                usersForm.DeleteUser(table.GetRowNumberByValueInCell(index, 5));
            }
            driver.Close();
            driver.Quit();
        }
    }
}
