using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BLL;
using FinalProject.DAL;
using System.Data.SqlClient;

namespace FinalProject.DAL
{
    public static class UserDB
    {
        public static void SaveUserRecord(User usr)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdNewUser = new SqlCommand();
            cmdNewUser.CommandText = "INSERT INTO UserAccounts (UserID,Password,EmployeeID,Comment)" +
                                     "VALUES (@UserID,@Password,@EmployeeID,@Comment)";
            cmdNewUser.Parameters.AddWithValue("@UserID", usr.UserID);
            cmdNewUser.Parameters.AddWithValue("@Password", usr.UserPassword);
            cmdNewUser.Parameters.AddWithValue("@EmployeeID", usr.UserEmployeeID);
            cmdNewUser.Parameters.AddWithValue("@Comment", usr.UserComment); 
            cmdNewUser.Connection = connDB;
            cmdNewUser.ExecuteNonQuery();
            connDB.Close();
        }

        public static List<User> GetUserRecordList()
        {
            List<User> listUser = new List<User>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectUser = new SqlCommand("SELECT * FROM UserAccounts", connDB);
            SqlDataReader sqlReader = cmdSelectUser.ExecuteReader();
            User usr;
            while (sqlReader.Read())
            {
                usr = new User();
                usr.UserID = Convert.ToInt32(sqlReader["UserID"]);
                usr.UserPassword = sqlReader["Password"].ToString();
                usr.UserEmployeeID = Convert.ToInt32(sqlReader["EmployeeID"]);
                usr.UserComment = sqlReader["Comment"].ToString();
                listUser.Add(usr);
            }
            return listUser;
        }

        public static User UserGetRecord(int userID)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectUser = new SqlCommand();

            cmdSelectUser.CommandText = "SELECT * FROM UserAccounts " +
                                    "WHERE UserID = " + userID;
            cmdSelectUser.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectUser.ExecuteReader();
            User usr = new User();
            if (sqlReader.Read())
            {
                usr.UserID = Convert.ToInt32(sqlReader["UserID"]);
                usr.UserPassword = sqlReader["Password"].ToString();
                usr.UserEmployeeID = Convert.ToInt32(sqlReader["EmployeeID"]);
                usr.UserComment = sqlReader["Comment"].ToString();
            }
            else
            {
                usr = null;
            }

            return usr;
        }
        
        public static User UserGetRecordPass(string pass)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectUserPass = new SqlCommand();

            cmdSelectUserPass.CommandText = "SELECT * FROM UserAccounts " +
                                    "WHERE Password= " + pass;
            cmdSelectUserPass.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectUserPass.ExecuteReader();
            User usr = new User();
            if (sqlReader.Read())
            {
                usr.UserID = Convert.ToInt32(sqlReader["UserID"]);
                usr.UserPassword = sqlReader["Password"].ToString();
                usr.UserEmployeeID = Convert.ToInt32(sqlReader["EmployeeID"]);
                usr.UserComment = sqlReader["Comment"].ToString();
            }
            else
            {
                usr = null;
            }

            return usr;
        }


        public static List<User> UserGetRecordListFirstPassword(string password)
        {
            List<User> listUser = new List<User>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectUser = new SqlCommand("SELECT * FROM UserAccounts WHERE Password = '" + password + "'", connDB);
            SqlDataReader sqlReader = cmdSelectUser.ExecuteReader();
            User usr;
            while (sqlReader.Read())
            {
                usr = new User();
                usr.UserID = Convert.ToInt32(sqlReader["UserID"]);
                usr.UserPassword = sqlReader["Password"].ToString();
                usr.UserEmployeeID = Convert.ToInt32(sqlReader["EmployeeID"]);
                usr.UserComment = sqlReader["Comment"].ToString();
                listUser.Add(usr);
            }
            return listUser;
        }



        public static void UpdateUserRecord(User usr)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdateUser = new SqlCommand();
            //Parameterized Query
            cmdUpdateUser.CommandText = "UPDATE UserAccounts " +
                "SET UserID='" + usr.UserID + "', Password='" + usr.UserPassword + "' ,EmployeeID='" + usr.UserEmployeeID + "', Comment='" + usr.UserComment + "' " +
                "WHERE UserID =" + usr.UserID;

            cmdUpdateUser.Connection = connDB;
            cmdUpdateUser.ExecuteNonQuery();
            connDB.Close();
        }

        public static void DeleteUserRecord(User usr)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdDeleteUser = new SqlCommand();
            //Parameterized Query
            cmdDeleteUser.CommandText = "DELETE FROM UserAccounts " +
                                    "WHERE UserID = " + usr.UserID;

            cmdDeleteUser.Connection = connDB;
            cmdDeleteUser.ExecuteNonQuery();
            connDB.Close();
        }
    }
}
