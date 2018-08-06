using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
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

        public static void SelectOptionFromDDL(IWebElement element, int index)
        {
            SelectElement DropDownList = new SelectElement(element);
            DropDownList.SelectByIndex(index);
        }

        public static void PressKeyboardButton(string button)
        {
            SendKeys.SendWait(button);
        }

        public static void UploadFile(String path)
        {
            Thread.Sleep(500);
            SendKeys.SendWait(path);
            Thread.Sleep(500);
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

        public static bool IsAlertPresent(WebDriverWait wait)
        {
            try
            {
                wait.Until(ExpectedConditions.AlertIsPresent());
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsElementVisible(IWebDriver driver, By byExpression)
        {
            try
            {
                if (driver.FindElement(byExpression).Displayed) return true;
                else return false;
            }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
        }

        public static bool IsElementExists(IWebDriver driver, By byExpression)
        {
            try
            {
                driver.FindElement(byExpression);
                return true;
            }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
        }

        public static void DropFile(IWebElement target, string filePath, double offsetX = 0, double offsetY = 0)
        {
            const string JS_DROP_FILE = "for(var b=arguments[0],k=arguments[1],l=arguments[2],c=b.ownerDocument,m=0;;)" +
                "{var e=b.getBoundingClientRect(),g=e.left+(k||e.width/2),h=e.top+(l||e.height/2),f=c.elementFromPoint(g,h);" +
                "if(f&&b.contains(f))break;if(1<++m)throw b=Error('Element not interractable')," +
                "b.code=15,b;b.scrollIntoView({behavior:'instant',block:'center',inline:'center'})}var a=c.createElement('INPUT');" +
                "a.setAttribute('type','file');a.setAttribute('style','position:fixed;z-index:2147483647;left:0;top:0;');" +
                "a.onchange=function(){var b={effectAllowed:'all',dropEffect:'none',types:['Files'],files:this.files," +
                "setData:function(){},getData:function(){},clearData:function(){},setDragImage:function(){}};window." +
                "DataTransferItemList&&(b.items=Object.setPrototypeOf([Object.setPrototypeOf" +
                "({kind:'file',type:this.files[0].type,file:this.files[0],getAsFile:function(){return this.file}," +
                "getAsString:function(b){var a=new FileReader;a.onload=function(a){b(a.target.result)};" +
                "a.readAsText(this.file)}},DataTransferItem.prototype)],DataTransferItemList.prototype));" +
                "Object.setPrototypeOf(b,DataTransfer.prototype);['dragenter','dragover','drop'].forEach(function(a)" +
                "{var d=c.createEvent('DragEvent');d.initMouseEvent(a,!0,!0,c.defaultView,0,0,0,g,h,!1,!1,!1,!1,0,null);" +
                "Object.setPrototypeOf(d,null);d.dataTransfer=b;Object.setPrototypeOf(d,DragEvent.prototype);" +
                "f.dispatchEvent(d)});a.parentElement.removeChild(a)};c.documentElement.appendChild(a);a.getBoundingClientRect();" +
                "return a;";

            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            IWebDriver driver = ((RemoteWebElement)target).WrappedDriver;
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            IWebElement input = (IWebElement)jse.ExecuteScript(JS_DROP_FILE, target, offsetX, offsetY);
            input.SendKeys(filePath);
        }
    }
}

