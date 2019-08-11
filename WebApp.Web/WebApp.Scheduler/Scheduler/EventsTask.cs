namespace WebApp.Scheduler.Scheduler
{
    using Data;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Services.EventService;

    public class EventsTask : ScheduledProcessor
    {
        public EventsTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "*/1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {

            var eventService = serviceProvider.GetService(typeof(IEventService)) as IEventService;
            var cache = serviceProvider.GetService(typeof(IDistributedCache)) as IDistributedCache;

            var events = eventService.GetAllEvents().ToList();

            var serializedObject = JsonConvert.SerializeObject(events);
            cache.SetString("events", serializedObject);
            Console.WriteLine($"Events saved to Redis.({DateTime.Now})");
            return Task.CompletedTask;
        }
    }
}