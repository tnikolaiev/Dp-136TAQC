using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class CenterContainer
    {
        public IWebDriver webDriver;
        public IWebElement GroupLocation { get => webDriver.FindElement(By.ClassName("groupLocation")); }
        public IWebElement GroupName { get => webDriver.FindElement(By.ClassName("groupName")); }
        public IWebElement GroupStageTitle { get => webDriver.FindElement(By.ClassName(" groupStageTitle")); }
        public IWebElement GroupStage { get => webDriver.FindElement(By.ClassName("groupStage")); }
        public CenterContainer(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public Func<IWebDriver, IWebElement> IsHintVisible()
        {
            return ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='hint']/p[@class='hint']"));
        }
    }
}
