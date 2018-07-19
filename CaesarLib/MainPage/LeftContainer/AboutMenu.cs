using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarLib
{
    public class AboutMenu
    {
        private IWebDriver driver;

        public AboutMenu(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}