using Framework.Infrastructure.Consul.Configuration;
using Framework.Infrastructure.Ioc.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qwerty.DDD.BootStrapping;

namespace Qwerty.DDD.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonConsul(Program.ConfigurationRegister)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注入configuration
            services.AddSingleton(Configuration);

            //add cors
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            //添加数据库连接
            services.Configure(Configuration["data:ConnectionString"]);
            // Add framework services.
            services
            .AddMvcCore(options =>
            {
                //options.Filters.Add(typeof(WebApiResultMiddleware));
                options.RespectBrowserAcceptHeader = true;
            })
            .AddApiExplorer()
            .AddAuthorization()
            .AddJsonFormatters()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
            })
            ;
            //验证授权地址
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["SP_BasicSetting:IssuerUri"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = "attendance";
                });

            //添加redis缓存
            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "";
                options.Configuration = Configuration["SP_BasicSetting:RedisServer"];
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            ServiceLocator.Instance = app.ApplicationServices;
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //snake请求转换驼峰
            //app.UseRewriteQueryString();

            app.UseAuthentication();

            app.UseMvc();

        }
    }
}
