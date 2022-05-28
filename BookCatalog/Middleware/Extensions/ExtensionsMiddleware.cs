using BookCatalog.Middleware.CustomException;

namespace BookCatalog.Middleware.Extensions
{
    public static class ExtensionsMiddleware
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
