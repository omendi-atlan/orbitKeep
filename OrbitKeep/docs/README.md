# OrbitKeep Station Control Console

**PRG2781 Group Project – Space Station Operations**  
*Theme: Galactic Command*

Console-based C# application that simulates real-time operations of an orbital station under galactic command protocols.

## How to Run

```bash
cd src/OrbitKeep.App
dotnet run
```

Or open `OrbitKeep.sln` in Visual Studio / Rider and press F5.

**Requirements:** .NET 8 SDK (or compatible).

## Seed

Random data is driven by:

```
Seed = GroupNumber + StudentNumber1 + StudentNumber2 + StudentNumber3
     = 7 + 111 + 222 + 333 = 673
```

This seed controls astronaut names, module O2 levels, pod delays and solar-flare probability so every group run is unique yet reproducible.

## Features Implemented

| Requirement | Implementation |
|-------------|----------------|
| Menu-driven interface | `Program.cs` – 15 options + exit |
| Create / modify / remove entities | Astronauts & Modules CRUD |
| Background operations | `Task` draining O2 every 2 s |
| Event reactions | SolarFlare, SupplyPodDocked, OxygenCritical, PowerCritical |
| Exception handling | try-catch-finally + 3 custom exceptions |
| All OOP principles | See design decisions below |
| ≥2 interfaces | `IEntity`, `IPowerConsumer` |
| Custom events + delegates | 4 events with custom `EventArgs` |
| Independent background thread | `StationController.StartBackgroundMonitor` |
| Bonus features | Logging, save/load state, LINQ report |

## Domain Rule

**EVA requires a buddy astronaut AND station O₂ reserve ≥ 40 %.**  
Violation throws `InsufficientOxygenException`.

## Custom Feature (not in course material)

**Auto power-shedding**  
On solar flare the system shuts down non-essential modules (`IPowerConsumer`) and reroutes remaining power to life support. Persistent deficit raises the `PowerCritical` event and may throw `PowerRerouteException`.

## Design Decisions

### OOP
- **Encapsulation** – private fields + validated properties on every entity.
- **Abstraction** – abstract `Entity` base + interfaces `IEntity` / `IPowerConsumer`.
- **Inheritance** – `Entity → CrewMember → Astronaut`; modules implement power contract.
- **Polymorphism** – `GetStatus()` overridden in each concrete type.

### Multithreading
- Background `Task` runs independently of the menu loop.
- Shared state protected by `lock (_stateLock)`.
- Alerts from the background thread are pushed into a `ConcurrentQueue` and drained on the main thread (`ProcessPendingAlerts`) so the console never freezes.
- Clean shutdown via `CancellationTokenSource`.

### Events
Publisher (`StationController`) exposes events; `Program` subscribes with named handlers.  
Custom `EventArgs` carry domain payload (intensity, oxygen %, deficit, etc.).  
Null-conditional `?.Invoke` is used throughout.

### File I/O & Logging
- `Logger` writes thread-safely to `logs/station.log`.
- `StatePersistence` serialises a snapshot to `station_state.json` (System.Text.Json).

## Project Structure

```
OrbitKeep/
├── .github/ISSUE_TEMPLATE/   # Bug & feature templates
├── assets/concept.png        # Mission-control concept art
├── docs/                     # Charter, viva sheet, demo script, member guides
├── logs/.gitkeep
├── src/OrbitKeep.App/
│   ├── Program.cs
│   ├── Domain/
│   │   ├── Entities/         # Entity, CrewMember, Astronaut, StationModule, LSS, SupplyPod
│   │   ├── Interfaces/       # IEntity, IPowerConsumer
│   │   └── Exceptions/       # InsufficientOxygen, PowerReroute, InvalidEva
│   ├── Events/               # Custom EventArgs
│   ├── Services/             # StationController, StatePersistence
│   └── Utilities/            # SeedHelper, NameGenerator, Logger
├── index.html                # Animated presentation website (GitHub Pages)
├── OrbitKeep.sln
├── LICENSE
└── README.md
```

## Group Members (placeholders)

| Member | Focus |
|--------|-------|
| Member 1 | Core OOP, CRUD, EVA domain rule |
| Member 2 | Events, threading, power-shedding |
| Member 3 | Exceptions, random seed, logging, file I/O, documentation |

See `docs/member-guides/` for per-member file ownership and viva questions.
