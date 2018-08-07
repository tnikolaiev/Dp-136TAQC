using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CaesarLib;

namespace CaesarTests
{
    
    [TestFixture]
    public class TC_3_0_04
    {

        IWebDriver driver;
        LoginPage loginPageInstance;
        WebDriverWait wait;
        MainPage mainPageInstance;
        MainPage mainPageInstance1;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
            mainPageInstance1 = new MainPage(driver);
        }

        static object[] DividedCases =
            {
                new object[] { "DP-065-AQC", "DP-001-Test", ".Net", "P. Kucher", "NewExpert"},
            };

        [Test, TestCaseSource("DividedCases")]
        public void EditingGroupStatic(string groupname, string newname, string direction, string teacher, string newexpert)
        {

            List<string> expected = new List<string>();
            expected.Add("DP-001-Test");
            expected.Add("P. Kucher");
            expected.Add("NewExpert");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName(groupname).Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.GroupNameField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.GroupNameField.SendKeys(newname);
            mainPageInstance.ModalWindow.GroupCreateWindow.SetDirection(direction);
            mainPageInstance.ModalWindow.GroupCreateWindow.EditTeacher(teacher);
            mainPageInstance.ModalWindow.GroupCreateWindow.EditExpert(newexpert);
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();

            List<string> actual = new List<string>();
            actual.Add(mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-001-Test").Text);
            actual.Add(mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupCoordination.GetValueFromCell(2, 2));
            actual.Add(mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupCoordination.GetValueFromCell(3, 2));

            CollectionAssert.AreEqual(expected,actual);

        }

        static IEnumerable<object[]> DataGroup = Instruments.ReadXML("DataGroups.xml", "testData", "groupname", "newname", "direction", "teacher", "expert");

        [Test, TestCaseSource("DataGroup")]
        public void EditingGroupXML(string groupname, string newname, string direction, string teacher, string newexpert)
        {

            List<string> expected = new List<string>();
            expected.Add("DP-001-Test");
            expected.Add("P. Kucher");
            expected.Add("NewExpert");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName(groupname).Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.GroupNameField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.GroupNameField.SendKeys(newname);
            mainPageInstance.ModalWindow.GroupCreateWindow.SetDirection(direction);
            mainPageInstance.ModalWindow.GroupCreateWindow.EditTeacher(teacher);
            mainPageInstance.ModalWindow.GroupCreateWindow.EditExpert(newexpert);
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();

            List<string> actual = new List<string>();
            actual.Add(mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-001-Test").Text);
            actual.Add(mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupCoordination.GetValueFromCell(2, 2));
            actual.Add(mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupCoordination.GetValueFromCell(3, 2));

            CollectionAssert.AreEqual(expected, actual);

        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
            mainPageInstance1.LeftContainer.GroupsInLocation.GetGroupByName("DP-001-Test").Click();
            mainPageInstance1.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance1.ModalWindow.GroupCreateWindow.GroupNameField.Clear();
            mainPageInstance1.ModalWindow.GroupCreateWindow.GroupNameField.SendKeys("DP-065-AQC");
            mainPageInstance1.ModalWindow.GroupCreateWindow.SetDirection("ATQC");
            mainPageInstance1.ModalWindow.GroupCreateWindow.EditTeacher("D. Petin");
            mainPageInstance1.ModalWindow.GroupCreateWindow.EditExpert("Testman");
            mainPageInstance1.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();

            driver.Quit();
        }


    }
}
