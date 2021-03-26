using DontThinkJustDrink.Api.Data;
using DontThinkJustDrink.Api.Data.Interfaces;
using DontThinkJustDrink.Api.Helpers;
using DontThinkJustDrink.Api.Managers;
using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Middlewares;
using DontThinkJustDrink.Api.Repositories;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using DontThinkJustDrink.Api.Settings;
using DontThinkJustDrink.Api.Settings.Interfaces;
using Microsoft.AspNetCore.Authentication;
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
            services.AddCors();
            services.AddControllers();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            #region Configuration Dependencies

            services.Configure<MainAppDatabaseSettings>(
                Configuration.GetSection(nameof(MainAppDatabaseSettings)));
            services.Configure<HashingSettings>(
                Configuration.GetSection(nameof(HashingSettings)));
            services.Configure<BasicSecuritySettings>(
                Configuration.GetSection(nameof(BasicSecuritySettings)));

            services.AddSingleton<IMainAppDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MainAppDatabaseSettings>>().Value);
            services.AddSingleton<IHashingSettings>(sp =>
                sp.GetRequiredService<IOptions<HashingSettings>>().Value);
            services.AddSingleton<IBasicSecuritySettings>(sp =>
                sp.GetRequiredService<IOptions<BasicSecuritySettings>>().Value);

            #endregion

            #region Project Dependencies

            services.AddTransient<IMainAppContext, MainAppContext>();
            services.AddTransient<IAppVersionsRepository, AppVersionsRepository>();
            services.AddTransient<IUserFeedbackRepository, UserFeedbackRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IDecksRepository, DecksRepository>();
            services.AddTransient<IAppVersionsManager, AppVersionManager>();
            services.AddTransient<IUserFeedbackManager, UserFeedbackManager>();
            services.AddTransient<IUsersManager, UsersManager>();
            services.AddTransient<IDecksManager, DecksManager>();
            services.AddTransient<IPasswordHelper, PasswordHelper>();

            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DontThinkJustDrink.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DontThinkJustDrink API v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();
#if !DEBUG
            app.UseHttpsRedirection();
#endif
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
