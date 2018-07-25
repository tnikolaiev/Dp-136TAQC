using CaesarLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
        private EditScheduleWindow _editScheduleWindowInstance;
        private MonthViewTab _scheduleMonthViewInstance;        
        private KeyDatesTab _scheduleKeyDatesInstance;
        private WeekViewTab _weekViewTabInstance;
        private LeftContainer _leftContainerInstance;

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
        public EditScheduleWindow EditScheduleWindowInstance
        {
            get
            {
                if (_editScheduleWindowInstance != null) return _editScheduleWindowInstance;
                else
                {
                    _editScheduleWindowInstance = new EditScheduleWindow(_driverInstance);
                    return _editScheduleWindowInstance;
                }
            }
        }
        public MonthViewTab MonthViewInstance
        {
            get
            {
                if (_scheduleMonthViewInstance != null) return _scheduleMonthViewInstance;
                else
                {
                    _scheduleMonthViewInstance = new MonthViewTab(_driverInstance);
                    return _scheduleMonthViewInstance;
                }
            }
        }

        public WeekViewTab WeekViewTabInstance
        {
            get
            {
                if (_weekViewTabInstance != null) return _weekViewTabInstance;
                else
                {
                    _weekViewTabInstance = new WeekViewTab(_driverInstance);
                    return _weekViewTabInstance;
                }
            }
        }


        public KeyDatesTab KeyDatesTabInstance
        {
            get
            {
                if (_scheduleKeyDatesInstance != null) return _scheduleKeyDatesInstance;
                else
                {
                    _scheduleKeyDatesInstance = new KeyDatesTab(_driverInstance);
                    return _scheduleKeyDatesInstance;
                }
            }
        }

        public LeftContainer LeftContainerInstance
        {
            get
            {
                if (_leftContainerInstance != null) return _leftContainerInstance;
                else
                {
                    _leftContainerInstance = new LeftContainer(_driverInstance);
                    return _leftContainerInstance;
                }
            }
        }

        //Actions

        public EditScheduleWindow ClickCogwheel()
        {
            ScheduleCogwheell.Click();
            return new EditScheduleWindow(_driverInstance);
        }

    }
}

