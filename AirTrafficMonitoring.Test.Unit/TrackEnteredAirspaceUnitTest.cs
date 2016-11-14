using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.EventHandler;
using AirTrafficMonitoring.EventPublisher;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TrackEnteredAirspaceUnitTest
    {
       private TrackEnteredAirspaceEventHandler _uut;
       private IEventListGenerator _eventListGenerator;
       private IEventObj _eventObj;

        [SetUp]
        public void Setup()
        {
            _eventListGenerator = Substitute.For<IEventListGenerator>();
            _eventObj = Substitute.For<IEventObj>();
            _uut = new TrackEnteredAirspaceEventHandler(_eventListGenerator);
            _uut.elg = _eventListGenerator;

        }

        [Test]
        public void HandleEventTreadFunc__DeleteEventFromListIsCalledOnEventListGenerator()
        {
            _uut.HandleEventThreadFunc(_eventObj);
            _eventListGenerator.Received().DeleteEventFromList(_eventObj);
        }

    }
}
