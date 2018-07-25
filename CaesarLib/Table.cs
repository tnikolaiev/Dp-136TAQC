using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib
{
    public class Table
    {
        private IWebElement tableElement;
        private IWebDriver driver;

        public IWebElement TableElement { get => tableElement; set => tableElement = value; }
        public IWebDriver Driver { get => driver; set => driver = value; }

        public Table(IWebElement tableElement, IWebDriver driver)
        {
            this.TableElement = tableElement;
            this.Driver = driver;
        }

        public virtual IList<IWebElement> GetRows()
        {
            IList<IWebElement> rows = TableElement.FindElements(By.XPath("//tbody//tr"));
            return rows;
        }

        public virtual IList<IWebElement> GetHeadings()
        {
            IWebElement HeadingsRow = TableElement.FindElement(By.XPath(".//thead//tr[1]"));
            IList<IWebElement> headingColumns = HeadingsRow.FindElements(By.XPath(".//th"));
            return headingColumns;
        }

        public virtual List<IList<IWebElement>> GetRowsWithColumns()
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

        public int GetRowNumberByValue(String value)
        {
            List<IList<IWebElement>> rowsWithColumns = GetRowsWithColumns();
            int rowNumber = 0;

            foreach (IList<IWebElement> el in rowsWithColumns)
            {
                IList<IWebElement> oneRowWithColumn = el;
                rowNumber++;

                foreach (IWebElement element in oneRowWithColumn)
                {
                    string textFromCell = element.Text;
                }
            }

            return rowNumber;
        }

        public String GetValueFromCell(int rowNumber, int columnNumber)
        {
            List<IList<IWebElement>> rowsWithColumns = GetRowsWithColumns();
            IList<IWebElement> row = rowsWithColumns[rowNumber - 1];
            IWebElement cell = row[columnNumber - 1];
            return cell.Text;
        }

        public String GetValueFromCell(int rowNumber, String columnName)
        {
            List<IDictionary<String, IWebElement>> rowsWithColumnsByHeadings = getRowsWithColumnsByHeadings();
            IDictionary<String, IWebElement> row = rowsWithColumnsByHeadings[rowNumber - 1];
            IWebElement cell = row[columnName];
            return cell.Text;
        }

        public String GetValueFromCell(string value, String columnName)
        {
            int rowNumber = GetRowNumberByValue(value);
            return GetValueFromCell(rowNumber, columnName);
        }

    }
}
