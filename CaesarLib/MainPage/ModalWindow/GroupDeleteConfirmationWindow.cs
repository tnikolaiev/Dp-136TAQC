using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class GroupDeleteConfirmationWindow
    {
        private IWebDriver driver;
        private IWebElement _deleteButton;
        private IWebElement _cancelButton;
        private IWebElement _messageSection;
        private IWebElement _groupDeleteConfirmationWindowInstance;

        public IWebElement GroupDeleteConfirmationWindowInstnace
        {
            get
            {
                if (_groupDeleteConfirmationWindowInstance != null) return _groupDeleteConfirmationWindowInstance;
                else
                {
                    _groupDeleteConfirmationWindowInstance = driver.FindElement(By.XPath("//div[@id='modal-window']//div[@class='modal-body']"));
                    return _groupDeleteConfirmationWindowInstance;
                }
            }
        }

        public IWebElement DeleteButton
        {
            get
            {
                if (_deleteButton != null) return _deleteButton;
                else
                {
                    _deleteButton = driver.FindElement(By.XPath("//button[contains(@class, 'btn-delete')]/i"));
                    return _deleteButton;
                }
            }
        }

        public IWebElement CancelButton
        {
            get
            {
                if (_cancelButton != null) return _cancelButton;
                else
                {
                    _cancelButton = driver.FindElement(By.XPath("//button[contains(@class, 'btn-cancel')]/i"));
                    return _cancelButton;
                }
            }
        }


        public IWebElement MessageSection
        {
            get
            {
                if (_messageSection != null) return _messageSection;
                else
                {
                    _messageSection = driver.FindElement(By.XPath("//div[@class='message-body']/p"));
                    return _messageSection;
                }
            }
        }

        public bool IsCancelButtonVisible()
        {
            return Acts.IsElementVisible(driver, (By.XPath("//button[contains(@class, 'btn-cancel')]/i")));
        }

        public GroupDeleteConfirmationWindow(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsOpened()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@id='modal-window']//div[@class='modal-body']"));
        }
    }
}
