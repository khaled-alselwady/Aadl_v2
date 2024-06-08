using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AADL_DataAccess;

namespace AADLBusiness
{
    public  class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? UserID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }
     
        public DateTime IssueDate { set; get; }

        public int CreatedByAdminID {  set; get; }

        public clsAdmin AdminInfo { get; }
        public clsUser()

        {     
            this.UserID =null;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            this.IssueDate=DateTime.Now;
            this.CreatedByAdminID = -1;
            Mode = enMode.AddNew;
        }

        private clsUser(int? UserID, string Username,string Password,
            bool IsActive,DateTime IssueDate, int CreatedByAdminID)

        {
            this.UserID = UserID; 
            this.UserName = Username;
            this.Password = Password;
            this.IsActive = IsActive;
            this.IssueDate = IssueDate; 
            this.CreatedByAdminID= CreatedByAdminID;
            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            //call DataAccess Layer 

            this.UserID = clsUserData.AddNewUser(this.UserName,
                this.Password,this.IsActive,this.IssueDate,this.CreatedByAdminID);

            return (this.UserID != null);
        }
        private bool _UpdateUser()
        {
            //call DataAccess Layer 

            return clsUserData.UpdateUser(this.UserID,this.UserName,
                this.Password,this.IsActive,this.IssueDate,this.CreatedByAdminID);
        }
        public static clsUser FindByUserID(int? UserID)
        {
            int CreatedByAdminID= -1;
            string UserName = "", Password = "";
            bool IsActive = false;
            DateTime IssueDate= DateTime.MinValue;

            if (UserID != null)
            {

            bool IsFound = clsUserData.GetUserInfoByUserID
                                ( UserID, ref UserName,ref Password,ref IsActive,ref IssueDate,ref CreatedByAdminID);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserID,UserName,Password,IsActive,IssueDate,CreatedByAdminID);
            }

           
                return null;

        }
        public static clsUser FindByUsernameAndPassword(string UserName,string Password)
        {
            int CreatedByAdminID = -1,UserID=-1;
            bool IsActive = false;
            DateTime IssueDate = DateTime.MinValue;

            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword
                                (UserName , Password,ref UserID, ref IsActive, ref IssueDate,ref  CreatedByAdminID);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserID, UserName, Password, IsActive,IssueDate,CreatedByAdminID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        public async  static Task<DataTable> GetAllUsers()
        {
            return await clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID); 
        }

        public static bool IsUserExist(int UserID)
        {
           return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }


    }
}
