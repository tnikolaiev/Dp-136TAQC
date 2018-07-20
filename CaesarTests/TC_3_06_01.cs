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
    class TC_3_06_01
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        GroupView groupViewInstance;
        EditStudentList editStudentListInstance;
        String path;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Url = baseURL;

            loginPageInstance = new LoginPage(webDriver);
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPage(d));

            webDriver.Url = baseURL+"/Students/Lviv/Lv-023-UX/list";

            groupViewInstance = new GroupView(webDriver);
            wait.Until((d) => GroupView.IsGroupView(d));

            Acts.Click(groupViewInstance.EditButton);

            editStudentListInstance = new EditStudentList(webDriver);
            wait.Until((d) => EditStudentList.IsEditStudentList(d));
        }
        [Test]
        public void ExecuteTest_ImportStudentList_ListImpoerted()
        {
            Acts.Click(editStudentListInstance.ImportStudentsButton);
            path = EditStudentList.GetTestFile("TC_3_06_01 ValidStudentList.txt");
            Acts.UploadFile(path);
            Acts.Click(editStudentListInstance.SaveFormButton);
            Assert.AreEqual(4, editStudentListInstance.Students.Count);
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            while (editStudentListInstance.DeleteButtons.Count != 0)
            {
                Acts.Click(editStudentListInstance.GetLastElement(editStudentListInstance.DeleteButtons));
                Acts.PressKeyboardButton(@"{Enter}");
            }
            webDriver.Close();
            webDriver.Quit();      
        }
    }
}
