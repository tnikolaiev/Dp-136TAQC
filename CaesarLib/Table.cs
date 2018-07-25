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

        public Table(IWebElement tableElement, IWebDriver driver)
        {
            this.tableElement = tableElement;
            this.driver = driver;
        }

        public IList<IWebElement> GetRows()
        {
            IList<IWebElement> rows = tableElement.FindElements(By.XPath(".//tbody//tr"));
            return rows;
        }

        public IList<IWebElement> GetHeadings()
        {
            IWebElement HeadingsRow = tableElement.FindElement(By.XPath(".//thead//tr[1]"));
            IList<IWebElement> headingColumns = HeadingsRow.FindElements(By.XPath(".//th"));
            return headingColumns;
        }

        public List<string> GetHeadingsText()
        {
            IWebElement HeadingsRow = tableElement.FindElement(By.XPath(".//thead//tr[1]"));
            IList<IWebElement> headingColumns = HeadingsRow.FindElements(By.XPath(".//th"));
            List<string> elements = new List<string>();

            foreach (IWebElement el in headingColumns)
            {
                elements.Add(el.Text);
            }
            return elements;
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

        //Return list of the text from row by columns 
        //if enter 0 return last row
        public List<string> getRowWithColumns(int rowNumber)
        {
            List<string> data = new List<string>();
            IWebElement row;
            if (rowNumber == 0) { row = GetRows().Last(); }
            else
            {
                row  = GetRows()[rowNumber];
            }

            IList<IWebElement> columns = row.FindElements(By.XPath(".//td"));
            foreach(var el in columns)
            {
                data.Add(el.Text);
            }
            return data;
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
                
        //Return a list of text from each table cell
        public List<string> GetTableElements()
        {
            IList<IWebElement> allElement = driver.FindElements(By.TagName("td"));
            List<string> elements = new List<string>();

            foreach (IWebElement el in allElement)
            {
                elements.Add(el.Text);
            }
            return elements;
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
