using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class ModalWindow
    {
        private IWebDriver driver;
        private CreateGroupWindow _createGroupWindow;
        private EditScheduleWindow _editScheduleWindow;
        private LocationWindow _locationWindow;

        public CreateGroupWindow CreateGroupWindow
        {
            get
            {
                if (_createGroupWindow != null) return _createGroupWindow;
                else
                {
                    _createGroupWindow = new CreateGroupWindow(driver);
                    return _createGroupWindow;
                }
            }
        }

        public EditScheduleWindow EditScheduleWindow
        {
            get
            {
                if (_editScheduleWindow != null) return _editScheduleWindow;
                else
                {
                    _editScheduleWindow = new EditScheduleWindow(driver);
                    return _editScheduleWindow;
                }
            }
        }

        public LocationWindow LocationWindow
        {
            get
            {
                if (_locationWindow != null) return _locationWindow;
                else
                {
                    _locationWindow = new LocationWindow(driver);
                    return _locationWindow;
                }
            }
        }

        public ModalWindow(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static bool IsModalWindowOpened(IWebDriver driver)
        {
            return driver.FindElements(By.XPath("//div[@id='modal-window']//div")).Count > 0;
        }
    }
}
