using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Receiver;
using AirTrafficMonitoring.Track;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Unit
{
  [TestFixture]
  class TransponderDataReceiverUnitTest
  {
    private ITransponderReceiver _transponderReceiver;
    private IReceiver _uut;
    private int _nEventsRaised;

    [SetUp]
    public void SetUp()
    {
      _nEventsRaised = 0;
      _transponderReceiver = Substitute.For<ITransponderReceiver>();
      _uut = new TransponderDataReceiver(_transponderReceiver, "yyyyMMddHHmmssFFFF");
    }

    [Test]
    public void OnTransponderDataReady_RaiseTransponderDataReadyOnce_OnReceivedDataEventRaisedOnce()
    {
      _uut.ReceivedData += (sender, args) =>
      {
        _nEventsRaised++;
      };

      var rawTransponderDataEventArgs = new RawTransponderDataEventArgs(new List<string>() { "Test01;55000;55000;9750;" + DateTime.Now.ToString("yyyyMMddHHmmssFFFF") });
      _transponderReceiver.TransponderDataReady += Raise.EventWith<RawTransponderDataEventArgs>(rawTransponderDataEventArgs);

      Assert.That(_nEventsRaised, Is.EqualTo(1));
    }

    [Test]
    public void OnTransponderDataReady_RaiseTransponderDataReadyWithEmptyArgs_OnReceivedDataEventNotRaised()
    {
      _uut.ReceivedData += (sender, args) =>
      {
        _nEventsRaised++;
      };

      var rawTransponderDataEventArgs = new RawTransponderDataEventArgs(new List<string>());
      _transponderReceiver.TransponderDataReady += Raise.EventWith<RawTransponderDataEventArgs>(rawTransponderDataEventArgs);

      Assert.That(_nEventsRaised, Is.EqualTo(0));
    }

    [Test]
    public void OnTransponderDataReady_RaiseTransponderDataReady_OnReceivedDataEventRaisedWithArgs()
    {
      var dateTime = new DateTime(2016, 11, 14, 18, 57, 53, 123);
      ITrackObj trackObj = new TrackObj("Test01", 55000, 55000, 9750, dateTime);
      ITrackObj dataTrackObj = new TrackObj();

      _uut.ReceivedData += (sender, args) =>
      {
        dataTrackObj = args.ReceivedTrackObjs[0];
      };

      var rawTransponderDataEventArgs = new RawTransponderDataEventArgs(new List<string>() { "Test01;55000;55000;9750;" + dateTime.ToString("yyyyMMddHHmmssFFFF") });
      _transponderReceiver.TransponderDataReady += Raise.EventWith<RawTransponderDataEventArgs>(rawTransponderDataEventArgs);

      Assert.AreEqual(trackObj.Tag, dataTrackObj.Tag);
      Assert.AreEqual(trackObj.XCoordinat, dataTrackObj.XCoordinat);
      Assert.AreEqual(trackObj.YCoordinat, dataTrackObj.YCoordinat);
      Assert.AreEqual(trackObj.Altitude, dataTrackObj.Altitude);
      Assert.AreEqual(trackObj.Timestamp, dataTrackObj.Timestamp);
    }
  }
}
