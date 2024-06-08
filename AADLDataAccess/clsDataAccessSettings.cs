using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
namespace AADL_DataAccess
{
    public static class clsDataAccessSettings
    {


        public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        public static bool WriteEventToLogFile(List<string>Messages, EventLogEntryType eventLogEntryType)
        {

            string SourceName = "AADL_APPLICATION";

            try
            {

                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, "Desktop_Application");
                    Console.WriteLine("Event source created");
                }


                foreach (string Message in Messages)
                {

                EventLog.WriteEntry(SourceName, Message, eventLogEntryType);
                }

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return true;
        }
        public static bool WriteEventToLogFile( string Message, EventLogEntryType eventLogEntryType)
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
