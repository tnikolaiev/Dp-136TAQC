using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib.MainPage
{
    public class Table
    {
        private IWebElement tableElement;
        private IWebDriver driver;

        public Table(IWebElement tableElement, IWebDriver driver)
        {
            this.tableElement = tableElement;
            this.driver = driver;
        }

        public IList<IWebElement> GetRows()
        {
            IList<IWebElement> rows = tableElement.FindElements(By.XPath("//tbody//tr"));
            //rows.RemoveAt(0);
            return rows;
        }

        public IList<IWebElement> GetHeadings()
        {
            IWebElement HeadingsRow = tableElement.FindElement(By.XPath(".//thead//tr[1]"));
            IList<IWebElement> headingColumns = HeadingsRow.FindElements(By.XPath(".//th"));
            return headingColumns;
        }

        public List<IList<IWebElement>> GetRowsWithColumns()
        {
            IList<IWebElement> rows = GetRows();
            List<IList<IWebElement>> rowsWithColumns = new List<IList<IWebElement>>();
            foreach (IWebElement row in rows)
            {
                IList<IWebElement> rowWithColumns = row.FindElements(By.XPath(".//td"));
                rowsWithColumns.Add(rowWithColumns);
            }
            return rowsWithColumns;
        }

        public List<IDictionary<String, IWebElement>> getRowsWithColumnsByHeadings()
        {
            List<IList<IWebElement>> rowsWithColumns = GetRowsWithColumns();
            List<IDictionary<String, IWebElement>> rowsWithColumnsByHeadings = new List<IDictionary<String, IWebElement>>();
            Dictionary<String, IWebElement> rowByHeadings;
            IList<IWebElement> headingColumns = GetHeadings();

            foreach (IList<IWebElement> row in rowsWithColumns)
            {
                rowByHeadings = new Dictionary<String, IWebElement>();
                for (int i = 0; i < headingColumns.Count(); i++)
                {
                    String heading = headingColumns[i].Text;
                    IWebElement cell = row[i];
                    rowByHeadings.Add(heading, cell);
                }
                rowsWithColumnsByHeadings.Add(rowByHeadings);
            }
            return rowsWithColumnsByHeadings;

        }

        public String getValueFromCell(int rowNumber, int columnNumber)
        {
            List<IList<IWebElement>> rowsWithColumns = GetRowsWithColumns();
            IList<IWebElement> row = rowsWithColumns[rowNumber - 1];
            IWebElement cell = row[columnNumber - 1];
            return cell.Text;
        }

        public String getValueFromCell(int rowNumber, String columnName)
        {
            List<IDictionary<String, IWebElement>> rowsWithColumnsByHeadings = getRowsWithColumnsByHeadings();
            IDictionary<String, IWebElement> row = rowsWithColumnsByHeadings[rowNumber - 1];
            IWebElement cell = row[columnName];
            return cell.Text;
        }        

    }
}


