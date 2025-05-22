using LMS.Common.Exceptions;

namespace LMS.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (DatabaseException dbEx)
            {
                _logger.LogError(dbEx, "Database exception occurred");

                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = dbEx.SqlErrorCode switch
                {
                    2627 or 2601 => StatusCodes.Status409Conflict, 
                    547 => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                var errorResult = new
                {
                    status = "failure",
                    message = "Database error occurred.",
                    code = dbEx.SqlErrorCode
                };

                await response.WriteAsJsonAsync(errorResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = "failure",
                    message = "Something went wrong!"
                });
            }
        }
    }
}
