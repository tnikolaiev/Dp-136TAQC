using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_3_06_01
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
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(1));
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            //Open Login Page
            webDriver.Url = baseURL;
            wait.Until((driver) => LoginPage.IsLoginPageOpened(driver));
            loginPageInstance = new LoginPage(webDriver);
            //Login as Teacher
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(webDriver);
            //Go to group's LV-023-UX students page
            webDriver.Url = baseURL + "/Students/Lviv/Lv-023-UX/list";
            wait.Until((d) => StudentsContent.IsStudentsContentOpened(d));
            //Open modal window 'EditStudentListWindow'
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
        }
        [Test]
        public void ExecuteTest_ImportStudentList_ListImported()
        {
            mainPageInstance.ModalWindow.EditStudentListWindow.ImportStudentsButton.Click();
            path = EditStudentListWindow.GetTestFile("TC_3_06_01 ValidStudentList.txt");
            Acts.UploadFile(path);
            mainPageInstance.ModalWindow.EditStudentListWindow.SaveFormButton.Click();
            Assert.AreEqual(4, mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count);
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
