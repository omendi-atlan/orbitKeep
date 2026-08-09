# ✧ OrbitKeep — Station Control Console

[![.NET](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![PRG2781](https://img.shields.io/badge/Course-PRG2781-22d3ee)](docs/project-charter.md)

**Galactic Command · Space Station Operations Simulator**  
Console-based C# application for the PRG2781 group project.

<p align="center">
  <img src="assets/concept.png" alt="OrbitKeep mission control concept art" width="860"/>
</p>

> Live presentation site (GitHub Pages): open [`index.html`](index.html) or enable Pages on the `main` branch.

---

## Mission Brief

OrbitKeep simulates life aboard a small orbital station:

- Manage **astronauts**, **modules**, **life-support** and **supply pods**
- Background monitor drains O₂ and power every **2 seconds**
- Solar flares trigger **auto power-shedding**
- EVA is only allowed with a **buddy** and **O₂ ≥ 40 %** (domain rule)

Every run is unique yet reproducible — random data is driven by the group seed.

```
Seed = GroupNumber + Student1 + Student2 + Student3
     = 7 + 111 + 222 + 333 = 673
```

---

## Quick Start

```bash
git clone https://github.com/omendi-atlan/orbitkeep.git
cd orbitkeep
dotnet run --project src/OrbitKeep.App
```

Requirements: **.NET 8 SDK** (or compatible).

---

## Features (Course + Custom)

| Requirement              | Implementation                                      |
|--------------------------|-----------------------------------------------------|
| Menu-driven interface    | 15-option galactic command menu                     |
| CRUD entities            | Astronauts & Modules                                |
| Background operations    | Independent `Task` (O₂/power every 2 s)             |
| Event reactions          | SolarFlare, SupplyPodDocked, OxygenCritical, PowerCritical |
| Exception handling       | try-catch-finally + 3 custom exceptions             |
| OOP principles           | Encapsulation, inheritance, polymorphism, abstraction |
| ≥ 2 interfaces           | `IEntity`, `IPowerConsumer`                         |
| Custom events + delegates| Publisher–subscriber with custom `EventArgs`        |
| Independent background thread | `CancellationToken` + `lock` / `ConcurrentQueue` |
| **Custom feature**       | Auto power-shedding on solar flare                  |
| **Bonus**                | Logging, save/load state, LINQ O₂ report            |

### Domain Rule
**EVA requires a buddy astronaut AND station O₂ reserve ≥ 40 %.**  
Violation throws `InsufficientOxygenException`.

### Custom Feature
On solar flare the system shuts down non-essential modules (`IPowerConsumer`) and reroutes remaining power to life support. Persistent deficit raises `PowerCritical`.

---

## Project Structure

```
OrbitKeep/
├── .github/ISSUE_TEMPLATE/     # Bug & feature templates
├── assets/concept.png          # Mission-control concept art
├── docs/                       # Charter, viva sheet, demo script, member guides
├── logs/                       # Runtime logs (gitignored content)
├── src/OrbitKeep.App/
│   ├── Program.cs              # Menu + event handlers
│   ├── Domain/
│   │   ├── Entities/           # Entity → CrewMember → Astronaut, Modules, Pods, LSS
│   │   ├── Interfaces/         # IEntity, IPowerConsumer
│   │   └── Exceptions/         # InsufficientOxygen, PowerReroute, InvalidEva
│   ├── Events/                 # Custom EventArgs
│   ├── Services/               # StationController, StatePersistence
│   └── Utilities/              # SeedHelper, NameGenerator, Logger
├── index.html                  # Animated presentation website (GitHub Pages)
├── OrbitKeep.sln
├── LICENSE
└── README.md
```

---

## Documentation

| Document | Purpose |
|----------|---------|
| [docs/member-guides/](docs/member-guides/) | Per-member ownership & viva questions |
| [index.html](index.html) | Visual presentation (telemetry, features, crew) |

---

## The Crew

| Member | Focus |
|--------|-------|
| **Member 1** | Core OOP, entities, CRUD, EVA domain rule, LINQ |
| **Member 2** | Events & delegates, background monitor, power-shedding |
| **Member 3** | Custom exceptions, seeded random, logging, file I/O, docs |

---

## Design Highlights

- **Encapsulation** – validated properties, private state
- **Inheritance** – `Entity → CrewMember → Astronaut`
- **Polymorphism** – `GetStatus()` overrides
- **Publisher–subscriber** – events raised with `?.Invoke`
- **Thread safety** – `lock` for shared lists, `ConcurrentQueue` for alerts
- **Clean shutdown** – `CancellationTokenSource` stops the monitor

---

## License

MIT — see [LICENSE](LICENSE).

---

*OrbitKeep · PRG2781 2026 · Belgium Campus · Built by the crew, for the crew 🛰️*
