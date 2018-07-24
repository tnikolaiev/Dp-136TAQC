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
        StudentsContent studentsContentInstance;
        EditStudentListWindow editStudentListWindowInstance;
        String path;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Url = baseURL;

            loginPageInstance = new LoginPage(webDriver);
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));

            webDriver.Url = baseURL + "/Students/Lviv/Lv-023-UX/list";

            studentsContentInstance = new StudentsContent(webDriver);
            wait.Until((d) => StudentsContent.IsStudentsContentOpened(d));

            Acts.Click(studentsContentInstance.EditButton);

            editStudentListWindowInstance = new EditStudentListWindow(webDriver);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
        }
        [Test]
        public void ExecuteTest_ImportStudentList_ListImpoerted()
        {
            Acts.Click(editStudentListWindowInstance.ImportStudentsButton);
            path = EditStudentListWindow.GetTestFile("TC_3_06_01 ValidStudentList.txt");
            Acts.UploadFile(path);
            Acts.Click(editStudentListWindowInstance.SaveFormButton);
            Assert.AreEqual(4, editStudentListWindowInstance.Students.Count);
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            while (editStudentListWindowInstance.DeleteButtons.Count != 0)
            {
                Acts.Click(editStudentListWindowInstance.GetLastElement(editStudentListWindowInstance.DeleteButtons));
                Acts.PressKeyboardButton(@"{Enter}");
            }
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
