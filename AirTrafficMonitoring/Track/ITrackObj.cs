using System;

namespace AirTrafficMonitoring.Track
{
  public interface ITrackObj
  {
    int Altitude { get; set; }
    double Course { get; set; }
    string Tag { get; set; }
    DateTime Timestamp { get; set; }
    double Velocity { get; set; }
    int XCoordinat { get; set; }
    int YCoordinat { get; set; }

    void CalculateCurse(ITrackObj previousTrackObj);
    void CalculateVelocity(ITrackObj previousTrackObj);
  }
}