using System;
using System.IO;
using AirTrafficMonitoring.EventPublisher;

namespace AirTrafficMonitoring.Output
{
  public class Log : ILog
  {
    private FileInfo _fi;


    public Log(IEventListGenerator eventListGenerator)
    {
      string path = "EventLog.txt";
      _fi = new FileInfo(path);

      // If file exists, create file with another name.
      if (_fi.Exists)
      {
        for (var i = 1; _fi.Exists; i++)
        {
          string numAppend = i + ".txt";
          path = "EventLog" + numAppend;
          _fi = new FileInfo(path);
        }
      }

      //Create a file to write to.
      using (StreamWriter sw = _fi.CreateText())
      {
        sw.WriteLine("Log of Events");
        sw.WriteLine("Log created: " + DateTime.Now);
        sw.WriteLine(" ");
      }

      ////Subscribe to Event from IEventListGenerator
      eventListGenerator.NewEventAdded += UpdateLog;
    }

    private void UpdateLog(object o, OnNewEventArgs args)
    {

      if (args.NewesteEventObj != null)
      {
        using (StreamWriter sw = _fi.AppendText())
        {
          sw.WriteLine("New Event:");
          sw.WriteLine("Event Category: " + args.NewesteEventObj.EventCategory);
          sw.WriteLine("Event Type: " + args.NewesteEventObj.EventType);

          foreach (var trackTag in args.NewesteEventObj.TrackTag)
          {
            sw.WriteLine("TrackTag: " + trackTag);
          }

          sw.WriteLine("TimeStamp: " + args.NewesteEventObj.TimeStamp);
        }
      }
      
      

    }



  }
}
