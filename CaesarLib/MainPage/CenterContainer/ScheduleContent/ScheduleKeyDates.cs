using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class ScheduleKeyDates 
    {

        private IWebDriver _driverInstance;

        public ScheduleKeyDates(IWebDriver driverInstance)
        {
            _driverInstance = driverInstance;
        }

        public bool IsKeyDatesDisplayed(IWebDriver driverInstance)
        {
            return driverInstance.FindElements(By.ClassName("keydates-schedule")).Count > 0 ?
               true : false;
        }
    }
}
