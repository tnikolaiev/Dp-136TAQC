using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaesarLib.Schedule; 

namespace CaesarLib
{
    public class SchedulePage : MainPage
    {
        //Fields
       
        private IWebElement _scheduleCogwheell;
        private IWebElement _monthButton;
        private IWebElement _weekButton;
        private IWebElement _keyDatesButton;        
        private EditScheduleWindow _editScheduleWindowInstance;
        private ScheduleMonthView _scheduleMonthViewInstance;
        private ScheduleWeekView _scheduleWeekViewInstance;
        private ScheduleKeyDates _scheduleKeyDatesInstance;

        //Constructor  

        public SchedulePage(IWebDriver driver) : base(driver)
        {
        }                

        // Properties

        public IWebElement ScheduleCogwheell
        {
            get
            {
                if (_scheduleCogwheell != null) return _scheduleCogwheell;
                else
                {
                    _scheduleCogwheell = DriverInstance.FindElement(By.XPath("//button[@class='btn editBtn']//i[@class='fa fa-cog fa-2x']"));
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
                    _monthButton = DriverInstance.FindElement(By.XPath("//button[@class='scBtn monthBtne']"));
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
                    _weekButton = DriverInstance.FindElement(By.XPath("//button[@class='scBtn weekBtn']"));
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
                    _keyDatesButton = DriverInstance.FindElement(By.XPath("///button[@class='scBtn keyDatesBtn']"));
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
                    _editScheduleWindowInstance = new EditScheduleWindow(DriverInstance);
                    return _editScheduleWindowInstance;
                }
            }
        }
        public ScheduleMonthView ScheduleMonthViewInstance
        {
            get
            {
                if (_scheduleMonthViewInstance != null) return _scheduleMonthViewInstance;
                else
                {
                    _scheduleMonthViewInstance = new ScheduleMonthView(DriverInstance);
                    return _scheduleMonthViewInstance;
                }
            }
        }
        public ScheduleWeekView ScheduleWeekViewInstance
        {
            get
            {
                if (_scheduleWeekViewInstance != null) return _scheduleWeekViewInstance;
                else
                {
                    _scheduleWeekViewInstance = new ScheduleWeekView(DriverInstance);
                    return _scheduleWeekViewInstance;
                }
            }
        }
        public ScheduleKeyDates ScheduleKeyDatesInstance
        {
            get
            {
                if (_scheduleKeyDatesInstance != null) return _scheduleKeyDatesInstance;
                else
                {
                    _scheduleKeyDatesInstance = new ScheduleKeyDates(DriverInstance);
                    return _scheduleKeyDatesInstance;
                }
            }
        }

        //Actions

        public EditScheduleWindow ClickCogwheel()
        {
            ScheduleCogwheell.Click();
            return new EditScheduleWindow(DriverInstance);
        }

    }

}
