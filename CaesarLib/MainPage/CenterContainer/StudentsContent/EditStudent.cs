using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarLib.StudentsPage
{
    public class EditStudent
    {
        IWebDriver webDriver;
        public IWebElement FirstName { get => webDriver.FindElement(By.Name("FirstName")); }
        public IWebElement LastName { get => webDriver.FindElement(By.Name("LastName")); }
        public IWebElement EnglishLevel { get => webDriver.FindElement(By.CssSelector("#modal-window > div > section > section > div:nth-child(4) > div:nth-child(1) > select")); }
        public IWebElement BrowseCVButton { get => webDriver.FindElement(By.ClassName("BrowseCV")); }
        public IWebElement IncomingTest { get => webDriver.FindElement(By.Name("incomingTest")); }
        public IWebElement EntryScore { get => webDriver.FindElement(By.Name("entryScore")); }
        public IWebElement ApprovedBy { get => webDriver.FindElement(By.CssSelector("#modal-window > div > section > section > div:nth-child(3) > div:nth-child(2) > select")); }
        public IWebElement BrowsePhotoButton { get => webDriver.FindElement(By.ClassName("BrowsePhoto")); }
        public IWebElement SaveButton { get => webDriver.FindElement(By.ClassName("save-changes")); }
        public IWebElement CancelButton { get => webDriver.FindElement(By.ClassName("close-modal-window")); }
        public IWebElement RemoveCVButton { get => webDriver.FindElement(By.ClassName("remove-cv")); }
        public IWebElement RemovePhotoButton { get => webDriver.FindElement(By.ClassName("remove-photo")); }
        public EditStudent(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public static bool IsEditStudent(IWebDriver driver)
        {
            if (driver.FindElements(By.ClassName("BrowsePhoto")).Count > 0 &&
                driver.FindElements(By.ClassName("BrowseCV")).Count > 0 &&
                driver.FindElements(By.Name("FirstName")).Count > 0 &&
                 driver.FindElements(By.Name("LastName")).Count > 0)
                return true;
            else
                return false;
        }
        public void FillForm(string firstName, string lastName, int englishLevelIndex, string incomingTest, string entryScore, int approvedByIndex)
        {
            Acts.InputValue(FirstName, firstName);
            Acts.InputValue(LastName, lastName);
            Acts.SelectElement(EnglishLevel, englishLevelIndex);
            Acts.InputValue(IncomingTest, incomingTest);
            Acts.InputValue(EntryScore, entryScore);
            Acts.SelectElement(ApprovedBy, approvedByIndex);
        }
        public int CountUploadedFiles()
        {
            return webDriver.FindElements(By.ClassName("list-item")).Count;             
        }
        public static String GetTestFile(String fileName)
        {
            Dictionary<String, String> fileNamePathPairs = new Dictionary<String, String>();

            String[] files = Directory.GetFiles(@"CaesarTests\TC_3_04 files");

            for (int i = 0; i < files.Length; i++)
            {
                fileNamePathPairs.Add(Path.GetFileName(files[i]), Path.GetFullPath(files[i]));
            }
            return fileNamePathPairs[fileName];
        }
    }
}
