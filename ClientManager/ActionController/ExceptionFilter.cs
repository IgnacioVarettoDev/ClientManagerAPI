using ClientManager.Service;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClientManager.ActionController;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionFilter> _logger;
    private readonly LogHttpService _logService;

    public ExceptionFilter(ILogger<ExceptionFilter> logger, LogHttpService logService)
    {
        _logger = logger;
        _logService = logService;
    }

    public override void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);
        _logService.WriteException(context.Exception);
        base.OnException(context);
    }
}