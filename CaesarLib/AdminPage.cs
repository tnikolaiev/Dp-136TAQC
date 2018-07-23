using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;

namespace CaesarLib
{
    public class AdminPage
    {
        private IWebDriver _driverInstance;
        private IWebElement _addButton;
        private IWebElement _goToUsers;
        private IWebElement _goToStudents;
        private IWebElement _goToGroups;
        private IWebElement _escapeHomeButton;
        private IList<IWebElement> _tableElements;
        private IList<IWebElement> _DeleteButtons;
        private IList<IWebElement> _EditButtons;


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

        public IWebElement EscapeHome
        {
            get
            {
                if (_escapeHomeButton != null) return _escapeHomeButton;
                else
                {
                    _escapeHomeButton = _driverInstance.FindElement(By.ClassName("btn-warning"));
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
                    var table = _driverInstance.FindElement(By.ClassName("tab-pane"));
                    _DeleteButtons = table.FindElements(By.ClassName("btn-danger"));
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
                    var table = _driverInstance.FindElement(By.ClassName("tab-pane"));
                    _EditButtons = _driverInstance.FindElements(By.ClassName("btn-info"));
                    return _EditButtons;
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

        public IList<IWebElement> tableElements
        {
            get
            {
                if (_tableElements != null) return _tableElements;
                else
                {
                    _tableElements = _driverInstance.FindElements(By.TagName("tr"));
                    return _tableElements;
                }
            }
        }

        public IWebElement getLastElement(IList<IWebElement> webElements)
        {
            return webElements.Last();
        }

        public static bool IsCreateForm(IWebDriver driver)
        {
            return driver.FindElements(By.ClassName("modal-header")).Count > 0 &
                driver.FindElements(By.Name("firstName")).Count > 0 ?
                true : false;
        }

    }
}
