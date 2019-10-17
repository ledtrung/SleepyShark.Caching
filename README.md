# SleepyShark.Caching
A distributed cache solution.

## Introdution
I build this simple version of a distributed cache service based on an interview request from [GoQuo](https://www.goquo.com/). There are much more to do with it and I will add more features, configuration, tuning... in the near future.

## How it works
#### Cache service
The cache service accepts 2 simple requests, obviously: Set and Get, via a socket connection to provide fast and secure responses. The service itself is only a dotnet core console application now. 
Configuration is done via `ServiceCollection`. 
```csharp
services.AddSleepySharkCaching(options =>
{
    options.UserInMemoryCache("InMemoryCache");
});
```
Other configurations are all reside in `SleepyShark.Caching.ServerConfiguration` for now.

The cache service is only provides in memory cache by now, but you can easily add more providers by adding more implementation of `ICachingProvider`.

#### Consumers
Cache consumer application need to add reference to `SleepyShark.Caching.Connector` in order to use the cache service.
Configuration is also done via `ServiceCollection`
```csharp
services.AddSleepySharkCaching(options =>
{
    options.AppId = "sampleApp";
    options.ServerAddress = GetLocalIPAddress();
    options.ServerPort = 4485;
});
```
To interact with cache service, you can use `SleepySharkCache`, which can be get by 2 ways:
```csharp
//Via dependency injection
ISleepySharkCache cache = serviceProvider.GetService<ISleepySharkCache>();
```
Or
```csharp
//Manually instantiate
SleepySharkCache otherCache = new SleepySharkCache("sampeApp", GetLocalIPAddress(), 4485);
```
