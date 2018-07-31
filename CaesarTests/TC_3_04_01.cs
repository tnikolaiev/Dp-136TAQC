using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_3_04_01
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        String path;
        static object[] fileNames = 
        {
            new object[] { 2, "TC_3_04 CV.doc", "TC_3_04 photo.jpeg" },
            new object[] { 2, "TC_3_04 CV.docx", "TC_3_04 photo.jpg" },
            new object[] { 2, "TC_3_04 CV.pdf", "TC_3_04 photo.png" },
            new object[] { 2, "TC_3_04 CV.rtf", "TC_3_04 photo.tiff" },
            new object[] { 0, "TC_3_04 CV.txt", "TC_3_04 photo.ico" }
        };
       [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
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
            wait.Until((d) => StudentsContent.IsOpened(d));
            //Open modal window 'EditStudentListWindow'
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
            //Create new student for test
            mainPageInstance.ModalWindow.EditStudentListWindow.CreateStudentButton.Click();
            wait.Until((d) => EditStudentWindow.IsOpened(d));
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
            wait.Until((d) => EditStudentWindow.IsOpened(d));
        }
        [Test, TestCaseSource("fileNames")]
        public void ExecuteTest_UploadFiles_FilesUploaded(int expected, string CV, string photo)
        {
            //Upload CV
            path = EditStudentWindow.GetTestFile(CV);
            mainPageInstance.ModalWindow.EditStudentWindow.BrowseCVButton.Click();
            Acts.UploadFile(path);
            //Accep Allert if necessary
            if (Acts.IsAlertPresent(wait))
                webDriver.SwitchTo().Alert().Accept();
            //Upload photo
            path = EditStudentWindow.GetTestFile(photo);
            mainPageInstance.ModalWindow.EditStudentWindow.BrowsePhotoButton.Click();
            Acts.UploadFile(path);
            //Accep Allert if necessary
            if (Acts.IsAlertPresent(wait))
                webDriver.SwitchTo().Alert().Accept();
            //Save changes
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
            //Check if files saved
            mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetElementFromCell
                (mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count, EditStudentListWindow.EditButtonsColumn).Click();
            wait.Until((d) => EditStudentWindow.IsOpened(d));
            Assert.AreEqual(expected, mainPageInstance.ModalWindow.EditStudentWindow.CountUploadedFiles());
        }
        [TearDown]
        public void TearDownTest()
        {
            //Delete files
            try { mainPageInstance.ModalWindow.EditStudentWindow.RemoveCVButton.Click(); }
            catch { }

            try { mainPageInstance.ModalWindow.EditStudentWindow.RemovePhotoButton.Click(); }
            catch { }
           
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
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
