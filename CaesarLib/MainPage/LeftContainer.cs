using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class LeftContainer
    {
        private IWebDriver _driverInstance;
        private GroupsInLocation _groupsInLocation;
        public GroupsInLocation GroupsInLocation
        {
            get
            {
                if (_groupsInLocation != null) return _groupsInLocation;
                else
                {
                    _groupsInLocation = new GroupsInLocation(_driverInstance);
                    return _groupsInLocation;
                }
            }
        }

        public LeftContainer(IWebDriver driver)
        {
            _driverInstance = driver;
        }
    }
}
