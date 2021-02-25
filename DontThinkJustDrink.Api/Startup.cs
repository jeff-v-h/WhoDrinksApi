using DontThinkJustDrink.Api.Data;
using DontThinkJustDrink.Api.Data.Interfaces;
using DontThinkJustDrink.Api.Managers;
using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Repositories;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using DontThinkJustDrink.Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace DontThinkJustDrink.Api
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

            #region Configuration Dependencies

            services.Configure<MainAppDatabaseSettings>(
                Configuration.GetSection(nameof(MainAppDatabaseSettings)));

            services.AddSingleton<IMainAppDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MainAppDatabaseSettings>>().Value);

            #endregion

            #region Project Dependencies

            services.AddTransient<IMainAppContext, MainAppContext>();
            services.AddTransient<IAppVersionRepository, AppVersionRepository>();
            services.AddTransient<IUserFeedbackRepository, UserFeedbackRepository>();
            services.AddTransient<IUserFeedbackManager, UserFeedbackManager>();

            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DontThinkJustDrink.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DontThinkJustDrink API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
