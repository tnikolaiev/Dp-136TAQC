using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class CreateEditStudentsForm : AdminPage
    {
        private IWebDriver _driver;

        private IWebElement _groupId;
        private IWebElement _name;
        private IWebElement _lastName;
        private IWebElement _englishLevel;
        private IWebElement _CvUrl;
        private IWebElement _imageUrl;
        private IWebElement _entryScore;
        private IWebElement _approvedBy;

        public CreateEditStudentsForm(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
        }
        public IWebElement GroupIdField
        {
            get
            {
                if (_groupId != null) return _groupId;
                else
                {
                    _groupId = _driver.FindElement(By.ClassName("groupId"));
                    return _groupId;
                }
            }
        }

        public CreateEditStudentsForm setGroupIdField(string value)
        {
            GroupIdField.SendKeys(value);
            return this;
        }

        public IWebElement NameField
        {
            get
            {
                if (_name != null) return _name;
                else
                {
                    _name = _driver.FindElement(By.ClassName("name"));
                    return _name;
                }
            }
        }

        public CreateEditStudentsForm setNameField(string value)
        {
            NameField.SendKeys(value);
            return this;
        }

        public IWebElement LastNameField
        {
            get
            {
                if (_lastName != null) return _lastName;
                else
                {
                    _lastName = _driver.FindElement(By.ClassName("lastName"));
                    return _lastName;
                }
            }
        }

        public CreateEditStudentsForm setLastNameField(string value)
        {
            LastNameField.SendKeys(value);
            return this;
        }

        public IWebElement EnglishLevelDDL
        {
            get
            {
                if (_englishLevel != null) return _englishLevel;
                else
                {
                    _englishLevel = _driver.FindElement(By.ClassName("englishLevel"));
                    return _englishLevel;
                }
            }
        }

        public CreateEditStudentsForm setEnglishLevelDDL(string value)
        {
            Acts.SelectOptionFromDDL(EnglishLevelDDL, value);
            return this;
        }

        public IWebElement CvUrlField
        {
            get
            {
                if (_CvUrl != null) return _CvUrl;
                else
                {
                    _CvUrl = _driver.FindElement(By.ClassName("CvUrl"));
                    return _CvUrl;
                }
            }
        }

        public CreateEditStudentsForm setCvUrlField(string value)
        {
            CvUrlField.SendKeys(value);
            return this;
        }

        public IWebElement ImageUrlField
        {
            get
            {
                if (_imageUrl != null) return _imageUrl;
                else
                {
                    _imageUrl = _driver.FindElement(By.ClassName("imageUrl"));
                    return _imageUrl;
                }
            }
        }

        public CreateEditStudentsForm setImageUrlField(string value)
        {
            ImageUrlField.SendKeys(value);
            return this;
        }

        public IWebElement EntryScoreField
        {
            get
            {
                if (_entryScore != null) return _entryScore;
                else
                {
                    _entryScore = _driver.FindElement(By.ClassName("entryScore"));
                    return _entryScore;
                }
            }
        }

        public CreateEditStudentsForm setEntryScoreField(string value)
        {
            EntryScoreField.SendKeys(value);
            return this;
        }

        public IWebElement ApprovedByField
        {
            get
            {
                if (_approvedBy != null) return _approvedBy;
                else
                {
                    _approvedBy = _driver.FindElement(By.ClassName("approvedBy"));
                    return _approvedBy;
                }
            }
        }

        public CreateEditStudentsForm setApprovedByField(string value)
        {
            ApprovedByField.SendKeys(value);
            return this;
        }
    }
}