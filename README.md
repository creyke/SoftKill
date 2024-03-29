# SoftKill
Kill your ASP.NET API methods softly

[![NuGet](https://img.shields.io/nuget/v/SoftKill.svg?style=flat)](https://www.nuget.org/packages/SoftKill)

# Overview
SoftKill allows you to gently retire API methods by placing an attribute on them which defines when their response time should begin to degrade, the speed at which this occurs, and a final condemnation date after which the method will return an HTTP 410 Gone status.

# Usage

## Installing
Install the [NuGet package](https://www.nuget.org/packages/SoftKill) into an ASP.NET project with a package manager.

### .NET CLI
```dotnet add package SoftKill```

### Package Manager
```Install-Package SoftKill```

## Enable SoftKill in Startup
```csharp
app.UseRouting();
app.UseSoftKill();
```

## Condemn your method
```csharp
[HttpGet]
[Obsolete("This method has a better alternative.")]
[KillSoftly(
    new[] { 2021, 01, 01 }, // DegredationDate
    7, // DegredationWindowDays
    2, // DegredationSeconds
    new[] { 2021, 02, 01 })] // CondemnationDate
public IEnumerable<WeatherForecast> Get()
```
