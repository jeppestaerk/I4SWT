using System.Collections.Generic;
using AirTrafficMonitoring.Track;
using AirTrafficMonitoring.Validator;

namespace AirTrafficMonitoring.EventPublisher
{
  public abstract class EventPublisher
  {
    public AirSpaceValidator PublisherAirSpaceValidator { get; set; }


    public EventListGenerator PublisherEventListGenerator { get; set; }

    public EventPublisher(EventListGenerator elg, IValidator airSpace)
    {
      PublisherEventListGenerator = elg;
      airSpace.ValidatedTracks += OnListUpdate;
    }
    /*
    public EventPublisher(IValidator airSpace)
    {
        airSpace.ValidatedTracks += OnListUpdate;
    }*/

    public abstract void EventCheck(List<ITrackObj> trackObjList);

    public abstract IEventObj EventObjGen(ITrackObj trackobj);


    //Subscribe til ListUpdateEvent() fra TrackList. 
    void OnListUpdate(object source, ValidatedTrackObjsEventArgs args) //Hvor args er en reference til listen dannet af AirSpaceValidator.
    {
      EventCheck(args.ValidatedTrackObjs);
    }
  }
}
