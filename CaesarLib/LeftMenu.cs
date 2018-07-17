using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class LeftMenu
    {
        private IWebElement _createButton, _searchButton, _settingsButton, _deleteButton, _leftMenuSection;
        private IWebDriver _driverInstance;

        public IWebElement CreateButton
        {
            get
            {
                if (_createButton != null) return _createButton;
                else
                {
                    _createButton = _driverInstance.FindElement(By.XPath("//div[@class='itemContextMenu']//i[@class='fa fa-plus-square-o fa-4x create']"));
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
                    _searchButton = _driverInstance.FindElement(By.XPath("//div[@class = 'itemContextMenu']//i[@class = 'fa fa-search fa-4x search']"));
                    return _searchButton;
                }
            }
        }

        public IWebElement SettingsButton
        {
            get
            {
                if (_settingsButton != null) return _settingsButton;
                else
                {
                    _settingsButton = _driverInstance.FindElement(By.XPath("//div[@class='itemContextMenu']//i[@class='fa fa-cog fa-4x edit']"));
                    return _settingsButton;
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
                    _deleteButton = _driverInstance.FindElement(By.XPath("//div[@class='itemContextMenu']//i[@class='fa fa-trash-o fa-4x delete']"));
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

    }
}
