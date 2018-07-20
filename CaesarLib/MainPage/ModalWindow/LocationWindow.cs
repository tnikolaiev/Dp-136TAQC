using System;
using OpenQA.Selenium;
using System.Collections.Generic;


namespace CaesarLib
{
   public class LocationWindow
    {
        private IWebElement groupOfLocations;
        private IWebElement cityChernivtsy;
        private IWebElement cityDnipro;
        private IWebElement cityIvanoFrankivsk;
        private IWebElement cityKyiv;
        private IWebElement cityLviv;
        private IWebElement cityRivne;
        private IWebElement citySofia;
        private IWebElement confurmButton;
        private IWebElement cancelButton;
        private IWebDriver driverInstance;


            public IWebElement GroupOfLocations
        {
            get
            {
                if (groupOfLocations != null) return groupOfLocations;
                else
                {
                    groupOfLocations = driverInstance.FindElement(By.Name("location-wrapper"));
                    return groupOfLocations;
                }
            }
        }

        public IWebElement CityChernivtsy
        {
            get
            {
                if (cityChernivtsy != null) return cityChernivtsy;
                else
                {
                    cityChernivtsy = driverInstance.FindElement(By.XPath("//p[contains(text(),'Chernivtsy')]"));
                    return cityChernivtsy;
                }
            }
        }

        public IWebElement CityDnipro
        {
            get
            {
                if (cityDnipro != null) return cityDnipro;
                else
                {
                    cityDnipro = driverInstance.FindElement(By.XPath("//p[contains(text(),'Dnipro')]"));
                    return cityDnipro;
                }
            }
        }

        public IWebElement CityIvanoFrankivsk
        {
            get
            {
                if (cityIvanoFrankivsk != null) return cityIvanoFrankivsk;
                else
                {
                    cityIvanoFrankivsk = driverInstance.FindElement(By.XPath("/p[contains(text(),'Ivano-Frankivsk')]"));
                    return cityIvanoFrankivsk;
                }
            }
        }

        public IWebElement CityKyiv
        {
            get
            {
                if (cityKyiv != null) return cityKyiv;
                else
                {
                    cityKyiv = driverInstance.FindElement(By.XPath("//p[contains(text(),'Kyiv')]"));
                    return cityKyiv;
                }
            }
        }

        public IWebElement CityLviv
        {
            get
            {
                if (cityLviv != null) return cityLviv;
                else
                {
                    cityLviv = driverInstance.FindElement(By.XPath("//p[contains(text(),'Lviv')]"));
                    return cityLviv;
                }
            }
        }

        public IWebElement CityRivne
        {
            get
            {
                if (cityRivne != null) return cityRivne;
                else
                {
                    cityRivne = driverInstance.FindElement(By.XPath("//p[contains(text(),'Rivne')]"));
                    return cityRivne;
                }
            }
        }

        public IWebElement CitySofia
        {
            get
            {
                if (citySofia != null) return citySofia;
                else
                {
                    citySofia = driverInstance.FindElement(By.XPath("//p[contains(text(),'Sofia')]"));
                    return citySofia;
                }
            }
        }

        public LocationWindow(IWebDriver driver)
        {
            driverInstance = driver;
        }

        public IWebElement ConfurmButton
        {
            get
            {
                if (confurmButton != null) return confurmButton;
                else
                {
                    confurmButton = driverInstance.FindElement(By.XPath("//i[@class='fa fa-check-circle-o fa-3x']"));
                    return confurmButton;
                }
            }
        }

        public IWebElement CancelButton
        {
            get
            {
                if (cancelButton != null) return cancelButton;
                else
                {
                    cancelButton = driverInstance.FindElement(By.XPath("//i[@class='fa fa-times-circle-o fa-3x']"));
                    return cancelButton;
                }
            }
        }

        public List<String> GetLocationActive()
        {
            IList<IWebElement> elements = driverInstance.FindElements(By.ClassName("location active-location"));
            List<String> activeButtonNames = new List<String>();
            foreach (var item in elements)
            {
                activeButtonNames.Add(item.Text);
            }
            return activeButtonNames;
        }
            
    }
}
