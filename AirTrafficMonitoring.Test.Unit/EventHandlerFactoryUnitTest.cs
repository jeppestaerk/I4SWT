using System;
using AirTrafficMonitoring.EventHandler;
using AirTrafficMonitoring.EventPublisher;
using AirTrafficMonitoring.Validator;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
  [TestFixture]
  class EventHandlerFactoryUnitTest
  {
    private EventHandlerFactory _uut;
    private IEventHandler _eventHandler;
    private IEventObj _eventObj;
    private IEventListGenerator _eventListGenerator;
    private IValidator _validator;

    [SetUp]
    public void Setup()
    {
      _eventListGenerator = Substitute.For<IEventListGenerator>();
      _eventHandler = Substitute.For<IEventHandler>();
      _validator = Substitute.For<IValidator>();
      _eventObj = Substitute.For<IEventObj>();
      _uut = new EventHandlerFactory();
    }

    [Test]
    public void CreateEventHandler_TrackEnteredAirspaceEventPassed_TrackEnteredAirspaceEventHandlerReturned()
    {
      _eventObj.EventType.Returns(EventType.Entered);
      _eventHandler = _uut.CreateEventHandler(_eventObj, _eventListGenerator);

      Assert.IsInstanceOf<TrackEnteredAirspaceEventHandler>(_eventHandler);
    }

    [Test]
    public void CreateEventHandler_TrackLeftAirspaceEventPassed_TrackLeftAirspaceEventHandlerReturned()
    {
      _eventObj.EventType.Returns(EventType.Left);
      _eventHandler = _uut.CreateEventHandler(_eventObj, _eventListGenerator);

      Assert.IsInstanceOf<TrackLeftAirspaceEventHandler>(_eventHandler);
    }

    [Test]
    public void CreateEventHandler_SeperationEventPassed_SeperationEventHandlerReturned()
    {
      _eventObj.EventType.Returns(EventType.Seperation);
      _eventHandler = _uut.CreateEventHandler(_eventObj, _eventListGenerator);

      Assert.IsInstanceOf<SeperationEventHandler>(_eventHandler);
    }

    [Test]
    public void CreateEventHandler_NullEventPassed_NullEventHandlerReturned()
    {
      _eventObj.EventType.Returns(default(EventType));

      Assert.That(() => { _uut.CreateEventHandler(_eventObj, _eventListGenerator); }, Throws.TypeOf<ArgumentOutOfRangeException>());
    }

  }
}
