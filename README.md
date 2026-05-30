<div align="center">

# Parking House

### An ASP.NET MVC parking house simulator with fee calculation and profit tracking

![C#](https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logo=csharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET_Framework_4.5-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET MVC](https://img.shields.io/badge/ASP.NET_MVC_4-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework_6-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Ninject](https://img.shields.io/badge/Ninject-7A4FBA?style=for-the-badge&logoColor=white)
[![Repo](https://img.shields.io/badge/GitHub-jackinf%2FParkingHouse-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/jackinf/ParkingHouse)

</div>

## Overview

Parking House is a small ASP.NET MVC 4 web application that simulates the operation of a
parking house. Vehicles of varying dimensions enter and leave the lot, and the application
tracks how many cars are currently parked, computes a parking fee per car, and accumulates the
running profit and total number of cars served. State is persisted with Entity Framework 6
against SQL Server, and dependencies are wired through Ninject.

## Features

- Add a vehicle to the lot with a length and width (validated to fit within configured limits).
- Remove a vehicle, charging it on the way out and updating the running profit.
- Fee calculation: a flat contract fee for vehicles with a contract, otherwise an hourly rate
  (see `HelperMethods/CalculateParkingSum.cs`).
- Live summary of cars currently parked, total cars served, and accumulated profit.
- "End simulation" action that clears the lot and resets persisted totals.
- Dependency injection via a Ninject-backed controller factory.
- Unit tests (MSTest + Moq) covering the parking limit logic.

## Tech Stack

| Area | Technology |
|------|------------|
| Language | C# (.NET Framework 4.5) |
| Web framework | ASP.NET MVC 4 + ASP.NET Web API |
| Data access | Entity Framework 6 (Code First migrations), SQL Server |
| Dependency injection | Ninject 3 |
| Front end | Razor views, jQuery, jQuery UI, Knockout, Modernizr |
| Testing | MSTest, Moq |

## Getting Started

### Prerequisites

- Windows with Visual Studio 2013 or newer (the solution targets `.NET Framework 4.5`).
- A SQL Server / LocalDB instance for the Entity Framework data store.
- NuGet (to restore the packages referenced in `packages.config`).

### Installation

```bash
git clone https://github.com/jackinf/ParkingHouse.git
```

Open `ParkingHouse.sln` in Visual Studio and let NuGet restore the referenced packages
(EntityFramework, Ninject, ASP.NET MVC/Web API, Moq, etc.).

### Running

- Build the solution (`Build > Build Solution`).
- Confirm the database connection string in `ParkingHouse/Web.config`, then run the app with
  IIS Express (`F5` / `Ctrl+F5`). Entity Framework Code First migrations create the schema.
- Run the unit tests via the Visual Studio Test Explorer (the `ParkingHouse.Tests` project).

> Note: this is a classic MSBuild/Visual Studio web project with no scripted build pipeline,
> so the steps above are driven from Visual Studio rather than a CLI build script.

## Project Structure

```
ParkingHouse/
├── ParkingHouse/              # ASP.NET MVC web application
│   ├── App_Start/             # Bundle, filter, route, and Web API config
│   ├── Controllers/           # ParkingController (add/remove/simulate)
│   ├── DB/                    # EF DbContext, repositories, entities
│   ├── HelperMethods/         # Parking fee calculation
│   ├── Infrastructure/        # Ninject controller factory
│   ├── Migrations/            # EF Code First migrations
│   ├── Models/                # ParkingLot view/state model
│   └── Views/                 # Razor views
├── ParkingHouse.Tests/        # MSTest + Moq unit tests
└── ParkingHouse.sln
```
