using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Receiver;
using AirTrafficMonitoring.Track;

namespace AirTrafficMonitoring.Validator
{
  public class AirSpaceValidator : IValidator
  {
    private readonly IReceiver _receiver;
    private readonly int _aMaximum;
    private readonly int _aMinimum;
    private readonly int _xMaximum;
    private readonly int _xMinimum;
    private readonly int _yMaximum;
    private readonly int _yMinimum;

    public AirSpaceValidator(IReceiver receiver, int xMinimum, int xMaximum, int yMinimum, int yMaximum, int aMinimum,
      int aMaximum)
    {
      _receiver = receiver;
      _xMinimum = xMinimum;
      _xMaximum = xMaximum;
      _yMinimum = yMinimum;
      _yMaximum = yMaximum;
      _aMinimum = aMinimum;
      _aMaximum = aMaximum;

      _receiver.ReceivedData += OnReceivedDataReady;
    }

    public event EventHandler<ValidatedTrackObjsEventArgs> ValidatedTracks;

    private void OnReceivedDataReady(object sender, ReceivedTrackObjsEventArgs e)
    {
      if(e.ReceivedTrackObjs.Count <= 0)
        return;

      var validatedTracks = new List<ITrackObj>();

      foreach(var track in e.ReceivedTrackObjs)
        if((track.XCoordinat >= _xMinimum) && (track.XCoordinat <= _xMaximum) && (track.YCoordinat >= _yMinimum) &&
            (track.YCoordinat <= _yMaximum) && (track.Altitude >= _aMinimum) && (track.Altitude <= _aMaximum))
          validatedTracks.Add(track);

      OnTrackValidated(new ValidatedTrackObjsEventArgs { ValidatedTrackObjs = validatedTracks });
    }

    protected virtual void OnTrackValidated(ValidatedTrackObjsEventArgs e)
    {
      ValidatedTracks?.Invoke(this, e);
    }
  }
}