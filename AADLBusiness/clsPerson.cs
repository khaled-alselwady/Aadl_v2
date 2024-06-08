using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AADLBusiness;
using AADL_DataAccess;
using MethodTimer;

namespace AADLBusiness
{
   /// <summary>
   /// This is the class , which is responsible to represent personal information in the system , 
   /// the person might be lawyer ,or citinzen  or anything like user or even admins, this is the core for all. 
   /// </summary>
    public  class clsPerson
    {
        public enum enPhoneType {  Standard,WhatsApp};
        public enum enSearchBy { PersonID,PassportNo,NationalNo,FullName,Phone,WhatsApp,Email};
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? PersonID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName
        {
            get {
           
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(FirstName + " ");
                stringBuilder.Append(SecondName + " ");
                stringBuilder.Append(ThirdName + " ");
                stringBuilder.Append(LastName);

                return stringBuilder.ToString();
            }

        }
        public string NationalNo { set; get; }
        public string PassportNo {  set; get; }

       // [Range(1924, 2007,"Date of birth must be between 1970 and 2000.")]
        public DateTime? DateOfBirth { set; get; }
        public short? Gender { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string WhatsApp { set; get; }

        public bool IsActive { set; get; }

        public string Email { set; get; }
        public int CountryID { set; get; }

        public int CityID { set; get; }

        public int? CreatedByUserID {  set; get; }
        public DateTime IssueDate { set; get; }

        public clsCity CityInfo;
        public clsCountry CountryInfo;

        public clsUser UserInfo;
       
        public clsUser EditedUserInfo;

        private string _ImagePath;
      
        public string ImagePath   
        {
            get { return _ImagePath; }   
            set { _ImagePath = value; }  
        }
        
        public DateTime? DateOfLastEdit { set; get; }

        public int? EditedByUserID { set; get; }

        public clsPerson()
        {
            this.PersonID =null;
            this.NationalNo = "";
            this.PassportNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth =null;
            this.Address = "";
            this.Phone = "";
            this.WhatsApp = "";
            this.Email = "";
            this.CountryID = -1;
            this.CityID = -1;
            this.IssueDate = DateTime.MinValue;
            this.ImagePath = "";
            this.IsActive = true;
            this.EditedByUserID = null;
            this.CreatedByUserID = null;

            Mode = enMode.AddNew;
        }

        private clsPerson(int? PersonID, string NationalNo, string PassportNo,
            string FirstName,string SecondName, string ThirdName,
            string LastName, short? Gender, DateTime? DateOfBirth,
             string Address, string Phone,string WhatsApp, string Email,
            string ImagePath,int CreatedByUserID, int CountryID,
            int CityID, DateTime IssueDate,   bool IsActive)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;   
            this.PassportNo = PassportNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Phone = Phone;
            this.WhatsApp = WhatsApp;
            this.Email = Email;
            this.ImagePath = ImagePath;
            this.CreatedByUserID = CreatedByUserID;
            this.CountryID = CountryID;
            this.CityID= CityID;
            this.IssueDate = IssueDate;
            this.IsActive = IsActive;
            this.CountryInfo = clsCountry.Find(CountryID);
            this.CityInfo = clsCity.Find(CityID);
            this.UserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }
     
        private bool _AddNewPerson()
        {
        
                this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.PassportNo,
                this.FirstName,this.SecondName ,this.ThirdName,
                this.LastName,this.Gender,
                this.DateOfBirth,  this.Address, this.Phone,this.WhatsApp, this.Email,
                 this.ImagePath,this.CreatedByUserID, this.CountryID,this.CityID, this.IssueDate, this.IsActive);
                
            return (this.PersonID !=null);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 
         

            return clsPersonData.UpdatePerson(
                this.PersonID,this.NationalNo,this.PassportNo, this.FirstName,this.SecondName,this.ThirdName,
                this.LastName, this.Gender, this.DateOfBirth,
                this.Address, this.Phone,this.WhatsApp, this.Email, this.ImagePath,
                 this.CreatedByUserID, this.CountryID,this.CityID, this.IssueDate, this.IsActive);
        }

        [Time]
        private static clsPerson GetPersonInfoByPersonID(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo="", PassportNo = "", Email = "",
                     Phone = "", WhatsApp = "", Address = "", ImagePath = "";
            DateTime IssueDate=DateTime.Now;
            DateTime? DateOfBirth = DateTime.Now;
            int?  EditedByUserID = -1;
            int CountryID = -1, CityID = -1, CreatedByUserID = -1;
            short? Gender = 0;
            bool IsActive = false;

            bool IsFound = clsPersonData.GetPersonInfoByID 
                                (
                                    PersonID,ref NationalNo,ref PassportNo, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
                                    ref Address, ref Phone,ref WhatsApp, ref Email,
                                    ref ImagePath,ref CreatedByUserID,ref CountryID,ref CityID,ref IssueDate,
                                      ref IsActive
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID,NationalNo,PassportNo,FirstName,SecondName,ThirdName,LastName,
                    Gender,DateOfBirth,Address,Phone,WhatsApp,Email,ImagePath,CreatedByUserID,CountryID,CityID,IssueDate,
                    IsActive);
            else
                return null;
        }

