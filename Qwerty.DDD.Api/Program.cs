using System;
using System.Net;
using Framework.Consul;
using Framework.Infrastructure.Consul.Configuration;
using Framework.Infrastructure.Consul.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Qwerty.DDD.Api
{

    public class Program
    {
        public const string ServiceName = "user";
        /// <summary>
        /// 
        /// </summary>
        public const string Version = "v1";
        /// <summary>
        /// 
        /// </summary>
        public static ConsulClient ConsulClient = new ConsulClient();
        /// <summary>
        /// 
        /// </summary>
        public static IServiceRegister ServiceRegistry;
        /// <summary>
        /// 
        /// </summary>
        public static IConfigurationRegister ConfigurationRegister;

        public static void Main(string[] args)
        {
            InitConfiguration();
            ServiceRegister(ServiceName);
            var host = new WebHostBuilder()
                .ConfigureServices(cfg => { cfg.AddSingleton(ServiceRegistry); })
                .ConfigureServices(cfg => { cfg.AddSingleton(ConfigurationRegister); })
                .UseUrls($"http://*:{ServiceRegistry.ServicePort}")//set url
                .UseKestrel()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();

        }
        public static void InitConfiguration()
        {
            var configurationRegister = new ConfigurationRegister(ConsulClient);
            configurationRegister.SetKeyValueAsync($"basic/{ServiceName}", "{\"database\":\"Database=aspros.user;\"}").Wait();
            configurationRegister.AddUpdatingPathAsync("basic").Wait();
            ConfigurationRegister = configurationRegister;
        }

        public static void ServiceRegister(string serviceName)
        {
            var ipAddress = ServiceRegistryExtensions.GetLocalIpAddress();
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new Exception("can't get server ip address.");
            }

            ServiceRegistry = new ServiceRegister(IPAddress.Parse(ipAddress));
            ServiceRegistry
                .SetConsul(ConsulClient)
                .AddHttpHealthCheck("health", 30, 10)
                .RegisterServiceAsync(serviceName).Wait();
        }
    }
}
