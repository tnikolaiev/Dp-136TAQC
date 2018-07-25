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
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        static object[] studentData =
        {
            new object[]{"Petrova Anna", "Anna", "Petrova", 2, "99", "4", 1 },
            new object[]{"Glinchikov Sergey", "Sergey", "Glinchikov", 8, "130", "5", 1 }
        };
        [OneTimeSetUp]
        public void OneTimeSetUp()
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
            wait.Until((d) => StudentsContent.IsStudentsContentOpened(d));
            //Open modal window 'EditStudentListWindow'
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));
        }
        [Test,TestCaseSource("studentData")]
        public void ExecuteTest_AddStudent_StudentAdded(string expected, string firstName, string lastName, 
            int englishLevelIndex, string incomingTest, string entryScore, int approvedByIndex)
        {
            mainPageInstance.ModalWindow.EditStudentListWindow.CreateStudentButton.Click();
            wait.Until((d) => EditStudentWindow.IsEditStudentWindowOpened(d));
            mainPageInstance.ModalWindow.EditStudentWindow.FillForm(firstName, lastName, englishLevelIndex, incomingTest, entryScore, approvedByIndex);
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            wait.Until((d) => EditStudentListWindow.IsEditStudentListWindowOpened(d));

            Assert.AreEqual(expected, mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetValueFromCell
                (mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count, "Name"));
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            for (int i = 0; i < studentData.Length; i++)
            {
                mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetElementFromCell
                (mainPageInstance.ModalWindow.EditStudentListWindow.Students.Count, EditStudentListWindow.DeleteButtonsColumn).Click();
                Acts.PressKeyboardButton(@"{Enter}");
            }
            webDriver.Quit();
            webDriver.Close();
        }
    }
}
