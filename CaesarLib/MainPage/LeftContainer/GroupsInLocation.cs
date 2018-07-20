using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarLib
{
    public class GroupsInLocation
    {
        private IWebDriver driver;
        private IWebElement _filterSearchButton;
        private IWebElement _myGroupsButton;
        private IWebElement _futureGroupsToggle;
        private IWebElement _currentGroupsToggle;
        private IWebElement _endedGroupsToggle;

        public IWebElement FilterSearchButton
        {
            get
            {
                if (_filterSearchButton != null) return _filterSearchButton;
                else
                {
                    _filterSearchButton = driver.FindElement(By.XPath("//div[@class='group-list-view']//div[@class='search']/img"));
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
                    _myGroupsButton = driver.FindElement(By.XPath("//div[@class='group-list-footer']/button[contains(text(), 'My Groups')]"));
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
                    _futureGroupsToggle = driver.FindElement(By.XPath("//div[@class='stage-toggle']/label[3]/div"));
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
                    _currentGroupsToggle = driver.FindElement(By.XPath("//div[@class='stage-toggle']/label[2]/div"));
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
                    _endedGroupsToggle = driver.FindElement(By.XPath("//div[@class='stage-toggle']/label[1]/div"));
                    return _endedGroupsToggle;
                }
            }
        }

        public GroupsInLocation(IWebDriver driver)
        {
            this.driver = driver;
        }

        public Func<IWebDriver, IWebElement> AreGroupsVisible()
        {
            return ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='group-collection row']/div//p"));
        }

        public List<String> GetAvailableGroupsNames()
        {
            IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@class='group-collection row']/div//p"));
            List<String> groupsNames = new List<String>();
            foreach (var item in elements)
            {
                groupsNames.Add(item.Text);
            }
            return groupsNames;
        }

        public bool IsGroupChosen(IWebElement group)
        {
            return Acts.GetAttribute(group, "class").Contains("chosen");
        }

        public IWebElement GetGroupByName(String name)
        {
            IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@class='group-collection row']/div"));
            List<String> groupsNames = new List<String>();
            foreach (var item in elements)
            {
                if (item.FindElement(By.TagName("p")).Text.Equals(name)) return item;
            }
            throw new Exception("There is no group with such name");
        }
    }
}