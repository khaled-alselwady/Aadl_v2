using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness
{
    internal class clsSettings
    {
        public static bool WriteEventToLogFile(string Message, EventLogEntryType eventLogEntryType)
        {

            string SourceName = "AADLAPPLICATION";

            try
            {

                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, "Application");
                    Console.WriteLine("Event source created");
                }

                EventLog.WriteEntry(SourceName, Message, eventLogEntryType);

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return true;
        }

    }
}
