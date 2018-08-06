using OpenQA.Selenium;
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
            IList<IWebElement> elements = driver.FindElements(By.ClassName("ContentAbout row"));
            List<String> titleGroup = new List<String>();
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
            IList<IWebElement> elements = driver.FindElements(By.ClassName("contributors-menu"));
            List<String> buttonNames = new List<String>();
            foreach (var item in elements)
            {
                buttonNames.Add((item.Text));
            }
            return buttonNames;
        }
    }
}
