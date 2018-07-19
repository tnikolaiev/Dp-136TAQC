using CaesarLib;
using CaesarLib.StudentsPage;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    class TC_3_04_02
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        GroupView groupViewInstance;
        EditStudentList editStudentListInstance;
        EditStudent editStudentInstance;
        String path;
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
            groupViewInstance = new GroupView(webDriver);
            wait.Until((d) => GroupView.IsGroupView(d));

            Acts.Click(groupViewInstance.EditButton);

            editStudentListInstance = new EditStudentList(webDriver);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));

            Acts.Click(editStudentListInstance.CreateStudentButton);
            editStudentInstance = new EditStudent(webDriver);
            wait.Until((d) => EditStudent.IsEditStudent(d));

            editStudentInstance.FillForm("Denis", "Petrov", 0, "120", "5", 0);
            Acts.Click(editStudentInstance.SaveButton);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));

        }
        [SetUp]
        public void SetUpTest()
        {
            Acts.Click(editStudentListInstance.GetLastElement(editStudentListInstance.EditButtons));
            wait.Until((d) => EditStudent.IsEditStudent(d));

            path = EditStudent.GetTestFile("TC_3_04 CV.docx");
            Acts.Click(editStudentInstance.BrowseCVButton);
            Acts.UploadFile(path);

            path = EditStudent.GetTestFile("TC_3_04 photo.png");
            Acts.Click(editStudentInstance.BrowsePhotoButton);
            Acts.UploadFile(path);

            Acts.Click(editStudentInstance.SaveButton);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));

            Acts.Click(editStudentListInstance.GetLastElement(editStudentListInstance.EditButtons));
            wait.Until((d) => EditStudent.IsEditStudent(d));
        }
        [Test]
        public void ExecuteTest_DeleteFiles_FilesDeleted()
        {
            Acts.Click(editStudentInstance.RemoveCVButton);
            Acts.Click(editStudentInstance.RemovePhotoButton);

            Acts.Click(editStudentInstance.SaveButton);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));

            Acts.Click(editStudentListInstance.GetLastElement(editStudentListInstance.EditButtons));
            wait.Until((d) => EditStudent.IsEditStudent(d));

            Assert.AreEqual(0, editStudentInstance.CountUploadedFiles());
        }
        [TearDown]
        public void TearDownTest()
        {
            Acts.Click(editStudentInstance.SaveButton);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            Acts.Click(editStudentListInstance.GetLastElement(editStudentListInstance.DeleteButtons));
            Acts.PressKeyboardButton(@"{Enter}");
            wait.Until((d) => EditStudentList.IsEditStudentList(d));
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
