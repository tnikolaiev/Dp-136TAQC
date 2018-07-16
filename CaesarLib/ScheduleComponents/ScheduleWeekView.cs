using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CaesarLib.Schedule
{
    public class ScheduleWeekView 
    {
        private IWebDriver DriverInstance;

        private List<IWebElement> _allCellsLocators;

        public List<IWebElement> AllCellsLocators
        {
            get
            {
                if (_allCellsLocators != null) return _allCellsLocators;
                else
                {
                    _allCellsLocators = GetAllCellsLocators();
                    return _allCellsLocators;
                }
            }
        }



        public ScheduleWeekView(IWebDriver driverInstance)
        {
            DriverInstance = driverInstance;
        }

        public List<IWebElement> GetAllCellsLocators()
        {
            List<IWebElement> cells =new List<IWebElement>();
            IWebElement table = DriverInstance.FindElement(By.XPath("//div[contains(@class, 'Table')]"));
            List<IWebElement> rows = table.FindElements(By.XPath("//div[contains(@class, 'Row')]")).ToList();

            for (int rnum = 0; rnum < rows.Count(); rnum++)
            {
                cells = rows[rnum].FindElements(By.CssSelector("div.Cell")).ToList();
                

                for (int cnum = 0; cnum < cells.Count(); cnum++)
                {
                   cells[cnum] = DriverInstance.FindElement(By.XPath("//div[@class='Table']//div[rnum]//div[cnum]"));
                }
            }
            return cells;
        }

        
    }
}
