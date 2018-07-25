using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib
{
    public class WeekViewTab
    {
        private IWebDriver _driverInstance;
        private ScheduleWeekTable _scheduleWeekTable;

        public ScheduleWeekTable ScheduleWeekTable
        {
            get
            {
                if (_scheduleWeekTable != null) return _scheduleWeekTable;
                else
                {

                    _scheduleWeekTable = new ScheduleWeekTable(_driverInstance);
                     return _scheduleWeekTable;

                  
                }
            }             
        }

      
        public WeekViewTab(IWebDriver driverInstance)
        {
            _driverInstance = driverInstance;
        }

        public bool IsScheduleWeekViewDisplayed(IWebDriver driverInstance)
        {
            return driverInstance.FindElements(By.ClassName("scheduleContainer")).Count > 0 ?
               true : false;
        }

    }
}
