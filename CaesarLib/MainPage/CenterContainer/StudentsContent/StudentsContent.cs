using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CaesarLib
{
    public class StudentsContent
    {
        IWebDriver webDriver;
        public IWebElement EditButton { get => webDriver.FindElement(By.ClassName("editBtn")); }
        public IWebElement StudentsButton { get => webDriver.FindElement(By.Name("students")); }
        public IWebElement ScheduleButton { get => webDriver.FindElement(By.Name("shedule")); }
        public IWebElement MessageButton { get => webDriver.FindElement(By.Name("message")); }
        public Table StudentTable { get => new Table(webDriver.FindElement(By.ClassName("students_list")), webDriver); }
        public StudentsContent(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public static bool IsOpened(IWebDriver driver)
        {
            if (Acts.IsElementExists(driver, By.ClassName("editBtn")) &&
                Acts.IsElementExists(driver, By.Name("students")) &&
                Acts.IsElementExists(driver, By.Name("shedule")) &&
                Acts.IsElementExists(driver, By.Name("message")) &&
                Acts.IsElementExists(driver, By.ClassName("students_list")))
                    return true;
            else return false;
        }
    }
}
