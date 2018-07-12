using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_02_01
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPage(d));
            mainPageInstance = new MainPage(driver);
        }

        [Test]
        public void ExecuteTest_ProfileButtonClick_RightMenuOpened()
        {
            Acts.Click(mainPageInstance.ProfileButton);
            Assert.IsTrue(wait.Until((d) => mainPageInstance.RightMenu.IsOpened()));
        }

        //[Test]
        //public void Executetest_DropMouseFocus_RightMenuClosed()
        //{
        //    ////mouseout(mainPageInstance.RightMenu);
        //    //Actions act = new Actions(driver);
        //    IJavaScriptExecutor jse = driver as IJavaScriptExecutor;
        //    //act.MoveToElement(mainPageInstance.ProfileButton)
        //    //    .Click()
        //    //    .Perform();
        //    Thread.Sleep(1000);
        //    mainPageInstance.OpenProfileDataSection();
        //    // jse.ExecuteScript("document.getElementByClassName('right-menu open').mouseout();");
        //    IWebElement el = driver.FindElement(By.ClassName("right-menu open"));
        //    //jse.ExecuteScript("argument[0]",el).;
        //    //Thread.Sleep(3000);
        //    //Actions acts1 = new Actions(driver);
        //    //acts1.MoveToElement(mainPageInstance.ProfileButton).Click().Build().Perform();
        //    //acts1.MoveByOffset(40, 40).Perform();
        //    Assert.IsTrue(wait.Until<bool>(d => Equals("right-menu", mainPageInstance.GetRightMenuCondition())));
        //    Assert.Fail();
        //}

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}