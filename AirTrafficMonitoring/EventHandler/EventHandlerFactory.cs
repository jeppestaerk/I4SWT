using System;
using AirTrafficMonitoring.EventPublisher;
using AirTrafficMonitoring.Validator;

namespace AirTrafficMonitoring.EventHandler
{
  public class EventHandlerFactory : IEventHandlerFactory
  {
    public IEventHandler CreateEventHandler(IEventObj eventObj, IEventListGenerator elg)
    {
      switch (eventObj.EventType)
      {
        case EventType.Seperation:
          IEventHandler eventHandlerS = new SeperationEventHandler(elg);
          return eventHandlerS;

        case EventType.Entered:
          IEventHandler eventHandlerE = new TrackEnteredAirspaceEventHandler(elg);
          return eventHandlerE;

        case EventType.Left:
          IEventHandler eventHandlerL = new TrackLeftAirspaceEventHandler(elg);
          return eventHandlerL;

        default:
          throw new ArgumentOutOfRangeException();

      }
    }
  }
}
