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
    public class TC_4_03_03_00cs
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditStudentsForm studentForm;
        WebDriverWait wait;
        Table table;
        string index;
        List<string> expectedResult;

        static object[] StudentInfo =
        {
           new object[] { "123456", "123456", "123456", 0, "123456", "123456" },
            new object[] { "InputFirstNameCharacter", "InputFirstNameCharacter", "InputFirstNameCharacter", 1, "InputFirstNameCharacter", "InputFirstNameCharacter" },
            new object[] { "!@#$%$^#@$", "!@#$%$^#@$", "!@#$%$^#@$", 2, "!@#$%$^#@$", "!@#$%$^#@$" },
            new object[] { "Фьмвл12%к", "Фьмвл12%к", "Фьмвл12%к", 3, "Фьмвл12%к", "Фьмвл12%к" }
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
            studentForm = new CreateEditStudentsForm(driver);
            studentForm.GoToStudents.Click();
            studentForm.addStudents();
            studentForm.IsOpened(wait);
            studentForm.setGroupId("DP-093-JS")
                .setName("Olga")
                .setLastName("Ivanova")
                .setEnglishLevelDDL(0)
                .setEntryScore("25")
                .setApprovedBy("N. Varenko")
                .SubmitButton.Click();
            index = studentForm.LastNameField.GetAttribute("value");

        }

        [SetUp]
        public void OpenStudentForm()
        {
            studentForm = new CreateEditStudentsForm(driver);
            table = new Table(studentForm.GetTable, driver);
            studentForm.EditStudent(table.GetRowNumberByValueInCell(index, 2));
            studentForm.IsOpened(wait);
        }
               
        [Test, TestCaseSource("StudentInfo")]
        public void Test_StudentFormGroupIdIsNotEdit(string groupId, string name, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.GroupIdField.Clear();
            studentForm.setGroupId(groupId)
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);

        }
        [Test, TestCaseSource("StudentInfo")]
        public void Test_StudentFormFirstNameIsNotEdit(string groupId, string name, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.NameField.Clear();
            studentForm.setName(name)
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test, TestCaseSource("StudentInfo")]
        public void Test_StudentFormLastNameIsNotEdit(string groupId, string name, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.LastNameField.Clear();
            studentForm.setLastName(lastName)
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test, TestCaseSource("StudentInfo")]
        public void Test_StudentFormEnglishLevelIsNotEdit(string groupId, string name, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.setEnglishLevelDDL(englishLevel)
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test, TestCaseSource("StudentInfo")]
        public void Test_StudentFormEntryScorelIsNotEdit(string groupId, string name, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.EntryScoreField.Clear();
            studentForm.setEntryScore(entryScore)
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test, TestCaseSource("StudentInfo")]
        public void Test_StudentFormApprovedByIsNotEdit(string groupId, string name, string lastName, int englishLevel, string entryScore, string approved)
        {
            studentForm.ApprovedByField.Clear();
            studentForm.setApprovedBy(approved)
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            if (table.FindRowInTable(expectedResult))
            {
                studentForm.DeleteStudent(table.GetRowNumberByValueInCell(index, 2));
            }
            driver.Close();
            driver.Quit();
        }
    }
}
