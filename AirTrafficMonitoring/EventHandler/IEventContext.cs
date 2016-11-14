using AirTrafficMonitoring.EventPublisher;

namespace AirTrafficMonitoring.EventHandler
{
    public interface IEventContext
    {
        IEventListGenerator ContextEventListGenerator { get; set; }

        void InitiateEventHandling(IEventObj newEvent);
        void OnNewEvent(object source, OnNewEventArgs args);
    }
}