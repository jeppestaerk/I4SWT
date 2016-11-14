using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.EventPublisher;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class EventObjUnitTest
    {
        private EventObj _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new EventObj();
        }

        [Test]
        public void setTimeStamp_Today_TimeStampIsToday()
        {
            _uut.TimeStamp = DateTime.Today;

            Assert.That(_uut.TimeStamp, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void setEventType_SeperationEvent_EventTypeIsSeperationEvent()
        {
            _uut.EventType = EventType.Seperation;

            Assert.That(_uut.EventType, Is.EqualTo(EventType.Seperation));
        }

        [Test]
        public void setEventCategory_WarningEvent_EventCategoryIsWarningEvent()
        {
            _uut.EventCategory = EventCategory.Warning;

            Assert.That(_uut.EventCategory, Is.EqualTo(EventCategory.Warning));
        }

        [Test]
        public void setTrackTag_TAG123_TrackTagHas1TrackTag()
        {
            _uut.TrackTag.Add("TAG123");

            Assert.That(_uut.TrackTag.Count, Is.EqualTo(1));
        }

        [Test]
        public void setTrackTag_TAG123AndNUM456_TrackTagHas2TrackTags()
        {
            _uut.TrackTag.Add("TAG123");
            _uut.TrackTag.Add("TAG123");

            Assert.That(_uut.TrackTag.Count, Is.EqualTo(2));
        }

        [Test]
        public void setTrackTag_TAG123_TrackTagIsTAG123()
        {
            _uut.TrackTag.Add("TAG123");

            Assert.That(_uut.TrackTag.Contains("TAG123"));
        }

        [Test]
        public void setTrackTag_TAG123AndNUM456_TrackTagIsTAG123AndNUM456()
        {
            _uut.TrackTag.Add("TAG123");
            _uut.TrackTag.Add("NUM456");

            Assert.That(_uut.TrackTag.Contains("TAG123")&&_uut.TrackTag.Contains("NUM456"));
        }
    }
}
