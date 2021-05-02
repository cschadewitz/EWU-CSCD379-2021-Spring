using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Web.Api;
using AutoMapper;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web
{
    public class Startup
    {
        public static HttpClient ApiClient = new()
        {
            BaseAddress = new Uri("https://localhost:5101")
        };

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<UsersClient>(_ => new UsersClient(ApiClient));
            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<MappingProfileWeb>();
            //    //cfg.CreateMap<UserDTO, UserViewModel>();
            //    //cfg.CreateMap<UserViewModel, UserDTO>();
            //
            //});
            //configuration.AssertConfigurationIsValid();
            services.AddAutoMapper(typeof(MappingProfileWeb));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
