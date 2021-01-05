using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL;

namespace FinalProject.BLL
{
    public class User
    {
        private int userID;
        private string userPass;
        private int userEmpID;
        private string userComment;


        public User(int userID, string userPass, int userEmpID, string userComment)
        {
            this.userID = userID;
            this.userPass = userPass;
            this.userEmpID = userEmpID;
            this.userComment = userComment;
        }

        public User()
        {
        }

        public int UserID { get => userID; set => userID = value; }
        public string UserPassword { get => userPass; set => userPass = value; }
        public int UserEmployeeID { get => userEmpID; set => userEmpID = value; }
        public string UserComment { get => userComment; set => userComment = value; }


        public void SaveUser(User usr)
        {
            UserDB.SaveUserRecord(usr);
        }

        public List<User> GetUserList()
        {
            return UserDB.GetUserRecordList();
        }

        public List<User> GetUserList(string name)
        {
            return UserDB.GetUserRecordList();
        }

        public User GetUser(int uID)
        {
            return UserDB.UserGetRecord(uID);
        }

        public User GetUserPass(string pass)
        {
            return UserDB.UserGetRecordPass(pass);
        }

        public List<User> GetUserListPass(string password)
        {
            return UserDB.UserGetRecordListFirstPassword(password);
        }

       

        public void UpdateUser(User usr)
        {
            UserDB.UpdateUserRecord(usr);
        }

        public void DeleteUser(User usr)
        {
            UserDB.DeleteUserRecord(usr);
        }


        
    }
}
