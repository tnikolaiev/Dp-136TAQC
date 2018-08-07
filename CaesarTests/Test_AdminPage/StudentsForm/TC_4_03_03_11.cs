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
    public class TC_4_03_03_11
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditStudentsForm studentForm;
        WebDriverWait wait;
        Table table;
        string index;
        List<string> expectedResult;

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

        [Test]
        public void Test_EditStudentFormIsDisplayed()
        {          
            expectedResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            expectedResult.RemoveAt(8);

            List<string> actualResult = studentForm.RememberStudent();
            studentForm.SubmitButton.Click();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Test_StudentFormGroupIdIsEdit()
        {
            studentForm.GroupIdField.Clear();
            studentForm.setGroupId("DP-136-TAQC")
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);

        }
        [Test]
        public void Test_StudentFormFirstNameIsEdit()
        {
            studentForm.NameField.Clear();
            studentForm.setName("Ivan")
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_StudentFormLastNameIsEdit()
        {
            studentForm.LastNameField.Clear();
            studentForm.setLastName("Ivanov")
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Test_StudentFormEnglishLevelIsEdit()
        {
            studentForm.setEnglishLevelDDL(5)
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Test_StudentFormCvUrllIsEdit()
        {
            studentForm.CvUrlField.Clear();
            studentForm.setCvUrl("someText")
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Test_StudentFormImageUrllIsEdit()
        {
            studentForm.ImageUrlField.Clear();
            studentForm.setImageUrl("someText")
                .SubmitButton.Click();
           expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Test_StudentFormEntryScorelIsEdit()
        {
            studentForm.EntryScoreField.Clear();
            studentForm.setEntryScore("130")
                .SubmitButton.Click();
            expectedResult = studentForm.RememberStudent();
            List<string> actualResult = table.getRowWithColumns(table.GetRowNumberByValueInCell(index, 2));
            actualResult.RemoveAt(8);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Test_StudentFormApprovedByIsEdit()
        {
            studentForm.ApprovedByField.Clear();
            studentForm.setApprovedBy("O. Reuta")
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