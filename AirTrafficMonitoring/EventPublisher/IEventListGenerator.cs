using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.EventPublisher
{
  public interface IEventListGenerator
  {
    event EventHandler<OnNewEventArgs> NewEventAdded;

    void AddEventToList(IEventObj eventObj);
    void DeleteEventFromList(IEventObj eventObj);
  }

  public class OnNewEventArgs : EventArgs
  {
    public List<IEventObj> EventObjs { get; set; }
    public IEventObj NewesteEventObj { get; set; }
  }
}