using SoftKill.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    public static class SoftKillApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSoftKill(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SoftKillMiddleware>();
            return builder;
        }
    }
}
