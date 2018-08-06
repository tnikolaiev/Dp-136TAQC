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
            iMessage = iMessage.Replace("<", "&lt;").Replace(">", "&gt;");
            iMessage = iMessage.Replace("\n", "</br>");

            List<String> lines = File.ReadAllLines(iPathReportFile).ToList();
            File.WriteAllLines(iPathReportFile, lines.GetRange(0, lines.Count - 4).ToArray());

            FileStream fs = new FileStream(iPathReportFile, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            String arguments = String.Empty;
            if (Arguments.Length > 0)
            {
                for (int i = 0; i < Arguments.Length; i++)
                {
                    arguments += (i == Arguments.Length - 1) ? String.Format("\"{0}\"", Arguments[i]) : String.Format("\"{0}\", ", Arguments[i]);
                }
            }

            if (MethodName.Length > 40) MethodName = (MethodName.Substring(0, 40) + "</br>" + MethodName.Substring(40, MethodName.Length - 40));

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