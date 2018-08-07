using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
namespace CaesarTests
{
    [TestFixture]
    public class TC_4_02_03_00
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditGroupsForm groupsForm;
        WebDriverWait wait;
        List<string> expectedResult;
        Table table;
        string index;

        static object[] GroupsText =
      {
            new object[] { "123456", "02/13/2018", "05/13/2018", "K. Kozak", "Expert" },
            new object[] { "InputFirstNameCharacter", "04/25/2018", "10/25/2018", "O. Reuta", "Expertik" },
            new object[] { "!@#$%$^#@$",  "07/30/2018", "11/30/2018", "D. Petin", "Expertok" },
            new object[] { "Фьмвл12%к",  "09/27/2017", "12/27/2017", "A. Koval", "Testwoman" }
        };
        static object[] GroupsDate =
      {
            new object[] { "groupNet",  "00/00/0000", "00/00/0000", "K. Kozak", "Expert" },
            new object[] { "groupISTQB",  "30/25/2018", "10/25/2018", "O. Reuta", "Expertik" },
            new object[] { "groupIOS",  "01/01/1900", "11/30/2018", "D. Petin", "Expertok" },
            new object[] { "groupUX", "09/27/2017", "12/27/2016", "A. Koval", "Testwoman" }
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
        [Test, TestCaseSource("GroupsText")]
        public void Test_GroupFormNameFieldIsNotEdit(string groupName, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.NameField.Clear();
            groupsForm.setName(groupName)
                .SubmitButton.Click();
            List<string> expectedResult = groupsForm.RememberGroup();
            index = groupsForm.NameField.GetAttribute("value");
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

      
        [Test, TestCaseSource("GroupsDate")]
        public void Test_GroupFormStartDateFieldIsNotEdit(string groupName, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.setStartDate(startDate)
                .SubmitButton.Click();
            List<string> expectedResult = groupsForm.RememberGroup();
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("GroupsDate")]
        public void Test_GroupFormFinishDateFieldIsNotEdit(string groupName, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.setFinishDate(finishDate)
                .SubmitButton.Click();
            List<string> expectedResult = groupsForm.RememberGroup();
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }

        [Test, TestCaseSource("GroupsText")]
        public void Test_GroupFormExpertsFieldIsNotEdit(string experts, string startDate, string finishDate, string teacher, string groupName)
        {
            groupsForm.ExpertsField.Clear();
            groupsForm.setExperts(experts)
                .SubmitButton.Click();
            List<string> expectedResult = groupsForm.RememberGroup();
            Assert.IsFalse(table.FindRowInTable(expectedResult));
        }
       
        [OneTimeTearDown]
        public void CleanUp()
        {
            if (table.FindRowInTable(expectedResult))
            {
                groupsForm.DeleteGroup(table.GetRowNumberByValueInCell(index, 0));
            }
            driver.Close();
            driver.Quit();
        }
    }
}