        private static clsPerson GetPersonInfoByNationalNo(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", PassportNo = "", Email = "",
            Phone = "", WhatsApp = "", Address = "", ImagePath = "";
            DateTime IssueDate=DateTime.Now;
            DateTime? DateOfBirth = DateTime.Now;
            int? PersonID =null;
            int CountryID = -1, CityID = -1, CreatedByUserID = -1;
            short? Gender = 0;
            bool IsActive = false;

            bool IsFound = clsPersonData.GetPersonInfoByNationalNo
                                (
                                     NationalNo, ref PersonID, ref PassportNo, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
                                    ref Address, ref Phone, ref WhatsApp, ref Email,
                                    ref ImagePath, ref CreatedByUserID, ref CountryID, ref CityID,ref IssueDate,
                                    ref      IsActive
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, NationalNo, PassportNo, FirstName, SecondName, ThirdName, LastName,
                    Gender, DateOfBirth, Address, Phone, WhatsApp, Email, ImagePath, CreatedByUserID, CountryID, CityID, IssueDate,
                       IsActive);
            else
                return null;
        }

        private static clsPerson GetPersonInfoByPassportNo(string PassportNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "",
            Phone = "", WhatsApp = "", Address = "", ImagePath = "";
            DateTime IssueDate=DateTime.Now;
            DateTime? DateOfBirth = DateTime.Now;
            int? PersonID = -1, EditedByUserID = -1;
            int CountryID = -1, CityID = -1, CreatedByUserID = -1;
            short? Gender = 0;
            bool IsActive = false;

            bool IsFound = clsPersonData.GetPersonInfoByPassportNo
                                (
                                     PassportNo, ref PersonID, ref NationalNo, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
                                    ref Address, ref Phone, ref WhatsApp, ref Email,
                                    ref ImagePath, ref CreatedByUserID, ref CountryID, ref CityID, ref IssueDate,
                                        ref IsActive
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, NationalNo, PassportNo, FirstName, SecondName, ThirdName, LastName,
                    Gender, DateOfBirth, Address, Phone, WhatsApp, Email, ImagePath, CreatedByUserID, CountryID, CityID, IssueDate,
                       IsActive);
            else
                return null;
        }

        /// <summary>
        /// Will return person info by searching on it through his name 
        /// This function will handle special case where person name exceed the main 4 parts, 
        /// in case might be first name consists of two parts ,
        /// or last name might be something more than one part. 
        /// The name will not be parse to several parts instead it will be used as it is.
        /// The name of person might contain 4 parts or special compound case , where also will be handle
        /// </summary>
        /// <param name="FullName">Full name of person</param>
        private static clsPerson GetPersonInfoByFullName(string FullName)
        {
            string  NationalNo = "", PassportNo = "", Email = "",
            Phone = "", WhatsApp = "", Address = "", ImagePath = "";
            DateTime IssueDate = DateTime.Now;
            DateTime? DateOfBirth = DateTime.Now;
            int? PersonID = -1, EditedByUserID = -1;
            int CountryID = -1, CityID = -1, CreatedByUserID = -1;
            short? Gender = 0;
            bool IsActive = false;
            bool IsFound = false;

            // Count the number of spaces
            int numberOfParts = FullName.Count(char.IsWhiteSpace) + 1;
            // Adding 1 to account for the last part after the last space

            Console.WriteLine("Number of parts: " + numberOfParts);
            try
            {

            //special case handling.
            if (numberOfParts > 4)
            {
                string FirstName = "", SecondName = "", ThirdName = "", LastName = "";
                IsFound = clsPersonData.GetPersonInfoByCompoundName
                                    (
                                         FullName,  ref FirstName, ref SecondName,
                                        ref ThirdName, ref LastName, ref PersonID, ref PassportNo, ref NationalNo, ref Gender, ref DateOfBirth,
                                        ref Address, ref Phone, ref WhatsApp, ref Email,
                                        ref ImagePath, ref CreatedByUserID, ref CountryID, ref CityID, ref IssueDate,
                                        ref      IsActive
                                    );
                if (IsFound)
                    //we return new object of that person with the right data
                    return new clsPerson(PersonID, NationalNo, PassportNo, FirstName, SecondName, ThirdName,
                        LastName, Gender, DateOfBirth, Address, Phone, WhatsApp, Email, ImagePath, CreatedByUserID, CountryID, CityID, IssueDate,
                          IsActive);
            }
          
            else
            {
                (string FirstName, string SecondName, string ThirdName, string LastName) nameComponents = clsHelperClasses.ParseFullName(FullName);

                 IsFound = clsPersonData.GetPersonInfoByFullName
                                    (
                                           nameComponents.FirstName, nameComponents.SecondName,
                                        nameComponents.ThirdName, nameComponents.LastName, ref PersonID, ref PassportNo, ref NationalNo, ref Gender, ref DateOfBirth,
                                        ref Address, ref Phone, ref WhatsApp, ref Email,
                                        ref ImagePath, ref CreatedByUserID, ref CountryID, ref CityID, ref IssueDate,
                                        ref      IsActive
                                    );
                if (IsFound)
                    //we return new object of that person with the right data
                    return new clsPerson(PersonID, NationalNo, PassportNo, nameComponents.FirstName, nameComponents.SecondName, nameComponents.ThirdName,
                        nameComponents.LastName, Gender, DateOfBirth, Address, Phone, WhatsApp, Email, ImagePath, CreatedByUserID, CountryID, CityID, IssueDate,
                           IsActive);

            }

                return null;
            }
                catch(Exception ex)
            {
                throw ex;
            }
        }
        private static clsPerson GetPersonInfoByPhone(string Value, enPhoneType PhoneType)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", PassportNo="", Email = "",
            Phone="",WhatsApp = "", Address = "", ImagePath = "";
            DateTime IssueDate=DateTime.Now;
            DateTime? DateOfBirth = DateTime.Now;
            int? PersonID = -1, EditedByUserID = -1;
            int CountryID = -1, CityID = -1, CreatedByUserID = -1;
            short? Gender = 0;
            bool IsActive = false;

