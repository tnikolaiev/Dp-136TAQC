using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CaesarLib.Schedule
{
    public class ScheduleMonthView 
    {
        private IWebDriver _driverInstance;

        public ScheduleMonthView(IWebDriver driverInstance)
        {
            _driverInstance = driverInstance;
        }
    }
}
