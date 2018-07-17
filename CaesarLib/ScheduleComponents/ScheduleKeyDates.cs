using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CaesarLib.Schedule
{
    public class ScheduleKeyDates 
    {

        private IWebDriver _driverInstance;

        public ScheduleKeyDates(IWebDriver driverInstance)
        {
            _driverInstance = driverInstance;
        }
    }
}
