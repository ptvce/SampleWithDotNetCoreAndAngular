using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleWithDotNetCoreAndAngular.Common;
using SampleWithDotNetCoreAndAngular.Data;
using SampleWithDotNetCoreAndAngular.Helper;
using SampleWithDotNetCoreAndAngular.Repository;
using Serilog;

namespace SampleWithDotNetCoreAndAngular
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
            #region DBContext Register
            //code first or db first
            services.AddDbContext<CoreLearningContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CoreLearningDB")));
            #endregion
            #region DI Register

            services.AddScoped(typeof(ICategoryHelper), typeof(CategoryHelper));
            services.AddScoped(typeof(IProductHelper), typeof(ProductHelper));
            services.AddScoped(typeof(ISqlUtility), typeof(SqlUtility));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            loggerFactory.AddFile("Logs/mvc-{Date}.txt");
                       
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "cultureRout",
                    pattern: "{culture=fa}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
