using ClientManager.Service;

namespace ClientManager.Middleware
{

    public static class LogAnswerHTTPExtensions
    {
        public static IApplicationBuilder UseLogAnswerHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogAnswerHTTP>();
        }
    }


    public class LogAnswerHTTP
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LogAnswerHTTP> logger;
        private readonly LogHttpService _logService;

        public LogAnswerHTTP(RequestDelegate next,
            ILogger<LogAnswerHTTP> logger, LogHttpService logService)
        {
            this.next = next;
            this.logger = logger;
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var body = context.Response.Body;
                context.Response.Body = ms;

                await next(context);

                ms.Seek(0, SeekOrigin.Begin);
                string answer = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(body);
                context.Response.Body = body;

                string requestInfo = $"Request finished {context.Request.Protocol} {context.Request.Method} {context.Request.Path}";
                string responseInfo = $"Response {context.Response.StatusCode} - {context.Response.ContentType} {context.Response.ContentLength} bytes {context.Response.Headers["elapsed-time"]}ms";

                _logService.WriteHttp($"{requestInfo} - {responseInfo}");
                logger.LogInformation(answer);
            }
        }
    }
}
