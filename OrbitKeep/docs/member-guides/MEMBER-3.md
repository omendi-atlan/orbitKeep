# Member 3 Guide – Exceptions, Random Seed, Logging, File I/O & Docs

## Your primary ownership

| Area | Files / Classes |
|------|-----------------|
| Custom exceptions | `Domain/Exceptions/InsufficientOxygenException.cs`, `PowerRerouteException.cs`, `InvalidEvaOperationException.cs` |
| Seed & name generation | `Utilities/SeedHelper.cs`, `Utilities/NameGenerator.cs` |
| Logging | `Utilities/Logger.cs` |
| State save/load | `Services/StatePersistence.cs` |
| LINQ report | `StationController.PrintOxygenReport` |
| Documentation | All files under `docs/` |
| try-catch-finally placement | `Program.StartEva`, `Program.LoadState`, `StationController.StartEva` |

## Key talking points

1. **Custom exceptions**  
   Derive from `Exception`, add domain properties (`CurrentOxygenPercent`, `DeficitKw`, …).  
   Thrown for *logical* errors, not programming bugs.

2. **try-catch-finally**  
   - `catch` specific exception types first  
   - `finally` always runs (logging, “request finished” message)  
   - Demonstrated around EVA and file load.

3. **Seed**  
   ```csharp
   Seed = 7 + 111 + 222 + 333   // = 673
   ```
   Same seed → same sequence of names, O₂ values, flare chances → reproducible demo.

4. **Logging**  
   Thread-safe (`lock`), never throws, writes to `logs/station.log`.

5. **File I/O**  
   JSON snapshot via `System.Text.Json`.  
   Save after changes; Load rebuilds modules & astronauts.

6. **LINQ**  
   `OrderBy(m => m.OxygenPercent)` – simple, easy to explain.

## Likely viva questions

- “Why create a custom exception instead of using `InvalidOperationException`?”  
  → Carries extra domain data and makes the failure reason obvious to callers.

- “Show me the finally block and explain why it is there.”  
  → Guarantees an audit / cleanup step even when an exception is thrown.

- “How is the random seed calculated?”  
  → Sum of group + three student numbers (placeholders 7/111/222/333).

- “Is the logger thread-safe? How?”  
  → Static lock object around `File.AppendAllText`.

- “What happens if the state file is missing or corrupt?”  
  → `TryLoad` returns false and prints a friendly message; application continues.

## Files you must be able to open and explain line-by-line
- All three files in `Domain/Exceptions/`
- `Utilities/SeedHelper.cs`, `Utilities/NameGenerator.cs`, `Utilities/Logger.cs`
- `Services/StatePersistence.cs`
- try/catch/finally blocks in `Program.cs`
- `docs/README.md` (you own the documentation narrative)
