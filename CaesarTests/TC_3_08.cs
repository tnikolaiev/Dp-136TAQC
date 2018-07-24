using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    public class TC_3_08
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage logingPageInstance;
        StudentsContent studentsContentInstance;
        EditStudentListWindow editStudentListWindowInstance;
        EditStudentWindow editStudentWindowInstance;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Url = baseURL;

            logingPageInstance = new LoginPage(webDriver);
            logingPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));

            webDriver.Url = baseURL + "/Students/Dnipro/DP-093-JS/list";
            studentsContentInstance = new StudentsContent(webDriver);
            wait.Until((d) => StudentsContent.IsStudentsContentOpened(d));

            Acts.Click(studentsContentInstance.EditButton);

            editStudentListWindowInstance = new EditStudentListWindow(webDriver);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

            editStudentWindowInstance = new EditStudentWindow(webDriver);
        }
        [Test]
        public void ExecuteTest_AddStudent_1_StudentAdded()
        {
            Acts.Click(editStudentListWindowInstance.CreateStudentButton);
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));

            editStudentWindowInstance.FillForm("Anna", "Petrova", 2, "99", "4", 1);

            Acts.Click(editStudentWindowInstance.SaveButton);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

            Assert.AreEqual("Petrova Anna", EditStudentListWindow.GetStudentName(editStudentListWindowInstance.GetLastElement(editStudentListWindowInstance.Students)));
        }
        [Test]
        public void ExecuteTest_AddStudent_2_StudentAddedt()
        {
            Acts.Click(editStudentListWindowInstance.CreateStudentButton);
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));

            editStudentWindowInstance.FillForm("Sergey", "Glinchikov", 8, "130", "5", 1);

            Acts.Click(editStudentWindowInstance.SaveButton);
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

            Assert.AreEqual("Glinchikov Sergey", EditStudentListWindow.GetStudentName(editStudentListWindowInstance.GetLastElement(editStudentListWindowInstance.Students)));
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            for (int i = 0; i < 2; i++)
            {
                Acts.Click(editStudentListWindowInstance.GetLastElement(editStudentListWindowInstance.DeleteButtons));
                Acts.PressKeyboardButton(@"{Enter}");
            }
            webDriver.Quit();
            webDriver.Close();
        }
    }
}
