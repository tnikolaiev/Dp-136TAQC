using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class TopMenu
    {
        private IWebElement _topMenuSection;
        private IWebElement _locationsItem;
        private IWebElement _groupsItem;
        private IWebElement _studentsItem;
        private IWebElement _scheduleItem;
        private IWebElement _addItem;
        private IWebElement _aboutItem;
        private IWebElement _logoutButton;
        private IWebDriver _driverInstance;
        
        public IWebElement TopMenuSection
        {
            get
            {
                if (_topMenuSection != null) return _topMenuSection;
                else
                {
                    _topMenuSection = _driverInstance.FindElement(By.Id("top-menu"));
                    return _topMenuSection;
                }
            }
        }

        public IWebElement LocationsItem
        {
            get
            {
                if (_locationsItem != null) return _locationsItem;
                else
                {
                    _locationsItem = _driverInstance.FindElement(By.XPath("//*[@class = 'containerMainMenu']//*[text() = 'Locations']"));
                    return _locationsItem;
                }
            }
        }

        public IWebElement GroupsItem
        {
            get
            {
                if (_groupsItem != null) return _groupsItem;
                else
                {
                    _groupsItem = _driverInstance.FindElement(By.XPath("//*[@class = 'containerMainMenu']//*[text() = 'Groups']"));
                    return _groupsItem;
                }
            }
        }

        public IWebElement StudentsItem
        {
            get
            {
                if (_studentsItem != null) return _studentsItem;
                else
                {
                    _studentsItem = _driverInstance.FindElement(By.XPath("//*[@class = 'containerMainMenu']//*[text() = 'Students']"));
                    return _studentsItem;
                }
            }
        }

        public IWebElement ScheduleItem
        {
            get
            {
                if (_scheduleItem != null) return _scheduleItem;
                else
                {
                    _scheduleItem = _driverInstance.FindElement(By.XPath("//*[@class = 'containerMainMenu']//*[text() = 'Schedule']"));
                    return _scheduleItem;
                }
            }
        }

        public IWebElement AddItem
        {
            get
            {
                if (_addItem != null) return _addItem;
                else
                {
                    _addItem = _driverInstance.FindElement(By.XPath("//*[@class = 'containerMainMenu']//*[text() = 'add']"));
                    return _addItem;
                }
            }
        }

        public IWebElement AboutItem
        {
            get
            {
                if (_aboutItem != null) return _aboutItem;
                else
                {
                    _aboutItem = _driverInstance.FindElement(By.XPath("//*[@class = 'containerMainMenu']//*[text() = 'About']"));
                    return _aboutItem;
                }
            }
        }

        public IWebElement LogoutButton
        {
            get
            {
                if (_logoutButton != null) return _logoutButton;
                else
                {
                    _logoutButton = _driverInstance.FindElement(By.XPath("//*[@id = 'top-menu']//a[@class = 'logout']"));
                    return _logoutButton;
                }
            }
        }

        public TopMenu(IWebDriver driver)
        {
            _driverInstance = driver;
        }

        public bool IsOpened()
        {
            return (TopMenuSection.GetAttribute("class").Equals("top-menu col-md-12 col-sm-12 col-xs-12 open")) ? true : false;
        }
    }
}
