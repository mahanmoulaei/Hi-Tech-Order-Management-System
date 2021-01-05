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
    public static class CategoryDB
    {
        //SAVE
        public static void SaveCategoryRecord(Category cat)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdNewCat = new SqlCommand();
            cmdNewCat.CommandText = "INSERT INTO Categories (CategoryID,CategoryName)" +
                                    "VALUES (@CategoryID,@CategoryName)";

            cmdNewCat.Parameters.AddWithValue("@CategoryID", cat.CategoryID);
            cmdNewCat.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
            cmdNewCat.Connection = connDB;
            cmdNewCat.ExecuteNonQuery();
            connDB.Close();
        }

        //DELETE
        public static void DeleteCategoryRecord(Category cat)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdDeleteCat = new SqlCommand();
            //Parameterized Query
            cmdDeleteCat.CommandText = "DELETE FROM Categories " +
                                    "WHERE CategoryID = " + cat.CategoryID;

            cmdDeleteCat.Connection = connDB;
            cmdDeleteCat.ExecuteNonQuery();
            connDB.Close();


        }

        //LIST
        public static List<Category> GetCategoryList()
        {
            List<Category> listCategory = new List<Category>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectCategory = new SqlCommand("SELECT * FROM Categories", connDB);
            SqlDataReader sqlReader = cmdSelectCategory.ExecuteReader();
            Category cat;
            while (sqlReader.Read())
            {
                cat = new Category();
                cat.CategoryID = Convert.ToInt32(sqlReader["CategoryID"]);
                cat.CategoryName = sqlReader["CategoryName"].ToString();
                listCategory.Add(cat);

            }
            return listCategory;

        }

        //UPDATE
        public static void UpdateCategoryRecord(Category cat)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdateCat = new SqlCommand();
            //Parameterized Query
            cmdUpdateCat.CommandText = "UPDATE Categories " +
                "SET CategoryID='" + cat.CategoryID + "', CategoryName='" + cat.CategoryName + "' " +
                "WHERE CategoryID =" + cat.CategoryID;

            cmdUpdateCat.Connection = connDB;
            cmdUpdateCat.ExecuteNonQuery();
            connDB.Close();
        }

        //SEARCH
        public static Category GetCategoryRecordID(int caId)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectCatID = new SqlCommand();

            cmdSelectCatID.CommandText = "SELECT * FROM Categories " +
                                    "WHERE CategoryID = " + caId;
            cmdSelectCatID.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectCatID.ExecuteReader();
            Category cat = new Category();
            if (sqlReader.Read())
            {
                cat.CategoryID = Convert.ToInt32(sqlReader["CategoryID"]);
                cat.CategoryName = sqlReader["CategoryName"].ToString();
            }
            else
            {
                cat = null;
            }

            return cat;
        }

        public static Category GetCategoryRecordName(string caName)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectCatName = new SqlCommand();

            cmdSelectCatName.CommandText = "SELECT * FROM Categories " +
                                    "WHERE CategoryName = '" + caName + "' ";
            cmdSelectCatName.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectCatName.ExecuteReader();
            Category cat = new Category();
            if (sqlReader.Read())
            {
                cat.CategoryID = Convert.ToInt32(sqlReader["CategoryID"]);
                cat.CategoryName = sqlReader["CategoryName"].ToString();
            }
            else
            {
                cat = null;
            }

            return cat;
        }


    }
}
