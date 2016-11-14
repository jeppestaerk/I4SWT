using System;
using AirTrafficMonitoring.Track;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TrackObjUnitTest
    {
        private TrackObj _uut;
        private ITrackObj _trackObj;

        [SetUp]
        public void SetUp()
        {
            _trackObj = Substitute.For<ITrackObj>();
            _uut = new TrackObj("TAG123", 12345, 54321, 15555, DateTime.Today);
        }

        [Test]
        public void SetTag_TAG123_tagIsTAG123()
        {
            Assert.That(_uut.Tag, Is.EqualTo("TAG123"));
        }
        [Test]
        public void SetxCoor_12345_xCoorIs12345()
        {
            Assert.That(_uut.XCoordinat, Is.EqualTo(12345));
        }
        [Test]
        public void SetyCoor_54321_yCoorIs54321()
        {
            Assert.That(_uut.YCoordinat, Is.EqualTo(54321));
        }
        [Test]
        public void SetAltitude_15555_altitudeIs15555()
        {
            Assert.That(_uut.Altitude, Is.EqualTo(15555));
        }
        [Test]
        public void SetTime_Today_TimeIsToday()
        {
            Assert.That(_uut.Timestamp, Is.EqualTo(DateTime.Today));
        }
        [Test]
        public void SetVelocity_105dot05_VelocityIs105dot05()
        {
            _uut.Velocity = 105.05;

            Assert.That(_uut.Velocity,Is.EqualTo(105.05));
        }

        [Test]
        public void SetCourse_99dot99_CourseIs_99dot99()
        {
            _uut.Course = 99.99;

            Assert.That(_uut.Course, Is.EqualTo(99.99));
        }
    }
}
