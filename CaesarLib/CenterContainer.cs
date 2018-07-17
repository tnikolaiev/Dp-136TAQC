using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class CenterContainer
    {
        IWebDriver webDriver;
        IWebElement groupLocation;
        IWebElement locationHint;
        IWebElement groupName;
        IWebElement groupStageTitle;
        IWebElement groupStage;
        public IWebElement GroupLocation
        {
            get
            {
                if (groupLocation != null) return groupLocation;
                else
                {
                    groupLocation = webDriver.FindElement(By.ClassName("groupLocation"));
                    return groupLocation;
                }
            }
        }
        public IWebElement LocationHint
        {
            get
            {
                if (locationHint != null) return locationHint;
                else
                {
                    locationHint = webDriver.FindElement(By.XPath("//p[@class='hint']"));
                    return locationHint;
                }
            }
        }
        public IWebElement GroupName
        {
            get
            {
                if (groupName != null) return groupName;
                else
                {
                    groupName = webDriver.FindElement(By.ClassName("groupName"));
                    return groupName;
                }
            }
        }

        public IWebElement GroupStageTitle
        {
            get
            {
                if (groupStageTitle != null) return groupStageTitle;
                else
                {
                    groupStageTitle = webDriver.FindElement(By.ClassName("groupStageTitle"));
                    return groupStageTitle;
                }
            }
        }
        public IWebElement GroupStage
        {
            get
            {
                if (groupStage != null) return groupStage;
                else
                {
                    groupStage = webDriver.FindElement(By.ClassName("groupStage"));
                    return groupStage;
                }
            }
        }
        public CenterContainer(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public Func<IWebDriver, IWebElement> IsHintVisible()
        {
            return ExpectedConditions.ElementIsVisible(By.XPath("//p[@class='hint']"));
        }
    }
}

