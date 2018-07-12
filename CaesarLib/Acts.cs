using OpenQA.Selenium;
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
    }
}
