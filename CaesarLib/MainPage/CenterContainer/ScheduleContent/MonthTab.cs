using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class MonthTab 
    {
        private IWebDriver _driverInstance;
        private IWebElement _prevMonthToggle;
        private IWebElement _nextMonthToggle;
        private IWebElement _monthName;

        public IWebElement PrevMonthToggle
        {
            get
            {
                if (_prevMonthToggle != null) return _prevMonthToggle;
                else
                {
                    _prevMonthToggle = _driverInstance.FindElement(By.XPath("//i[@class='fa fa-caret-left prevMonth']"));
                    return _prevMonthToggle;
                }
            }
        }
        public IWebElement NextMonthToggle
        {
            get
            {
                if (_nextMonthToggle != null) return _nextMonthToggle;
                else
                {
                    _nextMonthToggle = _driverInstance.FindElement(By.XPath("//i[@class='fa fa-caret-right nextMonth']"));
                    return _nextMonthToggle;
                }
            }
        }

        public IWebElement MonthName
        {
            get
            {
                if (_monthName != null) return _monthName;
                else
                {
                    _monthName = _driverInstance.FindElement(By.XPath("//div[@class='month-toggle']//p"));
                    return _monthName;
                }
            }
        }


        public MonthTab(IWebDriver driverInstance)
        {
            _driverInstance = driverInstance;
        }

        public bool IsMonthTabDisplayed(IWebDriver driverInstance)
        {
            return driverInstance.FindElements(By.ClassName("calendar-table")).Count > 0 ?
               true : false;
        }

    }
}
