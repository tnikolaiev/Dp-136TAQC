using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaesarLib
{
    public class EditStudentListWindow
    {
        IWebDriver webDriver;
        public IWebElement CreateStudentButton { get => webDriver.FindElement(By.ClassName("createStudent")); }
        public IWebElement ImportStudentsButton { get => webDriver.FindElement(By.ClassName("csv-button")); }
        public Table StudentTable { get => new Table(webDriver.FindElement(By.XPath("//*[@id='modal-window']//table"))); }
        public IList<IWebElement> Students { get => StudentTable.GetRows(); }
        public IWebElement SaveFormButton { get => webDriver.FindElement(By.ClassName("save")); }
        public IWebElement ExitFormButton { get => webDriver.FindElement(By.ClassName("exit")); }
        public IWebElement RightshifterButton { get => webDriver.FindElement(By.ClassName("right")); }
        public static int DownloadButtonsColumn { get => 4; }
        public static int EditButtonsColumn { get => 5; }
        public static int DeleteButtonsColumn { get => 6; }
        public IList<IWebElement> DownloadButtons { get => webDriver.FindElements(By.ClassName("download-attachments")); }
        public IList<IWebElement> EditButtons { get => webDriver.FindElements(By.ClassName("editStudent")); }
        public IList<IWebElement> DeleteButtons { get => webDriver.FindElements(By.ClassName("deleteStudent")); }
        public EditStudentListWindow(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public static bool IsOpened(IWebDriver driver)
        {
            if (Acts.IsElementVisible(driver, By.XPath("//*[@id='modal-window']//table")) &&
                driver.FindElements(By.ClassName("createStudent")).Count > 0 &&
                Acts.IsElementVisible(driver,By.ClassName("exit")))
                return true;
            else
                return false;
        }
        public IWebElement GetLastElement(IList<IWebElement> webElements)
        {
            return webElements.Last();
        }
        public static string GetStudentName(IWebElement webElement)
        {
            return webElement.FindElement(By.Name("studName")).Text;
        }
        public static String GetTestFile(String fileName)
        {
            return Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TC_3_06 files\" + fileName);
        }
    }
}
