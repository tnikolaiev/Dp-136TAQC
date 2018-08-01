using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
namespace CaesarTests
{
    [TestFixture]
    public class TC_4_03_CreateStudent_Success
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditStudentsForm studentForm;
        WebDriverWait wait;
        Table table;
        List<string> index = new List<string>();

        static object[] StudentInfo =
        {
            new object[] { "DP-093-JS", "Olga", "Ivanova", 0, "25", "N. Varenko" },
            new object[] { "DP-093-JS", "Hanna", "Lavrova", 1, "36", "N. Varenko" },
            new object[] { "DP-093-JS", "Thor", "Thorov", 2,  "45", "N. Varenko" },
            new object[] { "DP-093-JS", "Halk", "Halkov", 3,  "50", "N. Varenko" },
            new object[] { "DP-093-JS", "Iron", "Ironon",  4, "30", "N. Varenko" },
            new object[] { "DP-093-JS", "Lady", "Ivanova", 5, "45", "N. Varenko" },
            new object[] { "DP-093-JS", "Sima", "Kotova", 6, "50", "N. Varenko" },
             new object[] { "DP-093-JS", "Thor", "Thorov", 7,  "45", "N. Varenko" },
            new object[] { "DP-093-JS", "Halk", "Halkov", 8,  "50", "N. Varenko" },
            new object[] { "DP-093-JS", "Iron", "Ironon",  9, "30", "N. Varenko" },
            new object[] { "DP-093-JS", "Lady", "Ivanova", 10, "45", "N. Varenko" }
        };

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("Dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            driver.Url = @"http://localhost:3000/admin";
            wait.Until((d) => CreateEditStudentsForm.IsAdminPageOpened(d));
            studentForm = new CreateEditStudentsForm(driver);
            studentForm.GoToStudents.Click();
            studentForm.addStudents();
            wait.Until((d) => CreateEditStudentsForm.IsCreateEditFormOpened(d));
        }

        [Test]
        public void Test_CreateStudentFormIsDisplayed()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            List<string> expectedResult = new List<string> { "", "", "", "Elementary", "", "", "", "" };
            List<string> actualResult = studentForm.RememberStudent();

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        [Test, TestCaseSource("StudentInfo")]
        public void Test_CreateStudent_Success(string groupId, string name, string lastName, int englishLevel, string entryScore, string approvedBy)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
            studentForm.setGroupId(groupId)
                .setName(name)
                .setLastName(lastName)
                .setEnglishLevelDDL(englishLevel)
                .setEntryScore(entryScore)
                .setApprovedBy(approvedBy);
            
            List<String> expectedResult = studentForm.RememberStudent();
            index.Add(studentForm.LastNameField.GetAttribute("value"));
            studentForm.SubmitButton.Click();
            table = new Table(studentForm.GetTable, driver);

            Assert.IsTrue(table.FindRowInTable(expectedResult));

            
        }
       
        [OneTimeTearDown]
        public void CleanUp()
        {
            wait.Until((d) => CreateEditUsersForm.IsAdminPageOpened(d));
            Thread.Sleep(1000);
            foreach (var i in index)
            {
                studentForm.DeleteStudent(table.GetRowNumberByValue(i));
            }
            driver.Close();
            driver.Quit();
        }
    }
}
