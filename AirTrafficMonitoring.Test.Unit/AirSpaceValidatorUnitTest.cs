using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Receiver;
using AirTrafficMonitoring.Track;
using AirTrafficMonitoring.Validator;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
  [TestFixture]
  class AirSpaceValidatorUnitTest
  {
    private IReceiver _receiver;
    private IValidator _uut;
    private int _nEventsRaised;

    [SetUp]
    public void SetUp()
    {
      _nEventsRaised = 0;
      _receiver = Substitute.For<IReceiver>();
      _uut = new AirSpaceValidator(_receiver, 10000, 90000, 10000, 90000, 500, 20000);

    }

    [Test]
    public void OnReceivedDataReady_RaiseReceivedDataReadyOnce_ValidatedTracksRaiseOnce()
    {
      var dateTime = new DateTime(2016, 11, 14, 18, 57, 53, 123);
      ITrackObj trackObj = new TrackObj("Test01", 55000, 55000, 9750, dateTime);

      _uut.ValidatedTracks += (sender, args) =>
      {
        _nEventsRaised++;
      };

      var receivedTrackObjsEventArgs = new ReceivedTrackObjsEventArgs()
      {
        ReceivedTrackObjs = new List<ITrackObj>() { trackObj }
      };
      _receiver.ReceivedData += Raise.EventWith<ReceivedTrackObjsEventArgs>(receivedTrackObjsEventArgs);

      Assert.That(_nEventsRaised, Is.EqualTo(1));
    }

    [Test]
    public void OnReceivedDataReady_RaiseReceivedDataReadyOnce_ValidatedTracksNotRaise()
    {
      _uut.ValidatedTracks += (sender, args) =>
      {
        _nEventsRaised++;
      };

      var receivedTrackObjsEventArgs = new ReceivedTrackObjsEventArgs() { ReceivedTrackObjs = new List<ITrackObj>() };
      _receiver.ReceivedData += Raise.EventWith<ReceivedTrackObjsEventArgs>(receivedTrackObjsEventArgs);

      Assert.That(_nEventsRaised, Is.EqualTo(0));
    }
  }
}