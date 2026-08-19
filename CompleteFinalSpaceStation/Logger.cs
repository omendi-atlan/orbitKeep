using System;

namespace ProjectSpaceStation
{
    /// <summary>
    /// File logging tool.
    /// </summary>
    public static class Logger // saves to file
    {
        /// <summary>
        /// Writes log text.
        /// </summary>
        public static void Log(string message) // write log message
        {
            try // handle file errors
            {
                System.IO.File.AppendAllText("log.txt", message + Environment.NewLine); // save the text
            }
            catch (Exception) // if save fails
            {
                Console.WriteLine("Log save failed!"); // print error warning
            }
        }
    }
}