            switch (PhoneType)
            {
                case enPhoneType.Standard:
                {
                        Phone = Value;
                        bool IsFound = clsPersonData.GetPersonInfoByPhone
                                (
                                      ref Phone, ref PersonID, ref PassportNo, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
                                    ref Address, ref WhatsApp, ref Email,
                                    ref ImagePath, ref CreatedByUserID, ref CountryID, ref CityID, ref IssueDate,
                                   ref       IsActive, clsPersonData.enPhoneType.Standard
                                );

                        if (IsFound)
                            //we return new object of that person with the right data
                            return new clsPerson(PersonID, NationalNo, PassportNo, FirstName, SecondName, ThirdName,
                               LastName, Gender, DateOfBirth, Address, Phone, WhatsApp, Email, ImagePath, CreatedByUserID, CountryID, CityID, IssueDate,
                                  IsActive);
                        else
                            return null;
                    }

                case enPhoneType.WhatsApp:
                    {
                        WhatsApp = Value;
                        bool IsFound = clsPersonData.GetPersonInfoByPhone
                              (
                                   ref Phone, ref PersonID, ref PassportNo, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
                                  ref Address, ref WhatsApp, ref Email,
                                  ref ImagePath, ref CreatedByUserID, ref CountryID, ref CityID, ref IssueDate
                                  ,  ref IsActive, clsPersonData.enPhoneType.WhatsApp
                              );

                        if (IsFound)
                            //we return new object of that person with the right data
                            return new clsPerson(PersonID, NationalNo, PassportNo, FirstName, SecondName, ThirdName,
                               LastName, Gender, DateOfBirth, Address, Phone, WhatsApp, Email, ImagePath, CreatedByUserID, CountryID, CityID,IssueDate
                                , IsActive);
                        else
                            return null;
                    }
            }

