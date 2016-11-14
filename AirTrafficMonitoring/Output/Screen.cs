using System;
using System.Collections.Generic;
using AirTrafficMonitoring.EventPublisher;
using AirTrafficMonitoring.Track;
using AirTrafficMonitoring.Validator;

namespace AirTrafficMonitoring.Output
{
  public class Screen : IScreen
  {
    private List<IEventObj> _eventObjs = new List<IEventObj>();
    private List<ITrackObj> _trackObjs = new List<ITrackObj>();

    public Screen(EventListGenerator eventListGenerator, IValidator validator)
    {
      validator.ValidatedTracks += UpdateDisplayAllList;
      eventListGenerator.NewEventAdded += UpdateDisplayEventList;
      UpdateScreen();
    }

    public void UpdateScreen()
    {
      Console.Clear();

      Console.Write("".PadLeft(30, '-'));
      Console.Write("{0,8} ", "Tracks:");
      Console.Write("{0,-9:T}", DateTime.Now);
      Console.WriteLine("".PadRight(30, '-'));
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("{0,-8}{1,14}{2,14}{3,14}{4,14}{5,14}", "Tag", "X-Position", "Y-Position", "Altitude",
        "Velocity", "Course");
      Console.ForegroundColor = ConsoleColor.White;
      if(_trackObjs.Count != 0)
      {
        foreach(var trackObj in _trackObjs)
          Console.WriteLine(
            $"{trackObj.Tag,-8}{trackObj.XCoordinat,12:D} m{trackObj.YCoordinat,12:D} m{trackObj.Altitude,12:D} m{trackObj.Velocity,10:F2} m/s{trackObj.Course,10:F2} deg");
      }
      else
      {
        Console.WriteLine();
        Console.WriteLine("{0,50}", "No tracks in airspace!");
      }

      Console.WriteLine();

      Console.Write("".PadLeft(30, '-'));
      Console.Write("{0,8} ", "Events:");
      Console.Write("{0,-9:T}", DateTime.Now);
      Console.WriteLine("".PadRight(30, '-'));
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-18}", "Time", "Type", "Category", "Tag(s)");
      Console.ForegroundColor = ConsoleColor.White;
      if(_eventObjs.Count != 0)
      {
        foreach(var eventObj in _eventObjs)
        {
          Console.Write($"{eventObj.TimeStamp,-20}{eventObj.EventType,-20}{eventObj.EventCategory,-20}");
          foreach(var trackTag in eventObj.TrackTag)
            Console.Write($"{trackTag} ");
          Console.WriteLine();
        }
      }
      else
      {
        Console.WriteLine();
        Console.WriteLine("{0,50}", "No events in airspace!");
      }
    }

    private void UpdateDisplayAllList(object sender, ValidatedTrackObjsEventArgs e)
    {
      _trackObjs = e.ValidatedTrackObjs;
      UpdateScreen();
    }

    private void UpdateDisplayEventList(object sender, OnNewEventArgs e)
    {
      _eventObjs = e.EventObjs;
      UpdateScreen();
    }
  }
}