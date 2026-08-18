using System;
using System.Threading;

namespace ProjectSpaceStation
{
    public delegate void AlertEventHandler(string message, double currentLevel);
   

    public class StationMonitor
    {
        public event AlertEventHandler CriticalOxygenAlert;//first  required event
        public event EventHandler OxygenRestoredAlert; // Our second required event

        private bool _isRunning = false;
        private Thread _monitorThread;
        private readonly LifeSupportSystem _lifeSupport;
        private readonly object _lockObject = new object(); //Thread safety lock
        

        public StationMonitor(LifeSupportSystem lifeSupport)
        {
            _lifeSupport = lifeSupport;
        }

        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;

            _monitorThread = new Thread(RunMonitor)
            {
                IsBackground = true,
                Name = "StationMonitoringThread"
            };

            _monitorThread.Start();
            Console.WriteLine("[SYSTEM] Background station monitoring thread stated.");
        }

        public void Stop()
        {
            _isRunning = false;
            Console.WriteLine("[SYSTEM] Stopping background monitoring thread...");
        }

        private void RunMonitor()
        {
            while (_isRunning)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(_lockObject, ref lockTaken);

                    _lifeSupport.DrainOxygen(1.5);
                }
                finally
                {
                    Monitor.Exit(_lockObject);
                }

                if (_lifeSupport.StationOxygenReserve < 20.0)
                {
                    CriticalOxygenAlert?.Invoke("[ALERT] Critical O2 Warning! Current Level:", _lifeSupport.StationOxygenReserve);
                }

                Thread.Sleep(3000);
            }
        }
        public void ReplenishOxygen(double amount)
        {
            lock (_lockObject)
            {
                // We use the _lifeSupport object to get and set the oxygen
                _lifeSupport.StationOxygenReserve += amount;

                if (_lifeSupport.StationOxygenReserve > 100)
                {
                    _lifeSupport.StationOxygenReserve = 100;
                }

                // If oxygen is safely above 50%, trigger the second event
                if (_lifeSupport.StationOxygenReserve > 50)
                {
                    OxygenRestoredAlert?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
