using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Xunit.Abstractions;

namespace PH.LoggingExtensions.Test
{
    public abstract class BaseUnitTest
    {
        protected ILoggerFactory LoggerFactory;
        protected NLog.ILogger nLogger;
        
        protected BaseUnitTest(ITestOutputHelper output,Microsoft.Extensions.Logging.LogLevel configuredLevel)
        {
            Output = output;
            InitDi(configuredLevel);
        
           
        }
        
        protected ILifetimeScope Scope;
        protected ITestOutputHelper Output;


        private void InitDi(Microsoft.Extensions.Logging.LogLevel configuredLevel = LogLevel.Trace)
        {

            try
            {

                var cbuilder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                         
                               .AddEnvironmentVariables();

                IConfigurationRoot configuration = cbuilder.Build();
                


                var services = new ServiceCollection();

                

                NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
                nLogger = NLog.LogManager.GetCurrentClassLogger();


                services.AddSingleton<ILoggerFactory, LoggerFactory>();

                services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

                services.AddLogging((builder) =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(configuredLevel);
                    builder.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates  = true,
                        CaptureMessageProperties = true,
                        IncludeScopes            = true,
                        IgnoreEmptyEventId       = true,
                        ParseMessageTemplates    = true,
                        ShutdownOnDispose        = true,

                    });

                });




                var serviceProvider = services.BuildServiceProvider();

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                

                #region autofac

                var abuilder = new ContainerBuilder();
                abuilder.Populate(services);
               

            
                var container = abuilder.Build();

                Scope = container.BeginLifetimeScope("Unit Test");

                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}