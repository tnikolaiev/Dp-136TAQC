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
    public class TC_2_01_1 : BaseTest    {
        
        MonthTab MonthTabInstance;       

        [Test]

        public void IsCurrentMonthDisplayed()
        {
            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();

            //Selecting group
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Go to MonthTab and check month name
            wait.Until((d) => MainPageInstance.CenterContainer.ScheduleContent.MonthTabInstance.IsMonthTabDisplayed(driver));
            MonthTabInstance = new MonthTab(driver);
            String currentMonth = MonthTabInstance.MonthName.Text;
            Assert.AreEqual("August", currentMonth);
        }
    }
}