using CaesarLib;
using CaesarLib.StudentsPage;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarTests
{
    [TestFixture]
    public class TC_3_08
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage logingPageInstance;
        GroupView groupViewInstance;
        EditStudentList editStudentListInstance;
        EditStudent editStudentInstance;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Url = baseURL;

            logingPageInstance = new LoginPage(webDriver);
            logingPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPage(d));

            webDriver.Url = baseURL + "/Students/Dnipro/DP-093-JS/list";
            groupViewInstance = new GroupView(webDriver);
            wait.Until((d) => GroupView.IsGroupView(d));

            Acts.Click(groupViewInstance.EditButton);

            editStudentListInstance = new EditStudentList(webDriver);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));

            editStudentInstance = new EditStudent(webDriver);
        }
        [Test]
        public void ExecuteTest_AddStudent_1_StudentAdded()
        {
            Acts.Click(editStudentListInstance.CreateStudentButton);
            wait.Until((d) => EditStudent.IsEditStudent(d));

            editStudentInstance.FillForm("Anna", "Petrova", 2, "99", "4", 1);

            Acts.Click(editStudentInstance.SaveButton);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));

            Assert.AreEqual("Petrova Anna", EditStudentList.GetStudentName(editStudentListInstance.GetLastElement(editStudentListInstance.Students)));
        }
        [Test]
        public void ExecuteTest_AddStudent_2_StudentAddedt()
        {
            Acts.Click(editStudentListInstance.CreateStudentButton);
            wait.Until((d) => EditStudent.IsEditStudent(d));

            editStudentInstance.FillForm("Sergey", "Glinchikov", 8, "130", "5", 1);

            Acts.Click(editStudentInstance.SaveButton);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));

            Assert.AreEqual("Glinchikov Sergey", EditStudentList.GetStudentName(editStudentListInstance.GetLastElement(editStudentListInstance.Students)));
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            for (int i = 0; i < 2; i++)
            {
                Acts.Click(editStudentListInstance.GetLastElement(editStudentListInstance.DeleteButtons));
                Acts.PressKeyboardButton(@"{Enter}");
            }
            webDriver.Quit();
            webDriver.Close();
        }
    }
}
