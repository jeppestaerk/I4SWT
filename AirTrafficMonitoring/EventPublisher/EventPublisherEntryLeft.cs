using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.Track;
using AirTrafficMonitoring.Validator;

namespace AirTrafficMonitoring.EventPublisher
{
  //Klassen hedder ikke 'EventPublisherNotification', da systemet kan udvides med en anden type Notification events.
  public class EventPublisherEntryLeft : EventPublisher
  {
    private List<ITrackObj> _currenTrackObjs = new List<ITrackObj>();

    public EventPublisherEntryLeft(EventListGenerator elg, IValidator airspace) : base(elg, airspace)
    {
    }


    //Lav EventObj fra TrackObj.
    public override IEventObj EventObjGen(ITrackObj trackObj)
    {
      IEventObj eventObj = new EventObj();

      eventObj.TimeStamp = trackObj.Timestamp;
      eventObj.EventType = EventType.NotDefined;  //Kan ikke determineres endnu.
      eventObj.EventCategory = EventCategory.Notification;
      eventObj.TrackTag.Add(trackObj.Tag);

      return eventObj;
    }


    public override void EventCheck(List<ITrackObj> trackObjList)
    {

      foreach(var trackObj in trackObjList.ToList())
      {
        if(!_currenTrackObjs.Exists(t => t.Tag == trackObj.Tag))
        {
          var eventObj = EventObjGen(trackObj);
          eventObj.EventType = EventType.Entered;

          PublisherEventListGenerator.AddEventToList(eventObj);
          _currenTrackObjs.Add(trackObj);

        }

      }

      foreach(var currenTrackObj in _currenTrackObjs.ToList())
      {
        if(!trackObjList.Exists(t => t.Tag == currenTrackObj.Tag))
        {
          var eventObj = EventObjGen(currenTrackObj);
          eventObj.EventType = EventType.Left;

          PublisherEventListGenerator.AddEventToList(eventObj);
          _currenTrackObjs.Remove(currenTrackObj);

        }
      }


      //foreach (var trackObj in trackObjList)
      //{
      //  if (_currenTrackObjs.Count > 0)
      //  {
      //    if (_currenTrackObjs.Exists(t => t.Tag == trackObj.Tag))
      //    {
      //      var eventObj = EventObjGen(trackObj);
      //      eventObj.EventType = EventType.TrackEnteredAirspaceEvent;

      //      PublisherEventListGenerator.AddEventToList(eventObj);
      //    }
      //  }
      //}

      //Lav liste.
      //List<ITrackObj> tempEventList = new List<ITrackObj>();

      //Sammenlign tempEventList med TrackList og opdater den efterfølgende. 

      /*
      //Hvis tempEventList er tom, så kopier alt!
      if (!tempEventList.Any())
      {
          foreach (var trackObj in trackObjList)
          {
              tempEventList.Add(trackObj);
              PublisherEventListGenerator.AddEventToList(EventObjGen(trackObj));  //Tilføj TrackEntry event til event listen.
          }
      }
      else
      {
              //Indsæt den næste kode blok...
      }*/

      //Sammenlign de to lister, og tilføj EntryEvent til den overordnede liste.
      //var notOnEventList = tempEventList.Except(trackObjList);

      //foreach (var trackObj in notOnEventList)
      //{
      //  tempEventList.Add(trackObj);
      //  EventObjGen(trackObj).EventType = EventType.TrackEnteredAirspaceEvent;
      //  PublisherEventListGenerator.AddEventToList(EventObjGen(trackObj));  //Tilføj TrackEntry event til event listen.
      //}

      ////Slet tracks som ikke længere findes på TrackObjList fra tempEventList, og tilføj LeftEvent til den overordnede liste.
      //var notOnTrackList = tempEventList.Except(trackObjList);

      //foreach (var trackObj in notOnTrackList)
      //{
      //  tempEventList.Remove(trackObj);
      //  EventObjGen(trackObj).EventType = EventType.TrackLeftAirspaceEvent;
      //  PublisherEventListGenerator.AddEventToList(EventObjGen(trackObj));  //Tilføj TrackLeft event til event listen.
      //}
    }
  }
}
