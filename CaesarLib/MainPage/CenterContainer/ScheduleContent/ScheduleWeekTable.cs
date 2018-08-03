using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib
{
    public class ScheduleWeekTable : Table
    {
        public ScheduleWeekTable(IWebElement tableElement) : base(tableElement)
        {
        }

        public override IList<IWebElement> GetRows()
        {
            IList<IWebElement> rows = TableElement.FindElements(By.XPath(".//div[contains(@class, 'Row')]"));
            return rows;
        }

        public override IList<IWebElement> GetHeadings()
        {
            IWebElement HeadingsRow = TableElement.FindElement(By.XPath(".//div[@class='Heading']"));
            IList<IWebElement> headingColumns = HeadingsRow.FindElements(By.XPath(".//div[contains(@class,'Cell')]"));
            return headingColumns;
        }
                
        public override List<IList<IWebElement>> GetRowsWithColumns()
        {
            IList<IWebElement> rows = GetRows();
            List<IList<IWebElement>> rowsWithColumns = new List<IList<IWebElement>>();
            foreach (IWebElement row in rows)
            {
                IList<IWebElement> rowWithColumns = row.FindElements(By.XPath(".//div[contains(@class,'Cell')]"));
                rowsWithColumns.Add(rowWithColumns);
            }
            return rowsWithColumns;
        }
        //Get cell by value from row and column name

        public IWebElement GetCell(string valueInRow, String columnName)
        {
            int rowNumber = GetRowNumberByValue(valueInRow);
            List<IDictionary<String, IWebElement>> rowsWithColumnsByHeadings = getRowsWithColumnsByHeadings();
            IDictionary<String, IWebElement> row = rowsWithColumnsByHeadings[rowNumber - 1];
            IWebElement cell = row[columnName];
            return cell;
        }        

        //Method to verify if activity exists in cell
        public bool IsActivityExists(IWebElement cell)
        {
            return cell.FindElements(By.XPath(".//div[@class='activity']")).Count > 0 ?
               true : false;         
        }

        //Method to verify that activity in cell is correctly displayed
        public bool IsActivityCorrect(IWebElement cell, string textInActivity)
        {
           IWebElement activity = cell.FindElement(By.XPath(".//div[@class='activity']"));
           string text = activity.Text;                
           return text.Contains(textInActivity) ? true : false;
        }

        public int ActivitiesCountInCell (IWebElement cell)
        {         
            return cell.FindElements(By.XPath(".//div[@class='activity']")).Count;
        }
    }
}
