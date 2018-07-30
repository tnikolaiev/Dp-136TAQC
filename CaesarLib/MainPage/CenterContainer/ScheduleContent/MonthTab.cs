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
