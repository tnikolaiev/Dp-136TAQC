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

        IWebElement _cogWheel;
        IWebElement _infoTab;
        IWebElement _studentsTab;
        IWebElement _scheduleTab;
        IWebElement _messageTab;
        InfoPage _infoPage;
        MessagePage _messagePage;


        public IWebElement CogWheel
        {
            get
            {
                if (_cogWheel != null) return _cogWheel;
                else
                {
                    _cogWheel = driver.FindElement(By.XPath("//*[@id='main-section']/div/header/div[1]/button/i"));
                    return _cogWheel;
                }
            }
        }

        public IWebElement InfoTab
        {
            get
            {
                if (_infoTab != null) return _infoTab;
                else
                {
                    _infoTab = driver.FindElement(By.Name("info"));
                    return _infoTab;
                }
            }
        }

        public IWebElement StudentsTab
        {
            get
            {
                if (_studentsTab != null) return _studentsTab;
                else
                {
                    _studentsTab = driver.FindElement(By.Name("students"));
                    return _studentsTab;
                }
            }
        }

        public IWebElement ScheduleTab
        {
            get
            {
                if (_scheduleTab != null) return _scheduleTab;
                else
                {
                    _scheduleTab = driver.FindElement(By.Name("schedule"));
                    return _scheduleTab;
                }
            }
        }

        public IWebElement MessageTab
        {
            get
            {
                if (_messageTab != null) return _messageTab;
                else
                {
                    _messageTab = driver.FindElement(By.Name("message"));
                    return _messageTab;
                }
            }
        }

        public InfoPage InfoPage
        {
            get
            {
                if (_infoPage != null) return _infoPage;
                else
                {
                    _infoPage = new InfoPage(driver);
                    return _infoPage;
                }
            }
        }

        public MessagePage MessagePage
        {
            get
            {
                if (_messagePage != null) return _messagePage;
                else
                {
                    _messagePage = new MessagePage(driver);
                    return _messagePage;
                }
            }
        }


        public bool isCogwheelAvailable()
        {
            return driver.FindElements(By.XPath("//*[@id='main-section']/div/header/div[1]/button/i")).Count > 0 ?
                true : false;
        }

    }
}
