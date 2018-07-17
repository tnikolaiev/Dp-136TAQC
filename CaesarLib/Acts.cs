using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public static class Acts
    {
        public static void Click(IWebElement element)
        {
            element.Click();
        }

        public static void InputValue(IWebElement element, String value)
        {
            element.SendKeys(value);
        }

        public static void Clear(IWebElement element)
        {
            element.Clear();
        }

        public static String GetAttribute(IWebElement element, String attribute)
        {
            return element.GetAttribute(attribute);
        }


        public static void SelectOptionFromDDL(IWebElement element, String value)
        {
            SelectElement DropDownList = new SelectElement(element);

            DropDownList.SelectByValue(value);

        }
    }
}
