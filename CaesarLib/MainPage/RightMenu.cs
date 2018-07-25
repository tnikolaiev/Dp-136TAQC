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
        private IWebDriver driver;

        public IWebElement EditButton
        {
            get
            {
                if (_editButton != null) return _editButton;
                else
                {
                    _editButton = driver.FindElement(By.XPath("//div[@id='right-menu']//i[@class='fa fa-cog fa-2x']"));
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
                    _signOutButton = driver.FindElement(By.XPath("//div[@id='right-menu']//i[contains(@class, 'sign-out')]"));
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
                    _rightMenuSection = driver.FindElement(By.Id("right-menu"));
                    return _rightMenuSection;
                }
            }
        }

        public RightMenu(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsLogOutButtonVisible()
        {
            return Acts.IsElementVisible(driver, By.XPath("//div[@id='right-menu']//i[contains(@class, 'sign-out')]"));
        }

        public bool IsOpened()
        {
            return (RightMenuSection.GetAttribute("class").Equals("right-menu open")) ? true : false;
        }

        public void Open(WebDriverWait wait)
        {
            MainPage mainPage = new MainPage(driver);
            wait.Until((d) => Acts.IsElementExists(driver, By.XPath("//div[@id='right-menu']//i[contains(@class, 'sign-out')]")));
            mainPage.ProfileButton.Click();
            wait.Until((d) => IsLogOutButtonVisible());
        }
    }
}
