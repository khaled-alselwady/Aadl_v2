using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AADLBusiness;
using AADLBusiness;
using Microsoft.Win32;


namespace AADL
{
    internal static  class clsGlobal
    {
        // public static clsUser CurrentUser;
        public static clsAdmin CurrentAdmin = clsAdmin.FindByAdminID(2);
        public static clsUser CurrentUser=clsUser.FindByUserID(8);

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username=="" && File.Exists(filePath)) 
                { 
                     File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#"+Password ;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);
                   
                  return true;
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;
            }

        }
        public static bool RememberUsernameAndPasswordV2(string Username, string Password)
        {
            // Specify the Registry key and path
            string keyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\DVLD";
            string valueName = "USERNAME";
            string valueData = Username;
            string valueName2 = "PASSWORD";
            string valueData2 = Password;

            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, valueName, valueData, RegistryValueKind.String);
                
                Registry.SetValue(keyPath, valueName2, valueData2, RegistryValueKind.String);

                Console.WriteLine($"Value {valueName} successfully written to the Registry.");

                Console.WriteLine($"Value {valueName2} successfully written to the Registry.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
           
            return true;

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath  = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;   
            }

        }

        public static bool GetStoredCredentialV2(ref string Username, ref string Password)
        {
            // Specify the Registry key and path
            string keyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\DVLD";
            string valueName = "USERNAME";
            string valueName2 = "PASSWORD";
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                // read the value from the Registry
                string value = "", value2 = "";

                Username = Registry.GetValue(keyPath, valueName, null) as string;
                Password = Registry.GetValue(keyPath, valueName2, null) as string;


                Console.WriteLine($"Value {value} successfully read from {valueName} in the Registry.");

                Console.WriteLine($"Value {value2} successfully read from {valueName2} in the Registry.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return true;

        }

        public static bool WriteEventToLogFile(string Message, EventLogEntryType eventLogEntryType)
        {

            string SourceName = "Application";

            try
            {

            if(!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "AADLApplication");
                Console.WriteLine("Event source created");
            }
            
         
            EventLog.WriteEntry(SourceName, Message, eventLogEntryType);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return true;
        }

        /// <summary>
        /// Represents an item in a CheckedListBox.
        /// </summary>
        public class CheckListBoxItem : IEquatable<CheckListBoxItem>
        {
            public int ID;
            public string Text;

            /// <summary>
            /// Initializes a new instance of the CheckListBoxItem class.
            /// </summary>
            /// <param name="id">The ID of the item.</param>
            /// <param name="text">The text of the item.</param>
            public CheckListBoxItem(int id, string text)
            {
                ID = id;
                Text = text;
            }
            /// <summary>
            /// Returns a string representation of the item.
            /// </summary>
            /// <returns>The text of the item.</returns>
            public override string ToString() { return Text; }

            /// <summary>
            /// Determines whether the specified CheckListBoxItem is equal to the current CheckListBoxItem.
            /// </summary>
            /// <param name="other">The CheckListBoxItem to compare with the current CheckListBoxItem.</param>
            /// <returns>true if the specified CheckListBoxItem is equal to the current CheckListBoxItem; otherwise, false.</returns>
            public bool Equals(CheckListBoxItem other)
            {
                if (other == null)
                {
                    throw new ArgumentNullException(nameof(other), "The parameter 'other' cannot be null.");
                }

                return (ID == other.ID && Text == other.Text);

            }

            // Override GetHashCode to ensure consistency with Equals
            public override int GetHashCode()
            {
                return ID.GetHashCode() ^ Text.GetHashCode();
            }
        }

    }
}
