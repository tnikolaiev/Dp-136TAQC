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

        private List<IWebElement> GetAllCells()
        {
            List<IWebElement> cells =new List<IWebElement>();
            IWebElement table = DriverInstance.FindElement(By.XPath("//div[contains(@class, 'Table')]"));
            List<IWebElement> rows = table.FindElements(By.XPath("//div[contains(@class, 'Row')]")).ToList();

            for (int rnum = 2; rnum < rows.Count(); rnum++)
            {
                cells = rows[rnum].FindElements(By.CssSelector("div.Cell")).ToList();
                

                for (int cnum = 2; cnum < cells.Count(); cnum++)
                {
                   string strMyXPath = "//div[contains(@class,'Table')]//div["+rnum+"]//div["+cnum+"]";
                   cells[cnum] = DriverInstance.FindElement(By.XPath(strMyXPath));
                }
            }
            return cells;
        }

        //In progress
        public IWebElement GetCellByIndex (int index)
        {
            IWebElement cell = GetAllCells()[index];            

            return cell;
        }

        
    }
}
