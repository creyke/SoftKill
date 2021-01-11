namespace Microsoft.AspNetCore.Builder
{
    public static class SoftKillApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSoftKill(this IApplicationBuilder builder)
        {
            return builder;
        }
    }
}
