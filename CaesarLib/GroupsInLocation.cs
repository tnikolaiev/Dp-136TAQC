using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CaesarLib
{
    public class GroupsInLocation
    {
        private IWebDriver _driverInstance;
        private IWebElement _filterSearchButton, _myGroupsButton, _futureGroupsToggle, _currentGroupsToggle, _endedGroupsToggle;
        public IWebElement FilterSearchButton
        {
            get
            {
                if (_filterSearchButton != null) return _filterSearchButton;
                else
                {
                    _filterSearchButton = _driverInstance.FindElement(By.XPath("//div[@class='group-list-view']//div[@class='search']/img"));
                    return _filterSearchButton;
                }
            }
        }

        public IWebElement MyGroupsFilter
        {
            get
            {
                if (_myGroupsButton != null) return _myGroupsButton;
                else
                {
                    _myGroupsButton = _driverInstance.FindElement(By.XPath("//div[@class='group-list-footer']/button[text() = 'My Groups']"));
                    return _myGroupsButton;
                }
            }
        }

        public IWebElement FutureGroupsToggle
        {
            get
            {
                if (_futureGroupsToggle != null) return _futureGroupsToggle;
                else
                {
                    _futureGroupsToggle = _driverInstance.FindElement(By.CssSelector("#left-side-bar > div > div.group-list-footer > div > label:nth-child(6) > div"));
                    return _futureGroupsToggle;
                }
            }
        }

        public IWebElement CurrentGroupsToggle
        {
            get
            {
                if (_currentGroupsToggle != null) return _currentGroupsToggle;
                else
                {
                    _currentGroupsToggle = _driverInstance.FindElement(By.CssSelector("#left-side-bar > div > div.group-list-footer > div > label:nth-child(4) > div"));
                    return _currentGroupsToggle;
                }
            }
        }

        public IWebElement EndedGroupsToggle
        {
            get
            {
                if (_endedGroupsToggle != null) return _endedGroupsToggle;
                else
                {
                    _endedGroupsToggle = _driverInstance.FindElement(By.CssSelector("#left-side-bar > div > div.group-list-footer > div > label:nth-child(2) > div"));
                    return _endedGroupsToggle;
                }
            }
        }

        public GroupsInLocation(IWebDriver driver)
        {
            _driverInstance = driver;
        }

        public List<String> GetAvailableGroupsNames()
        {
            IList<IWebElement> elements = _driverInstance.FindElements(By.XPath("//div[@class='small-group-view col-md-6']//p"));
            List<String> groupsNames = new List<String>();
            foreach (var item in elements)
            {
                groupsNames.Add(item.Text);
            }
            return groupsNames;
        }

        public IWebElement GetGroupByName(String name)
        {
            IList<IWebElement> elements = _driverInstance.FindElements(By.XPath("//div[@class='small-group-view col-md-6']"));
            List<String> groupsNames = new List<String>();
            foreach (var item in elements)
            {
                if (item.FindElement(By.TagName("p")).Text.Equals(name)) return item;
            }
            throw new Exception("There is no group with such name");
        }
    }

}