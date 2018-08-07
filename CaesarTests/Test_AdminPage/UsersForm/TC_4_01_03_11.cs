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
    public class TC_4_01_03_11
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditUsersForm usersForm;
        WebDriverWait wait;
        Table table;
        string index ;
        List<string> expectedResult;

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
            table = new Table(usersForm.GetTable, driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("add-new-user")));
            usersForm.EditUser(table.GetRowNumberByValueInCell(index, 5));
            usersForm.IsOpened(wait);
        }

        [Test]
        public void Test_EditUserFormIsDisplayed()
        {
            expectedResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            expectedResult.RemoveAt(7);                                   
           
            List<string> actualResult = usersForm.RememberUser();
            usersForm.SubmitButton.Click();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        public void Test_UserFormFirstNameIsEdit()
        {
            usersForm.FirstNameField.Clear();
            usersForm.setFirstName("Olga")
                .SubmitButton.Click();
            expectedResult = usersForm.RememberUser();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_UserFormLastNameIsEdit()
        {
            usersForm.LastNameField.Clear();

            usersForm.setLastName("Ivanova")
                .SubmitButton.Click();
           expectedResult = usersForm.RememberUser();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_UserFormRoleIsEdit()
        {
            usersForm.selectRole(0)
                .SubmitButton.Click();
            expectedResult = usersForm.RememberUser();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Test_UserFormLocationIsEdit()
        {
            usersForm.selectLocation(0)
                .SubmitButton.Click();
            expectedResult = usersForm.RememberUser();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Test_UserFormLoginIsEdit()
        {
            usersForm.Login.Clear();

            usersForm.setLogin("IvanovaOlga")
                .SubmitButton.Click();
            expectedResult = usersForm.RememberUser();
            index = usersForm.Login.GetAttribute("value");
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_UserFormPasswordIsEdit()
        {
            usersForm.Password.Clear();
            usersForm.setPassword("asdfghj12`")
                .SubmitButton.Click();
            expectedResult = usersForm.RememberUser();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 5));
            actualResult.RemoveAt(7);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }        

        [OneTimeTearDown]
        public void CleanUp()
        {
            if (table.FindRowInTable(expectedResult))
            {
                usersForm.DeleteUser(table.GetRowNumberByValueInCell(index, 5));
            }
            driver.Quit();
        }
    }
}
