using FluentAssertions;
using MongoDB.Bson.Serialization;
using NUnit.Framework;
using Serilog.Sinks.Mongodb.TimeSeries.Configurations;
using Serilog.Sinks.Mongodb.TimeSeries.Models;

namespace Serilog.Sinks.Mongodb.TimeSeries.Tests.Configurations;

[TestFixture]
public class BaseDocumentCollectionConfiguratorTests
{
    [Test]
    public static void ShouldConfigureLogCollection()
    {
        // Act
        LogCollectionConfig.ConfigureLogDocumentCollection();

        // Assert
        BsonClassMap.IsClassMapRegistered(typeof(LogDocument)).Should().BeTrue();
    }
}