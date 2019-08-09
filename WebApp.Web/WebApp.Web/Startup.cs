namespace WebApp.Web
{
    using Data;
    using Data.Repo;
    using Data.Seeding;
    using Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Notifications;
    using Notifications.Entities;
    using Scheduler.Scheduler;
    using Services.EventService;
    using System;
    using WebApp.Data.CustomRepos;
    using WebApp.Services.EventAttendance;
    using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<WebAppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<WebAppUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<WebAppDbContext>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddSingleton<IHostedService, EventsTask>();
            services.AddSingleton<IHostedService, SendEmailsTask>();

            services.AddSingleton<IRatingRepo, RatingRepo>();

            //TODO register services and repos
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<IEventRepository, EventRepository>();

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration["Redis"];
                option.InstanceName = "SampleInstance";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //register repos
            services.AddScoped<IEventAttendeesToBeApprovedRepo, EventAttendeesToBeApprovedRepo>();
            services.AddScoped<IEventAttendeesRepo, EventAttendeesRepo>();

            //register services
            services.AddScoped<IEventAttendanceService, EventAttendanceService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<WebAppDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new WebAppDbContextSeeder().SeedAsync(dbContext).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
