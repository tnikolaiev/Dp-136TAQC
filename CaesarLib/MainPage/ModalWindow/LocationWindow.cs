using System;
using OpenQA.Selenium;
using System.Collections.Generic;


namespace CaesarLib
{
    public class LocationWindow
    {
        private IWebElement _groupOfLocations;
        private IWebElement _cityChernivtsy;
        private IWebElement _cityDnipro;
        private IWebElement _cityIvanoFrankivsk;
        private IWebElement _cityKyiv;
        private IWebElement _cityLviv;
        private IWebElement _cityRivne;
        private IWebElement _citySofia;
        private IWebElement _confurmButton;
        private IWebElement _cancelButton;
        private IWebDriver _driverInstance;
        private IWebElement _clickActiveButtonNames;


        public IWebElement GroupOfLocations
        {
            get
            {
                if (_groupOfLocations != null) return _groupOfLocations;
                else
                {
                    _groupOfLocations = _driverInstance.FindElement(By.Name("location-wrapper"));
                    return _groupOfLocations;
                }
            }
        }

        public IWebElement CityChernivtsy
        {
            get
            {
                if (_cityChernivtsy != null) return _cityChernivtsy;
                else
                {
                    _cityChernivtsy = _driverInstance.FindElement(By.XPath("//p[contains(text(),'Chernivtsy')]"));
                    return _cityChernivtsy;
                }
            }
        }

        public IWebElement CityDnipro
        {
            get
            {
                if (_cityDnipro != null) return _cityDnipro;
                else
                {
                    _cityDnipro = _driverInstance.FindElement(By.XPath("//p[contains(text(),'Dnipro')]"));
                    return _cityDnipro;
                }
            }
        }

        public IWebElement CityIvanoFrankivsk
        {
            get
            {
                if (_cityIvanoFrankivsk != null) return _cityIvanoFrankivsk;
                else
                {
                    _cityIvanoFrankivsk = _driverInstance.FindElement(By.XPath("//p[contains(text(),'Ivano-Frankivsk')]"));
                    return _cityIvanoFrankivsk;
                }
            }
        }

        public IWebElement CityKyiv
        {
            get
            {
                if (_cityKyiv != null) return _cityKyiv;
                else
                {
                    _cityKyiv = _driverInstance.FindElement(By.XPath("//p[contains(text(),'Kyiv')]"));
                    return _cityKyiv;
                }
            }
        }

        public IWebElement CityLviv
        {
            get
            {
                if (_cityLviv != null) return _cityLviv;
                else
                {
                    _cityLviv = _driverInstance.FindElement(By.XPath("//p[contains(text(),'Lviv')]"));
                    //"//body//div[@id='modal-window']//div[@class='locationsWindow']//div[@class='location-wrapper']//div//ul//li[5]"));
                    return _cityLviv;
                }
            }
        }

        public IWebElement CityRivne
        {
            get
            {
                if (_cityRivne != null) return _cityRivne;
                else
                {
                    _cityRivne = _driverInstance.FindElement(By.XPath("//p[contains(text(),'Rivne')]"));
                    return _cityRivne;
                }
            }
        }

        public IWebElement CitySofia
        {
            get
            {
                if (_citySofia != null) return _citySofia;
                else
                {
                    _citySofia = _driverInstance.FindElement(By.XPath("//p[contains(text(),'Sofia')]"));
                    return _citySofia;
                }
            }
        }

        public LocationWindow(IWebDriver driver)
        {
            _driverInstance = driver;
        }

        public IWebElement ConfurmButton
        {
            get
            {
                if (_confurmButton != null) return _confurmButton;
                else
                {
                    _confurmButton = _driverInstance.FindElement(By.XPath("//i[@class='fa fa-check-circle-o fa-3x']"));
                    return _confurmButton;
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
                    _cancelButton = _driverInstance.FindElement(By.XPath("//i[@class='fa fa-times-circle-o fa-3x']"));
                    return _cancelButton;
                }
            }
        }

        public List<String> GetLocationActive()
        {
            IList<IWebElement> elements = _driverInstance.FindElements(By.ClassName("location active-location"));
            List<String> activeButtonNames = new List<String>();
            foreach (var item in elements)
            {
                activeButtonNames.Add(item.Text);
            }
            return activeButtonNames;
        }

        public IList<IWebElement> GetLocationActiveWebElements()
        {
            IList<IWebElement> locationActiveButtonNames = _driverInstance.FindElements(By.ClassName("location active-location"));
            return locationActiveButtonNames;
        }

        public IList<IWebElement> GetLocationNonActiveWebElements()
        {
            IList<IWebElement> nonActiveButtonNames = _driverInstance.FindElements(By.ClassName("location"));
            return nonActiveButtonNames;
        }

        public void ClickNonActiveButtonNames(IList<IWebElement> GetLocationNonActiveWebElements, List<string> listOfCity)
        {
            foreach (var listItem in listOfCity)
            {
                foreach (var item in GetLocationNonActiveWebElements)
                {
                    if (item.Text == listItem)
                    {
                        item.Click();
                    }
                }
            }
        }
    }
}

