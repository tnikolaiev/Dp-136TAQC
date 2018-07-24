using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_3_04_03
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        StudentsContent studentsContentInstance;
        EditStudentListWindow EditStudentListWindowInstance;
        EditStudentWindow editStudentInstance;
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
            studentsContentInstance = new StudentsContent(webDriver);
            wait.Until((d) => StudentsContent.IsStudentsContentOpened(d));

            Acts.Click(studentsContentInstance.EditButton);

            EditStudentListWindowInstance = new EditStudentListWindow(webDriver);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

            Acts.Click(EditStudentListWindowInstance.CreateStudentButton);

            editStudentInstance = new EditStudentWindow(webDriver);
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));

            editStudentInstance.FillForm("Denis", "Petrov", 0, "120", "5", 0);
            Acts.Click(editStudentInstance.SaveButton);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
        }
        [SetUp]
        public void SetUpTest()
        {
            Acts.Click(EditStudentListWindowInstance.GetLastElement(EditStudentListWindowInstance.EditButtons));
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));
        }
        [Test]
        public void ExecuteTest_UploadInvalidFiles_FilesNotUploaded()
        {
            Acts.Click(editStudentInstance.BrowseCVButton);
            path = EditStudentWindow.GetTestFile("TC_3_04 CV.txt");
            Acts.UploadFile(path);
            wait.Until(ExpectedConditions.AlertIsPresent());
            webDriver.SwitchTo().Alert().Accept();

            Acts.Click(editStudentInstance.BrowsePhotoButton);
            path = EditStudentWindow.GetTestFile("TC_3_04 photo.ico");
            Acts.UploadFile(path);
            wait.Until(ExpectedConditions.AlertIsPresent());
            webDriver.SwitchTo().Alert().Accept();
            Assert.AreEqual(0, editStudentInstance.CountUploadedFiles());
        }
        [TearDown]
        public void TearDownTest()
        {
            Acts.Click(editStudentInstance.SaveButton);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            Acts.Click(EditStudentListWindowInstance.GetLastElement(EditStudentListWindowInstance.DeleteButtons));
            Acts.PressKeyboardButton(@"{Enter}");
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
