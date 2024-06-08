using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace AADLBusiness
{ 
    public interface IValidationAttribute
    {
        bool IsValid(object value);
        string ErrorMessage { get; }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeAttribute : Attribute, IValidationAttribute
    {
        public int Min { get; }
        public int Max { get; }
        public string ErrorMessage { get; }

        public RangeAttribute(int min, int max, string errorMessage)
        {
            Min = min;
            Max = max;
            ErrorMessage = errorMessage;
        }

        public bool IsValid(object value)
        {   
            if (value is int intValue)
            {
                return intValue >= Min && intValue <= Max;
            }
            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinLengthAttribute : Attribute, IValidationAttribute
    {
        public int Length { get; }
        public string ErrorMessage { get; }

        public MinLengthAttribute(int length, string errorMessage)
        {
            Length = length;
            ErrorMessage = errorMessage;
        }

        public bool IsValid(object value)
        {
            var stringValue = value as string;
            return stringValue != null && stringValue.Length >= Length;
        }
    }
    public static class Validator
    {
        public static bool Validate(object obj, out List<string> errorMessages)
        {
            bool isValid = true;
            errorMessages = new List<string>();
            Type type = obj.GetType();

            foreach (var property in type.GetProperties())
            {
                // Get all attributes for the property
                var attributes = Attribute.GetCustomAttributes(property);

                foreach (var attribute in attributes)
                {
                    // Check if the attribute implements IValidationAttribute
                    if (attribute is IValidationAttribute validationAttribute)
                    {
                        var value = property.GetValue(obj);
                        if (!validationAttribute.IsValid(value))
                        {
                            isValid = false;
                            errorMessages.Add($"Validation failed for '{property.Name}': {validationAttribute.ErrorMessage}");

                        }

                    }
                }
            }
            if (errorMessages.Count > 0)
            {
                WriteEventToLogFile(errorMessages,EventLogEntryType.Error);
            }

            return isValid;
        }
        public static bool WriteEventToLogFile(List<string> Messages, EventLogEntryType eventLogEntryType)
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

    }
    public static class validiate
    {
    public static bool IsPersonValid(clsPerson person)
    {
        Type typeOfClass = typeof(clsPerson);

        foreach(var property in typeOfClass.GetProperties())
        {
            if (Attribute.IsDefined(property, typeof(RangeAttribute)))
            {
                var rangeAttri = (RangeAttribute)Attribute.GetCustomAttribute(property, typeof(RangeAttribute));

                int value = (int)property.GetValue(person);

                if (!rangeAttri.IsValid(value))
                {
                    Console.WriteLine($"Validation failed for property '{property.Name}': {rangeAttri.ErrorMessage}");
                    return false; 
                }
            }
                if (Attribute.IsDefined(property, typeof(MinLengthAttribute)))
                {
                    var MinLenAttri = (MinLengthAttribute)Attribute.GetCustomAttribute(property, typeof(MinLengthAttribute));

                    int value = (int)property.GetValue(person);

                    if (!MinLenAttri.IsValid(value))
                    {
                        Console.WriteLine($"Validation failed for property '{property.Name}': {MinLenAttri.ErrorMessage}");
                        return false;
                    }
                }
            }
        return true;
    }
    }

}
