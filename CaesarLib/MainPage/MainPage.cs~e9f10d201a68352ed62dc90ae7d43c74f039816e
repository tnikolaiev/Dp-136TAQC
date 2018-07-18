using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CaesarLib
{
    public class MainPage
    {
        private IWebElement _profileButton, _logo;
        private IWebDriver _driverInstance;
        private RightMenu _rightMenu;
        private TopMenu _topMenu;
        private LeftMenu _leftMenu;
        private LeftContainer _leftContainer;
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

        public TopMenu TopMenu
        {
            get
            {
                if (_topMenu != null)
                    return _topMenu;
                else
                {
                    return new TopMenu(_driverInstance);
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

        //Is public in case this method will be needed in tests (access modifier can be discussed)
        public TopMenu MoveToTopMenu()
        {
            Actions builder = new Actions(_driverInstance);
            builder.MoveToElement(_driverInstance.FindElement(By.Id("top-menu"))).Build().Perform();
            return new TopMenu(_driverInstance);
        }

        //This method can return new instance of 'Locations' window so change it in case of need
        public void OpenLocationsWindow()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Assert.IsTrue(topMenuInstance.IsOpened(), "Top menu cannot be opened");
            Acts.Click(topMenuInstance.LocationsItem);
        }

        //This method can return new instance of 'Groups' page so change it in case of need
        public void OpenGroupsPage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Assert.IsTrue(topMenuInstance.IsOpened(), "Top menu cannot be opened");
            Acts.Click(topMenuInstance.GroupsItem);
        }

        //This method can return new instance of 'Students' page so change it in case of need
        public void OpenStudentsPage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Assert.IsTrue(topMenuInstance.IsOpened(), "Top menu cannot be opened");
            Acts.Click(topMenuInstance.StudentsItem);
        }

        //This method can return new instance of 'Schedule' page so change it in case of need
        public void OpenSchedulePage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Assert.IsTrue(topMenuInstance.IsOpened(), "Top menu cannot be opened");
            Acts.Click(topMenuInstance.ScheduleItem);
        }

        //This method can return new instance of 'add' page so change it in case of need
        public void OpenAddPage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Assert.IsTrue(topMenuInstance.IsOpened(), "Top menu cannot be opened");
            Acts.Click(topMenuInstance.AddItem);
        }

        //This method can return new instance of 'About' page so change it in case of need
        public void OpenAboutPage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Assert.IsTrue(topMenuInstance.IsOpened(), "Top menu cannot be opened");
            Acts.Click(topMenuInstance.AboutItem);
        }

        public void LogoutUsingTopMenu()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Assert.IsTrue(topMenuInstance.IsOpened(), "Top menu cannot be opened");
            Acts.Click(topMenuInstance.LogoutButton);
        }
    }
}
