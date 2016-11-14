using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.Validator;

namespace AirTrafficMonitoring.EventPublisher
{
  public class EventListGenerator : IEventListGenerator
  {
    private Object thisLock = new Object();

    private void OnValidateUpdate(object sender, ValidatedTrackObjsEventArgs e)
    {
      foreach(var eventObj in EventObjList.ToList())
      {
        if(eventObj.TimeStamp <= DateTime.Now.AddSeconds(-5))
        {
          EventObjList.Remove(eventObj);
        }
      }
    }

    //Create list of events. 
    public List<IEventObj> EventObjList = new List<IEventObj>();

    public void AddEventToList(IEventObj eventObj)
    {
      lock(thisLock)
      {
        EventObjList.Add(eventObj);
        OnNewEvent(eventObj);
      }
    }

    public void DeleteEventFromList(IEventObj eventObj)
    {
      if(!EventObjList.Contains(null))
      {
        lock(thisLock)
        {
          EventObjList.Remove(eventObj);
          OnNewEvent(null);
        }
      }
    }

    public event EventHandler<OnNewEventArgs> NewEventAdded;

    public void OnNewEvent(IEventObj eventObj)
    {
      NewEventAdded?.Invoke(this, new OnNewEventArgs() { NewesteEventObj = eventObj, EventObjs = EventObjList });
    }

  }
}
