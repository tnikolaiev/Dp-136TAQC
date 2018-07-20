using OpenQA.Selenium;
using System.Linq;

namespace CaesarLib.StudentsPage
{
    public class GroupView
    {
        IWebDriver webDriver;
        public IWebElement EditButton { get => webDriver.FindElement(By.ClassName("editBtn")); }
        public IWebElement StudentsButton { get => webDriver.FindElement(By.Name("students")); }
        public IWebElement ScheduleButton { get => webDriver.FindElement(By.Name("shedule")); }
        public IWebElement MessageButton { get => webDriver.FindElement(By.Name("message")); }
        public IWebElement StudentsList { get => webDriver.FindElement(By.ClassName("students_list")); }
        public GroupView(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public static bool IsGroupView(IWebDriver driver)
        {
            if (driver.FindElements(By.ClassName("editBtn")).Count > 0 &&
                driver.FindElements(By.Name("students")).Count > 0 &&
                driver.FindElements(By.Name("shedule")).Count > 0 &&
                driver.FindElements(By.Name("message")).Count > 0 &&
                driver.FindElements(By.ClassName("students_list")).Count > 0)
                return true;
            else
                return false;
        }
    }
}
