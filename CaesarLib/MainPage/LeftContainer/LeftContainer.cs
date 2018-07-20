using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class LeftContainer
    {
        private IWebDriver driver;
        private GroupsInLocation _groupsInLocation;
        public GroupsInLocation GroupsInLocation
        {
            get
            {
                if (_groupsInLocation != null) return _groupsInLocation;
                else
                {
                    _groupsInLocation = new GroupsInLocation(driver);
                    return _groupsInLocation;
                }
            }
        }

        public LeftContainer(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
