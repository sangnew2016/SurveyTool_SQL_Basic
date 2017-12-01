using log4net;
using System.Web.Http.ExceptionHandling;

namespace LearningPlatform.Api.ErrorHandling
{
    public class GlobalErrorLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var exception = context.Exception;
            ILog logger = LogManager.GetLogger(typeof(GlobalErrorLogger));
            logger.Error("", exception);
        }
    }
}