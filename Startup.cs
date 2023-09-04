using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCourse.Models.Services.Application;
using MyCourse.Models.Services.Infrastructure;

namespace MyCourse
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // aggiungo ai servizi, i servizi Mvc (Model, View, Controller)
            services.AddMvc();
            // services.AddTransient<ICourseService, AdoNetCourseService>();
            services.AddTransient<ICourseService, EfCoreCourseService>();
            services.AddTransient<IDataBaseAccessor, SqliteDatabaseAccessor>();

            // services.AddScoped<MyCourseDbContext>();
            // services.AddDbContext<MyCourseDbContext>();
            services.AddDbContextPool<MyCourseDbContext>(optionsBuilder => {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source=Data/MyCourse.db");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            //if (env.IsDevelopment())
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();

                //Aggiorniamo un file per notificare al browserSync che deve aggionrare la pagina
                lifetime.ApplicationStarted.Register(() =>
                {
                    string filePath = Path.Combine(env.ContentRootPath, "bin/reload.txt");
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                }
                );
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
