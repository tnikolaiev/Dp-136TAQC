using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarLib
{
    public class SelectGroupWindow
    {
        IWebDriver webDriver;
        public IList<IWebElement> SelectedGroups { get => webDriver.FindElements(By.XPath("//li[@class='group-item']/p")); }
        public IWebElement SaveButton { get => webDriver.FindElement(By.ClassName("save")); }
        public IWebElement CancelButton { get => webDriver.FindElement(By.ClassName("cancel")); }
        public SelectGroupWindow(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public bool IsSelectGroupWindowOpened(IWebDriver webDriver)
        {
            return Acts.IsElementPresent(webDriver, By.ClassName("editGroup-wrapper"));
        }
        public List<string> GetSelectedGroupNames()
        {
            List<string> groupNames = new List<string>();
            foreach (var item in SelectedGroups)
            {
                groupNames.Add(item.Text);
            }
            return groupNames;
        }
        public IWebElement GetGroupByName(string groupName)
        {
            foreach(var group in SelectedGroups)
            {
                if (group.Text.Equals(groupName)) return group;
            }
            throw new Exception("There is no group with such name");
        }
    }
}
