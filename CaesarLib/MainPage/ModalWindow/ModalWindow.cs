using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarLib
{
    public class ModalWindow
    {
        private IWebDriver driver;
        private GroupCreateWindow _groupCreateWindow;
        private EditScheduleWindow _editScheduleWindow;
        private LocationWindow _locationWindow;
        private GroupDeleteConfirmationWindow _groupDeleteConfirmationWindow;

        public GroupCreateWindow GroupCreateWindow
        {
            get
            {
                if (_groupCreateWindow != null) return _groupCreateWindow;
                else
                {
                    _groupCreateWindow = new GroupCreateWindow(driver);
                    return _groupCreateWindow;
                }
            }
        }
        private EditStudentListWindow _editStudentListWindow;
        private EditStudentWindow _editStudentWindow;

        public GroupDeleteConfirmationWindow GroupDeleteConfirmationWindow
        {
            get
            {
                if (_groupDeleteConfirmationWindow != null) return _groupDeleteConfirmationWindow;
                else
                {
                    _groupDeleteConfirmationWindow = new GroupDeleteConfirmationWindow(driver);
                    return _groupDeleteConfirmationWindow;
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

        public EditStudentListWindow EditStudentListWindow
        {
            get
            {
                if (_editStudentListWindow != null) return _editStudentListWindow;
                else
                {
                    _editStudentListWindow = new EditStudentListWindow(driver);
                    return _editStudentListWindow;
                }
            }
        }

        public EditStudentWindow EditStudentWindow
        {
            get
            {
                if (_editStudentWindow != null) return _editStudentWindow;
                else
                {
                    _editStudentWindow = new EditStudentWindow(driver);
                    return _editStudentWindow;
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
