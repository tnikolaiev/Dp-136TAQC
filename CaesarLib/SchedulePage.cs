﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaesarLib.Schedule; 

namespace CaesarLib
{
    public class SchedulePage 
    {
        //Fields

        private IWebDriver _driverInstance;
        private IWebElement _scheduleCogwheell;
        private IWebElement _monthButton;
        private IWebElement _weekButton;
        private IWebElement _keyDatesButton;        
        private EditScheduleWindow _editScheduleWindowInstance;
        private ScheduleMonthView _scheduleMonthViewInstance;
        private ScheduleWeekViewAndEdit _scheduleWeekViewInstance;
        private ScheduleKeyDates _scheduleKeyDatesInstance;
        private LeftContainer _leftContainerInstance;

        //Constructor  

        public SchedulePage(IWebDriver driverInstance)
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
        public ScheduleMonthView ScheduleMonthViewInstance
        {
            get
            {
                if (_scheduleMonthViewInstance != null) return _scheduleMonthViewInstance;
                else
                {
                    _scheduleMonthViewInstance = new ScheduleMonthView(_driverInstance);
                    return _scheduleMonthViewInstance;
                }
            }
        }
        public ScheduleWeekViewAndEdit ScheduleWeekViewAndEditInstance
        {
            get
            {
                if (_scheduleWeekViewInstance != null) return _scheduleWeekViewInstance;
                else
                {
                    _scheduleWeekViewInstance = new ScheduleWeekViewAndEdit(_driverInstance);
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
                    _scheduleKeyDatesInstance = new ScheduleKeyDates(_driverInstance);
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
