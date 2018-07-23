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
        StudentsContent studentsContentInstance;
        EditStudentListWindow EditStudentWindowListInstance;
        EditStudentWindow EditStudentWindowInstance;
        String path;
        static object[] testData =
        {
            new object[] { 2, "TC_3_04 CV.doc", "TC_3_04 photo.jpeg" },
            new object[] { 2, "TC_3_04 CV.docx", "TC_3_04 photo.jpg" },
            new object[] { 2, "TC_3_04 CV.pdf", "TC_3_04 photo.png" },
            new object[] { 2, "TC_3_04 CV.rtf", "TC_3_04 photo.tiff" } 
        };
       [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Url = baseURL;

            loginPageInstance = new LoginPage(webDriver);
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));

            webDriver.Url = baseURL + "/Students/Dnipro/DP-093-JS/list";
            studentsContentInstance = new StudentsContent(webDriver);
            wait.Until((d) => StudentsContent.IsStudentsContentOpened(d));

            Acts.Click(studentsContentInstance.EditButton);

            EditStudentWindowListInstance = new EditStudentListWindow(webDriver);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

            Acts.Click(EditStudentWindowListInstance.CreateStudentButton);

            EditStudentWindowInstance = new EditStudentWindow(webDriver);
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));

            EditStudentWindowInstance.FillForm("Denis", "Petrov", 0, "120", "5", 0);
            Acts.Click(EditStudentWindowInstance.SaveButton);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
        }
        [SetUp]
        public void SetUpTest()
        {
            Acts.Click(EditStudentWindowListInstance.GetLastElement(EditStudentWindowListInstance.EditButtons));
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));
        }
        [Test, TestCaseSource("testData")]
        public void ExecuteTest_UploadFiles_FilesUploaded(int expected, string CV, string photo)
        {
            path = EditStudentWindow.GetTestFile(CV);
            Acts.Click(EditStudentWindowInstance.BrowseCVButton);
            Acts.UploadFile(path);

            path = EditStudentWindow.GetTestFile(photo);
            Acts.Click(EditStudentWindowInstance.BrowsePhotoButton);
            Acts.UploadFile(path);

            Acts.Click(EditStudentWindowInstance.SaveButton);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

            Acts.Click(EditStudentWindowListInstance.GetLastElement(EditStudentWindowListInstance.EditButtons));
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));

            Assert.AreEqual(expected, EditStudentWindowInstance.CountUploadedFiles());
        }
        [TearDown]
        public void TearDownTest()
        {
            Acts.Click(EditStudentWindowInstance.RemoveCVButton);
            Acts.Click(EditStudentWindowInstance.RemovePhotoButton);
            Acts.Click(EditStudentWindowInstance.SaveButton);
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            Acts.Click(EditStudentWindowListInstance.GetLastElement(EditStudentWindowListInstance.DeleteButtons));
            Acts.PressKeyboardButton(@"{Enter}");
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
