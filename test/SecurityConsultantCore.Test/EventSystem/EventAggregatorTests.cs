using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.EventSystem;
using System.Diagnostics.CodeAnalysis;

namespace SecurityConsultantCore.Test.EventSystem
{
    public class EventOne { }

    public class EventTwo { }

    [TestClass, ExcludeFromCodeCoverage]
    public class EventAggregatorTests
    {
        [TestMethod]
        public void EventAggregator_OnPublish_SubscriberActionRun()
        {
            var sut = new EventAggregator();
            var didActionRun = false;
            sut.Subscribe<EventOne>(e => didActionRun = true);

            sut.Publish(new EventOne());

            Assert.IsTrue(didActionRun);
        }

        [TestMethod]
        public void EventAggregator_OnPublishOneType_DoesNotPublishOtherTypes()
        {
            var sut = new EventAggregator();
            var actionOneFired = false;
            var actionTwoFired = false;
            sut.Subscribe<EventOne>(e => actionOneFired = true);
            sut.Subscribe<EventTwo>(e => actionTwoFired = true);

            sut.Publish(new EventTwo());

            Assert.IsFalse(actionOneFired);
            Assert.IsTrue(actionTwoFired);
        }
    }
}
