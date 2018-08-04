using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace CaesarLib
{
    public class ScheduleContent
    {

        //Fields

        private IWebDriver _driverInstance;
        private IWebElement _scheduleCogwheell;
        private IWebElement _monthButton;
        private IWebElement _weekButton;
        private IWebElement _keyDatesButton;
        private WeekTab _weekTabInstance;
        private MonthTab _monthTabInstance;        
        private KeyDatesTab _keyDatesTabInstance;       
        private EditScheduleWindow _editScheduleWindowInstance;

        //Constructor  

        public ScheduleContent(IWebDriver driverInstance)
        {
            _driverInstance = driverInstance;
        }


        // Properties

        public IWebElement ScheduleCogwheell
        {
            get
            {
                if (_scheduleCogwheell != null) return _scheduleCogwheell;
                else
                {
                    _scheduleCogwheell = _driverInstance.FindElement(By.XPath("//button[@class='btn editBtn']//i[@class='fa fa-cog fa-2x']"));
                    return _scheduleCogwheell;
                }
            }
        }
        public IWebElement MonthButton
        {
            get
            {
                if (_monthButton != null) return _monthButton;
                else
                {
                    _monthButton = _driverInstance.FindElement(By.XPath("//button[contains(@class,'monthBtn')]"));
                    return _monthButton;
                }
            }
        }
        public IWebElement WeekButton
        {
            get
            {
                if (_weekButton != null) return _weekButton;
                else
                {
                    _weekButton = _driverInstance.FindElement(By.XPath("//button[contains(@class,'weekBtn')]"));
                    return _weekButton;
                }
            }
        }
        public IWebElement KeyDatesButton
        {
            get
            {
                if (_keyDatesButton != null) return _keyDatesButton;
                else
                {
                    _keyDatesButton = _driverInstance.FindElement(By.XPath("//button[contains(@class,'keyDatesBtn')]"));
                    return _keyDatesButton;
                }
            }
        }
        
        public MonthTab MonthTabInstance
        {
            get
            {
                if (_monthTabInstance != null) return _monthTabInstance;
                else
                {
                    _monthTabInstance = new MonthTab(_driverInstance);
                    return _monthTabInstance;
                }
            }
        }

        public WeekTab WeekTabInstance
        {
            get
            {
                if (_weekTabInstance != null) return _weekTabInstance;
                else
                {
                    _weekTabInstance = new WeekTab(_driverInstance);
                    return _weekTabInstance;
                }
            }
        }

        public KeyDatesTab KeyDatesTabInstance
        {
            get
            {
                if (_keyDatesTabInstance != null) return _keyDatesTabInstance;
                else
                {
                    _keyDatesTabInstance = new KeyDatesTab(_driverInstance);
                    return _keyDatesTabInstance;
                }
            }
        }

        public EditScheduleWindow EditScheduleWindowInstance
        {
            get
            {
                
                    _editScheduleWindowInstance = new EditScheduleWindow(_driverInstance);
                    return _editScheduleWindowInstance;
            }
        }
        

        //Actions

        public EditScheduleWindow ClickCogwheel(WebDriverWait wait)
        {
            ScheduleCogwheell.Click();
            wait.Until((d) => EditScheduleWindowInstance.IsScheduleEditorDisplayed(_driverInstance));
            return new EditScheduleWindow(_driverInstance);
        }

        public WeekTab OpenWeekTab(WebDriverWait wait)
        {            
            WeekButton.Click();
            wait.Until((d) => WeekTabInstance.IsWeekTabDisplayed(_driverInstance));
            return new WeekTab(_driverInstance);
        }

        public KeyDatesTab OpenKeyDatesTab(WebDriverWait wait)
        {
            KeyDatesButton.Click();
            wait.Until((d) =>KeyDatesTabInstance.IsKeyDatesDisplayed(_driverInstance));
            return new KeyDatesTab(_driverInstance);
        }

    }
}

