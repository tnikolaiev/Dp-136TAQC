using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CaesarLib
{
    public class GroupCreateWindow
    {
        private IWebElement _createGroupWindowInstance;
        private IWebElement _groupNameField;
        private IWebElement _returnNameButton;
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

        public IWebElement CreateGroupWindowInstnace
        {
            get
            {
                if (_createGroupWindowInstance != null) return _createGroupWindowInstance;
                else
                {
                    _createGroupWindowInstance = driver.FindElement(By.XPath("//div[@id='modal-window']//section[@class='modal-window create']"));
                    return _createGroupWindowInstance;
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

        public IWebElement RemoveTeacherButton
        {
            get
            {
                if (_removeTeacherButton != null) return _removeTeacherButton;
                else
                {
                    _removeTeacherButton = driver.FindElement(By.XPath("//span[contains(@class,'remove-teacher')]"));
                    return _removeTeacherButton;
                }
            }
        }

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
                    _teacherDDL = driver.FindElement(By.XPath("//div[@id = 'teachers']//select[@id = 'teachers']"));
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

        public IWebElement GroupNameHint
        {
            get
            {
                return driver.FindElement(By.XPath("//div[@class='row'][1]//div[contains(@class, 'name-wrapper')]//p[@class='hint']"));
            }
        }

        public IWebElement DirectionHint
        {
            get
            {
                return driver.FindElement(By.XPath("//div[@class='row'][2]//p[@class='hint']"));
            }
        }

        public IWebElement StartDateHint
        {
            get
            {
                return driver.FindElement(By.XPath("//div[@class='row'][2]/div[contains (@class, 'calendar-wrapper')]//p[@class='hint']"));
            }
        }

        public IWebElement FinishDateHint
        {
            get
            {
                return driver.FindElement(By.XPath("//div[@class='row'][3]/div[contains(@class, 'calendar-wrapper')]//p[@class='hint']"));
            }
        }

        public IWebElement ExpertHint
        {
            get
            {
                return driver.FindElement(By.XPath("//div[@class='add-expert clearfix']//p[@class='hint']"));
            }
        }

        public GroupCreateWindow SetGroupName(String value)
        {
            GroupNameField.SendKeys(value);
            return this;
        }

        public GroupCreateWindow ReturnNameButtonClick()
        {
            ReturnNameButton.Click();
            return this;
        }

        public GroupCreateWindow SetDirection(String value)
        {
            Acts.SelectOptionFromDDL(DirectionDDL, value);
            return this;
        }

        public GroupCreateWindow AddTeacher(String value)
        {
            AddTeacherButton.Click();
            Acts.SelectOptionFromDDL(TeacherDDL, value);
            AcceptSelectTeacherButton.Click();
            return this;
        }

        public GroupCreateWindow SetBudgetOwner(String value)
        {
            if (value.Equals(value, StringComparison.OrdinalIgnoreCase)) BudgetOwnerSoftServeToggle.Click();
            else BudgetOwnerOpenGroupToggle.Click();
            return this;
        }

        public GroupCreateWindow SetStartDate(String value)
        {
            StartDateField.SendKeys(value);
            return this;
        }

        public GroupCreateWindow SetExpertName(String value)
        {
            ExpertInputField.SendKeys(value);
            return this;
        }

        public GroupCreateWindow AddExpert(String value)
        {
            AddExpertButton.Click();
            ExpertInputField.SendKeys(value);
            AcceptInputExpertButton.Click();
            return this;
        }

        public GroupCreateWindow(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsOpened()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@id='modal-window']//section[@class='modal-window create']"));
        }

        public bool IsGroupNameHintVisible()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@class='form-group name-wrapper col-xs-12']//p[@class='hint']"));
        }

        public bool IsDirectionHintVisible()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@class='row'][2]//p[@class='hint']"));
        }

        public bool IsStartDateHintVisible()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@class='row'][2]/div[contains (@class, 'calendar-wrapper')]//p[@class='hint']"));
        }

        public bool IsFinishDateHintVisible()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@class='row'][3]/div[contains(@class, 'calendar-wrapper')]//p[@class='hint']"));
        }

        public bool IsExpertHintVisible()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@class='add-expert clearfix']//p[@class='hint']"));
        }

        public bool IsCancelButtonVisible()
        {
            return Acts.IsElementVisible(driver, By.Id("cancel"));
        }

        public void Open(Actions action, WebDriverWait wait)
        {
            MainPage mainPage = new MainPage(driver);
            var leftMenu = mainPage.LeftMenu;

            leftMenu.Open(action, wait);
            leftMenu.IsCreateButtonVisible();
            leftMenu.CreateButton.Click();
            wait.Until((d) => IsCancelButtonVisible());
        }

        private IWebElement removeteacher;
        private IWebElement addteacher;
        private IWebElement stageteacher;
        private IWebElement confirmteacher;

        public IWebElement RemoveTeacher
        {
            get
            {
                if (removeteacher != null) return removeteacher;
                else
                {
                    removeteacher = driver.FindElement(By.XPath("//*[@id='teachers']/div/ul/li/span[2]"));
                    return removeteacher;
                }
            }
        }

        public IWebElement AddingTeacher
        {
            get
            {
                if (addteacher != null) return addteacher;
                else
                {
                    addteacher = driver.FindElement(By.XPath("//*[@id='teachers']/div/div/span"));
                    return addteacher;
                }
            }
        }
        public IWebElement StageTeacher
        {
            get
            {
                if (stageteacher != null) return stageteacher;
                else
                {
                    stageteacher = driver.FindElement(By.Name("teacher"));
                    return stageteacher;
                }
            }
        }

        public IWebElement ConfirmTeacher
        {
            get
            {
                if (confirmteacher != null) return confirmteacher;
                else
                {
                    confirmteacher = driver.FindElement(By.Id("acceptSelect"));
                    return confirmteacher;
                }
            }
        }

        public void EditTeacher(string teachername)
        {
            RemoveTeacher.Click();
            AddingTeacher.Click();
            var selTeacher = new SelectElement(StageTeacher);
            selTeacher.SelectByValue(teachername);
            ConfirmTeacher.Click();
        }


        private IWebElement removeexpert;
        private IWebElement addexpert;
        private IWebElement enterexpert;
        private IWebElement confirmexpert;

        public IWebElement RemoveExpert
        {
            get
            {
                if (removeexpert != null) return removeexpert;
                else
                {
                    removeexpert = driver.FindElement(By.XPath("//*[@id='experts']/div/ul/li/span[2]"));
                    return removeexpert;
                }
            }
        }

        public IWebElement AddingExpert
        {
            get
            {
                if (addexpert != null) return addexpert;
                else
                {
                    addexpert = driver.FindElement(By.XPath("//*[@id='experts']/div/div/span"));
                    return addexpert;
                }
            }
        }

        public IWebElement EnterExpert
        {
            get
            {
                if (enterexpert != null) return enterexpert;
                else
                {
                    enterexpert = driver.FindElement(By.Name("expert"));
                    return enterexpert;
                }
            }
        }

        public IWebElement ConfirmExpert
        {
            get
            {
                if (confirmexpert != null) return confirmexpert;
                else
                {
                    confirmexpert = driver.FindElement(By.XPath("//*[@id='acceptInput']"));
                    return confirmexpert;
                }
            }
        }

        public void EditExpert(string expertname)
        {
            RemoveExpert.Click();
            AddingExpert.Click();
            EnterExpert.SendKeys(expertname);
            ConfirmExpert.Click();
        }

        private IWebElement _oneMoreTeacher;
        private IWebElement _stageTeacher;
        private IWebElement _saveTeacher;


        public IWebElement OneMoreTeacher
        {
            get
            {
                if (_oneMoreTeacher != null) return _oneMoreTeacher;
                else
                {
                    _oneMoreTeacher = driver.FindElement(By.XPath("//*[@id='teachers']/div/div/span"));
                    return _oneMoreTeacher;
                }
            }
        }
        public IWebElement SecondStageTeacher
        {
            get
            {
                if (_stageTeacher != null) return _stageTeacher;
                else
                {
                    _stageTeacher = driver.FindElement(By.XPath("//*[@id='teachers']"));
                    return _stageTeacher;
                }
            }
        }
        public IWebElement SaveSecondTeacher
        {
            get
            {
                if (_saveTeacher != null) return _saveTeacher;
                else
                {
                    _saveTeacher = driver.FindElement(By.XPath("//*[@id='acceptSelect']"));
                    return _saveTeacher;
                }
            }
        }

        public void AddSecondTeacher(string teachername)
        {
            OneMoreTeacher.Click();
            var selTeacher = new SelectElement(SecondStageTeacher);
            selTeacher.SelectByValue(teachername);
            SaveSecondTeacher.Click();
        }

        private IWebElement _oneMoreExpert;
        private IWebElement _enterSecondExpert;
        private IWebElement _saveSecondExpert;

        public IWebElement OneMoreExpert
        {
            get
            {
                if (_oneMoreExpert != null) return _oneMoreExpert;
                else
                {
                    _oneMoreExpert = driver.FindElement(By.XPath("//span[@class='add-expert-btn']"));
                    return _oneMoreExpert;
                }
            }
        }

        public IWebElement EnterSecondExpert
        {
            get
            {
                if (_enterSecondExpert != null) return _enterSecondExpert;
                else
                {
                    _enterSecondExpert = driver.FindElement(By.XPath("//input[@name='expert']"));
                    return _enterSecondExpert;
                }
            }
        }

        public IWebElement SaveSecondExpert
        {
            get
            {
                if (_saveSecondExpert != null) return _saveSecondExpert;
                else
                {
                    _saveSecondExpert = driver.FindElement(By.XPath("//*[@id='acceptInput']"));
                    return _saveSecondExpert;
                }
            }
        }

        public void AddSecondExpert(string expertname)
        {
            OneMoreExpert.Click();
            EnterSecondExpert.SendKeys(expertname);
            SaveSecondExpert.Click();
        }

        private IWebElement _deleteSecondTeacher;
        private IWebElement _deleteSecondExpert;

        public IWebElement DeleteSecondTeacher
        {
            get
            {
                if (_deleteSecondTeacher != null) return _deleteSecondTeacher;
                else
                {
                    _deleteSecondTeacher = driver.FindElement(By.XPath("//*[@id='teachers']/div/ul/li[2]/span[2]"));
                    return _deleteSecondTeacher;
                }
            }
        }

        public IWebElement DeleteSecondExpert
        {
            get
            {
                if (_deleteSecondExpert != null) return _deleteSecondExpert;
                else
                {
                    _deleteSecondExpert = driver.FindElement(By.XPath("//*[@id='experts']/div/ul/li[2]/span[2]"));
                    return _deleteSecondExpert;
                }
            }
        }
    }
}
