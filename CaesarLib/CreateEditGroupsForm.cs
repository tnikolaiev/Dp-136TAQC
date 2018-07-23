using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class CreateEditGroupsForm
    {
        private IWebElement _name;
        private IWebElement _location;
        private IWebElement _budgetOwner;
        private IWebElement _direction;
        private IWebElement _startDate;
        private IWebElement _finishDate;
        private IWebElement _teachers;
        private IWebElement _experts;
        private IWebElement _stage;

        private IWebElement _submitButton;
        private IWebElement _editButton;
        private IWebElement _closeButton;
        private IWebElement _close;
        private IWebDriver _driver;

        public CreateEditGroupsForm(IWebDriver driver)
        {
            this._driver = driver;
        }

        public IWebElement NameField
        {
            get
            {
                if (_name != null) return _name;
                else
                {
                    _name = _driver.FindElement(By.Name("name"));
                    return _name;
                }
            }
        }

        public IWebElement LocationDropDown
        {
            get
            {
                if (_location != null) return _location;
                else
                {
                    _location = _driver.FindElement(By.Name("location"));
                    return _location;
                }
            }
        }

        public IWebElement BudgetOwnerCheckbox
        {
            get
            {
                if (_budgetOwner != null) return _budgetOwner;
                else
                {
                    _budgetOwner = _driver.FindElement(By.Name("budgetOwner"));
                    return _budgetOwner;
                }
            }
        }

        public IWebElement DirectionDropDown
        {
            get
            {
                if (_direction != null) return _direction;
                else
                {
                    _direction = _driver.FindElement(By.Name("direction"));
                    return _direction;
                }
            }
        }

        public IWebElement StartDate
        {
            get
            {
                if (_startDate != null) return _startDate;
                else
                {
                    _startDate = _driver.FindElement(By.Name("startDate"));
                    return _startDate;
                }
            }
        }

        public IWebElement FinishDate
        {
            get
            {
                if (_finishDate != null) return _finishDate;
                else
                {
                    _finishDate = _driver.FindElement(By.Name("finishDate"));
                    return _finishDate;
                }
            }
        }

        public IWebElement TeachersField
        {
            get
            {
                if (_teachers != null) return _teachers;
                else
                {
                    _teachers = _driver.FindElement(By.Name("teachers"));
                    return _teachers;
                }
            }
        }

        public IWebElement StageDropDown
        {
            get
            {
                if (_stage != null) return _stage;
                else
                {
                    _stage = _driver.FindElement(By.Name("stage"));
                    return _stage;
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
    }
}
