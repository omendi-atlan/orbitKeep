# Member 2 Guide – Events, Threading & Power-Shedding

## Your primary ownership

| Area | Files / Classes |
|------|-----------------|
| Custom events & EventArgs | `Events/StationEventArgs.cs` |
| Event declarations & raising | `Services/StationController.cs` (event fields + `?.Invoke`) |
| Event subscription | `Program.cs` (handlers `OnSolarFlare`, etc.) |
| Background monitor | `StationController.StartBackgroundMonitor`, `Tick`, `StopBackgroundMonitor` |
| Power-shedding logic | `HandleSolarFlare`, `PerformPowerShedding`, `RestorePowerAfterFlare` |
| IPowerConsumer usage | `Domain/Interfaces/IPowerConsumer.cs` + `StationModule` |

## Key talking points

1. **Events**  
   - Declared as `event EventHandler<TEventArgs>?`  
   - Raised with null-conditional `?.Invoke(this, args)`  
   - Custom EventArgs carry payload (intensity, deficit, oxygen %).

2. **Publisher-Subscriber**  
   `StationController` = publisher; `Program` = subscriber.  
   Decouples alert generation from console display.

3. **Background Task**  
   - `Task.Run` + `CancellationToken`  
   - Sleeps 2 s, drains O₂, may enqueue a flare alert  
   - Shared collections guarded by `lock`  
   - Alerts travel through `ConcurrentQueue` so the UI thread stays free.

4. **Power-shedding (custom feature)**  
   - Flare adds extra load  
   - Non-essential modules (those with `IsEssential == false`) are shut down via `IPowerConsumer.ShutDown`  
   - If deficit remains → `PowerRerouteException` → `PowerCritical` event.

5. **Safe shutdown**  
   Cancel token → Wait with timeout → Dispose CTS in `finally`.

## Likely viva questions

- “How do you prevent the background thread from freezing the menu?”  
  → ConcurrentQueue + main-thread `ProcessPendingAlerts`.

- “Show the exact line that raises the solar-flare event.”  
  → `SolarFlareDetected?.Invoke(...)`.

- “What is the difference between a delegate and an event?”  
  → Event is a restricted multicast delegate; only the declaring class can raise it.

- “Why use `IPowerConsumer` instead of checking a bool on StationModule?”  
  → Abstraction – any future power-consuming device can be shed without changing the shedding algorithm.

- “How is the monitor stopped cleanly?”  
  → `CancellationTokenSource.Cancel()` + `Task.Wait` + dispose.

## Files you must be able to open and explain line-by-line
- `Events/StationEventArgs.cs`
- `Domain/Interfaces/IPowerConsumer.cs`
- `Services/StationController.cs` (event section + background + power-shedding)
- Event handler methods at the bottom of `Program.cs`
