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
    public class TC_4_01_02_11
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;
        Table table;
        string index;
        List<string> expectedResult;

        static object[] UsersInfo =
        {
            new object[] {"Olga", "Ivanova", 0, 0, "IvanovaO@", "qwerty12#" },
            new object[] {"Hanna", "Lavrova", 1, 1, "IvanovaH@", "qwerty12#" },
            new object[] {"Thor", "Thorov", 2, 2, "Thor@", "qwerty12#" },
            new object[] {"Halk", "Halkov", 0, 3, "Halk@", "qwerty12#" },
            new object[] {"Iron", "Ironon", 1, 4, "Iron@", "qwerty12#" },
            new object[] {"Lady", "Ivanova", 1, 5, "Lady@", "qwerty12#" },
            new object[] {"Sima", "Kotova", 1, 5, "Sima@", "qwerty12#" }
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
        }

        [SetUp]
        public void OpenUsersForm()
        {            
            usersForm = new CreateEditUsersForm(driver);
            usersForm.addUsers();            
        }
       
        [Test, TestCaseSource("UsersInfo")]
        public void Test_CreateUser_Success(string name, string sername, int role, int location, string login, string password)
        {
            usersForm.IsOpened(wait);
            usersForm.setFirstName(name)
                .setLastName(sername)
                .selectRole(role)
                .selectLocation(location)
                .setLogin(login)
                .setPassword(password)
                .SubmitButton.Click();

            expectedResult = usersForm.RememberUser();
            index = usersForm.Login.GetAttribute("value");
            table = new Table(usersForm.GetTable, driver);
            Assert.IsTrue(table.FindRowInTable(expectedResult));
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