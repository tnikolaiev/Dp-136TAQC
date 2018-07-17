using OpenQA.Selenium;

namespace CaesarLib
{
    public class MainPage
    {
        private IWebElement _profileButton, _logo;
        private IWebDriver _driverInstance;
        private RightMenu _rightMenu;
        private LeftMenu _leftMenu;
        private LeftContainer _leftContainer;

        public IWebElement ProfileButton
        {
            get
            {
                if (_profileButton != null) return _profileButton;
                else
                {
                    _profileButton = _driverInstance.FindElement(By.XPath("//*[@id='icon']//img[@class='img-circle']"));
                    return _profileButton;
                }
            }
        }

        public IWebElement Logo
        {
            get
            {
                if (_logo != null) return _logo;
                else
                {
                    _logo = _driverInstance.FindElement(By.CssSelector("#logo > a > img"));
                    return _logo;
                }
            }
        }

        public RightMenu RightMenu
        {
            get
            {
                if (_rightMenu != null) return _rightMenu;
                else
                {
                    _rightMenu = new RightMenu(_driverInstance);
                    return _rightMenu;
                }
            }
        }

        public LeftMenu LeftMenu
        {
            get
            {
                if (_leftMenu != null) return _leftMenu;
                else
                {
                    _leftMenu = new LeftMenu(_driverInstance);
                    return _leftMenu;
                }
            }
        }

        public LeftContainer LeftContainer
        {
            get
            {
                if (_leftContainer != null) return _leftContainer;
                else
                {
                    _leftContainer = new LeftContainer(_driverInstance);
                    return _leftContainer;
                }
            }
        }
      
        public MainPage(IWebDriver driver)
        {
            _driverInstance = driver;
        }

        public static bool IsMainPage(IWebDriver driver)
        {
            return driver.FindElements(By.Id("main-section")).Count > 0 &&
                driver.FindElements(By.Id("left-side-bar")).Count > 0 &&
                driver.FindElements(By.Id("right-side-bar")).Count > 0 ?
                true : false;
        }
    }
}