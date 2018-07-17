using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class RightMenu
    {
        private IWebElement _editButton;
        private IWebElement _signOutButton;
        private IWebElement _rightMenuSection;
        private IWebDriver _driverInstance;

        public IWebElement EditButton
        {
            get
            {
                if (_editButton != null) return _editButton;
                else
                {
                    _editButton = _driverInstance.FindElement(By.XPath("//*[@id='right-menu']//i[@class='fa fa-cog fa-2x']"));
                    return _editButton;
                }
            }
        }

        public IWebElement SignOutButton
        {
            get
            {
                if (_signOutButton != null) return _signOutButton;
                else
                {
                    _signOutButton = _driverInstance.FindElement(By.XPath("//*[@id='right-menu']//i[@class='fa fa-sign-out fa-3x']"));
                    return _signOutButton;
                }
            }
        }

        public IWebElement RightMenuSection
        {
            get
            {
                if (_rightMenuSection != null) return _rightMenuSection;
                else
                {
                    _rightMenuSection = _driverInstance.FindElement(By.Id("right-menu"));
                    return _rightMenuSection;
                }
            }
        }

        public RightMenu(IWebDriver driver)
        {
            _driverInstance = driver;
        }

        public Func<IWebDriver, IWebElement> IsLogOutButtonClickable()
        {
            return ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='right-menu']//i[contains(@class, 'sign-out')]"));
        }

        public bool IsOpened()
        {
            return (RightMenuSection.GetAttribute("class").Equals("right-menu open")) ? true : false;
        }
    }
}
