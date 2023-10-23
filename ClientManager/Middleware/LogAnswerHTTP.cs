using ClientManager.Service;

namespace ClientManager.Middleware
{

    public static class LogAnswerHttpExtensions
    {
        public static IApplicationBuilder UseLogAnswerHttp(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogAnswerHttp>();
        }
    }


    public class LogAnswerHttp
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogAnswerHttp> _logger;
        private readonly LogHttpService _logService;

        public LogAnswerHttp(RequestDelegate next,
            ILogger<LogAnswerHttp> logger, LogHttpService logService)
        {
            this._next = next;
            this._logger = logger;
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var body = context.Response.Body;
                context.Response.Body = ms;

                await _next(context);

                ms.Seek(0, SeekOrigin.Begin);
                string answer = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(body);
                context.Response.Body = body;

                string requestInfo = $"Request finished {context.Request.Protocol} {context.Request.Method} {context.Request.Path}";
                string responseInfo = $"Response {context.Response.StatusCode} - {context.Response.ContentType} {context.Response.ContentLength} bytes {context.Response.Headers["elapsed-time"]}ms";

                _logService.WriteHttp($"{requestInfo} - {responseInfo}");
                _logger.LogInformation(answer);
            }
        }
    }
}
