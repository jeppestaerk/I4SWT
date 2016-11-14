using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Track;

namespace AirTrafficMonitoring.Validator
{
  public interface IValidator
  {
    event EventHandler<ValidatedTrackObjsEventArgs> ValidatedTracks;

  }

  public class ValidatedTrackObjsEventArgs : EventArgs
  {
    public List<ITrackObj> ValidatedTrackObjs { get; set; }
  }
}