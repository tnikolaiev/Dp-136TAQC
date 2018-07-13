using OpenQA.Selenium;

namespace CaesarLib
{
    public class MainPage
    {
        private IWebElement _profileButton;        
        private IWebDriver _driverInstance;
        private RightMenu _rightMenu;
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