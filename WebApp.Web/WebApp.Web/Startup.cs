namespace WebApp.Web
{
    using Controllers.Mappers;
    using Data;
    using Data.Repo.EventAttendeesRepo;
    using Data.Repo.EventAttendeesToBeApprovedRepo;
    using Data.Repo.EventRepo;
    using Data.Repo.PositionRepo;
    using Data.Repo.RatingRepo;
    using Data.Repo.ScoreRepo;
    using Data.Repo.SportRepo;
    using Data.Repo.UnitOfWork;
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
    using Services.EventAttendanceService;
    using Services.EventService;
    using Services.PositionService;
    using Services.RatingService;
    using Services.ScoreService;
    using Services.SportService;
    using System;
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

            services.AddSingleton<IRatingRepository, RatingRepository>();
            services.AddSingleton<IRatingService, RatingService>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IRatingService, RatingService>();

            services.AddSingleton<IScoreRepository, ScoreRepository>();
            services.AddSingleton<IScoreService, ScoreService>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<IScoreService, ScoreService>();

            //TODO register services and repos
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddSingleton<IEventAttendanceService, EventAttendanceService>();
            services.AddSingleton<IEventAttendeesRepository, EventAttendeesRepository>();
            services.AddSingleton<IEventAttendeesToBeApprovedRepository, EventAttendeesToBeApprovedRepository>();
            services.AddScoped<IEventAttendanceService, EventAttendanceService>();
            services.AddScoped<IEventAttendeesRepository, EventAttendeesRepository>();
            services.AddScoped<IEventAttendeesToBeApprovedRepository, EventAttendeesToBeApprovedRepository>();


            services.AddSingleton<ISportService, SportService>();
            services.AddSingleton<ISportRepository, SportRepository>();
            services.AddScoped<ISportService, SportService>();
            services.AddScoped<ISportRepository, SportRepository>();
            services.AddSingleton<IPositionService, PositionService>();
            services.AddSingleton<IPositionRepository, PositionRepository>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddSingleton<IEventMapper, EventMapper>();
            services.AddScoped<IEventMapper, EventMapper>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration["Redis"];
                option.InstanceName = "SampleInstance";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
