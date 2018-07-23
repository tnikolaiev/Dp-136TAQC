using System;
using System.Collections.Generic;
using OpenQA.Selenium;


namespace CaesarLib
{
    public class CreateEditUsersForm
    {
        private IWebElement _firstName;
        private IWebElement _lastName;
        private IWebElement _role;
        private IWebElement _location;
        private IWebElement _photo;
        private IWebElement _login;
        private IWebElement _password;
        private IWebElement _submitButton;
        private IWebElement _closeButton;
        private IWebElement _close;
        private IWebDriver _driver;



        public IWebElement FirstNameField
        {
            get
            {
                if (_firstName != null) return _firstName;
                else
                {
                    _firstName = _driver.FindElement(By.Name("firstName"));
                    return _firstName;
                }
            }
        }

        public IWebElement LastNameField
        {
            get
            {
                if (_lastName != null) return _lastName;
                else
                {
                    _lastName = _driver.FindElement(By.Name("lastName"));
                    return _lastName;
                }
            }
        }

        public IWebElement Role
        {
            get
            {
                if (_role != null) return _role;
                else
                {
                    _role = _driver.FindElement(By.Name("role"));
                    return _role;
                }
            }
        }

        public IWebElement Location
        {
            get
            {
                if (_location != null) return _location;
                else
                {
                    _location = _driver.FindElement(By.Name("location"));
                    return _location;
                }
            }
        }

        public IWebElement Photo
        {
            get
            {
                if (_photo != null) return _photo;
                else
                {
                    _photo = _driver.FindElement(By.Name("photo"));
                    return _photo;
                }
            }
        }
        public IWebElement Login
        {
            get
            {
                if (_login != null) return _login;
                else
                {
                    _login = _driver.FindElement(By.Name("login"));
                    return _login;
                }
            }
        }

        public IWebElement Password
        {
            get
            {
                if (_password != null) return _password;
                else
                {
                    _password = _driver.FindElement(By.Name("password"));
                    return _password;
                }
            }
        }

        public IWebElement SubmitButton
        {
            get
            {
                if (_submitButton != null) return _submitButton;
                else
                {
                    _submitButton = _driver.FindElement(By.ClassName("btn-primary"));
                    return _submitButton;
                }
            }
        }

        public IWebElement CloseButton
        {
            get
            {
                if (_closeButton != null) return _closeButton;
                else
                {
                    _closeButton = _driver.FindElement(By.ClassName("btn-default"));
                    return _closeButton;
                }
            }
        }

        public IWebElement Close
        {
            get
            {
                if (_close != null) return _close;
                else
                {
                    _close = _driver.FindElement(By.ClassName("close"));
                    return _close;
                }
            }
        }

        public CreateEditUsersForm(IWebDriver driver)
        {
            _driver = driver;
        }

    }
}
