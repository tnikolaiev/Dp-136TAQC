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
    public class TC_4_03_02_00
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditStudentsForm studentForm;
        WebDriverWait wait;
        Table table;
        string index;
        List<string> expectedResult;

        static object[] StudentText =
        {
            new object[] { "123456", "Olga", "Ivanova", 0, "25", "N. Varenko" },
            new object[] { "InputFirstNameCharacter", "Hanna", "Lavrova", 1, "36", "N. Varenko" },
            new object[] { "!@#$%$^#@$", "Thor", "Thorov", 2,  "45", "N. Varenko" },
            new object[] { "Фьмвл12%к", "Halk", "Halkov", 3,  "50", "N. Varenko" }
        };

        static object[] StudentInfo =
       {
            new object[] { "DP-093-JS", "Sima", "Kotova", 6, "9999999999999999999999999999999999", "N. Varenko" },
            new object[] { "DP-093-JS", "Thor", "Thorov", 7,  "-100", "N. Varenko" },
            new object[] { "DP-093-JS", "Halk", "Halkov", 8,  "0", "N. Varenko" },
            new object[] { "DP-093-JS", "Iron", "Ironon",  9, "0.00000000001", "N. Varenko" },
            new object[] { "DP-093-JS", "Lady", "Ivanova", 10, "6", "N. Varenko" }

        };

        [OneTimeSetUp]
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

        }
        [SetUp]
        public void OpenStudentForm()
        {
            studentForm = new CreateEditStudentsForm(driver);
            studentForm.GoToStudents.Click();
            table = new Table(studentForm.GetTable, driver);
            studentForm.addStudents();
            studentForm.IsOpened(wait);
        }

       
        [Test, TestCaseSource("StudentText")]
        public void Test_StudentFormGroupIdIsNotEdit(string groupId, string name, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.setGroupId(groupId)
                .setName(name)
                .setLastName(lastName)
                .setEnglishLevelDDL(englishLevel)
                .setEntryScore(entryScore)
                .setApprovedBy(approved)
                .SubmitButton.Click();
            index = studentForm.LastNameField.GetAttribute("value");
            expectedResult = studentForm.RememberStudent();
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }
      
        [Test, TestCaseSource("StudentText")]
        public void Test_StudentFormFirstNameIsNotEdit(string name, string groupId, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.setGroupId(groupId)
               .setName(name)
               .setLastName(lastName)
               .setEnglishLevelDDL(englishLevel)
               .setEntryScore(entryScore)
               .setApprovedBy(approved)
               .SubmitButton.Click();
            index = studentForm.LastNameField.GetAttribute("value");
            expectedResult = studentForm.RememberStudent();
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("StudentText")]
        public void Test_StudentFormLastNameIsNotEdit(string lastName, string name, string groupId, int englishLevel, string entryScore, string approved)
        {
            studentForm.setGroupId(groupId)
               .setName(name)
               .setLastName(lastName)
               .setEnglishLevelDDL(englishLevel)
               .setEntryScore(entryScore)
               .setApprovedBy(approved)
               .SubmitButton.Click();
            index = studentForm.LastNameField.GetAttribute("value");
            expectedResult = studentForm.RememberStudent();
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }
      
        [Test, TestCaseSource("StudentInfo")]
        public void Test_StudentFormEntryScorelIsNotEdit(string groupId, string name, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.setGroupId(groupId)
                 .setName(name)
                 .setLastName(lastName)
                 .setEnglishLevelDDL(englishLevel)
                 .setEntryScore(entryScore)
                 .setApprovedBy(approved)
                 .SubmitButton.Click();
            index = studentForm.LastNameField.GetAttribute("value");
            expectedResult = studentForm.RememberStudent();
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }
        [Test, TestCaseSource("StudentText")]
        public void Test_StudentFormApprovedByIsNotEdit(string approved, string name, string lastName, int englishLevel, string entryScore, string groupId)
        {
            studentForm.setGroupId(groupId)
                  .setName(name)
                  .setLastName(lastName)
                  .setEnglishLevelDDL(englishLevel)
                  .setEntryScore(entryScore)
                  .setApprovedBy(approved)
                  .SubmitButton.Click();
            index = studentForm.LastNameField.GetAttribute("value");
            expectedResult = studentForm.RememberStudent();
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [TearDown]
        public void Delete()
        {
            if (table.FindRowInTable(expectedResult))
            {
                studentForm.DeleteStudent(table.GetRowNumberByValueInCell(index, 2));
            }
        }
        [OneTimeTearDown]
        public void CleanUp()
        {

            driver.Close();
            driver.Quit();
        }
    }
}