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
        List<string> user;

        static object[] UsersInfo =
        {
            new object[] { "123456", "123456", "123456", "123456789" },
            new object[] { "InputFirstNameCharacter", "InputFirstNameCharacter", "InputFirstNameCharacter", "InputFirstNameCharacter" },
            new object[] { "!@#$%$^#@$", "!@#$%$^#@$", "!@#$%$^#@$@", "qwe12#" },
            new object[] { "Lora", "Lavrova", "IvanovaH@", "Фьмвл12%к" },
             new object[] { "Thor", "Thorov", "Thor@", "!@#$%$^#@$@" }
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
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            table = new Table(usersForm.GetTable, driver);
            usersForm.EditUser(table.GetRowNumberByValueInCell(index, 5));
        }

        [Test, TestCaseSource("UsersName") ]
        public void Test_UserFormFirstNameIsNotEdit_Fail(string name, string sername, string login, string password)
        {
            usersForm = new CreateEditUsersForm(driver);
            usersForm.IsOpened(wait);
            usersForm.FirstNameField.Clear();
            usersForm.setFirstName(name)
                .SubmitButton.Click();
            List<string> expectedResult = usersForm.RememberUser();
            index = usersForm.Login.GetAttribute("value");
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_UserFormLastNameIsNotEdit_Fail(string name, string sername, string login, string password)
        {
            usersForm = new CreateEditUsersForm(driver);
            usersForm.IsOpened(wait);
            usersForm.LastNameField.Clear();

            usersForm.setLastName(sername)
                .SubmitButton.Click();
            List<string> expectedResult = usersForm.RememberUser();
            index = usersForm.Login.GetAttribute("value");
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }              
    
        [Test]
        public void Test_UserFormLoginIsNotEdit_Fail(string name, string sername, string login, string password)
        {
            usersForm = new CreateEditUsersForm(driver);
            usersForm.IsOpened(wait);
            usersForm.Login.Clear();

            usersForm.setLogin(login)
                .SubmitButton.Click();
            List<string> expectedResult = usersForm.RememberUser();
            index = usersForm.Login.GetAttribute("value");
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_UserFormPasswordIsNotEdit_Fail(string name, string sername, string login, string password)
        {
            usersForm = new CreateEditUsersForm(driver);
            usersForm.IsOpened(wait);
            usersForm.Password.Clear();
            usersForm.setPassword(password)
                .SubmitButton.Click();
            List<string> expectedResult = usersForm.RememberUser();
            index = usersForm.Login.GetAttribute("value");
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TearDown]
        public void Delete()
        {
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            Thread.Sleep(1000);
            usersForm.DeleteUser(table.GetRowNumberByValueInCell(index, 5));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            usersForm.DeleteUser(table.GetRowNumberByValueInCell(index, 5));
            driver.Quit();
        }
    }
}
