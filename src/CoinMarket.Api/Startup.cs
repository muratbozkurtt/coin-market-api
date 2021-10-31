using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMarker.Infrastructure.Helper;
using CoinMarker.Infrastructure.Settings;
using CoinMarket.Api.Extension;
using CoinMarket.Data.Context;
using CoinMarket.Service.Service;
using CoinMarket.Service.Service.Define;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CoinMarket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CoinMarket.Api", Version = "v1"});
            });
            services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase(databaseName: "User"));
            services.AddScoped<ICoinService, CoinService>();
            services.AddScoped(typeof(IApiHelper<>), typeof(ApiHelper<>));
            
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            });

            var configSection = Configuration.GetSection("AppSettings");
            var settings = new AppSettings();
            configSection.Bind(settings);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            services.AddControllers();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            services.AddOptions();
            services.AddAntiforgery(options => { options.Cookie.Expiration = TimeSpan.Zero; });
            services.AddJwtAuthentication(Configuration);

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.Configure<CoinServiceSettings>(Configuration.GetSection("CoinServiceSettings"));
            services.Configure<CoinMarketApiSettings>(Configuration.GetSection("CoinMarketApiSetting"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoinMarket.Api v1"));
            }

            app.UseHttpsRedirection();
            
            app.UseMiddleware<JWTMiddleware>();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}