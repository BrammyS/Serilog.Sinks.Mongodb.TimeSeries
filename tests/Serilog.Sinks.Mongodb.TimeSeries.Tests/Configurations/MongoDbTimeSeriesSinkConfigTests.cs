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
            config.LogsExpireAfter.Should().BeNull();
            
            config.CreateCollectionOptions.Capped.Should().BeFalse();
            config.CreateCollectionOptions.ExpireAfter.Should().BeNull();
            config.CreateCollectionOptions.MaxDocuments.Should().BeNull();
            config.CreateCollectionOptions.MaxSize.Should().BeNull();
            config.CreateCollectionOptions.TimeSeriesOptions.Granularity.Should().Be(TimeSeriesGranularity.Seconds);
            config.CreateCollectionOptions.TimeSeriesOptions.TimeField.Should().Be("timestamp");
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
                LogsExpireAfter = TimeSpan.FromMilliseconds(100)
            };
            
            // Assert
            config.CollectionName.Should().Be("Logs");
            config.BatchSizeLimit.Should().Be(50);
            config.QueueLimit.Should().Be(100);
            config.EagerlyEmitFirstEvent.Should().Be(false);
            config.SyncingPeriod.Should().Be(TimeSpan.FromSeconds(4));
            config.TimeSeriesGranularity.Should().Be(TimeSeriesGranularity.Hours);
            config.LogsExpireAfter.Should().Be(TimeSpan.FromMilliseconds(100));
            
            config.CreateCollectionOptions.Capped.Should().BeTrue();
            config.CreateCollectionOptions.ExpireAfter.Should().Be(TimeSpan.FromMilliseconds(100));
            config.CreateCollectionOptions.MaxDocuments.Should().Be(10);
            config.CreateCollectionOptions.MaxSize.Should().Be(200);
            config.CreateCollectionOptions.TimeSeriesOptions.Granularity.Should().Be(TimeSeriesGranularity.Hours);
            config.CreateCollectionOptions.TimeSeriesOptions.TimeField.Should().Be("timestamp");
        }
    }
}