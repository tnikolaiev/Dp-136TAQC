using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Windows.Forms;

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

        public static void SelectElement(IWebElement webElement, int index)
        {
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByIndex(index);
        }
        public static void PressKeyboardButton(string button)
        {
            SendKeys.SendWait(button);
        }
        public static void UploadFile(String path)
        {
            Thread.Sleep(1000);
            SendKeys.SendWait(path);
            Thread.Sleep(1000);
            SendKeys.SendWait(@"{Enter}");
        }

        public static void Wait(WebDriverWait wait, bool сondition)
        {
            wait.Until((d) => сondition);
        }

        public static bool IsElementPresent(IWebDriver driverInstance, By by)
        {    
            return driverInstance.FindElements(by).Count > 0 ? true : false;
        }
    }
}

