using System.Collections.Generic;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class AdminPage
    {
        private IWebDriver _driverInstance;
        private IWebElement _addButton;
        private IWebElement _goToUsers;
        private IWebElement _goToStudents;
        private IWebElement _goToGroups;

        public IWebElement AddButton
        {
            get
            {
                if (_addButton != null) return _addButton;
                else
                {
                    _addButton = _driverInstance.FindElement(By.Id("add-new-user"));
                    return _addButton;
                }
            }
        }

        public IWebElement GoToUsers
        {
            get
            {
                if (_goToUsers != null) return _goToUsers;
                else
                {
                    _goToUsers = _driverInstance.FindElement(By.XPath("/html/body/div[1]/ul/li[1]/a"));
                    return _goToUsers;
                }
            }
        }

        public IWebElement GoToGroups
        {
            get
            {
                if (_goToGroups != null) return _goToGroups;
                else
                {
                    _goToGroups = _driverInstance.FindElement(By.XPath("/html/body/div[1]/ul/li[2]/a"));
                    return _goToGroups;
                }
            }
        }

        public IWebElement GoToStudents
        {
            get
            {
                if (_goToStudents != null) return _goToStudents;
                else
                {
                    _goToStudents = _driverInstance.FindElement(By.XPath("/html/body/div[1]/ul/li[3]/a"));
                    return _goToStudents;
                }
            }
        }

        public AdminPage(IWebDriver driver)
        {
            _driverInstance = driver;
        }

        public List<string> GetTableElements(string tagName)
        {
            IList<IWebElement> allElement = _driverInstance.FindElements(By.TagName(tagName));
            List<string> elements = new List<string>();

            foreach (IWebElement el in allElement)
            {
                elements.Add(el.Text);
            }
            return elements;
        }

        public static bool IsCreateForm(IWebDriver driver)
        {
            return driver.FindElements(By.ClassName("modal-content")).Count > 0 ?
                true : false;
        }
    }
}
