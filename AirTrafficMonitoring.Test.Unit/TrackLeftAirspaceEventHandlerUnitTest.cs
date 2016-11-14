using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.EventHandler;
using AirTrafficMonitoring.EventPublisher;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TrackLeftAirspaceEventHandlerUnitTest
    {
        private IEventListGenerator _eventListGenerator;
        private IEventObj _eventObj;
        private TrackLeftAirspaceEventHandler _uut;

        [SetUp]
        public void Setup()
        {
            _eventListGenerator = Substitute.For<IEventListGenerator>();
            _eventObj = Substitute.For<IEventObj>();
            _uut = new TrackLeftAirspaceEventHandler(_eventListGenerator);
        }

        [Test]
        public void HandleEventTreadFunc__DeleteEventFromListIsCalledOnEventListGenerator()
        {
            _uut.HandleEventThreadFunc(_eventObj);

            _eventListGenerator.Received().DeleteEventFromList(_eventObj);
        }
    }
}
