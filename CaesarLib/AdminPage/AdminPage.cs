using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class AdminPage
    {
        private IWebDriver _driver;       
        private IWebElement _goToUsers;
        private IWebElement _goToStudents;
        private IWebElement _goToGroups;

        private IList<IWebElement> _AddButton;
        private IList<IWebElement> _DeleteButtons;
        private IList<IWebElement> _EditButtons;        
        private IWebElement _submitButton;
        private IWebElement _closeButton;
        private IWebElement _close;
        private IWebElement _escapeHomeButton;
                
        public IList<IWebElement> AddButton
        {
            get
            {
                if (_AddButton != null) return _AddButton;
                else
                {
                    _AddButton = _driver.FindElements(By.Id("add-new-user"));
                    return _AddButton;
                }
            }
        }
        
        public AdminPage AddButtonClick(int index)
        {
            IList<IWebElement> add;
            add = _driver.FindElements(By.Id("add-new-user"));
            IWebElement addClick = add[index];
            addClick.Click();

            return this;
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
              
        //public AdminPage EditClick(IList<IList<IWebElement>> rows, int rowIndex)
        //{
        //    IList<IWebElement> row = rows[rowIndex-1];
        //    IWebElement action = row[row.Count - 1];
        //    IWebElement edit = action.FindElement(By.ClassName("btn-info"));
        //    edit.Click();

        //    return this;
        //}

        //public AdminPage DeleteClick(IList<IList<IWebElement>> rows, int rowIndex)
        //{
        //    IList<IWebElement> row = rows[rowIndex - 1];
        //    IWebElement action = row[row.Count - 1];
        //    IWebElement delete = action.FindElement(By.ClassName("btn-info"));
        //    delete.Click();

        //    return this;
        //}

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

       
        public static bool IsAdminPageOpened(IWebDriver driver)
        {
            return driver.FindElements(By.ClassName("nav-tabs")).Count > 0 &
                driver.FindElements(By.ClassName("btn-warning")).Count > 0 &
                driver.FindElements(By.ClassName("btn-info")).Count > 0 ?
                true : false;
        }

        public static bool IsCreateEditFormOpened(IWebDriver driver)
        {
            return driver.FindElements(By.ClassName("modal-content")).Count > 0 &
                driver.FindElements(By.ClassName("modal-header")).Count > 0 &
                driver.FindElements(By.ClassName("modal-body")).Count > 0 ?
                true : false;
        }
        
        public bool IsOpened(WebDriverWait wait)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("modal-title")));
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

