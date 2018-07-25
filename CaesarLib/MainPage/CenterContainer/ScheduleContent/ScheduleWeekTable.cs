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
        public ScheduleWeekTable(IWebElement tableElement, IWebDriver driver) : base(tableElement, driver)
        {
        }

        public override IList<IWebElement> GetRows()
        {
            IList<IWebElement> rows = TableElement.FindElements(By.XPath("//div[contains(@class, 'Table')]/div[contains(@class, 'Row')]"));
            return rows;
        }

        public override IList<IWebElement> GetHeadings()
        {
            IWebElement HeadingsRow = TableElement.FindElement(By.XPath("//div[contains(@class, 'Table')]//div[@class='Heading']"));
            IList<IWebElement> headingColumns = HeadingsRow.FindElements(By.XPath("//.div[contains(@class, 'Cell')]"));
            return headingColumns;
        }

        public override List<IList<IWebElement>> GetRowsWithColumns()
        {
            IList<IWebElement> rows = GetRows();
            List<IList<IWebElement>> rowsWithColumns = new List<IList<IWebElement>>();
            foreach (IWebElement row in rows)
            {
                IList<IWebElement> rowWithColumns = row.FindElements(By.XPath("//.div[contains(@class,'Cell')]"));
                rowsWithColumns.Add(rowWithColumns);
            }
            return rowsWithColumns;
        }       

        public bool IsActivityExists(IWebElement cell, string value)
        {
            TableElement.FindElement(By.XPath("//div[@class='activity']"));
            return true;
        }

    }
}
