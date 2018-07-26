using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;

namespace CaesarLib
{
    public class AdminPage
    {
        private IWebDriver _driver;
        private IWebElement _addButton;
        private IWebElement _goToUsers;
        private IWebElement _goToStudents;
        private IWebElement _goToGroups;
        private IWebElement _escapeHomeButton;
        private IWebElement _table;
        private IList<IWebElement> _DeleteButtons;
        private IList<IWebElement> _EditButtons;

        private IWebElement _submitButton;
        private IWebElement _closeButton;
        private IWebElement _close;

        public IWebElement GetTable
        {
            get
            {
                if (_table != null) return _table;
                else
                {
                    _table = _driver.FindElement(By.ClassName("tab-pane"));
                    return _table;
                }
            }
        }
        public IWebElement AddButton
        {
            get
            {
                if (_addButton != null) return _addButton;
                else
                {
                    _addButton = _driver.FindElement(By.Id("add-new-user"));
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
                    _goToUsers = _driver.FindElement(By.XPath("/html/body/div[1]/ul/li[1]/a"));
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
                    _goToGroups = _driver.FindElement(By.XPath("/html/body/div[1]/ul/li[2]/a"));
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
                    _goToStudents = _driver.FindElement(By.XPath("/html/body/div[1]/ul/li[3]/a"));
                    return _goToStudents;
                }
            }
        }

        public IWebElement EscapeHome
        {
            get
            {
                if (_escapeHomeButton != null) return _escapeHomeButton;
                else
                {
                    _escapeHomeButton = _driver.FindElement(By.ClassName("btn-warning"));
                    return _escapeHomeButton;
                }
            }
        }

        public IList<IWebElement> Delete
        {

            get
            {
                if (_DeleteButtons != null) return _DeleteButtons;
                else
                {                    
                    _DeleteButtons = GetTable.FindElements(By.ClassName("btn-danger"));
                    return _DeleteButtons;
                }
            }
        }

        public IList<IWebElement> Edit
        {
            get
            {
                if (_EditButtons != null) return _EditButtons;
                else
                {
                    _EditButtons = GetTable.FindElements(By.ClassName("btn-info"));
                    return _EditButtons;
                }
            }
        }

        public IWebElement SubmitButton
        {
            get
            {
                if (_submitButton != null) return _submitButton;
                else
                {
                    _submitButton = _driver.FindElement(By.ClassName("btn-primary"));
                    return _submitButton;
                }
            }
        }

        public IWebElement CloseButton
        {
            get
            {
                if (_closeButton != null) return _closeButton;
                else
                {
                    _closeButton = _driver.FindElement(By.ClassName("btn-default"));
                    return _closeButton;
                }
            }
        }

        public IWebElement Close
        {
            get
            {
                if (_close != null) return _close;
                else
                {
                    _close = _driver.FindElement(By.ClassName("close"));
                    return _close;
                }
            }
        }

        public AdminPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement getLastElement(IList<IWebElement> webElements, int number)
        {
            if (number == 0)
            {
                return webElements.Last();
            }
            else return webElements[number];
        }

        public static bool IsAdminPageOpened(IWebDriver driver)
        {
            return driver.FindElements(By.ClassName("nav-tabs")).Count > 0 &
                driver.FindElements(By.ClassName("btn-warning")).Count > 0 &
                driver.FindElements(By.ClassName("btn-info")).Count > 0 ?
                true : false;
        }
        
        public static bool IsCreateFormOpened(IWebDriver driver)
        {
            return driver.FindElements(By.ClassName("modal-header")).Count > 0 &
                driver.FindElements(By.Name("firstName")).Count > 0 ?
                true : false;
        }

    }
}
