using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.EventPublisher
{
  public interface IEventObj
  {
    EventCategory EventCategory { get; set; }
    EventType EventType { get; set; }
    DateTime TimeStamp { get; set; }
    List<string> TrackTag { get; set; }
  }
}