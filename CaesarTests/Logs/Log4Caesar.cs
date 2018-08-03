using NLog;
using NUnit.Framework;

namespace CaesarTests
{
    public class Log4Caesar
    {
        private static Logger logger = LogManager.GetLogger("Logger");

        public static void Log()
        {
            logger.Debug(TestContext.CurrentContext.Test.ClassName);
            logger.Debug(TestContext.CurrentContext.Test.Name);
            logger.Debug(TestContext.CurrentContext.Result.Outcome.Status);
        }
    }
}
