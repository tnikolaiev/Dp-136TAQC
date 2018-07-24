using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_3_04_02
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        String path;
        [OneTimeSetUp]
        public void OneTimeSetUpTest()
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
            //Go to group's DP-093-JS students page
            webDriver.Url = baseURL + "/Students/Dnipro/DP-093-JS/list";
            wait.Until((d) => StudentsContent.IsStudentsContentOpened(d));
            //Open modal window 'EditStudentListWindow'
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
            //Create new student for test
            mainPageInstance.ModalWindow.EditStudentListWindow.CreateStudentButton.Click();
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));
            mainPageInstance.ModalWindow.EditStudentWindow.FillForm("Andrey", "Magera", 3, "137", "4.2", 1);
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

        }
        [SetUp]
        public void SetUpTest()
        {
            //Open last student in table for editing
            mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetElementFromCell
                (mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count, EditStudentListWindow.EditButtonsColumn).Click();
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));
            //Upload CV and photo
            path = EditStudentWindow.GetTestFile("TC_3_04 CV.docx");
            mainPageInstance.ModalWindow.EditStudentWindow.BrowseCVButton.Click();
            Acts.UploadFile(path);
            path = EditStudentWindow.GetTestFile("TC_3_04 photo.png");
            mainPageInstance.ModalWindow.EditStudentWindow.BrowsePhotoButton.Click();
            Acts.UploadFile(path);
            //Save changes
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
            //Open last student in table for editing
            mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetElementFromCell
                (mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count, EditStudentListWindow.EditButtonsColumn).Click();
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));
        }
        [Test]
        public void ExecuteTest_DeleteFiles_FilesDeleted()
        {
            //Remove files
            mainPageInstance.ModalWindow.EditStudentWindow.RemoveCVButton.Click();
            mainPageInstance.ModalWindow.EditStudentWindow.RemovePhotoButton.Click();
            //Save changes
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

            //Open last student in table for editing
            mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetElementFromCell
                (mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count, EditStudentListWindow.EditButtonsColumn).Click();
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));

            Assert.AreEqual(0, mainPageInstance.ModalWindow.EditStudentWindow.CountUploadedFiles());
        }
        [TearDown]
        public void TearDownTest()
        {
            //Save changes
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            //Delete test students
            mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetElementFromCell
                (mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count, EditStudentListWindow.DeleteButtonsColumn).Click();
            Acts.PressKeyboardButton(@"{Enter}");
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
