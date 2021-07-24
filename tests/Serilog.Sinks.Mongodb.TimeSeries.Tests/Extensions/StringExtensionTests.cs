using FluentAssertions;
using NUnit.Framework;
using Serilog.Sinks.Mongodb.TimeSeries.Extensions;

namespace Serilog.Sinks.Mongodb.TimeSeries.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase(null, "[NULL]")]
        [TestCase("test.test", "test-test")]
        [TestCase("$test", "_test")]
        public void ShouldGetExpectedSavableString(string? value, string expected)
        {
            // Act
            var result = value.ToSaveAbleString();

            // Assert
            result.Should().Be(expected);
        }
    }
}