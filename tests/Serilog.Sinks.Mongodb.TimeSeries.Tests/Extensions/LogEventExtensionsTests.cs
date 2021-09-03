using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.Mongodb.TimeSeries.Extensions;

namespace Serilog.Sinks.Mongodb.TimeSeries.Tests.Extensions
{
    [TestFixture]
    public class LogEventExtensionsTests
    {
        [Test]
        public void Should_convert_LogEvents_to_LogDocuments()
        {
            // Arrange
            var logsEvents = new List<LogEvent>
            {
                new (
                    new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    LogEventLevel.Information,
                    null,
                    new MessageTemplate(new List<MessageTemplateToken>
                    {
                        new TextToken("test message, "),
                        new PropertyToken("content", "raw text")
                    }),
                    new List<LogEventProperty>
                    {
                        new ("test property", new ScalarValue(1))
                    }
                )
            };
            
            // Act
            var docs = logsEvents.ToDocuments().ToList();
            
            // Assert
            docs.Count.Should().Be(logsEvents.Count);
            docs.First().Message.Should().Be("test message, raw text");
            docs.First().Timestamp.Should().Be(new DateTime(2000, 1, 1, 1, 1, 1, 1));
            docs.First().Properties.First().Value.Should().Be(1);
        }
    }
}