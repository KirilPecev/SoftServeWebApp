namespace WebApp.Scheduler.Scheduler
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Notifications;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RatingsTask : ScheduledProcessor
    {
        public RatingsTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "*/1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService(typeof(WebAppDbContext)) as WebAppDbContext;
            var emailSender = serviceProvider.GetService(typeof(IEmailSender)) as IEmailSender;

            var events = db.Events
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Where(e => e.Time == DateTime.Today.AddDays(1))
                .ToList();

            List<string> emails = new List<string>();

            foreach (var e in events)
            {
                foreach (var user in e.Users)
                {
                    emails.Add(user.User.Email);
                }
            }

            //foreach (var email in emails)
            //{
            //    emailSender.SendEmailAsync(email, "Sport events", "You have incoming events tomorrow").Wait();
            //}

            Console.WriteLine($"Sended emails to {emails.Count} users.({DateTime.Now})");
            return Task.CompletedTask;
        }
    }
}