            return null;
        }
        private static clsPerson GetPersonInfoByEmail(string Email)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", PassportNo = "",
            Phone = "", WhatsApp = "", Address = "", ImagePath = "";
            DateTime IssueDate=DateTime.Now;
            DateTime? DateOfBirth = DateTime.Now;
            int? PersonID = -1, EditedByUserID = -1;
            int CountryID = -1, CityID = -1, CreatedByUserID = -1;
            short? Gender = 0;
            bool IsActive = false;

            bool IsFound = clsPersonData.GetPersonInfoByEmail
                                (
                                     Email, ref PersonID,ref PassportNo, ref NationalNo, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
                                    ref Address,  ref WhatsApp, ref Phone,
                                    ref ImagePath, ref CreatedByUserID, ref CountryID, ref CityID,ref IssueDate
                                    , ref IsActive
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, NationalNo, PassportNo, FirstName, SecondName, ThirdName, LastName,
                    Gender, DateOfBirth, Address, Phone, WhatsApp, Email, ImagePath, CreatedByUserID, CountryID, CityID, IssueDate,
                       IsActive);
            else
                return null;
        }


        [Time]
        /// <summary>
        /// Enter a value either national number or passport number , you need to choose the search type 
        /// and based on it searching will happen
        /// </summary>
        /// <param name="value"> It could be either (National No, Passport No) on both cases there is a function to search for 
        /// bother values .</param>
        /// <param name="searchBy"> Type of the value you wants to search for or it  the most unique and the most common ,
        /// are national no  and passport no </param>
        /// <returns>a person object returned from database in case was found or NULL in case nothing was found.</returns>
        public static clsPerson Find<T>(T value, enSearchBy searchBy)
        {
          
            switch (searchBy)
            {
                case enSearchBy.PersonID:
                    {
                        if (int.TryParse(value.ToString(), out int ID))
                        {
                            return GetPersonInfoByPersonID(ID);
                        }
                        else
                        {
                            return null;
                        }

                    }
           
                case enSearchBy.NationalNo:
                    {
                        if(!string.IsNullOrEmpty(value.ToString()))
                        {

                        return GetPersonInfoByNationalNo(value.ToString());

                        }
                        else
                        {
                            return null;
                        }

                    }
         
                case enSearchBy.PassportNo:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            return GetPersonInfoByPassportNo(value.ToString());
                        }
                        else
                        {
                            return null;
                        }

                    }

                case enSearchBy.FullName:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            return GetPersonInfoByFullName(value.ToString());
                        }
                        else
                        {
                            return null;
                        }

                    }
               
                case enSearchBy.Phone:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            return GetPersonInfoByPhone(value.ToString(),enPhoneType.Standard);
                        }
                        else
                        {
                            return null;
                        }

                    }
            
                case enSearchBy.WhatsApp:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            return GetPersonInfoByPhone(value.ToString(), enPhoneType.WhatsApp);
                        }
                        else
                        {
                            return null;
                        }

                    }
        
                case enSearchBy.Email:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            return GetPersonInfoByEmail(value.ToString());
                        }
                        else
                        {
                            return null;
                        }

                    }
            }

            return null;
        
        }

        /// <summary>
        /// This method can take current properties of the object and save or update in the database.
        /// 
        /// </summary>
        /// <returns>The result of saving or updating operation.</returns>
        [Obsolete("This method in the future version of project AADL , will be deprecated method ( useless ).")]
        
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }

        public async  static Task < DataTable > GetAllPeople()
        {
            return await clsPersonData.GetAllPeople();
        }
        public async static Task<DataTable> GetAllDuplicatedFullNamePeople(string FullName)
        {

            return await clsPersonData.GetAllPeopleByFullName(FullName);
        }
        
        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID); 
        }

     /// <summary>
     /// Check weather a person existed in database table or not .
     /// </summary>
     /// <typeparam name="T"> Parameter accept different data type , so you can use different type to search on it .</typeparam>
     /// <param name="value"> the value will be search for the main properties and unique (ID,NationalNo,PassportNo,personID,
     /// it might be any unique properties ..)</param>
     /// <param name="searchBy"> based on the mode search will happen choose the properties type you to search for it .</param>
     /// <returns> either false which means no person exists with parameter value , or true </returns>
        public static bool IsPersonExist<T>(T value, enSearchBy searchBy)
        {

            switch (searchBy)
            {
                case enSearchBy.PersonID:
                    {
                       
                        if (int.TryParse(value.ToString(), out int PersonID))
                        {

                            return clsPersonData.IsPersonExist(clsPersonData.PersonColumn.PersonID, PersonID.ToString());
                        }
                        else
                        {
                            return false;
                        }

                    }
          
                case enSearchBy.NationalNo:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {

                            return clsPersonData.IsPersonExist(clsPersonData.PersonColumn.NationalNo, value.ToString().Trim());
                        }
                        else
                        {
                            return false;
                        }
                    }
           
                case enSearchBy.PassportNo:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {

                            return clsPersonData.IsPersonExist(clsPersonData.PersonColumn.PassportNo, value.ToString().Trim());
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enSearchBy.FullName:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {

                            return clsPersonData.IsPersonExist(clsPersonData.PersonColumn.FullName, value.ToString().Trim());
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enSearchBy.WhatsApp:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {

                            return clsPersonData.IsPersonExist(clsPersonData.PersonColumn.WhatsApp,value.ToString().Trim());
                        }
                        else
                        {
                            return false;
                        }
                    }
               
                case enSearchBy.Phone:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {

                            return clsPersonData.IsPersonExist(clsPersonData.PersonColumn.Phone, value.ToString().Trim());
                        }
                        else
                        {
                            return false;
                        }
                    }
     
                case enSearchBy.Email:
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {

                            return clsPersonData.IsPersonExist(clsPersonData.PersonColumn.Email, value.ToString().Trim());
                        }
                        else
                        {
                            return false;
                        }
                    }

            }
            return false;
        }

        public static bool IsFullNameDuplicated(string FullName)
        {
            return clsPersonData.IsPersonFullNameDuplicated(FullName);

        }
        
    }
}
