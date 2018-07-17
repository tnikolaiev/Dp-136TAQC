using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaesarTests
{
    class TestScheduleComponents
    {

        IWebDriver driver = new ChromeDriver();
      
        [Test]
        public void CheckScheduleEditor()
        {
            LoginPage loginPageInstance = new LoginPage(driver);
            MainPage mainPageInstance = new MainPage(driver);
            SchedulePage schedulePageInstance = new SchedulePage(driver);
          
            //Login to app 
            driver.Url = "http://localhost:3000/";
            Thread.Sleep(20);
            driver.Manage().Window.Maximize();
            Thread.Sleep(20);
            loginPageInstance.LogIn("sasha", "1234");
            Thread.Sleep(20);

            // Go to Schedule Page

            //driver.Url = "http://localhost:3000/Schedule/";

            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.Id("top-menu"))).Build().Perform(); //move to top menu
            driver.FindElement(By.XPath("//*[@class = 'containerMainMenu']//*[text() = 'Schedule']")).Click(); // click schedule


            //Select some group
            //Acts.Click (schedulePageInstance.LeftContainerInstance.GroupsInLocation.GetGroupByName("DP-094-MQC"));
            Thread.Sleep(10);
            driver.FindElement(By.CssSelector("div.wrapper.container-fluid:nth-child(2) div.page div.left-side-bar div.group-list-view div.group-collection.row div.small-group-view.col-md-6:nth-child(2) div:nth-child(1) > p:nth-child(1)")).Click();


            //Click on Cogweel to open Schedule Editor
           
            schedulePageInstance.ClickCogwheel();

            // Select some options FROM DDLs

            Acts.SelectOptionFromDDL(schedulePageInstance.EditScheduleWindowInstance.TeacherControl, "D. Petin");
                     
            //Select Event Type

             Acts.Click(schedulePageInstance.EditScheduleWindowInstance.WorkWithExpertEvent);

            //Put this event to some cell

             schedulePageInstance.ScheduleWeekViewAndEditInstance.GetCellByIndex(3);



        }

    }
}
