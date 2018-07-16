using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
