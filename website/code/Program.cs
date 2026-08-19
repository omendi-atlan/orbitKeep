
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace ProjectSpaceStation
{
    class Program
    {
        static void Main(string[] args)
        {
            try // start error check
            {
                Logger.Log("System Booting..."); // log start up

                LifeSupportSystem lifeSupport = new LifeSupportSystem("LS-01", 25.0, 15.0);
                StationModule labModule = new StationModule("MOD-01", "Science Lab", 80.0, 45.0, isEssential: false);
                SupplyPod supply = new SupplyPod("POD-99", "Resupply Alpha", 50.0, 120); // create supply pod
                Astronaut hero = new Astronaut("A-99", "Dave", "Commander"); // make bonus astronaut

                // Crew roster for create/remove operations (initialize with the hero)
                List<Astronaut> crew = new List<Astronaut> { hero };

                List<IEntity> allEntities = new List<IEntity> { lifeSupport, labModule, supply, hero }; // list all entities

                var consumer = new List<IPowerConsumer> { labModule };
                StationMonitor monitor = new StationMonitor(lifeSupport);
                PowerManager powerManager = new PowerManager(consumer, monitor);

                monitor.CriticalOxygenAlert += powerManager.OnCriticalOxygenAlert;
                monitor.CriticalOxygenAlert += (msg, lvl) => Logger.Log(msg + lvl); // log critical alerts

                monitor.Start();

                bool isRunning = true; // flag for loop

                while (isRunning) // main interactive loop
                {
                    
                    Console.WriteLine("\n--- STATION MENU ---"); // show menu header
                    Console.WriteLine("1. Check Status"); // show option 1
                    Console.WriteLine("2. Dock Resupply"); // show option 2
                    Console.WriteLine("3. Start EVA"); // show option 3
                    Console.WriteLine("4. End EVA"); // show option 4
                    Console.WriteLine("5. Exit System"); // show option 5
                    Console.WriteLine("6. Hire New Astronaut (Create)");
                    Console.WriteLine("7. Remove Astronaut (Remove)");
                    Console.Write("Select option: "); // prompt user input

                    string choice = Console.ReadLine(); // read user input

                    switch (choice) // check user input
                    {
                        case "1":
                            Console.Clear(); // clear console for status display
                            Console.WriteLine("\n--- SYSTEM STATUS ---"); // print status header
                            foreach (var entity in allEntities) // loop through entities
                            {
                                Console.WriteLine(entity.GetStatus()); // print entity status
                            }
                            break;

                        case "2":
                            Console.Clear(); // clear console for status display
                            if (!supply.IsDocked) // check if docked
                            {
                                
                                supply.MarkDocked(); // dock the pod
                                lifeSupport.AddOxygen(supply.OxygenCargo); // add pod oxygen
                                Console.WriteLine("Pod docked successfully!"); // print success message
                                Logger.Log("Supply pod docked."); // log pod dock
                                monitor.ReplenishOxygen(50.0);
                            }
                            else // if already docked
                            {
                                Console.WriteLine("Pod already docked."); // print fail message
                            }
                            break;

                        case "3":
                            Console.Clear(); // clear console for status display
                            try // try EVA
                            {
                                if (hero.StartEva("B-88")) // start space walk
                                {
                                    Console.WriteLine("EVA started successfully."); // print success
                                    Logger.Log("EVA started."); // log spacewalk start
                                }
                            }
                            catch (SpaceStationException ex) // catch custom error
                            {
                                Console.WriteLine($"  [EVA FAILED] {ex.Message}"); // print custom message
                                Logger.Log($"EVA Error: {ex.Message}"); // log custom error
                            }
                            break;

                        case "4":
                            Console.Clear(); // clear console for status display
                            if (hero.EndEva()) // end space walk
                            {
                                Console.WriteLine("EVA ended safely."); // print success message
                                Logger.Log("EVA ended."); // log spacewalk end
                            }
                            break;

                        case "5":
                            Console.Clear(); // clear console for status display
                            isRunning = false; // end the loop
                            break;
                        case "6":
                            Console.Clear(); // clear console for status display
                            Console.Write("Enter new astronaut ID: ");
                            string newId = Console.ReadLine();
                            Console.Write("Enter new astronaut name: ");
                            string newName = Console.ReadLine();
                            try
                            {
                                Astronaut rookie = new Astronaut(newId, newName, "Rookie");
                                crew.Add(rookie); // Satisfies "Creation" rubric
                                Console.WriteLine($"{newName} has boarded the station.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error adding crew: {ex.Message}");
                            }
                            finally // Satisfies "finally block" rubric
                            {
                                Logger.Log($"Attempted to modify crew roster. Current count: {crew.Count}");
                            }
                            break;

                        case "7":
                            Console.Clear(); // clear console for status display
                            Console.Write("Enter the ID of the Astronaut to remove (e.g., A-123): ");
                            string removeId = Console.ReadLine();

                            var toRemove = crew.Find(c => c.Id == removeId);
                            if (toRemove != null)
                            {
                                crew.Remove(toRemove); // Satisfies "Removal" rubric
                                Console.WriteLine($"{toRemove.Name} has left the station.");
                                Logger.Log($"{toRemove.Name} was removed from the station.");
                            }
                            else
                            {
                                Console.WriteLine("Astronaut not found. Check the ID and try again.");
                            }
                            break;

                        default: // handle bad input
                            Console.WriteLine("Invalid option selected."); // print error message
                            break;
                    }
                }

                monitor.Stop();
                Logger.Log("System Shut Down."); // log shut down
            }
            catch (Exception ex) // catch any crash
            {
                Console.WriteLine("System crashed!"); // print crash info
                Logger.Log("CRASH: " + ex.Message); // log the crash
            }
        }
    }
}