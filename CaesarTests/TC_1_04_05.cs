using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_04_05
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver;
        WebDriverWait wait;
        Actions action;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            action = new Actions(driver);
        }

        [Test]
        public void ExecuteTest_SignInAsCoordinator_OpenGroupCreateWindow_LocationDDlnotEnabled()
        {
            loginPageInstance.LogIn("dmytro", "1234", wait);
            mainPageInstance = new MainPage(driver);
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            bool LocationDdlEnabled = groupCreateWindow.LocationDDL.Enabled;
            
            Assert.IsTrue(groupCreateWindow.IsOpened() & !LocationDdlEnabled);
        }

        static IEnumerable<object[]> NewGroupsCreationData = Instruments.ReadXML("NewGroupsCreationData.xml",
            "testData", "direction", "groupName", "teacher", "budgetOwner", "startDate", "expert");

        [Test, TestCaseSource("NewGroupsCreationData")]
        public void ExecuteTest_NewGroupCreate_GroupAppearedInGroupsList
            (String direction, String groupName, String teacher, String budgetOwner, String startDate, String expert)
        {
            loginPageInstance.LogIn("dmytro", "1234", wait);
            mainPageInstance = new MainPage(driver);
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            var groupsList = mainPageInstance.LeftContainer.GroupsInLocation;

            groupCreateWindow.Open(action, wait);
            groupCreateWindow.SetGroupName(groupName)
                .SetDirection(direction)
                .ReturnNameButtonClick()
                .AddTeacher(teacher)
                .SetBudgetOwner(budgetOwner)
                .AddExpert(expert)
                .SetStartDate(startDate);
            groupCreateWindow.SaveGroupButton.Click();
            bool isGroupCreated = groupsList.GetGroupByName(groupName, wait) != null;
            groupsList.DeleteGroup(groupName, action, wait);
            bool isGroupDeleted = groupsList.GetGroupByName(groupName, wait) == null;
            
            Assert.IsTrue(isGroupCreated & isGroupDeleted);
        }

        [Test]
        public void ExecuteTest_SignInAsAdministrator_OpenGroupCreateWindow_LocationDDlEnabled()
        {
            loginPageInstance.LogIn("admin", "1234", wait);
            mainPageInstance = new MainPage(driver);
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            bool LocationDdlEnabled = groupCreateWindow.LocationDDL.Enabled;
            
            Assert.IsTrue(groupCreateWindow.IsOpened() & LocationDdlEnabled);
        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }

        [OneTimeTearDown]
        public void FinalCleanUp()
        {
            driver.Quit();
        }
    }
}
