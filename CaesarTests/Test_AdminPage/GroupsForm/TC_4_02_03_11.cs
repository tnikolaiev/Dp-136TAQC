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
    public class TC_4_02_03_11
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditGroupsForm groupsForm;
        WebDriverWait wait;
        Table table;
        string index;
        List<string> expectedResult;

        static object[] GroupsInfo =
       {
            new object[] { "groupISTQB", 1, true, 11, "04/25/2018", "10/25/2018", "O. Reuta", "Expertik" },
            new object[] { "groupIOS", 2, false, 5, "07/30/2018", "11/30/2018", "D. Petin", "Expertok" },
            new object[] { "groupUX", 3, true, 13, "09/27/2017", "12/27/2017", "A. Koval", "Testwoman" },
            new object[] { "groupC/C++", 4, false, 6, "01/05/2011", "4/05/2011", "P. Kucher", "Testman" }
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
            wait.Until((d) => CreateEditGroupsForm.IsAdminPageOpened(d));
            groupsForm = new CreateEditGroupsForm(driver);
            groupsForm.GoToGroups.Click();
            groupsForm.addGroups();
            groupsForm.IsOpened(wait);
            groupsForm.setName("groupNet")
                .setLocationDDL(0)
                .setDirectionDDL(3)
                .setStartDate("02/13/2018")
                .setFinishDate("05/13/2018")
                .setTeachers("K. Kozak")
                .setExperts("Expert")
                .SubmitButton.Click();
            index = groupsForm.NameField.GetAttribute("value");
        }

        [SetUp]
        public void EditGroupClick()
        {
            groupsForm = new CreateEditGroupsForm(driver);
            table = new Table(groupsForm.GetTable);
            groupsForm.EditGroup(table.GetRowNumberByValueInCell(index, 0));
            groupsForm.IsOpened(wait);
        }

        [Test, TestCaseSource("GroupsInfo")]
        public void Test_GroupFormNameFieldIsEdit(string groupName, int location, bool budgetOwner, int direction, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.NameField.Clear();
            groupsForm.setName(groupName);
            index = groupsForm.NameField.GetAttribute("value");
            groupsForm.SubmitButton.Click();
            expectedResult = groupsForm.RememberGroup();            
            Assert.IsTrue(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("GroupsInfo")]
        public void Test_GroupFormLocationDDLFieldIsEdit(string groupName, int location, bool budgetOwner, int direction, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.setLocationDDL(location)
                .SubmitButton.Click();
            expectedResult = groupsForm.RememberGroup();
            Assert.IsTrue(table.FindRowInTable(expectedResult));
        }
        [Test, TestCaseSource("GroupsInfo")]
        public void Test_GroupFormDirectionDDLFieldIsEdit(string groupName, int location, bool budgetOwner, int direction, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.setDirectionDDL(direction)
                .SubmitButton.Click();
            expectedResult = groupsForm.RememberGroup();
            Assert.IsTrue(table.FindRowInTable(expectedResult));
        }
        [Test, TestCaseSource("GroupsInfo")]
        public void Test_GroupFormStartDateFieldIsEdit(string groupName, int location, bool budgetOwner, int direction, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.setStartDate(startDate)
                .SubmitButton.Click();
            expectedResult = groupsForm.RememberGroup();
            Assert.IsTrue(table.FindRowInTable(expectedResult));
        }
       
        [Test, TestCaseSource("GroupsInfo")]
        public void Test_GroupFormFinishDateFieldIsEdit(string groupName, int location, bool budgetOwner, int direction, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.setFinishDate(finishDate)
                .SubmitButton.Click();
            expectedResult = groupsForm.RememberGroup();            
            Assert.IsTrue(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("GroupsInfo")]
        public void Test_GroupFormExpertsFieldIsEdit(string groupName, int location, bool budgetOwner, int direction, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.ExpertsField.Clear();
            groupsForm.setExperts(experts)
                .SubmitButton.Click();
            expectedResult = groupsForm.RememberGroup();
            Assert.IsTrue(table.FindRowInTable(expectedResult));
        } 
         [Test, TestCaseSource("GroupsInfo")]
         public void Test_GroupFormBudgetOwnerCheck(string groupName, int location, bool budgetOwner, int direction, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.setBudgetOwner(budgetOwner);                   
            bool expectedResult = groupsForm.BudgetOwnerCheckbox.Selected;
            groupsForm.SubmitButton.Click();
            bool actualResult = (table.GetRowsWithColumns()[table.GetRowNumberByValueInCell(index, 0)-1][2].GetAttribute("checked")) != null;
            Assert.AreEqual(expectedResult, actualResult);
        }
       
        [OneTimeTearDown]
        public void CleanUp()
        {
            groupsForm.DeleteGroup(table.GetRowNumberByValueInCell(index, 0));
            driver.Close();
            driver.Quit();
        }
    }
}
