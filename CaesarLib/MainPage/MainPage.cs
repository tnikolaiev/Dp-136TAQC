using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CaesarLib
{
    public class MainPage
    {
        private IWebElement _profileButton, _logo;
        private RightMenu _rightMenu;
        private TopMenu _topMenu;
        private LeftMenu _leftMenu;
        private LeftContainer _leftContainer;
        private CenterContainer _centerContainer;
        private ModalWindow _modalWindow;
        private IWebDriver driver;

        public IWebElement ProfileButton
        {
            get
            {
                if (_profileButton != null) return _profileButton;
                else
                {
                    _profileButton = driver.FindElement(By.XPath("//div[@id='icon']//img[@class='img-circle']"));
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
                    _logo = driver.FindElement(By.CssSelector("#logo > a > img"));
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
                    _rightMenu = new RightMenu(driver);
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
                    return new TopMenu(driver);
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
                    _leftMenu = new LeftMenu(driver);
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
                    _leftContainer = new LeftContainer(driver);
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
                    _centerContainer = new CenterContainer(driver);
                    return _centerContainer;
                }
            }
        }

        public ModalWindow ModalWindow
        {
            get
            {
                if (_modalWindow != null) return _modalWindow;
                else
                {
                    _modalWindow = new ModalWindow(driver);
                    return _modalWindow;
                }
            }
        }

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static bool IsMainPageOpened(IWebDriver driver)
        {
            return Acts.IsElementExists(driver, By.Id("main-section")) &
                Acts.IsElementExists(driver, By.Id("left-side-bar")) &
                Acts.IsElementExists(driver, By.Id("right-side-bar")) ?
                true : false;
        }

        public bool IsMainPageOpened()
        {
            return Acts.IsElementExists(driver, By.Id("main-section")) &
                Acts.IsElementExists(driver, By.CssSelector("#logo > a > img"));
        }

        //Is public in case this method will be needed in tests (access modifier can be discussed)
        public TopMenu MoveToTopMenu()
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.Id("top-menu"))).Build().Perform();
            //if (!TopMenu.IsOpened())
            //{
            //    _driverInstance.FindElement(By.ClassName("cancel")).Click();
            //}
            return new TopMenu(driver);
        }

        //This method can return new instance of 'Locations' window so change it in case of need
        public void OpenLocationsWindow()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Acts.Click(topMenuInstance.LocationsItem);
        }

        //This method can return new instance of 'Groups' page so change it in case of need
        public void OpenGroupsPage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Acts.Click(topMenuInstance.GroupsItem);
        }

        //This method can return new instance of 'Students' page so change it in case of need
        public void OpenStudentsPage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Acts.Click(topMenuInstance.StudentsItem);
        }

        //This method can return new instance of 'Schedule' page so change it in case of need
        public void OpenSchedulePage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Acts.Click(topMenuInstance.ScheduleItem);
        }

        //This method can return new instance of 'add' page so change it in case of need
        public void OpenAddPage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Acts.Click(topMenuInstance.AddItem);
        }

        //This method can return new instance of 'About' page so change it in case of need
        public void OpenAboutPage()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Acts.Click(topMenuInstance.AboutItem);
        }

        public void LogoutUsingTopMenu()
        {
            TopMenu topMenuInstance = MoveToTopMenu();
            Acts.Click(topMenuInstance.LogoutButton);
        }

        public CenterContainer MoveToCenterContainer()
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.ClassName("groupLocation"))).Build().Perform();
            return new CenterContainer(driver);
        }
    }
}
