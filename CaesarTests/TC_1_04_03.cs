using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_04_03
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;
        Actions action;


        [SetUp]
        public void Initialize()
        {
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            driver.Url = @"http://localhost:3000/logout";
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
            action = new Actions(driver);

        }

        [Test]
        public void ExecuteTest_SignInAsCoordinator_OpenGroupCreateWindow_DirectionDDlnotEnabled()
        {
            loginPageInstance.LogIn("dmytro", "1234");

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(action);
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            mainPageInstance.LeftMenu.CreateButton.Click();
            bool groupCreateWindowOpened = wait.Until((d) => mainPageInstance.ModalWindow.GroupCreateWindow.IsOpened());

            bool LocationDdlEnabled = mainPageInstance.ModalWindow.GroupCreateWindow.LocationDDL.Enabled;
            Assert.IsTrue(groupCreateWindowOpened & !LocationDdlEnabled);
        }

        [Test]
        public void ExecuteTest_OpenGroupCreateWindow_PressCancelButton_GroupCreateWindowClosed()
        {
            loginPageInstance.LogIn("dmytro", "1234");

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(action);
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            mainPageInstance.LeftMenu.CreateButton.Click();
            wait.Until((d) => mainPageInstance.ModalWindow.GroupCreateWindow.IsStartDateFieldClickable());

            mainPageInstance.ModalWindow.GroupCreateWindow.CancelGroupAddingButton.Click();
            wait.Until((d) => !mainPageInstance.ModalWindow.GroupCreateWindow.IsOpened());
            Assert.IsFalse(mainPageInstance.ModalWindow.GroupCreateWindow.IsOpened());
        }

        static IEnumerable<object[]> StartFinishDateData = Instruments.ReadXML("StartFinishDateData.xml", "testData", "direction", "startDate", "finishDate");

        [Test, TestCaseSource("StartFinishDateData")]
        public void ExecuteTest_EnterStartDate_FinishDateFilled(String direction, String startDate, String finishDate)
        {
            loginPageInstance.LogIn("dmytro", "1234");

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(action);
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            mainPageInstance.LeftMenu.CreateButton.Click();
            wait.Until((d) => mainPageInstance.ModalWindow.GroupCreateWindow.IsStartDateFieldClickable());

            Acts.SelectOptionFromDDL(mainPageInstance.ModalWindow.GroupCreateWindow.DirectionDDL, direction);
            Acts.InputValue(mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField, startDate);
            mainPageInstance.ModalWindow.GroupCreateWindow.GroupNameField.Click();

            String actualResult = mainPageInstance.ModalWindow.GroupCreateWindow.FinishDateField.GetAttribute("value");
            Assert.AreEqual(finishDate, actualResult);
        }

        static IEnumerable<object[]> NewGroupsCreationData = Instruments.ReadXML("NewGroupsCreationData.xml",
            "testData", "direction", "groupName", "teacher", "budgetOwner", "startDate", "expert");

        [Test, TestCaseSource("NewGroupsCreationData")]
        public void ExecuteTest_NewGroupCreate_GroupAppearedInGroupsList
            (String direction, String groupName, String teacher, String budgetOwner, String startDate, String expert)
        {
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            var groupsList = mainPageInstance.LeftContainer.GroupsInLocation;

            /*--------Create and Delete group-------*/
            //mainPageInstance.LeftMenu.Open(action);
            //wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());
            //var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            //var groupsList = mainPageInstance.LeftContainer.GroupsInLocation;
            //var groupDeleteConfirmationWindow = mainPageInstance.ModalWindow.GroupDeleteConfirmationWindow;
            //mainPageInstance.LeftMenu.CreateButton.Click();
            //wait.Until(groupCreateWindow.IsStartDateFieldClickable());
            //Acts.SelectOptionFromDDL(groupCreateWindow.DirectionDDL, direction);
            //groupCreateWindow.GroupNameField.Clear();
            //groupCreateWindow.GroupNameField.SendKeys(groupName);
            //groupCreateWindow.AddTeacherButton.Click();
            //Acts.SelectOptionFromDDL(groupCreateWindow.TeacherDDL, teacher);
            //groupCreateWindow.AcceptSelectTeacherButton.Click();
            //if (budgetOwner.Equals("SoftServe")) groupCreateWindow.BudgetOwnerSoftServeToggle.Click();
            //else if (budgetOwner.Equals("OpenGroup")) groupCreateWindow.BudgetOwnerOpenGroupToggle.Click();
            //groupCreateWindow.AddExpertButton.Click();
            //groupCreateWindow.ExpertInputField.SendKeys(expert);
            //groupCreateWindow.AcceptInputExpertButton.Click();
            //groupCreateWindow.StartDateField.SendKeys(startDate);
            //groupCreateWindow.SaveGroupButton.Click();
            //wait.Until((d) => !groupCreateWindow.IsOpened());
            //wait.Until(groupsList.AreGroupsVisible());
            //Assert.NotNull(groupsList.GetGroupByName(groupName));
            //wait.Until(groupsList.AreGroupsVisible());
            //groupsList.EndedGroupsToggle.Click();
            //groupsList.GetGroupByName("groupNet1").Click();
            //mainPageInstance.LeftMenu.Open(action);
            //wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());
            //mainPageInstance.LeftMenu.DeleteButton.Click();
            //wait.Until(groupDeleteConfirmationWindow.IsCancelButtonClickable());
            //groupDeleteConfirmationWindow.DeleteButton.Click();
            /*-----wtf-----*/

            groupCreateWindow.Open(action, wait);
            groupCreateWindow.SetGroupName(groupName)
                .SetDirection(direction)
                .ReturnNameButtonClick()
                .AddTeacher(teacher)
                .SetBudgetOwner(budgetOwner)
                .AddExpert(expert)
                .SetStartDate(startDate);
            groupCreateWindow.SaveGroupButton.Click();
            Assert.DoesNotThrow(() => groupsList.GetGroupByName(groupName, wait));
            //groupsList.DeleteGroup(groupName, action, wait);
        }

        static List<String> GroupsToDelete = new List<String> { "Dp-115 .NetgroupNet1", "Dp-113 .NetgroupNet1", "groupC/C++", "groupISTQB", "groupIOS", "groupUX" };

        [Test, TestCaseSource("GroupsToDelete")]
        public void ExecuteTest_DeleteGroup_GroupDisappearedFromGroupsList(String groupName)
        {
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
            var groupsList = mainPageInstance.LeftContainer.GroupsInLocation;
            Thread.Sleep(3000);

            groupsList.DeleteGroup(groupName, action, wait);
            Assert.IsNull(groupsList.GetGroupByName(groupName, wait));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
