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
    public class TC_4_Close_CreateEditForms
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        AdminPage adminPage;
        CreateEditUsersForm usersForm;
        CreateEditGroupsForm groupForm;
        CreateEditStudentsForm studentForm;

        WebDriverWait wait;
        Table table;

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
            wait.Until((d) => AdminPage.IsCreateFormOpened(d));
            

        }
        //===================================
        //User Form

        [Test]
        public void Test_CloseButtonUserForm()
        {
            usersForm = new CreateEditUsersForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.CloseButton.Click();
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        [Test]
        public void Test_CloseIconUserForm()
        {
            usersForm = new CreateEditUsersForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            usersForm.Close.Click();
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        [Test]
        public void Test_EscUserForm()
        {
            usersForm = new CreateEditUsersForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.PressKeyboardButton(@"{Escape}");
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        //======================================
        //Grop Form
        [Test]
        public void Test_CloseButtonGroupForm()
        {
            groupForm= new CreateEditGroupsForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            groupForm.CloseButton.Click();
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        [Test]
        public void Test_CloseIconGroupForm()
        {
            groupForm = new CreateEditGroupsForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            groupForm.Close.Click();
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        [Test]
        public void Test_EscGroupForm()
        {
            groupForm = new CreateEditGroupsForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.PressKeyboardButton(@"{Escape}");
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        //==========================================
        //Student Form
        [Test]
        public void Test_CloseButtonStudentForm()
        {
            studentForm = new CreateEditStudentsForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.CloseButton.Click();
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        [Test]
        public void Test_CloseIconStudentForm()
        {
            studentForm = new CreateEditStudentsForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.Close.Click();
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        [Test]
        public void Test_EscStudentForm()
        {
            studentForm = new CreateEditStudentsForm(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            Acts.PressKeyboardButton(@"{Escape}");
            Assert.IsTrue(wait.Until((d) => AdminPage.IsAdminPageOpened(d)));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }

    }
}
