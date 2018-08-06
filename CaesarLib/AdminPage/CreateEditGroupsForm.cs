using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class CreateEditGroupsForm : AdminPage
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
        private IWebDriver _driver;
        private IWebElement _table;
        private IList<IWebElement> _EditButtons;
        private IList<IWebElement> _DeleteButtons;

        public IWebElement GetTable
        {
            get
            {
                if (_table != null) return _table;
                else
                {
                    _table = _driver.FindElement(By.Id("groups"));
                    return _table;
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
        public CreateEditGroupsForm DeleteGroup(int index)
        {
            Delete[index - 1].Click();
            return this;
        }
        public CreateEditGroupsForm EditStudent(int index)
        {
            Edit[index - 1].Click();
            return this;
        }
               

        public CreateEditGroupsForm addGroups()
        {
            AddButtonClick(1);
            return this;
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

        public CreateEditGroupsForm setName(string value)
        {
            NameField.SendKeys(value);
            return this;
        }

        public IWebElement LocationDDL
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

        public CreateEditGroupsForm setLocationDDL(int value)
        {
            Acts.SelectOptionFromDDL(LocationDDL, value);
            return this;
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

        public IWebElement DirectionDDL
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

        public CreateEditGroupsForm setDirectionDDL(int value)
        {
            Acts.SelectOptionFromDDL(DirectionDDL, value);
            return this;
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
        public CreateEditGroupsForm setStartDate(string value)
        {
            StartDate.SendKeys(value);
            return this;
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
        public CreateEditGroupsForm setFinishDate(string value)
        {
            FinishDate.SendKeys(value);
            return this;
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

        public CreateEditGroupsForm setTeachers(string value)
        {
            TeachersField.SendKeys(value);
            return this;
        }

        public IWebElement ExpertsField
        {
            get
            {
                if (_experts != null) return _experts;
                else
                {
                    _experts = _driver.FindElement(By.Name("experts"));
                    return _experts;
                }
            }
        }

        public CreateEditGroupsForm setExperts(string value)
        {
            ExpertsField.SendKeys(value);
            return this;
        }

        public IWebElement StageDDL
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

        public CreateEditGroupsForm setStageDDL(int value)
        {
            Acts.SelectOptionFromDDL(StageDDL, value);
            return this;
        }

        public CreateEditGroupsForm(IWebDriver driver):base(driver)
        {
            this._driver = driver;
        }
        public List<string> RememberGroup()
        {
            List<string> group = new List<string>();
            group.Add(NameField.GetAttribute("value"));
            group.Add(LocationDDL.GetAttribute("value"));
            group.Add(DirectionDDL.GetAttribute("value"));
            group.Add(StartDate.GetAttribute("value"));
            group.Add(FinishDate.GetAttribute("value"));
            group.Add(TeachersField.GetAttribute("value"));
            group.Add(ExpertsField.GetAttribute("value"));
            group.Add(StageDDL.GetAttribute("value"));

            return group;
        }

    }
}