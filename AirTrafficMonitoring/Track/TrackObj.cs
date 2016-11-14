using System;

namespace AirTrafficMonitoring.Track
{
  public class TrackObj : ITrackObj
  {
    public TrackObj(string tag, int xCoordinat, int yCoordinat, int altiude, DateTime timestamp)
    {
      Tag = tag;
      XCoordinat = xCoordinat;
      YCoordinat = yCoordinat;
      Altitude = altiude;
      Timestamp = timestamp;
      Velocity = 0;
      Course = 0;
    }

    public TrackObj()
    {
    }

    public string Tag { get; set; }
    public int XCoordinat { get; set; }
    public int YCoordinat { get; set; }
    public int Altitude { get; set; }
    public DateTime Timestamp { get; set; }
    public double Velocity { get; set; }
    public double Course { get; set; }

    public void CalculateVelocity(ITrackObj previousTrackObj)
    {
      var xDifference = Math.Abs(XCoordinat - previousTrackObj.XCoordinat);
      var yDifference = Math.Abs(YCoordinat - previousTrackObj.YCoordinat);

      var timeDifference = Timestamp - previousTrackObj.Timestamp;

      var distance = Math.Sqrt(Math.Pow(xDifference, 2) + Math.Pow(yDifference, 2));

      Velocity = distance / timeDifference.TotalSeconds;
    }

    public void CalculateCurse(ITrackObj previousTrackObj)
    {
      var xDifference = XCoordinat - previousTrackObj.XCoordinat;
      var yDifference = YCoordinat - previousTrackObj.YCoordinat;

      var angle = 90 - Math.Atan2(yDifference, xDifference) * (180 / Math.PI);

      Course = angle < 0 ? angle + 360 : angle;
    }
  }
}