using AADLBusiness;
using AADLDataAccess;
using Iced.Intel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness
{
    public class clsAdmin
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int? AdminID { get; set; }

        public string UserName {  get; set; }

        public string Password { get; set; }    

        public bool IsActive { get; set; }

        public DateTime IssueDate { get; set; }

        public int ?CreatedByAdminID {  get; set; }
        public clsAdmin()

        {
            this.AdminID = null;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            this.IssueDate = DateTime.Now;
            this.CreatedByAdminID = null;
            Mode = enMode.AddNew;
        }

        private clsAdmin(int? AdminID, string Username, string Password,
       bool IsActive, DateTime IssueDate, int ?CreatedByAdminID)

        {
            this.AdminID = AdminID;
            this.UserName = Username;
            this.Password = Password;
            this.IsActive = IsActive;
            this.IssueDate = IssueDate;
            this.CreatedByAdminID = CreatedByAdminID;
            Mode = enMode.Update;
        }

        private bool _AddNewAdmin()
        {
            //call DataAccess Layer 

            this.AdminID = clsAdminData.AddNewAdmin(this.UserName,
                this.Password, this.IsActive, this.IssueDate, this.CreatedByAdminID);

            return (this.AdminID != null);
        }
        private bool _UpdateAdmin()
        {
            //call DataAccess Layer 

            return clsAdminData.UpdateAdmin(this.AdminID, this.UserName,
                this.Password, this.IsActive, this.IssueDate, this.CreatedByAdminID);
        }

        public static clsAdmin FindByAdminID(int? AdminID)
        {
            int ?CreatedByAdminID = null;
            string UserName = "", Password = "";
            bool IsActive = false;
            DateTime IssueDate = DateTime.MinValue;

            if (AdminID != null)
            {

                bool IsFound = clsAdminData.GetAdminInfoByAdminID
                                    (AdminID, ref UserName, ref Password, ref IsActive, ref IssueDate, ref CreatedByAdminID);

                if (IsFound)
                    //we return new object of that User with the right data
                    return new clsAdmin(AdminID, UserName, Password, IsActive, IssueDate, CreatedByAdminID);
            }


            return null;

        }
        public static clsAdmin FindByUsernameAndPassword(string UserName, string Password)
        {
            int ?CreatedByAdminID = null, AdminID =null;
            bool IsActive = false;
            DateTime IssueDate = DateTime.MinValue;

            bool IsFound = clsAdminData.GetAdminInfoByUsernameAndPassword
                                (UserName, Password, ref AdminID, ref IsActive, ref IssueDate, ref CreatedByAdminID);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsAdmin(AdminID, UserName, Password, IsActive, IssueDate, CreatedByAdminID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAdmin())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateAdmin();

            }

            return false;
        }

        public async static Task<DataTable> GetAllAdmins()
        {
            return await clsAdminData.GetAllAdmins();
        }

        public static clsAdmin FindByAdminID(int AdminID)
        {
            int? CreatedByAdminID = null;
            string UserName = "", Password = "";
            bool IsActive = false;
            DateTime IssueDate = DateTime.MinValue;

            if (AdminID != null)
            {

                bool IsFound = clsAdminData.GetAdminInfoByAdminID
                                    (AdminID, ref UserName, ref Password, ref IsActive, ref IssueDate, ref CreatedByAdminID);

                if (IsFound)
                    //we return new object of that User with the right data
                    return new clsAdmin(AdminID, UserName, Password, IsActive, IssueDate, CreatedByAdminID);
            }


            return null;

        }

    }
}
