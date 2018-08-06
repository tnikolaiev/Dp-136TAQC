using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CaesarTests
{
    [TestFixture]
    class TC_3_06_04
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        String path;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            //Open Login Page
            webDriver.Url = baseURL;
            loginPageInstance = new LoginPage(webDriver);
            //Login as Teacher
            loginPageInstance.LogIn("sasha", "1234", wait);
            mainPageInstance = new MainPage(webDriver);
            //Go to group's LV-023-UX students page
            webDriver.Url = baseURL + "/Students/Lviv/Lv-023-UX/list";
            wait.Until((d) => StudentsContent.IsOpened(d));
            //Open modal window 'EditStudentListWindow'
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            wait.Until((d) => EditStudentListWindow.IsOpened(d));
        }
        [Test]
        public void ExecuteTest_ImportStudentList_CorrectDataImported()
        {
            mainPageInstance.ModalWindow.EditStudentListWindow.ImportStudentsButton.Click();
            path = EditStudentListWindow.GetTestFile("TC_3_06_04.csv");
            Acts.UploadFile(path);
            wait.Until((d) => EditStudentListWindow.IsOpened(d));
            mainPageInstance.ModalWindow.EditStudentListWindow.SaveFormButton.Click();
            Assert.AreEqual(1, mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count);
        }
        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            while (mainPageInstance.ModalWindow.EditStudentListWindow.DeleteButtons.Count != 0)
            {
                mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetElementFromCell
               (mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count, EditStudentListWindow.DeleteButtonsColumn).Click();
                Acts.PressKeyboardButton(@"{Enter}");
            }
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
