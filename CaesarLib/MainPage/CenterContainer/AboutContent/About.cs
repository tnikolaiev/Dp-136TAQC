using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib
{
    public class About
    {
        private IWebDriver driver;
        private IWebElement developmentResearch;
        private IWebElement qualityAssurance;
        private IWebElement managementAndMentoring;
        private IWebElement additionalThanks;
        private IWebElement contentHeaderGroupNameHint;
        private IWebElement contentHeaderGroupNumberHint;

        public About(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement DevelopmentResearch
        {
            get
            {
                if (developmentResearch != null) return developmentResearch;
                else
                {
                    developmentResearch = driver.FindElement(By.XPath("//p[contains(.,'Development & Research')]"));
                    return developmentResearch;
                }
            }
        }

        public IWebElement QualityAssurance
        {
            get
            {
                if (qualityAssurance != null) return qualityAssurance;
                else
                {
                    qualityAssurance = driver.FindElement(By.XPath("//p[contains(.,'Quality Assurance')]"));
                    return qualityAssurance;
                }
            }
        }

        public IWebElement ManagementAndMentoring
        {
            get
            {
                if (managementAndMentoring != null) return managementAndMentoring;
                else
                {
                    managementAndMentoring = driver.FindElement(By.XPath("//p[contains(.,'Management and Mentoring')]"));
                    return managementAndMentoring;
                }
            }
        }

        public IWebElement AdditionalThanks
        {
            get
            {
                if (additionalThanks != null) return additionalThanks;
                else
                {
                    additionalThanks = driver.FindElement(By.XPath("//p[contains(.,'Additional Thanks')]"));
                    return additionalThanks;
                }
            }
        }

        public List<String> GetTitleGroup()
        {
            IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@class='ContentAbout row']/div//p"));
            List <String> titleGroup = new List<String>();
            foreach (var item in elements)
            {
                titleGroup.Add(item.Text);
            }
            return titleGroup;
        }

        public bool AreAboutButtonVisible()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@class='contributors-menu']"));
        }

        public List<String> GetButtonsName(WebDriverWait wait)
        {
            wait.Until((d) => AreAboutButtonVisible());
            IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@class='contributors-menu']/div//p"));
            List<String> buttonNames = new List<String>();
            foreach (var item in elements)
            {
                buttonNames.Add(item.Text);
            }
            return buttonNames;
        }

        public IWebElement ContentHeaderGroupNameHint
        {
            get
            {
                if (contentHeaderGroupNameHint != null) return contentHeaderGroupNameHint;
                else
                {
                    contentHeaderGroupNameHint = driver.FindElement(By.XPath("//div[@class='content-header-group-name']//p"));
                    return contentHeaderGroupNameHint;
                }
            }
        }

        public IWebElement ContentHeaderGroupNumberHint
        {
            get
            {
                if (contentHeaderGroupNumberHint != null) return contentHeaderGroupNumberHint;
                else
                {
                    contentHeaderGroupNumberHint = driver.FindElement(By.XPath("//div[@class='stageView']//p"));
                    return contentHeaderGroupNumberHint;
                }
            }
        }

        public Func<IWebDriver, IWebElement> IsContentHeaderGroupNameHintVisible()
        {
            return ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='content-header-group-name']//p"));
        }

        public IList<IWebElement> GetTitleGroups()
        {
            IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@class='ContentAbout row']/div//p"));
            List<IWebElement> titleGroup = new List<IWebElement>();
            foreach (var item in elements)
            {
                titleGroup.Add(item);
            }
            return titleGroup;
        }

        public void MoveToAboutCourse(IList<IWebElement> GetTitleGroups, string name)
        {
                 foreach (var item in GetTitleGroups)
                {
                    if (item.Text == name)
                    {
                        Actions actions = new Actions(driver);
                        actions.MoveToElement(item).Build().Perform();
                    }
                }
        }

        public void ClickToGroupeName(IList<IWebElement> GetTitleGroups, string name)
        {
            foreach (var item in GetTitleGroups)
            {
                if (item.Text == name)
                {
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(item).Build().Perform();
                }
            }
        }

    }
}
