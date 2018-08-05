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
    public class TC_4_02_01
    {

        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        CreateEditGroupsForm groupForm;
        WebDriverWait wait;

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
            groupForm = new CreateEditGroupsForm(driver);
            groupForm.GoToGroups.Click();
            groupForm.addGroups();
            groupForm.IsOpened(wait);
        }

        [Test]
        public void Test_CreateStudentFormIsDisplayed()
        {
            List<string> expectedResult = new List<string> { "", "Chernivtsy",  "WebUI", "", "", "", "", "planned" };
            List<string> actualResult = groupForm.RememberGroup();
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void Test_NameFieldDisplayedText()
        {
            groupForm.setName("someText");
            Assert.AreEqual("someText", groupForm.NameField.GetAttribute("value"));
        }
        [Test]
        public void Test_LocationIsDisplayedText()
        {
            groupForm.setLocationDDL(1);
            Assert.AreEqual("Dnipro", groupForm.LocationDDL.GetAttribute("value"));
        }
        [Test]
        public void Test_DirectionIsDisplayedText()
        {
            groupForm.setDirectionDDL(2);
            Assert.AreEqual("LAMP", groupForm.DirectionDDL.GetAttribute("value"));
        }
        [Test]
        public void Test_BudgetOwnerCheckBox()
        {
            groupForm.checkBudgetOwner();
            Assert.IsTrue(groupForm.BudgetOwnerCheckbox.Selected);
        }
        [Test]
        public void Test_StartDateDisplayedText()
        {
            groupForm.setStartDate("12.03.2018");
            Assert.AreEqual("2018-03-12", groupForm.StartDate.GetAttribute("value"));
        }

        [Test]
        public void Test_FinishDateFieldDisplayedText()
        {
            groupForm.setFinishDate("30.12.2018");
            Assert.AreEqual("2018-12-30", groupForm.FinishDate.GetAttribute("value"));
        }

        [Test]
        public void Test_TeacherFieldIsDisplayedText()
        {
            groupForm.setTeachers("someText");
            Assert.AreEqual("someText", groupForm.TeachersField.GetAttribute("value"));
        }

        [Test]
        public void Test_ExpertsFieldDisplayedText()
        {
            groupForm.setExperts("someText");
            Assert.AreEqual("someText", groupForm.ExpertsField.GetAttribute("value"));
        }

        [Test]
        public void Test_StageDDLDisplayedText()
        {
            groupForm.setStageDDL(5);
            Assert.AreEqual("finished", groupForm.StageDDL.GetAttribute("value"));
        }

        //[OneTimeTearDown]
        //public void CleanUp()
        //{
        //    driver.Close();
        //    driver.Quit();
        //}

    }
}

