using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.Track;
using TransponderReceiver;

namespace AirTrafficMonitoring.Receiver
{
  public class TransponderDataReceiver : IReceiver
  {
    private readonly string _dateTimeFormat;
    private readonly ITransponderReceiver _transponderReceiver;
    private List<ITrackObj> _previousTracks;

    public TransponderDataReceiver(ITransponderReceiver transponderReceiver, string dateTimeFormat)
    {
      _transponderReceiver = transponderReceiver;
      _dateTimeFormat = dateTimeFormat;
      _transponderReceiver.TransponderDataReady += OnTransponderDataReady;

      _previousTracks = new List<ITrackObj>();
    }

    public event EventHandler<ReceivedTrackObjsEventArgs> ReceivedData;

    private void OnTransponderDataReady(object sender, RawTransponderDataEventArgs args)
    {
      if(args.TransponderData.Count <= 0)
        return;

      var recevedTracks = new List<ITrackObj>();

      foreach(var dataSet in args.TransponderData)
      {
        var data = dataSet.Split(';');

        var track = new TrackObj
        (
          data[0],
          Convert.ToInt32(data[1]),
          Convert.ToInt32(data[2]),
          Convert.ToInt32(data[3]),
          DateTime.ParseExact(data[4], _dateTimeFormat, null)
        );

        if(_previousTracks.Count > 0)
          if(_previousTracks.Exists(t => t.Tag == track.Tag))
          {
            var previousTrack = _previousTracks.First(t => t.Tag == track.Tag);
            track.CalculateCurse(previousTrack);
            track.CalculateVelocity(previousTrack);
          }

        recevedTracks.Add(track);
      }

      _previousTracks = recevedTracks;

      OnReceivedData(new ReceivedTrackObjsEventArgs { ReceivedTrackObjs = recevedTracks });
    }

    protected virtual void OnReceivedData(ReceivedTrackObjsEventArgs e)
    {
      ReceivedData?.Invoke(this, e);
    }
  }
}