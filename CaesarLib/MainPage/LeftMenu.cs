using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarLib
{
    public class LeftMenu
    {
        private IWebElement _createButton;
        private IWebElement _searchButton;
        private IWebElement _editButton;
        private IWebElement _deleteButton;
        private IWebElement _leftMenuSection;
        private IWebDriver driver;

        public IWebElement CreateButton
        {
            get
            {
                if (_createButton != null) return _createButton;
                else
                {
                    _createButton = driver.FindElement(By.XPath("//div[@id='left-menu']//button[@title='Create']/i"));
                    return _createButton;
                }
            }
        }

        public IWebElement SearchButton
        {
            get
            {
                if (_searchButton != null) return _searchButton;
                else
                {
                    _searchButton = driver.FindElement(By.XPath("//div[@id='left-menu']//button[@title='Search']/i"));
                    return _searchButton;
                }
            }
        }

        public IWebElement EditButton
        {
            get
            {
                if (_editButton != null) return _editButton;
                else
                {
                    _editButton = driver.FindElement(By.XPath("//div[@id='left-menu']//button[@title='Edit']/i"));
                    return _editButton;
                }
            }
        }

        public IWebElement DeleteButton
        {
            get
            {
                if (_deleteButton != null) return _deleteButton;
                else
                {
                    _deleteButton = driver.FindElement(By.XPath("//div[@id='left-menu']//button[@title='Delete']/i"));
                    return _deleteButton;
                }
            }
        }

        public IWebElement LeftMenuSection
        {
            get
            {
                if (_leftMenuSection != null) return _leftMenuSection;
                else
                {
                    _leftMenuSection = driver.FindElement(By.CssSelector("#left-menu > div"));
                    return _leftMenuSection;
                }
            }
        }

        public List<String> GetAvailableButtonsTitles()
        {
            IList<IWebElement> webElems = driver.FindElements(By.XPath("//div[@id='left-menu']//button"));
            List<String> titles = new List<String>();
            foreach (var item in webElems)
            {
                titles.Add(Acts.GetAttribute(item, "title"));
            }
            return titles;
        }

        public LeftMenu(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Open(Actions act)
        {
            act.MoveToElement(LeftMenuSection, 100, 200).Perform();
        }

        public void Open(Actions act, WebDriverWait wait)
        {
            wait.Until((d) => IsLeftMenuSectionExists());
            act.MoveToElement(LeftMenuSection, 100, 200).Perform();
            wait.Until((d) => IsSearchButtonVisible());
        }

        public bool IsOpened()
        {
            return (LeftMenuSection.GetAttribute("class").Equals("contextMenu open")) ? true : false;
        }

        public bool IsLeftMenuSectionExists()
        {
            return Acts.IsElementExists(driver, By.CssSelector("#left-menu > div"));
        }

        public bool IsSearchButtonVisible()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@id='left-menu']//button[@title='Search']"));
        }
    }
}