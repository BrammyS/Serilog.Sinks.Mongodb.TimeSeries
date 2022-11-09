[![NuGet][nuget-version-shield]][package-url]
[![NuGet][nuget-downloads-shield]][package-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]



<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries">
    <img src="https://cdn.brammys.com/file/brammys/img/potable-water_1f6b0.png" alt="Logo" width="120" height="120">
  </a>

  <h3 align="center">Serilog.Sinks.Mongodb.TimeSeries</h3>

  <p align="center">
    A simple to use sink for Serilog that saves logs in a Mongodb time series collection.
    <br />
    <a href="https://sinks-mongodb-timeseries.brammys.com/"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries/issues">Report Bug</a>
    ·
    <a href="https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#versioning">Versioning</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Serilog.Sinks.Mongodb.TimeSeries is a nuget package to save all your logs to a [time series](https://docs.mongodb.com/manual/core/timeseries-collections/) mongodb collection.  
It is super simple to setup! There are only a couple lines needed. Head down to the Installation guide for more info.


### Built With

* [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)
* [Serilog](https://github.com/serilog/serilog)
* [Mongodb](https://github.com/mongodb/mongo)



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

* [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

### Installation

#### Nuget

Serilog.Sinks.Mongodb.TimeSeries is available on [NuGet](Serilog.Sinks.Mongodb.TimeSeries).
* [Serilog.Sinks.Mongodb.TimeSeries](Serilog.Sinks.Mongodb.TimeSeries)


  ```powershell
  Install-Package Serilog.Sinks.Mongodb.TimeSeries
  ```

  OR

  ```powershell
  dotnet add package Serilog.Sinks.Mongodb.TimeSeries
  ```

#### Cloning


1. Clone the repo
   ```sh
   git clone https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries.git
   ```
2. Build the repo
   ```sh
   dotnet build
   ```


<!-- USAGE EXAMPLES -->
## Usage

You will need to do the following to add Serilog.Sinks.Mongodb.TimeSeries as a sink in your program.
#### Default configurations
```csharp
var client = new MongoClient("mongodb://mongodb0.example.com:27017");
var mongoDatabase = client.GetDatabase("dbName");

Log.Logger = new LoggerConfiguration()
                 .WriteTo.MongoDbTimeSeriesSink(mongoDatabase)
                 .CreateLogger();
```
#### Custom configurations
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



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


## Versioning

Color-Chan.Discord uses [Semantic Versioning 2.0.0](https://semver.org/#semantic-versioning-200) for its versioning.


### Summary

The versioning will be using the following format: MAJOR.MINOR.PATCH.

* MAJOR version when you make incompatible API changes,
* MINOR version when you add functionality in a backwards compatible manner, and
* PATCH version when you make backwards compatible bug fixes.
* Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.


<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.


<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

* [Readme-template](https://github.com/othneildrew/Best-README-Template)
* [Serilog.Sinks.PeriodicBatching](https://github.com/serilog/serilog-sinks-periodicbatching)





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[stars-shield]: https://img.shields.io/github/stars/BrammyS/Serilog.Sinks.Mongodb.TimeSeries.svg?style=for-the-badge
[stars-url]: https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries/stargazers
[issues-shield]: https://img.shields.io/github/issues/BrammyS/Serilog.Sinks.Mongodb.TimeSeries.svg?style=for-the-badge
[issues-url]: https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries/issues
[license-shield]: https://img.shields.io/github/license/BrammyS/Serilog.Sinks.Mongodb.TimeSeries.svg?style=for-the-badge
[license-url]: https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries/blob/master/LICENSE.txt
[package-url]: https://www.nuget.org/packages/Serilog.Sinks.Mongodb.TimeSeries
[nuget-version-shield]: https://img.shields.io/nuget/vpre/Serilog.Sinks.Mongodb.TimeSeries.svg?maxAge=600&style=for-the-badge
[nuget-downloads-shield]: https://img.shields.io/nuget/dt/Serilog.Sinks.Mongodb.TimeSeries.svg?maxAge=600&style=for-the-badge
