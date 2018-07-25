using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib
{
    public class EditScheduleWindow 
    {
        //fields

        private IWebDriver _driverInstance;
        private IWebElement _saveButton;
        private IWebElement _cancelButton;
        private IWebElement _teacherControl;
        private IWebElement _roomControl;
        private IWebElement _eventControl;
        private IWebElement _lectureEvent;
        private IWebElement _weeklyReportEvent;
        private IWebElement _workWithExpertEvent;
        private IWebElement _consultationEvent;
        private IWebElement _practiceEvent;
        private IWebElement _scheduleWeekTable;
               

        // properties

        public IWebElement SaveButton
        {
            get
            {
                if (_saveButton!= null) return _saveButton;
                else
                {
                    _saveButton = _driverInstance.FindElement(By.Id("save"));
                    return _saveButton;
                }
            }
        }
        public IWebElement CancelButton
        {
            get
            {
                if (_cancelButton != null) return _cancelButton;
                else
                {
                    _cancelButton = _driverInstance.FindElement(By.Id("'cancel'"));
                    return _cancelButton;
                }
            }
        }
        public IWebElement TeacherControl
        {
            get
            {
                if (_teacherControl != null) return _teacherControl;
                else
                {
                    _teacherControl = _driverInstance.FindElement(By.XPath("//select[@name='resourceteacher']"));
                    return _teacherControl;
                }
            }
        }
        public IWebElement RoomControl
        {
            get
            {
                if (_roomControl != null) return _roomControl;
                else
                {
                    _roomControl = _driverInstance.FindElement(By.XPath("//select[@name='room']"));
                    return _roomControl;
                }
            }
        }
        public IWebElement EventControl
        {
            get
            {
                if (_eventControl != null) return _eventControl;
                else
                {
                    _eventControl = _driverInstance.FindElement(By.XPath("//select[@name='event']"));
                    return _eventControl;
                }
            }
        }
        public IWebElement LectureEvent
        {
            get
            {
                if (_lectureEvent != null) return _lectureEvent;
                else
                {
                    _lectureEvent = _driverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Lecture')]"));
                    return _lectureEvent;
                }
            }
        }
        public IWebElement WeeklyReportEvent
        {
            get
            {
                if (_weeklyReportEvent != null) return _weeklyReportEvent;
                else
                {
                    _weeklyReportEvent = _driverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Weekly report')]"));
                    return _weeklyReportEvent;
                }
            }
        }
        public IWebElement WorkWithExpertEvent
        {
            get
            {
                if (_workWithExpertEvent != null) return _workWithExpertEvent;
                else
                {
                    _workWithExpertEvent = _driverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Work with Expert')]"));
                    return _workWithExpertEvent;
                }
            }
        }
        public IWebElement ConsultationEvent
        {
            get
            {
                if (_consultationEvent != null) return _consultationEvent;
                else
                {
                    _consultationEvent = _driverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Consultation')]"));
                    return _consultationEvent;
                }
            }
        }
        public IWebElement PracticeEvent
        {
            get
            {
                if (_practiceEvent != null) return _practiceEvent;
                else
                {
                    _practiceEvent = _driverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Practice')]"));
                    return _practiceEvent;
                }
            }
        }
        public IWebElement ScheduleWeekTable
        {
            get
            {
                if (_scheduleWeekTable != null) return _scheduleWeekTable;
                else
                {
                    _scheduleWeekTable = _driverInstance.FindElement(By.XPath("//table[@class='Table']"));
                    return _scheduleWeekTable;
                }
            }
        }

        //constructor 


        public EditScheduleWindow(IWebDriver driverInstance)
        {
            _driverInstance = driverInstance;
        }



        // actions

        public String GetEventCounter(IWebElement element)
        {
            String counter = element.FindElement(By.CssSelector(".event-score")).Text;

            return counter;
        }

        public bool IsScheduleEditorDisplayed(IWebDriver driverInstance)
        {
            return driverInstance.FindElements(By.ClassName("scheduleEditorWeek-view")).Count > 0 ?
               true : false;
        }





    }
}
