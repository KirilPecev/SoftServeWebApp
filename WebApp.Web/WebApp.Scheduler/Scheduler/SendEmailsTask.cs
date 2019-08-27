namespace WebApp.Scheduler.Scheduler
{
    using Data;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Notifications;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SendEmailsTask : ScheduledProcessor
    {
        public SendEmailsTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory) { }

        protected override string Schedule => "*/1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var events = EventsForTomorrow(serviceProvider);

            NotifyUsers(serviceProvider, events).Wait();

            return Task.CompletedTask;
        }

        private IEnumerable<Event> EventsForTomorrow(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService(typeof(WebAppDbContext)) as WebAppDbContext;

            var tomorrow = DateTime.Today.AddDays(1).AddHours(12);

            var events = db.Events
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Where(e => e.Time == tomorrow && e.IsDeleted == false)
                .ToList();

            return events;
        }

        private async Task NotifyUsers(IServiceProvider serviceProvider, IEnumerable<Event> events)
        {
            var emailSender = serviceProvider.GetService(typeof(IEmailSender)) as IEmailSender;
            List<string> emails = new List<string>();

            foreach (var currentEvent in events)
            {
                foreach (var user in currentEvent.Users)
                {
                    emails.Add(user.User.Email);
                }
            }

            foreach (var email in emails)
            {
                await emailSender.SendEmailAsync(email, "Sport events", "You have incoming events tomorrow");
            }

            Console.WriteLine($"Sended emails to {emails.Count} users.({DateTime.Now})");
        }
    }
}