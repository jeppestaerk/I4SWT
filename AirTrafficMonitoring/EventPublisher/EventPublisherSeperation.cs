using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.Track;
using AirTrafficMonitoring.Validator;

namespace AirTrafficMonitoring.EventPublisher
{
  public class EventPublisherSeperation : EventPublisher
  {
    private List<IEventObj> _currentEventObjs = new List<IEventObj>();
    public EventPublisherSeperation(EventListGenerator elg, IValidator airSpace) : base(elg, airSpace)
    {
    }

    public override IEventObj EventObjGen(ITrackObj trackObj)
    {
      IEventObj eventObj = new EventObj();

      eventObj.TimeStamp = trackObj.Timestamp;
      eventObj.EventType = EventType.NotDefined;  //Kan ikke determineres endnu.
      eventObj.EventCategory = EventCategory.Warning;
      eventObj.TrackTag.Add(trackObj.Tag);

      return eventObj;
    }

    public int CalcDistance(ITrackObj a, ITrackObj b)
    {
      var distance = 0;

      var laDis = Math.Abs((double)b.XCoordinat - (double)a.XCoordinat);
      var loDis = Math.Abs((double)b.YCoordinat - (double)a.YCoordinat);

      var distanceSqr = ((Math.Pow(laDis, 2)) + (Math.Pow(loDis, 2)));

      distance = (int)Math.Sqrt(distanceSqr);

      return distance;
    }

    public override void EventCheck(List<ITrackObj> trackObjList)
    {
      foreach(var trackObj in trackObjList)
      {
        CheckForSeperation(trackObj, trackObjList);
      }
    }

    private void CheckForSeperation(ITrackObj trackObj, List<ITrackObj> trackObjList)
    {
      foreach(var obj in trackObjList.ToList())
      {
        if(trackObj.Tag == obj.Tag)
        {
          break;
        }
        var vertSeperation = Math.Abs(trackObj.Altitude - obj.Altitude);
        var horiSeperation = CalcDistance(trackObj, obj);

        if(vertSeperation < 300 && horiSeperation < 5000)
        {
          if(_currentEventObjs.Count > 0)
          {
            foreach(var currentEventObj in _currentEventObjs.ToList())
            {
              if(currentEventObj.TrackTag.Contains(trackObj.Tag) && currentEventObj.TrackTag.Contains(obj.Tag))
              {
                break;
              }
              else
              {
                var eventObj = EventObjGen(obj);
                eventObj.TrackTag.Add(trackObj.Tag);
                eventObj.EventType = EventType.Seperation;

                PublisherEventListGenerator.AddEventToList(eventObj);

                _currentEventObjs.Add(eventObj);
              }
            }
          }
          else
          {
            var eventObj = EventObjGen(obj);
            eventObj.TrackTag.Add(trackObj.Tag);
            eventObj.EventType = EventType.Seperation;

            PublisherEventListGenerator.AddEventToList(eventObj);

            _currentEventObjs.Add(eventObj);
          }
        }
        else
        {
          foreach(var currentEventObj in _currentEventObjs.ToList())
          {
            if(currentEventObj.TrackTag.Contains(trackObj.Tag) && currentEventObj.TrackTag.Contains(obj.Tag))
            {
              PublisherEventListGenerator.DeleteEventFromList(currentEventObj);
            }
          }
        }
      }
    }
  }
}
