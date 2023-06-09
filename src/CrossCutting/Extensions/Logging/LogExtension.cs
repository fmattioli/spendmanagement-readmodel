﻿using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CrossCutting.Extensions.Logging
{
    public static class LogExtension
    {
        public static IServiceCollection AddLoggingDependency(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .CreateLogger();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            return services.AddSingleton(Log.Logger);
        }
    }
}
