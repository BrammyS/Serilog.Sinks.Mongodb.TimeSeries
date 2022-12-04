using FluentAssertions;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Sinks.Mongodb.TimeSeries.Extensions;

namespace Serilog.Sinks.Mongodb.TimeSeries.Tests.Extensions;

[TestFixture]
public class LogEventLevelExtensionsTests
{
    [TestCase(LogEventLevel.Debug, "debug")]
    [TestCase(LogEventLevel.Verbose, "verbose")]
    [TestCase(LogEventLevel.Error, "error")]
    [TestCase(LogEventLevel.Fatal, "fatal")]
    [TestCase(LogEventLevel.Information, "information")]
    [TestCase(LogEventLevel.Warning, "warning")]
    public void ShouldGetSeverityString(LogEventLevel level, string expected)
    {
        // Act
        var result = level.ToSeverityString();

        // Assert
        result.Should().Be(expected);
    }
}