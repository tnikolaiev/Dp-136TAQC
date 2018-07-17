using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib.StudentsPage
{
    public class EditStudentList
    {
        IWebDriver webDriver;
        public IWebElement ModalWindow { get => webDriver.FindElement(By.Id("modal-window")); }
        public IWebElement CreateStudentButton { get => webDriver.FindElement(By.ClassName("createStudent")); }
        public IWebElement ImportStudentsButton { get => webDriver.FindElement(By.ClassName("csv-button")); }
        public IWebElement StudentTable { get => ModalWindow.FindElement(By.ClassName("students_list")); }
        public IList<IWebElement> Students { get => StudentTable.FindElements(By.TagName("tr")); }
        public IWebElement SaveFormButton { get => webDriver.FindElement(By.ClassName("save")); }
        public IWebElement ExitFormButton { get => webDriver.FindElement(By.ClassName("exit")); }
        public IWebElement RightshifterButton { get => webDriver.FindElement(By.ClassName("right")); }
        public IList<IWebElement> DownloadButtons { get => webDriver.FindElements(By.ClassName("download-attachments")); }
        public IList<IWebElement> EditButtons { get => webDriver.FindElements(By.ClassName("editStudent")); }
        public IList<IWebElement> DeleteButtons { get => webDriver.FindElements(By.ClassName("deleteStudent")); }
        public EditStudentList(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public static bool IsEditStudentList(IWebDriver driver)
        {
            if (driver.FindElement(By.Id("modal-window")).FindElements(By.ClassName("students_list")).Count > 0 &&
                driver.FindElements(By.ClassName("createStudent")).Count > 0 &&
                driver.FindElements(By.ClassName("exit")).Count > 0)
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
    }
}
