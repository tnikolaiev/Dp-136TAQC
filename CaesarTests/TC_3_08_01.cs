using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    public class TC_3_08_01
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        static object[] studentData =
        {
            new object[]{"S3RG3Y", "Sinotov", 5, "100", "5", 1 },
            new object[]{"Ma\r!n'a", "Glinchikova", 1, "112", "4", 1 },
            new object[]{"", "Sinotov", 5, "100", "5", 1 },
            new object[]{"Sergey", "S1n0t0v", 5, "100", "5", 1 },
            new object[]{"Marina", "Tin&ova", 1, "112", "4", 1 },
            new object[]{"Vlad", "", 5, "100", "5", 1 },
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
            wait.Until((d) => StudentsContent.IsOpened(d));
            //Open modal window 'EditStudentListWindow'
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            wait.Until((d) => EditStudentListWindow.IsOpened(d));
            mainPageInstance.ModalWindow.EditStudentListWindow.CreateStudentButton.Click();
            wait.Until((d) => EditStudentWindow.IsOpened(d));
        }
        [Test, TestCaseSource("studentData")]
        public void ExecuteTest_AddStudent_StudentAdded(string firstName, string lastName,
            int englishLevelIndex, string incomingTest, string entryScore, int approvedByIndex)
        {
            mainPageInstance.ModalWindow.EditStudentWindow.setFirstName(firstName)
                 .setLastName(lastName)
                 .setEnglishLevel(englishLevelIndex)
                 .setIncomingTest(incomingTest)
                 .setEntryScore(entryScore);
            mainPageInstance.ModalWindow.EditStudentWindow.SaveButton.Click();
            Assert.IsTrue(mainPageInstance.ModalWindow.EditStudentWindow.IsHintVisible());
        }
        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            webDriver.Quit();
            webDriver.Close();
        }
    }
}
