![sink](https://cdn.brammys.com/file/brammys/img/potable-water_1f6b0.png "Sink img")

# Serilog.Sinks.Mongodb.TimeSeries
[![serilogsink-mongodb-timeseries](https://img.shields.io/github/workflow/status/BrammyS/Serilog.Sinks.Mongodb.TimeSeries/.NET%20Build%20Serilog.Sinks.Mongodb.TimeSeries/main?style=for-the-badge)](https://github.com/brammys/Serilog.Sinks.Mongodb.TimeSeries/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/vpre/Serilog.Sinks.Mongodb.TimeSeries.svg?maxAge=600&style=for-the-badge)](https://www.nuget.org/packages/Serilog.Sinks.Mongodb.TimeSeries)
[![NuGet](https://img.shields.io/nuget/dt/Serilog.Sinks.Mongodb.TimeSeries.svg?maxAge=600&style=for-the-badge)](https://www.nuget.org/packages/Serilog.Sinks.Mongodb.TimeSeries)
[![License](https://img.shields.io/github/license/BrammyS/Serilog.Sinks.Mongodb.TimeSeries?style=for-the-badge)](https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries/blob/main/LICENSE)

A sink for Serilog that saves logs in a MongoDb [time series](https://docs.mongodb.com/manual/core/timeseries-collections/) collection.

## 1. Installation
Serilog.Sinks.Mongodb.TimeSeries is available on [NuGet](https://www.nuget.org/packages/Serilog.Sinks.Mongodb.TimeSeries).
* [Serilog.Sinks.Mongodb.TimeSeries](https://www.nuget.org/packages/Serilog.Sinks.Mongodb.TimeSeries)
```powershell
Install-Package Serilog.Sinks.Mongodb.TimeSeries
```
OR
```powershell
dotnet add package Serilog.Sinks.Mongodb.TimeSeries
```

## 2. Usage
You will need to do the following to add Serilog.Sinks.Mongodb.TimeSeries as a sink in your program.

```csharp
var client = new MongoClient("mongodb://mongodb0.example.com:27017");
var mongoDatabase = client.GetDatabase("dbName");

Log.Logger = new LoggerConfiguration()
                 .WriteTo.MongoDbTimeSeriesSink(mongoDatabase)
                 .CreateLogger();
```
OR
```csharp
var client = new MongoClient("mongodb://mongodb0.example.com:27017");
var mongoDatabase = client.GetDatabase("dbName");

var configs = new MongoDbTimeSeriesSinkConfig(mongoDatabase)
{
    CollectionName = "Logs",
    TimeSeriesGranularity = TimeSeriesGranularity.Seconds,
    SyncingPeriod = TimeSpan.FromSeconds(10),
    LogsExpireAfter = TimeSpan.FromDays(7),
    MaxCollectionSize = 100 * 1024 * 1024,
    EagerlyEmitFirstEvent = true,
    MaxLogsAmount = 100000,
    BatchSizeLimit = 500,
    QueueLimit = 20000
};

Log.Logger = new LoggerConfiguration()
                 .WriteTo.MongoDbTimeSeriesSink(configs)
                 .CreateLogger();
```

## 3. Compiling
You will need the following to compile Serilog.Sinks.Mongodb.TimeSeries:

### With an IDE
* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Rider](https://www.jetbrains.com/rider/download/)
* [.NET sdk](https://dotnet.microsoft.com/download)

### With the command line
* [.NET sdk](https://dotnet.microsoft.com/download)

## 4. Versioning
Serilog.Sinks.Mongodb.TimeSeries uses [Semantic Versioning 2.0.0](https://semver.org/#semantic-versioning-200).
### Summary
The versioning will be using the following format: MAJOR.MINOR.PATCH.

* MAJOR version when you make incompatible API changes,
* MINOR version when you add functionality in a backwards compatible manner, and
* PATCH version when you make backwards compatible bug fixes.
* Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.
