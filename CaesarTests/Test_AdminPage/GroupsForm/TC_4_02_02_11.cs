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
    public class TC_4_02_02_11
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditGroupsForm groupsForm;
        WebDriverWait wait;
        Table table;
        string index;
        static IEnumerable<object[]> CreateGroupsAdmin = Instruments.ReadXML("NewGroupsAdmin.xml",
            "testData", "location", "direction", "groupName", "teacher", "startDate", "finishDate", "expert");

        static object[] GroupsInfo =
       {
            new object[] { "groupNet", 0, 3, "02/13/2018", "05/13/2018", "K. Kozak", "Expert" },
            new object[] { "groupISTQB", 1, 11, "04/25/2018", "10/25/2018", "O. Reuta", "Expertik" },
            new object[] { "groupIOS", 2,  5, "07/30/2018", "11/30/2018", "D. Petin", "Expertok" },
            new object[] { "groupUX", 3, 13, "09/27/2017", "12/27/2017", "A. Koval", "Testwoman" },
            new object[] { "groupC/C++", 4, 6, "01/05/2011", "4/05/2011", "P. Kucher", "Testman" }
        };
        [OneTimeSetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("Dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            driver.Url = @"http://localhost:3000/admin";
            wait.Until((d) => CreateEditGroupsForm.IsAdminPageOpened(d));
            groupsForm = new CreateEditGroupsForm(driver);
        }

        [SetUp]
        public void AddUserClick()
        {            
            table = new Table(groupsForm.GetTable, driver);
            groupsForm.GoToGroups.Click();
            groupsForm.addGroups();
            groupsForm.IsOpened(wait);
        }
        
        [Test, TestCaseSource("CreateGroupsAdmin")]
        public void Test_CreateGroup_Success(int location, int direction, string groupName, string startDate, string finishDate, string teacher, string experts)
        {
            groupsForm.setName(groupName)
                .setLocationDDL(location)
                .setDirectionDDL(direction)
                .setStartDate(startDate)
                .setFinishDate(finishDate)
                .setTeachers(teacher)
                .setExperts(experts)
                .SubmitButton.Click();
            List<string> expectedResult = groupsForm.RememberGroup();
            index = groupsForm.NameField.GetAttribute("value");
            Assert.IsTrue(table.FindRowInTable(expectedResult));
        }

        [TearDown]
        public void Delete()
        {
            groupsForm.DeleteGroup(table.GetRowNumberByValueInCell(index, 0));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
