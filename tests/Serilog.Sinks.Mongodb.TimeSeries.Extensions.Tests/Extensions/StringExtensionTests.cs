using FluentAssertions;
using NUnit.Framework;

namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions.Tests.Extensions
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