using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using WebApplication_Library_.Context;
using WebApplication_Library_.Repositories;
using WebApplication_Library_.Services;

namespace WebApplication_Library_.Infrastructure
{
    public static class AppServiceProvider
    {
        private static bool _isInitialized = false;
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Initialize()
        {
            if (_isInitialized) return;

            var services = new ServiceCollection();

            //Serilog
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "log.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    logFilePath,
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 20 * 1024 * 1024,
                    retainedFileCountLimit: 7,
                    rollOnFileSizeLimit: true
                )
                .CreateLogger();

            //DbContext
            services.AddTransient<LibraryDBContext>();

            //Repositories
            services.AddTransient<BookRepository>();
            services.AddTransient<UserRepository>();

            //Services
            services.AddTransient<AuthorizationService>();
            services.AddTransient<BookService>();
            services.AddTransient<ShoppingCartService>();
            services.AddTransient<UserService>();

            ServiceProvider = services.BuildServiceProvider();

            _isInitialized = true;
        }
    }
}
