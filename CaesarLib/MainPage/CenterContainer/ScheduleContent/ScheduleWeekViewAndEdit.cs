using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CaesarLib
{
    public class ScheduleWeekViewAndEdit

    {
        //Fields

        private IWebDriver DriverInstance;

        private List<IWebElement> _allCellsLocators;

        public List<IWebElement> AllCellsLocators
        {
            get
            {
                if (_allCellsLocators != null) return _allCellsLocators;
                else
                {
                    _allCellsLocators = GetAllCells();
                    return _allCellsLocators;
                }
            }
        }


        // Constructor

        public ScheduleWeekViewAndEdit(IWebDriver driverInstance)
        {
            DriverInstance = driverInstance;
        }

        
        // Actions

         //In progress
        private List<IWebElement> GetAllCells()
        {
            IWebElement table = DriverInstance.FindElement(By.XPath("//div[contains(@class, 'Table')]"));
            IList<IWebElement> rows = table.FindElements(By.XPath("//div[contains(@class, 'Row')]"));
            IList<IWebElement> cells;
            Console.WriteLine(rows.Count());

            for (int rnum = 0; rnum < rows.Count(); rnum++)
            {
                cells = rows[rnum].FindElements(By.CssSelector("div.Cell"));
                Console.WriteLine(cells.Count());

                for (int cnum = 0; cnum < cells.Count(); cnum++)
                {
                    string strMyXPath = "???";

                    cells[cnum] = DriverInstance.FindElement(By.XPath(strMyXPath));
                }
               
            }
            return null;
        }

        //In progress
        public IWebElement GetCellByIndex (int index)
        {
            IWebElement cell = GetAllCells()[index];            

            return cell;
        }

        public bool IsScheduleWeekViewDisplayed(IWebDriver driverInstance)
        {
            return driverInstance.FindElements(By.ClassName("scheduleWeek-view")).Count > 0 ?
               true : false;
        }


    }
}
