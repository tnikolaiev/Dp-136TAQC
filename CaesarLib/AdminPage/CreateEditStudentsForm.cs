﻿using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class CreateEditStudentsForm
    {
        private IWebElement _submitButton;
        private IWebElement _closeButton;
        private IWebElement _close;
        private IWebDriver _driver;

        private IWebElement _groupId;
        private IWebElement _name;
        private IWebElement _lastName;
        private IWebElement _englishLevel;
        private IWebElement _CvUrl;
        private IWebElement _imageUrl;
        private IWebElement _entryScore;
        private IWebElement _approvedBy;

        public CreateEditStudentsForm(IWebDriver driver)
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

        public IWebElement EnglishLevelDropDown
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

