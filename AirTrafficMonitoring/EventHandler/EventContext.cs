using System.Linq;
using AirTrafficMonitoring.EventPublisher;
using AirTrafficMonitoring.Validator;

namespace AirTrafficMonitoring.EventHandler
{
  public class EventContext : IEventContext
  {
    public IEventListGenerator ContextEventListGenerator { get; set; }
    public IValidator ContextValidator { get; set; }

    public IEventHandlerFactory Factory = new EventHandlerFactory();

    public EventContext(IEventListGenerator elg, IValidator vali)
    {
      ContextEventListGenerator = elg;
      ContextValidator = vali;
      elg.NewEventAdded += OnNewEvent;
    }

    public void InitiateEventHandling(IEventObj eventObj)
    {
      Factory.CreateEventHandler(eventObj, ContextEventListGenerator).Handle(eventObj);
    }


    public void OnNewEvent(object source, OnNewEventArgs args)
    {
      foreach(var eventObj in args.EventObjs.ToList())
      {
        InitiateEventHandling(eventObj);
      }

    }
  }
}