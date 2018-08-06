using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace CaesarLib
{
    public class InfoPage
    {
        IWebDriver driver;
        
        public InfoPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        
        public Table GroupCoordination { get => new Table(driver.FindElement(By.ClassName("tg"))); }

        public Table GroupInfo { get => new Table(driver.FindElement(By.XPath("//div[@class='group_info']//table[@class='tg']"))); }

        




        }
}
