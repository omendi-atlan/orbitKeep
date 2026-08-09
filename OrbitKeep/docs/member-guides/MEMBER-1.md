# Member 1 Guide – Core OOP, CRUD & EVA Domain Rule

## Your primary ownership

| Area | Files / Classes |
|------|-----------------|
| Abstract base & inheritance | `Domain/Entities/Entity.cs`, `Domain/Entities/CrewMember.cs`, `Domain/Entities/Astronaut.cs` |
| Modules & Life Support | `Domain/Entities/StationModule.cs`, `Domain/Entities/LifeSupportSystem.cs`, `Domain/Entities/SupplyPod.cs` |
| Interfaces | `Domain/Interfaces/IEntity.cs` (and awareness of `IPowerConsumer`) |
| Domain rule (EVA) | `StationController.StartEva` / `EndEva` + menu options 8 & 9 |
| CRUD menu actions | Add/Remove/List astronauts & modules in `Program.cs` |

## Key talking points

1. **Inheritance chain**  
   `Entity` (abstract) → `CrewMember` → `Astronaut`.  
   Why intermediate class? Shared duty/rank behaviour without polluting pure Entity.

2. **Encapsulation**  
   Private backing fields; properties validate ranges (O₂ 0-100, non-empty Id/Name).

3. **Polymorphism**  
   Call `GetStatus()` on a list of `Entity` / `IEntity` and each type prints its own format.

4. **Domain rule**  
   EVA needs **buddy + station O₂ ≥ 40 %**.  
   Show the exact `if (o2 < 40.0)` check and the throw of `InsufficientOxygenException`.

5. **CRUD**  
   Demonstrate adding an astronaut with a blank name (random seeded name appears) and removing a module that is essential (exception).

## Likely viva questions

- “Show me where encapsulation is enforced.”  
  → Property setters in `Entity` / `StationModule`.

- “Why is `GetStatus` abstract?”  
  → Forces every concrete entity to supply its own status string (polymorphism).

- “Walk me through an EVA that fails.”  
  → Menu 8 → `StartEva` → oxygen check → throw → catch in `Program.StartEva` → finally block.

- “What happens if I try to remove an astronaut who is on EVA?”  
  → `InvalidEvaOperationException`.

## Files you must be able to open and explain line-by-line
- `Domain/Entities/Entity.cs`
- `Domain/Entities/Astronaut.cs`
- `Domain/Entities/StationModule.cs`
- Relevant parts of `Services/StationController.cs` (Add/Remove + StartEva/EndEva)
- Menu cases 2–9 in `Program.cs`
