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

        public LogAnswerHTTP(RequestDelegate next,
            ILogger<LogAnswerHTTP> logger)
        {
            this.next = next;
            this.logger = logger;
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

                logger.LogInformation(answer);
            }
        }
        
    }
}
