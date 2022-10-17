using System;
using System.Collections.Generic;
using FluentAssertions;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using Xunit;

namespace MozaeekCore.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void PointDeleteEventShouldDeserializeByOutBoxProperly()
        {
            PointDeleted expected = new PointDeleted(1);
            var oBox = new OutboxMessage(expected, expected.EventId, expected.PublishDateTime);
            var actual = oBox.RecreateMessage();
            ((Guid)actual.GetType().GetProperty("EventId").GetValue(actual, null)).Should().Be(expected.EventId);
        }
        [Fact]
        public void PointCreateEventShouldDeserializeByOutBoxProperly()
        {
            PointCreatedOrUpdated expected = new PointCreatedOrUpdated(1, "1", null, true);
            var oBox = new OutboxMessage(expected, expected.EventId, expected.PublishDateTime);
            var actual = oBox.RecreateMessage();
            ((Guid)actual.GetType().GetProperty("EventId").GetValue(actual, null)).Should().Be(expected.EventId);
        }

        [Fact]
        public void LabelDeleteEventShouldDeserializeByOutBoxProperly()
        {
            LabelDeleted expected = new LabelDeleted(1);
            var oBox = new OutboxMessage(expected, expected.EventId, expected.PublishDateTime);
            var actual = oBox.RecreateMessage();
            ((Guid)actual.GetType().GetProperty("EventId").GetValue(actual, null)).Should().Be(expected.EventId);
        }

        [Fact]
        public void LabelCreateEventShouldDeserializeByOutBoxProperly()
        {
            LabelCreatedOrUpdated expected = new LabelCreatedOrUpdated(1, "1", null, true);
            var oBox = new OutboxMessage(expected, expected.EventId, expected.PublishDateTime);
            var actual = oBox.RecreateMessage();
            ((Guid)actual.GetType().GetProperty("EventId").GetValue(actual, null)).Should().Be(expected.EventId);
        }


        [Fact]
        public void RequestTargetDeleteEventShouldDeserializeByOutBoxProperly()
        {
            RequestTargetDeleted expected = new RequestTargetDeleted(1);
            var oBox = new OutboxMessage(expected, expected.EventId, expected.PublishDateTime);
            var actual = oBox.RecreateMessage();
            ((Guid)actual.GetType().GetProperty("EventId").GetValue(actual, null)).Should().Be(expected.EventId);
        }

        [Fact]
        public void RequestTargetCreateEventShouldDeserializeByOutBoxProperly()
        {
            RequestTargetCreatedOrUpdated expected = new RequestTargetCreatedOrUpdated(1, "1", "", new List<long>(), new List<long>(), true, true);
            var oBox = new OutboxMessage(expected, expected.EventId, expected.PublishDateTime);
            var actual = oBox.RecreateMessage();
            ((Guid)actual.GetType().GetProperty("EventId").GetValue(actual, null)).Should().Be(expected.EventId);
        }
        [Fact]
        public void RequestActDeleteEventShouldDeserializeByOutBoxProperly()
        {
            RequestActDeleted expected = new RequestActDeleted(1);
            var oBox = new OutboxMessage(expected, expected.EventId, expected.PublishDateTime);
            var actual = oBox.RecreateMessage();
            ((Guid)actual.GetType().GetProperty("EventId").GetValue(actual, null)).Should().Be(expected.EventId);
        }

        [Fact]
        public void RequestActCreateEventShouldDeserializeByOutBoxProperly()
        {
            RequestActCretedOrUpdated expected = new RequestActCretedOrUpdated(1, "1", true);
            var oBox = new OutboxMessage(expected, expected.EventId, expected.PublishDateTime);
            var actual = oBox.RecreateMessage();
            ((Guid)actual.GetType().GetProperty("EventId").GetValue(actual, null)).Should().Be(expected.EventId);
        }
    }
}
