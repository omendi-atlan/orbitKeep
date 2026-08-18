# Orbital Command Console

**PRG2781 · Smart Operations Console System**  
*Space Station Operations — console-first C# with a mission-control web showcase*

<p align="center">
  <img src="assets/station-view.png" alt="Cockpit view — orbital station against Earth" width="100%" />
</p>

<p align="center">
  <strong>Real-time oxygen monitoring · EVA control · Event-driven alerts · Background threading</strong><br/>
  <em>Domain: Space Station Operations · Unique system name: Orbital Command Console</em>
</p>

---

## What this is

A **console-based C# application** that simulates live space-station operations, paired with a dark, responsive web front-end that presents the system, technical requirements, source highlights, and crew.

The console is the assessed artefact (no GUI frameworks). The website is the public face: video background, cockpit hero, requirement mapping, and direct links into `Program.cs` for Codespaces / local demos.

| Layer | Role |
|-------|------|
| **Console app** | Menu-driven simulation — status, resupply, EVA, crew create/remove, background O₂ drain, custom events & exceptions |
| **Web showcase** | Professional presentation of the same system for viva, portfolio, and assessors |

---

## Live preview of the showcase

Open `index.html` (or serve the `website/` folder). Background video is the crew operations sequence; the hero image is the station view from the command deck.

```text
website/
├── index.html
├── css/styles.css
├── js/main.js
├── assets/
│   ├── space-crew.mp4      ← looping background
│   ├── station-view.png    ← hero / cockpit
│   └── team.png            ← group roster
└── code/                   ← full C# sources
    ├── Program.cs
    ├── StationMonitor.cs
    └── …
```

---

## Domain & uniqueness

**Chosen domain:** Space Station Operations  

**System name:** Orbital Command Console  

**Domain rules enforced:**
- Oxygen continuously drains on a background thread
- Critical O₂ threshold raises a custom event
- Illegal EVA state throws `SpaceStationException`
- Resupply restores oxygen and can fire a restore event
- Crew roster supports create / remove with logging

Randomised / student-owned entities (IDs, modules, pods, astronauts) keep the run distinct from other groups.

---

## Technical requirements covered

| Requirement | Implementation |
|-------------|----------------|
| **OOP (all four)** | Encapsulation via properties; abstraction via interfaces; inheritance (`Astronaut` → `CrewMember` → `Entity`); polymorphism on `GetStatus()` |
| **Interfaces (≥2)** | `IEntity`, `IPowerConsumer` |
| **Custom exception** | `SpaceStationException` for illegal EVA / domain errors |
| **Events & delegates (≥2)** | `CriticalOxygenAlert`, `OxygenRestoredAlert` |
| **Multithreading** | Background `StationMonitoringThread` with lock-safe oxygen drain |
| **Bonus** | File logging to `log.txt` via static `Logger` |

---

## Console menu (runtime)

```text
--- STATION MENU ---
1. Check Status
2. Dock Resupply
3. Start EVA
4. End EVA
5. Exit System
6. Hire New Astronaut (Create)
7. Remove Astronaut (Remove)
```

Background monitoring continues independently of the menu. Alerts are logged and can drive power-management reactions.

---

## How to run

### Console application

```bash
cd CompleteFinalSpaceStation   # or website/code after cloning
# Open in Visual Studio / Rider, or:
dotnet run                     # if targeting a modern SDK
# or build the existing .NET Framework solution and run the .exe
```

Link `Program.cs` in a GitHub Codespace for one-click demonstration during viva.

### Web showcase

```bash
cd website
python3 -m http.server 8080
# open http://localhost:8080
```

On mobile (same network): use the machine’s LAN IP instead of localhost. Layout is responsive; video is muted + `playsinline`.

---

## Project crew

| Member | Role |
|--------|------|
| **Louwrens Marthinus Jacobs** | Developer |
| **Mongezi Mahlangu** | Developer |
| **Hendrik Louis Steyl** | Developer |

<p align="center">
  <img src="assets/team.png" alt="Group roster" width="420" />
</p>

---

## Repository layout (recommended)

```text
/
├── README.md                 ← this file
├── CompleteFinalSpaceStation/
│   ├── Program.cs
│   ├── StationMonitor.cs
│   ├── …
│   └── *.csproj / solution
└── website/                  ← showcase (optional but recommended)
    ├── index.html
    ├── assets/
    └── code/                 ← mirrors of key sources for download
```

---

## Design notes (web)

- Video background with cinematic overlay so content stays readable
- Station cockpit image used as primary visual anchor
- Code tabs surface the exact patterns assessed (events, threads, exceptions)
- Download links for `Program.cs` and supporting sources
- Sticky nav + mobile drawer; no framework bloat

---

## Assessment alignment

Built against PRG2781 2026 Project brief:

- Console-only (no WinForms / WPF / MAUI)
- Menu-driven create / modify / remove
- Background process independent of user input
- Event reaction to alerts and completions
- Exception handling with custom domain type
- README (this file) + source structure for viva

---

<p align="center">
  <strong>Orbital Command Console</strong><br/>
  PRG2781 · Belgium Campus · 2026
</p>
