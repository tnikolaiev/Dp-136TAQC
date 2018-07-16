using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib.Schedule
{
    public class EditScheduleWindow : MainPage
    {
        //fields
       
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
        private ScheduleWeekView _scheduleWeekView;


        // properties

        public IWebElement SaveButton
        {
            get
            {
                if (_saveButton!= null) return _saveButton;
                else
                {
                    _saveButton = DriverInstance.FindElement(By.XPath("//span[@id='save']"));
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
                    _cancelButton = DriverInstance.FindElement(By.XPath("//span[@id='cancel']"));
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
                    _teacherControl = DriverInstance.FindElement(By.XPath("//select[@name='resourceteacher']"));
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
                    _roomControl = DriverInstance.FindElement(By.XPath("//select[@name='room']"));
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
                    _eventControl = DriverInstance.FindElement(By.XPath("//select[@name='event']"));
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
                    _lectureEvent = DriverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Lecture')]"));
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
                    _weeklyReportEvent = DriverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Weekly report')]"));
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
                    _workWithExpertEvent = DriverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Work with Expert')]"));
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
                    _consultationEvent = DriverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Consultation')]"));
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
                    _practiceEvent = DriverInstance.FindElement(By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Practice')]"));
                    return _practiceEvent;
                }
            }
        }
        public ScheduleWeekView ScheduleWeekViewInstance
        {
            get
            {
                if (_scheduleWeekView != null) return _scheduleWeekView;
                else
                {
                    _scheduleWeekView = new ScheduleWeekView(DriverInstance);
                    return _scheduleWeekView;
                }
            }
        }


        //constructor 

        public EditScheduleWindow(IWebDriver driver) : base(driver)
        {
        }

       
        // actions

        public String getEventCounter(IWebElement element)
        {
            String counter = element.FindElement(By.CssSelector(".event-score")).Text;

            return counter;
        }

        
    }
}
