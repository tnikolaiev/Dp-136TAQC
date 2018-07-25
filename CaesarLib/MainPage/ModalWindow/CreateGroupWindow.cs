using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarLib
{
    public class CreateGroupWindow
    {
        private IWebElement _createGroupWindowSection;
        private IWebElement _groupNameField;
        private IWebElement _directionDDL;
        private IWebElement _locationDDL;
        private IWebElement _addTeacherButton;
        private IWebElement _removeTeacherButton;
        private IWebElement _acceptSelectTeacherButton;
        private IWebElement _cancelSelectTeacherButton;
        private IWebElement _teacherDDL;
        private IWebElement _budgetOwnerSoftServeToggle;
        private IWebElement _budgetOwnerOpenGroupToggle;
        private IWebElement _startDateField;
        private IWebElement _finishDateField;
        private IWebElement _addExpertButton;
        private IWebElement _expertInputField;
        private IWebElement _acceptInputExpertButton;
        private IWebElement _cancelInputExpertButton;
        private IWebElement _saveGroupButton;
        private IWebElement _cancelGroupAddingButton;
        private IWebDriver driver;

        public IWebElement CreateGroupWindowSection
        {
            get
            {
                if (_createGroupWindowSection != null) return _createGroupWindowSection;
                else
                {
                    _createGroupWindowSection = driver.FindElement(By.XPath("//div[@id='modal-window']//section[@class='modal-window create']"));
                    return _createGroupWindowSection;
                }
            }
        }

        public IWebElement ReturnNameButton
        {
            get
            {
                if (_returnNameButton != null) return _returnNameButton;
                else
                {
                    _returnNameButton = driver.FindElement(By.XPath("//span[@class='return-name']"));
                    return _returnNameButton;
                }
            }
        }


        public IWebElement GroupNameField
        {
            get
            {
                if (_groupNameField != null) return _groupNameField;
                else
                {
                    _groupNameField = driver.FindElement(By.Name("name"));
                    return _groupNameField;
                }
            }
        }

        public IWebElement DirectionDDL
        {
            get
            {
                if (_directionDDL != null) return _directionDDL;
                else
                {
                    _directionDDL = driver.FindElement(By.Name("direction"));
                    return _directionDDL;
                }
            }
        }

        public IWebElement LocationDDL
        {
            get
            {
                if (_locationDDL != null) return _locationDDL;
                else
                {
                    _locationDDL = driver.FindElement(By.Name("location"));
                    return _locationDDL;
                }
            }
        }

        public IWebElement AddTeacherButton
        {
            get
            {
                if (_addTeacherButton != null) return _addTeacherButton;
                else
                {
                    _addTeacherButton = driver.FindElement(By.ClassName("add-teacher-btn"));
                    return _addTeacherButton;
                }
            }
        }

        //public IWebElement RemoveTeacherButton
        //{
        //    get
        //    {
        //        if (_removeTeacherButton != null) return _removeTeacherButton;
        //        else
        //        {
        //            _removeTeacherButton = driver.FindElement(By.XPath("//*[contains(@class,'remove-teacher')]"));
        //            return _removeTeacherButton;
        //        }
        //    }
        //}

        public IWebElement AcceptSelectTeacherButton
        {
            get
            {
                if (_acceptSelectTeacherButton != null) return _acceptSelectTeacherButton;
                else
                {
                    _acceptSelectTeacherButton = driver.FindElement(By.Id("acceptSelect"));
                    return _acceptSelectTeacherButton;
                }
            }
        }

        public IWebElement CancelSelectTeacherButton
        {
            get
            {
                if (_cancelSelectTeacherButton != null) return _cancelSelectTeacherButton;
                else
                {
                    _cancelSelectTeacherButton = driver.FindElement(By.Id("cancelSelect"));
                    return _cancelSelectTeacherButton;
                }
            }
        }

        public IWebElement TeacherDDL
        {
            get
            {
                if (_teacherDDL != null) return _teacherDDL;
                else
                {
                    _teacherDDL = driver.FindElement(By.Id("teachers"));
                    return _teacherDDL;
                }
            }
        }

        public IWebElement BudgetOwnerSoftServeToggle
        {
            get
            {
                if (_budgetOwnerSoftServeToggle != null) return _budgetOwnerSoftServeToggle;
                else
                {
                    _budgetOwnerSoftServeToggle = driver.FindElement(By.XPath("//button[@data-value='SoftServe']"));
                    return _budgetOwnerSoftServeToggle;
                }
            }
        }

        public IWebElement BudgetOwnerOpenGroupToggle
        {
            get
            {
                if (_budgetOwnerOpenGroupToggle != null) return _budgetOwnerOpenGroupToggle;
                else
                {
                    _budgetOwnerOpenGroupToggle = driver.FindElement(By.XPath("//button[@data-value='OpenGroup']"));
                    return _budgetOwnerOpenGroupToggle;
                }
            }
        }

        public IWebElement StartDateField
        {
            get
            {
                if (_startDateField != null) return _startDateField;
                else
                {
                    _startDateField = driver.FindElement(By.Name("startDate"));
                    return _startDateField;
                }
            }
        }

        public IWebElement FinishDateField
        {
            get
            {
                if (_finishDateField != null) return _finishDateField;
                else
                {
                    _finishDateField = driver.FindElement(By.Name("finishDate"));
                    return _finishDateField;
                }
            }
        }

        public IWebElement AddExpertButton
        {
            get
            {
                if (_addExpertButton != null) return _addExpertButton;
                else
                {
                    _addExpertButton = driver.FindElement(By.ClassName("add-expert-btn"));
                    return _addExpertButton;
                }
            }
        }

        public IWebElement ExpertInputField
        {
            get
            {
                if (_expertInputField != null) return _expertInputField;
                else
                {
                    _expertInputField = driver.FindElement(By.Name("expert"));
                    return _expertInputField;
                }
            }
        }

        public IWebElement AcceptInputExpertButton
        {
            get
            {
                if (_acceptInputExpertButton != null) return _acceptInputExpertButton;
                else
                {
                    _acceptInputExpertButton = driver.FindElement(By.Id("acceptInput"));
                    return _acceptInputExpertButton;
                }
            }
        }

        public IWebElement CancelInputExpertButton
        {
            get
            {
                if (_cancelInputExpertButton != null) return _cancelInputExpertButton;
                else
                {
                    _cancelInputExpertButton = driver.FindElement(By.Id("cancelInput"));
                    return _cancelInputExpertButton;
                }
            }
        }

        public IWebElement SaveGroupButton
        {
            get
            {
                if (_saveGroupButton != null) return _saveGroupButton;
                else
                {
                    _saveGroupButton = driver.FindElement(By.Id("save"));
                    return _saveGroupButton;
                }
            }
        }

        public IWebElement CancelGroupAddingButton
        {
            get
            {
                if (_cancelGroupAddingButton != null) return _cancelGroupAddingButton;
                else
                {
                    _cancelGroupAddingButton = driver.FindElement(By.Id("cancel"));
                    return _cancelGroupAddingButton;
                }
            }
        }

        public CreateGroupWindow(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsCreateGroupWindowOpened()
        {
            return driver.FindElements(By.XPath("//div[@id='modal-window']//section[@class='modal-window create']")).Count > 0;
        }

        public bool IsOpened()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@id='modal-window']//section[@class='modal-window create']"));
        }

        //public Func<IWebDriver, IWebElement> IsStartDateFieldClickable()
        //{
        //    //return ExpectedConditions.ElementToBeClickable(By.Name("name"));
        //    return Acts.IsElementExists(driver, By.Name("name"));
        //}

        public bool IsStartDateFieldClickable()
        {
            //return Acts.IsElementExists(driver, By.Name("name"));
            return Acts.IsElementVisible(driver, By.Id("cancel"));
        }

        public void Open(Actions action, WebDriverWait wait)
        {
            MainPage mainPage = new MainPage(driver);
            var leftMenu = mainPage.LeftMenu;

            leftMenu.Open(action, wait);
            leftMenu.CreateButton.Click();
            wait.Until((d) => IsStartDateFieldClickable());
        }
    }
}
