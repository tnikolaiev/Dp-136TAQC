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
        private IWebElement _tableElement;

        public Table(IWebElement tableElement)
        {
            _tableElement = tableElement;
        }

        public IWebElement TableElement { get => _tableElement; set => _tableElement = value; }





        //Use this method to get all Rows in table without column names
        public virtual IList<IWebElement> GetRows()
        {
            IList<IWebElement> rows = TableElement.FindElements(By.XPath(".//tbody//tr"));
            return rows;
        }

        //Use this method to get all column names
        public virtual IList<IWebElement> GetHeadings()
        {
            IWebElement HeadingsRow = TableElement.FindElement(By.XPath(".//thead//tr[1]"));
            IList<IWebElement> headingColumns = HeadingsRow.FindElements(By.XPath(".//th"));
            return headingColumns;
        }

        //Method for getting all rows with all their cells 
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

        //Method for getting list of cells with column names in each row
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

        //Method to find out number of row where specific value is situated
        public int GetRowNumberByValue(String valueInRow)
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
                    if (textFromCell==valueInRow)
                    {
                        return rowNumber;
                    }                    
                }
            }

            return rowNumber;
        }

        //Getting text from cell by column number and row number
        public String GetValueFromCell(int rowNumber, int columnNumber)
        {
            List<IList<IWebElement>> rowsWithColumns = GetRowsWithColumns();
            IList<IWebElement> row = rowsWithColumns[rowNumber - 1];
            IWebElement cell = row[columnNumber - 1];
            return cell.Text;
        }

        //Getting text from cell by row number and column name
        public String GetValueFromCell(int rowNumber, String columnName)
        {
            List<IDictionary<String, IWebElement>> rowsWithColumnsByHeadings = getRowsWithColumnsByHeadings();
            IDictionary<String, IWebElement> row = rowsWithColumnsByHeadings[rowNumber - 1];
            IWebElement cell = row[columnName];
            return cell.Text;
        }

        //Getting text from cell by value from row and column name
        public String GetValueFromCell(string valueInRow, String columnName)
        {
            int rowNumber = GetRowNumberByValue(valueInRow);
            return GetValueFromCell(rowNumber, columnName);
        }

        //Getting text from cell by value from row and column number
        public String GetValueFromCell(string valueInRow, int columnNumber)
        {
            int rowNumber = GetRowNumberByValue(valueInRow);
            return GetValueFromCell(rowNumber, columnNumber);                      
        }       


        public IWebElement GetElementFromCell(int rowNumber, int columnNumber)
        {
            List<IList<IWebElement>> rowsWithColumns = GetRowsWithColumns();
            IList<IWebElement> row = rowsWithColumns[rowNumber - 1];
            IWebElement cell = row[columnNumber - 1];
            return cell.FindElement(By.TagName("i"));
        }

    }
}
