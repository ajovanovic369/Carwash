using Microsoft.AspNetCore.Mvc.Filters;

namespace CarWash.Filters
{
    public class LoggerExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<LoggerExceptionFilter> logger;

        public LoggerExceptionFilter(ILogger<LoggerExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);

            base.OnException(context);
        }
    }
}
