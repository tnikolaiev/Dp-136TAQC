using OpenQA.Selenium;

namespace CaesarLib
{
    public class MainPage
    {
        private IWebElement _profileButton;        
        private IWebDriver _driverInstance;
        private RightMenu _rightMenu;
        private CenterContainer _centerContainer;

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

        public CenterContainer CenterContainer
        {
            get
            {
                if (_centerContainer != null) return _centerContainer;
                else
                {
                    _centerContainer = new CenterContainer(_driverInstance);
                    return _centerContainer;
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