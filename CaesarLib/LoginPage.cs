using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class LoginPage
    {
        private IWebElement _loginField;
        private IWebElement _passwordField;
        private IWebElement _loginButton;
        private IWebElement _messageField;
        private IWebDriver driver;

        public IWebElement LoginField
        {
            get
            {
                if (_loginField != null) return _loginField;
                else
                {
                    _loginField = driver.FindElement(By.Name("login"));
                    return _loginField;
                }
            }
        }

        public IWebElement PasswordField
        {
            get
            {
                if (_passwordField != null) return _passwordField;
                else
                {
                    _passwordField = driver.FindElement(By.Name("password"));
                    return _passwordField;
                }
            }
        }

        public IWebElement LoginButton
        {
            get
            {
                if (_loginButton != null) return _loginButton;
                else
                {
                    _loginButton = driver.FindElement(By.XPath("//*[@class='login']/button[@class='submit fa fa-check-circle-o fa-3x']"));
                    return _loginButton;
                }
            }
        }

        public IWebElement MessageField
        {
            get
            {
                if (_messageField != null) return _messageField;
                else
                {
                    _messageField = driver.FindElement(By.XPath("//*[@class='login']/span[@class='message']"));
                    return _messageField;
                }
            }
        }

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void LogIn(String login, String password)
        {
            LoginField.SendKeys(login);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }

        public void LogIn(String login, String password, WebDriverWait wait)
        {
            wait.Until((d) => IsLoginPageOpened(d));
            LoginField.SendKeys(login);
            PasswordField.SendKeys(password);
            LoginButton.Click();
            wait.Until((d) => MainPage.IsMainPageOpened(d));
        }

        public static bool IsLoginPageOpened(IWebDriver driver)
        {
            return driver.FindElements(By.Name("login")).Count > 0 &
                driver.FindElements(By.Name("password")).Count > 0 &
                driver.FindElements(By.XPath("//*[@class='login']/button[contains(@class, 'submit')]")).Count > 0 ?
                true : false;
        }
    }
}