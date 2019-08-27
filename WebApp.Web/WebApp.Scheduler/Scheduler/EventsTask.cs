namespace WebApp.Scheduler.Scheduler
{
    using Domain;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Services.EventService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventsTask : ScheduledProcessor
    {
        public EventsTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory) { }

        protected override string Schedule => "*/1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var events = GetAllEvents(serviceProvider);

            UpdateCache(serviceProvider, events);

            return Task.CompletedTask;
        }

        private IEnumerable<Event> GetAllEvents(IServiceProvider serviceProvider)
        {
            var eventService = serviceProvider.GetService(typeof(IEventService)) as IEventService;

            return eventService.GetAllEvents().ToList();
        }

        private void UpdateCache(IServiceProvider serviceProvider, IEnumerable<Event> events)
        {
            var cache = serviceProvider.GetService(typeof(IDistributedCache)) as IDistributedCache;

            var serializedObject = JsonConvert.SerializeObject(events);
            cache.SetString("events", serializedObject);

            Console.WriteLine($"Events saved to Redis.({DateTime.Now})");
        }
    }
}