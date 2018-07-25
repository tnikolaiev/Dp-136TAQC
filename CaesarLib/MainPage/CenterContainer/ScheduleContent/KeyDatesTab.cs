using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class KeyDatesTab
    {

        private IWebDriver _driverInstance;
        private IWebElement _keyDatesTable;

        public IWebElement KeyDatesTable
        {
            get
            {
                if (_keyDatesTable != null) return _keyDatesTable;
                else
                {
                    _keyDatesTable = _driverInstance.FindElement(By.XPath("//table[@class='keydates-schedule']"));
                    return _keyDatesTable;
                }
            }
        }

        public KeyDatesTab(IWebDriver driverInstance)
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

