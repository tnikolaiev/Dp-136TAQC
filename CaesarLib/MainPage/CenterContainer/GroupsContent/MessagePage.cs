using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class MessagePage
    {
        IWebDriver driver;
        IWebElement _message;
        IWebElement _messageView;

        public MessagePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement Message
        {
            get
            {
                if (_message != null) return _message;
                else
                {
                    _message = driver.FindElement(By.XPath("//*[@id='main-section']/div/header/div[2]/button[3]/i"));
                    return _message;
                }
            }
        }
        public IWebElement MessageView
        {
            get
            {
                if (_messageView != null) return _messageView;
                else
                {
                    _messageView = driver.FindElement(By.XPath("//*[@id='main-section']/div/header/div[2]/button[3]/i"));
                    return _messageView;
                }
            }
        }
    }
}
