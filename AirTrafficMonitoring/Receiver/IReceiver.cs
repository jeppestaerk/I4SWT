using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Track;

namespace AirTrafficMonitoring.Receiver
{
  public interface IReceiver
  {
    event EventHandler<ReceivedTrackObjsEventArgs> ReceivedData;
  }

  public class ReceivedTrackObjsEventArgs : EventArgs
  {
    public List<ITrackObj> ReceivedTrackObjs { get; set; }
  }
}