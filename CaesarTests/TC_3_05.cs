using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CaesarTests
{
    [TestFixture]
    public class TC_3_05
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            //Open Login Page
            webDriver.Url = baseURL;
            wait.Until((driver) => LoginPage.IsLoginPageOpened(driver));
            loginPageInstance = new LoginPage(webDriver);
            //Login as Teacher
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(webDriver);
            //Go to group's DP-093-JS students page
            webDriver.Url = baseURL + "/Students/Dnipro/DP-093-JS/list";
            wait.Until((d) => StudentsContent.IsOpened(d));
            //Open modal window 'EditStudentListWindow'
            mainPageInstance.CenterContainer.StudentsContent.EditButton.Click();
            wait.Until((d) => EditStudentListWindow.IsOpened(d));
        }
        [Test]
        public void ExecuteTest_ClickRightshofter_ApprovedByViewOpened()
        {
            List<string> headings = new List<string>();
            mainPageInstance.ModalWindow.EditStudentListWindow.RightshifterButton.Click();
            foreach (var item in mainPageInstance.ModalWindow.EditStudentListWindow.StudentTable.GetHeadings())
            {
                headings.Add(item.Text);
            }
            CollectionAssert.Contains(headings, "Approved by");          
        }
        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            webDriver.Quit();
            webDriver.Close();
        }
    }
}
