using System;
using FluentAssertions;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using Serilog.Sinks.Mongodb.TimeSeries.Configurations;

namespace Serilog.Sinks.Mongodb.TimeSeries.Tests.Configurations
{
    [TestFixture]
    public class MongoDbTimeSeriesSinkConfigTests
    {
        [Test]
        public void Config_should_contains_defaults()
        {
            // Arrange
            var mockDb = new Mock<IMongoDatabase>();
            
            // Act
            var config = new MongoDbTimeSeriesSinkConfig(mockDb.Object);
            
            // Assert
            config.CollectionName.Should().Be("logs");
            config.BatchSizeLimit.Should().Be(1000);
            config.QueueLimit.Should().Be(10000);
            config.EagerlyEmitFirstEvent.Should().Be(true);
            config.SyncingPeriod.Should().Be(TimeSpan.FromSeconds(5));
            config.TimeSeriesGranularity.Should().Be(TimeSeriesGranularity.Seconds);
            config.MaxCollectionSize.Should().BeNull();
            config.MaxLogsAmount.Should().BeNull();
            config.LogsExpireAfter.Should().BeNull();
        }
        
        [Test]
        public void Config_should_contains_custom_values()
        {
            // Arrange
            var mockDb = new Mock<IMongoDatabase>();
            
            // Act
            var config = new MongoDbTimeSeriesSinkConfig(mockDb.Object)
            {
                CollectionName = "Logs",
                BatchSizeLimit = 50,
                QueueLimit = 100,
                EagerlyEmitFirstEvent = false,
                SyncingPeriod = TimeSpan.FromSeconds(4),
                TimeSeriesGranularity = TimeSeriesGranularity.Hours,
                MaxCollectionSize = 200,
                MaxLogsAmount = 10,
                LogsExpireAfter = TimeSpan.FromMilliseconds(100)
            };
            
            // Assert
            config.CollectionName.Should().Be("Logs");
            config.BatchSizeLimit.Should().Be(50);
            config.QueueLimit.Should().Be(100);
            config.EagerlyEmitFirstEvent.Should().Be(false);
            config.SyncingPeriod.Should().Be(TimeSpan.FromSeconds(4));
            config.TimeSeriesGranularity.Should().Be(TimeSeriesGranularity.Hours);
            config.MaxCollectionSize.Should().Be(200);
            config.MaxLogsAmount.Should().Be(10);
            config.LogsExpireAfter.Should().Be(TimeSpan.FromMilliseconds(100));
        }
    }
}