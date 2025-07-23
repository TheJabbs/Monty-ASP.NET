namespace api.MiddleWare;

public class LoggerMiddleWare
{
    private readonly ILogger<LoggerMiddleWare> _logger;
    private readonly RequestDelegate _next;
    
    public LoggerMiddleWare(ILogger<LoggerMiddleWare> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);
        var startTime = DateTime.UtcNow;
        await _next(context); 
        var duration =  DateTime.UtcNow - startTime;
        _logger.LogInformation("Finished handling request: {Method} {Path} in {Duration} ms", 
            context.Request.Method, context.Request.Path, duration.TotalMilliseconds);
    }
    
}