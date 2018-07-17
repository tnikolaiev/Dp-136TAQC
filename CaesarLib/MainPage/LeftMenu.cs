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
        private IWebDriver _driverInstance;

        public IWebElement CreateButton
        {
            get
            {
                if (_createButton != null) return _createButton;
                else
                {
                    _createButton = _driverInstance.FindElement(By.XPath("//*[@id='left-menu']//button[@title='Create']"));
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
                    _searchButton = _driverInstance.FindElement(By.XPath("//*[@id='left-menu']//button[@title='Search']"));
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
                    _editButton = _driverInstance.FindElement(By.XPath("//*[@id='left-menu']//button[@title='Edit']"));
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
                    _deleteButton = _driverInstance.FindElement(By.XPath("//*[@id='left-menu']//button[@title='Delete']"));
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
                    _leftMenuSection = _driverInstance.FindElement(By.CssSelector("#left-menu > div"));
                    return _leftMenuSection;
                }
            }
        }

        public List<String> GetAvailableButtonsTitles()
        {
            IList<IWebElement> webElems = _driverInstance.FindElements(By.XPath("//*[@id='left-menu']//button"));
            List<String> titles = new List<String>();
            foreach (var item in webElems)
            {
                titles.Add(Acts.GetAttribute(item, "title"));
            }
            return titles;
        }

        public LeftMenu(IWebDriver driver)
        {
            _driverInstance = driver;
        }

        public void Open(Actions act)
        {
            act.MoveToElement(LeftMenuSection, 100, 200).Perform();
        }

        public bool IsOpened()
        {
            return (LeftMenuSection.GetAttribute("class").Equals("contextMenu open")) ? true : false;
        }

        public Func<IWebDriver, IWebElement> IsSearchButtonVisible()
        {
            return ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='left-menu']//button[@title='Search']"));
        }
    }
}
