using System;
using System.Collections.Generic;
using System.Threading;

namespace ProjectSpaceStation
{
    public class PowerManager
    {
        public bool b = true;
        private readonly List<IPowerConsumer> _consumers;

        public PowerManager(List<IPowerConsumer> consumers, StationMonitor monitor)
        {
            _consumers = consumers;

            // Subscribe to monitor events so the power manager can react when oxygen is restored
            if (monitor != null)
            {
                monitor.OxygenRestoredAlert += HandleOxygenRestored;
            }
        }

        public void OnCriticalOxygenAlert(string message, double currentLevel)
        {
            if ((currentLevel == 0) && (b == true)) 
            {
                Console.WriteLine("\n[POWER MANAGER] CRITICAL ALERT: Oxygen levels have dropped to 0%! Immediate action required!");
                b= false;
            }
            else if (currentLevel > 1)
            {
                
                Console.WriteLine($"\n[POWER MANAGER] {message} Level: {currentLevel:F1}%. Initiating power-shedding...");

                foreach (var consumer in _consumers)
                {
                    if (!consumer.IsEssential && consumer.IsOnline)
                    {
                        consumer.ShutDown("Emergency O2 Preservation");
                    }
                }
            }
        }
        private void HandleOxygenRestored(object sender, EventArgs e)
        {
            Console.WriteLine("\n[POWER MANAGER] Oxygen stable. Booting up non-essential systems...");
            foreach (var module in _consumers)
            {
                if (!module.IsEssential && !module.IsOnline)
                {
                    module.PowerOn();
                }
            }
        }

    }
}
