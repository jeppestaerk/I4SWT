using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.EventPublisher
{
    public enum EventType
    {
        NotDefined = 0,
        Seperation = 1,
        Entered = 2,
        Left = 3
    };

    public enum EventCategory
    {
        NotDefined = 0,
        Notification = 1,
        Warning = 2
    };


    public class EventObj : IEventObj
    {
        public DateTime TimeStamp { get; set; }

        public EventType EventType { get; set; }

        public EventCategory EventCategory { get; set; }

        public List<string> TrackTag { get; set; }

        public EventObj()
        {
            TrackTag = new List<string>();
        }
    }


}
