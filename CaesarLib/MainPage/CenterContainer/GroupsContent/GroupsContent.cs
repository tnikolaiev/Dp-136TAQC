using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CaesarLib
{
    public class GroupsContent
    {
        private IWebDriver driver;

        public GroupsContent(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
