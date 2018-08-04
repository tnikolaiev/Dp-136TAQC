using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaesarTests
{
    public class Log4Caesar
    {
        private static Logger logger = LogManager.GetLogger("Logger");

        public static void Log()
        {
            var className = TestContext.CurrentContext.Test.ClassName;
            var testName = TestContext.CurrentContext.Test.Name;
            var result = TestContext.CurrentContext.Result.Outcome;

            logger.Debug(className);
            logger.Debug(testName);
            logger.Debug(result.Status);

            var methodName = TestContext.CurrentContext.Test.MethodName;
            var arguments = TestContext.CurrentContext.Test.Arguments;
            var dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            var message = TestContext.CurrentContext.Result.Message;

            GenerateReport(dateTime, result, className, methodName, arguments, String.IsNullOrEmpty(message) ? "-" : message);
        }

        public static void GenerateReport(String iTime, ResultState iResult, String ClassName, String MethodName, object[] Arguments, String iMessage)
        {
            String iPathReportFile = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Logs\log.html";
            //String iPathReportFile = AppDomain.CurrentDomain.BaseDirectory + @"C:\Users\Nikita\source\repos\Dp-136TAQC\CaesarTests\Logs\log.html";
            iMessage = iMessage.Replace("<", "&lt;").Replace(">", "&gt;"); // замена угловых кавычек в тегах выводимых в ошибках, чтобы вёрстка отчёта не "ехала"
            iMessage = iMessage.Replace("\n", "</br>"); // замена, для вставки HTML переносов строк
                                                        //String iTime = String.Format(format: "{MM:dd:YY HH:mm:ss}", arg0: DateTime.Now); // время фиксирования данных в отчёте (вторая колонка отчёта)

            List<String> lines = File.ReadAllLines(iPathReportFile).ToList();
            File.WriteAllLines(iPathReportFile, lines.GetRange(0, lines.Count - 4).ToArray());

            FileStream fs = new FileStream(iPathReportFile, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            String arguments = String.Empty;
            if (Arguments.Length > 0)
            {
                for (int i = 0; i < Arguments.Length; i++)
                {
                    arguments += (i == Arguments.Length-1) ? String.Format("\"{0}\"",Arguments[i]) : String.Format("\"{0}\", ", Arguments[i]);
                }
            }

            if (MethodName.Length > 40) MethodName = (MethodName.Substring(0, 40) + "</br>" + MethodName.Substring(40, MethodName.Length-40));

            if (iResult == ResultState.Success) // passed test record
            {
                sw.WriteLine(@"<tr style='color: green;'>" + "\n" + 
                    @"<td>" + iTime + @"</td>" + "\n" +
                    @"<td>Passed</td>" + "\n" +                  
                    @"<td>" + ClassName + "</td>" + "\n" +
                    @"<td>" + MethodName + "</td>" + "\n" +
                    @"<td>" + arguments + "</td>" + "\n" +
                    @"<td>" + iMessage + @"</td>" + "\n" +
                    @"</tr>");
            }
            if (!(iResult == ResultState.Success)) // failed test record
            {
                sw.WriteLine(@"<tr style='color: red;'>" + "\n" + 
                    @"<td>" + iTime + @"</td>" + "\n" +
                    @"<td>Failed</td>" + "\n" +                   
                    @"<td>" + ClassName + "</td>" + "\n" +
                    @"<td>" + MethodName + "</td>" + "\n" +
                    @"<td>" + arguments + "</td>" + "\n" +
                    @"<td>" + iMessage + @"</td>" + "\n" +
                    @"</tr>");
            }

            sw.WriteLine(@"</table>" + "\n" +
                    @"</body>" + "\n" +
                    @"" + "\n" +
                    @"</html>");
            sw.Close();
        }
    }
}










//using NLog;
//using NUnit.Framework;

//using System;
//using System.IO;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Diagnostics;
//using System.Collections.Generic;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Remote;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Support.UI;
//using System.Drawing.Imaging;
//using System.Reflection;
//using System.Globalization;
//using System.Linq;
//using NUnit.Framework.Interfaces;

//namespace CaesarTests
//{
//    public class Log4Caesar
//    {
//        private static Logger logger = LogManager.GetLogger("Logger");

//        public static void Log()
//        {
//            logger.Debug(TestContext.CurrentContext.Test.ClassName);
//            logger.Debug(TestContext.CurrentContext.Test.Name);
//            logger.Debug(TestContext.CurrentContext.Result.Outcome.Status);
//            GenerateReport(TestContext.CurrentContext.Result.Outcome, TestContext.CurrentContext.Result.Message);
//        }

//        public static void GenerateHtmlLogRecord()
//        {

//        }

//        public static void GenerateReport(ResultState iResult, string iMessage = "-")
//        {
//            //String iPathReportFile = @"\..\..\Logs\log.html";
//            String iPathReportFile = @"C:\Users\Nikita\source\repos\Dp-136TAQC\CaesarTests\Logs\log.html";
//            iMessage = iMessage.Replace("<", "&lt;").Replace(">", "&gt;"); // замена угловых кавычек в тегах выводимых в ошибках, чтобы вёрстка отчёта не "ехала"
//            iMessage = iMessage.Replace("\n", "</br>"); // замена, для вставки HTML переносов строк
//            string iTime = String.Format("{0:HH:mm:ss}", DateTime.Now); // время фиксирования данных в отчёте (вторая колонка отчёта)

//            List<string> lines = File.ReadAllLines(iPathReportFile).ToList();
//            File.WriteAllLines(iPathReportFile, lines.GetRange(0, lines.Count - 3).ToArray());

//            // далее для записи данных в файл
//            FileStream fs = new FileStream(iPathReportFile, FileMode.Append, FileAccess.Write);
//            StreamWriter sw = new StreamWriter(fs);
//            // далее формирование отчёта
//            //if (iStep == 0) // начало отчёта
//            //{
//            //    iTestCountGood = 0; // количество успешно пройденных тестов, в начале формирования отчёта сбрасываем в ноль
//            //    iTestCountFail = 0; // количество не пройденных тестов, в начале формирования отчёта сбрасываем в ноль
//            //    sw.WriteLine(@"<!DOCTYPE html>" + "\n" +
//            //        @"<html lang='ru-RU'>" + "\n" +
//            //        @"<head>" + "\n" +
//            //        @"<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />" + "\n" +
//            //        @"<title>Отчёт о тестировании</title>" + "\n" +
//            //        @"</head>" + "\n" +
//            //        @"<body>" + "\n" +
//            //        @"<div style='font-size:22px;' align='center'><strong>Тест начат: " + String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now) + @"</strong></div></br>" + "\n" +
//            //        @"<table border='1' align='center' cellpadding='5' cellspacing='0' width='100%'>" + "\n" +
//            //        @"<tr style='text-align:center;'>" + "\n" +
//            //        @"<td width='100px'><strong>Тест</strong></td>" + "\n" +
//            //        @"<td width='70px'><strong>Результат</strong></td>" + "\n" +
//            //        @"<td width='70px'><strong>Время</strong></td>" + "\n" +
//            //        @"<td width='70px'><strong>Снимок</strong></td>" + "\n" +
//            //        @"<td><strong>Сообщение</strong></td>" + "\n" +
//            //        @"</tr>");
//            //}
//            if (iResult == ResultState.Success /*& iStep == 1*/) // запись о положительном тесте
//            {
//                //iTestCountGood = iTestCountGood + 1;
//                sw.WriteLine(@"<tr style='color: green;'>" + "\n" +
//                    //@"<td><a target='_blank' href='http://jira.ru/browse/" + iTestNum + "'>" + iTestNum + @"</a></td>" + "\n" +
//                    @"<td>Успех</td>" + "\n" +
//                    @"<td>" + iTime + @"</td>" + "\n" +
//                    @"<td>-</td>" + "\n" +
//                    @"<td>" + iMessage + @"</td>" + "\n" +
//                    @"</tr>" + "\n");
//            }
//            if (!(iResult == ResultState.Success) /*& iStep == 1*/) // запись о отрицательном тесте
//            {
//                //iTestCountFail = iTestCountFail + 1;
//                //ITakesScreenshot screenshotDriver = Test.driver as ITakesScreenshot; // сделать скриншот
//                //Screenshot screenshot = screenshotDriver.GetScreenshot();
//                //string iNameScreen = iTestNum + "_" + String.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.Now) + ".png"; // префикс имени файла
//                //screenshot.SaveAsFile(iFolderScreen + @"\" + iNameScreen, ImageFormat.Png);
//                sw.WriteLine(@"<tr style='color: red;'>" + "\n" + // создать отчёт
//                    //@"<td><a target='_blank' href='http://jira.ru/browse/" + iTestNum + "'>" + iTestNum + @"</a></td>" + "\n" +
//                    @"<td>Провал</td>" + "\n" +
//                    @"<td>" + iTime + @"</td>" + "\n" +
//                    //@"<td><a target='_blank' href='screen/" + iNameScreen + "'>смотреть</a></td>" + "\n" +
//                    @"<td>" + iMessage + @"</td>" + "\n" +
//                    @"</tr>" + "\n");
//            }
//            /* if (iStep == 9) // конец отчёта
//             {
//                 Decimal iProcent = 0;
//                 if (igTestCountGood > 0 || igTestCountFail > 0)
//                 {
//                     iProcent = ((100 * igTestCountGood) / (igTestCountGood + igTestCountFail)); // процент пройденных тестов, далее через Math.Round округлим до десятых
//                 }
//                 sw.WriteLine(@"<tr style='text-align:center;'>" + "\n" +
//                     @"<td colspan='5'>&nbsp;</center></td>" + "\n" +
//                     @"</tr>" + "\n" +
//                     @"<tr style='text-align:center;'>" + "\n" +
//                     @"<td colspan='5'>Всего тестов запущено: " + (iTestCountGood + iTestCountFail) + " || <span style='color: green;'>Успешно: " + iTestCountGood +
//                              "</span> || <span style='color: red;'>Провалено: " + iTestCountFail + "</span> || Процент пройденных: " + iProcent + "% || Тест завершён: " + String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now) + "</td>" + "\n" +
//                     @"</tr>" + "\n" +
//                     @"</table>" + "\n" +
//                     @"</body>" + "\n" +
//                     @"</html>");
//             }*/
//            sw.WriteLine(/*@"<tr style='text-align:center;'>" + "\n" +*/
//                    //@"<td colspan='5'>&nbsp;</center></td>" + "\n" +
//                    //@"</tr>" + "\n" +
//                    //@"<tr style='text-align:center;'>" + "\n" +
//                    //@"<td colspan='5'>Всего тестов запущено: " + (iTestCountGood + iTestCountFail) + " || <span style='color: green;'>Успешно: " + iTestCountGood +
//                    //         "</span> || <span style='color: red;'>Провалено: " + iTestCountFail + "</span> || Процент пройденных: " + iProcent + "% || Тест завершён: " + String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now) + "</td>" + "\n" +
//                    //@"</tr>" + "\n" +
//                    @"</tabler>" + "\n" +
//                    @"</body>" + "\n" +
//                    @"</html>");
//            sw.Close();
//            // iPathReportFile - указывается полный путь до файла отчёта включая имя самого файла
//        }
//    }
//}
