using AirTrafficMonitoring.EventHandler;
using AirTrafficMonitoring.EventPublisher;
using AirTrafficMonitoring.Validator;
using NSubstitute;
using NUnit.Framework;


namespace AirTrafficMonitoring.Test.Unit
{
  [TestFixture]

  class EventContextUnitTest
  {
    //other needed stuff
    private IEventHandler _eventHandler;
    private IEventListGenerator _eventListGenerator;
    private IValidator _validator;
    private IEventObj _eventObj;
    private IEventHandlerFactory _eventHandlerFactory;
    private EventContext _uut;

    [SetUp]
    public void Setup()
    {
      _eventListGenerator = Substitute.For<IEventListGenerator>();
      _eventHandler = Substitute.For<IEventHandler>();
      _validator = Substitute.For<IValidator>();
      _eventObj = Substitute.For<IEventObj>();
      _eventHandlerFactory = Substitute.For<IEventHandlerFactory>();
      _uut = new EventContext(_eventListGenerator, _validator);
      _uut.Factory = _eventHandlerFactory;
    }

    [Test]
    public void InitiateEventHandling_EventObjOfTypeTrackEnteredAirspace_FactoryMethodIsCalled()
    {
      _eventObj.EventType.Returns(EventType.Entered);
      _eventHandlerFactory.CreateEventHandler(_eventObj, _eventListGenerator).Returns(_eventHandler);
      _uut.InitiateEventHandling(_eventObj);

      _eventHandlerFactory.Received().CreateEventHandler(_eventObj, _eventListGenerator);
    }

    [Test]
    public void InitiateEventHandling_EventObjOfTypeTrackEnteredAirspace_HandleMethodIsCalled()
    {
      _eventObj.EventType.Returns(EventType.Entered);
      _eventHandlerFactory.CreateEventHandler(_eventObj, _eventListGenerator).Returns(_eventHandler);
      _uut.InitiateEventHandling(_eventObj);

      _eventHandler.Received().Handle(_eventObj);
    }

  }
}