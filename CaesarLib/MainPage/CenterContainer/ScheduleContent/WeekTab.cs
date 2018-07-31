using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib
{
    public class WeekTab
    {
        private ScheduleWeekTable _scheduleViewWeekTable;
        private IWebDriver _driverInstance;

        public WeekTab(IWebDriver driverInstance)
        {
            _driverInstance = driverInstance;
        }


        public ScheduleWeekTable ScheduleViewWeekTable
        {
            get
            {
                if (_scheduleViewWeekTable != null) return _scheduleViewWeekTable;
                else
                {
                    _scheduleViewWeekTable = new ScheduleWeekTable(_driverInstance.FindElement(By.XPath("//div[@class='Table']")));
                    return _scheduleViewWeekTable;
                }
            }
        }

            public bool IsWeekTabDisplayed(IWebDriver driverInstance)
        {
            return driverInstance.FindElements(By.ClassName("scheduleWeek-view")).Count > 0 ?
               true : false;
        }
    }


}

