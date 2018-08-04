using CaesarLib;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaesarTests
{
    [TestFixture]
    public class TC_1_01_01
    {
        IWebDriver driver;
        WebDriverWait wait;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        static List<String> LinksList = new List<String> { @"http://localhost:3000", @"http://localhost:3000/Groups/Dnipro", @"http://localhost:3000/admin" };

        [Test, TestCaseSource("LinksList")]
        public void Test_NavigateToLinks_LoginPageOpened(String link)
        {

            //String iMessage = TestContext.CurrentContext.Result.Message;
            //ResultState iResult = TestContext.CurrentContext.Result.Outcome;
            //String iPathReportFile = @"C:\Users\Nikita\source\repos\Dp-136TAQC\CaesarTests\Logs\log.html";

            //string iTime = String.Format("{0:HH:mm:ss}", DateTime.Now); // время фиксирования данных в отчёте (вторая колонка отчёта)
            //List<string> lines = File.ReadAllLines(iPathReportFile).ToList();
            //File.WriteAllLines(iPathReportFile, lines.GetRange(0, lines.Count - 3).ToArray());

            //// далее для записи данных в файл
            //FileStream fs = new FileStream(iPathReportFile, FileMode.Append, FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs);

            //if (iResult == ResultState.Success /*& iStep == 1*/) // запись о положительном тесте
            //{
            //    //iTestCountGood = iTestCountGood + 1;
            //    sw.WriteLine(@"<tr style='color: green;'>" + "\n" +
            //        //@"<td><a target='_blank' href='http://jira.ru/browse/" + iTestNum + "'>" + iTestNum + @"</a></td>" + "\n" +
            //        @"<td>Успех</td>" + "\n" +
            //        @"<td>" + iTime + @"</td>" + "\n" +
            //        @"<td>-</td>" + "\n" +
            //        @"<td>" + iMessage + @"</td>" + "\n" +
            //        @"</tr>" + "\n");
            //}
            //if (!(iResult == ResultState.Success) /*& iStep == 1*/) // запись о отрицательном тесте
            //{
            //    //iTestCountFail = iTestCountFail + 1;
            //    //ITakesScreenshot screenshotDriver = Test.driver as ITakesScreenshot; // сделать скриншот
            //    //Screenshot screenshot = screenshotDriver.GetScreenshot();
            //    //string iNameScreen = iTestNum + "_" + String.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.Now) + ".png"; // префикс имени файла
            //    //screenshot.SaveAsFile(iFolderScreen + @"\" + iNameScreen, ImageFormat.Png);
            //    sw.WriteLine(@"<tr style='color: red;'>" + "\n" + // создать отчёт
            //                                                      //@"<td><a target='_blank' href='http://jira.ru/browse/" + iTestNum + "'>" + iTestNum + @"</a></td>" + "\n" +
            //        @"<td>Провал</td>" + "\n" +
            //        @"<td>" + iTime + @"</td>" + "\n" +
            //        //@"<td><a target='_blank' href='screen/" + iNameScreen + "'>смотреть</a></td>" + "\n" +
            //        @"<td>" + iMessage + @"</td>" + "\n" +
            //        @"</tr>" + "\n");
            //}

            //sw.WriteLine(
            //        @"</table>" + "\n" +
            //        @"</body>" + "\n" +
            //        @"</html>");
            //sw.Close();

            driver.Url = link;
            Assert.IsTrue(wait.Until((d) => LoginPage.IsLoginPageOpened(driver)));
            //Assert.Fail();
        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }

        [OneTimeTearDown]
        public void FinalCleanUp()
        {
            driver.Quit();
        }
    }
}