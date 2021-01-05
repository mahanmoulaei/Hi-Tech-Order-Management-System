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
    public static class PublisherDB
    {
        
        

        //SAVE
        public static void SavePublisherRecord(Publisher pub)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdNewPub = new SqlCommand();
            cmdNewPub.CommandText = "INSERT INTO Publishers (PublisherID,PublisherName,WebAddress)" +
                                    "VALUES (@PublisherID,@PublisherName,@WebAddress)";

            cmdNewPub.Parameters.AddWithValue("@PublisherID", pub.PublisherID);
            cmdNewPub.Parameters.AddWithValue("@PublisherName", pub.PublisherName);
            cmdNewPub.Parameters.AddWithValue("@WebAddress", pub.PublisherWeb);
            cmdNewPub.Connection = connDB;
            cmdNewPub.ExecuteNonQuery();
            connDB.Close();
        }

        //DELETE
        public static void DeletePublisherRecord(Publisher pub)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdDeletePub = new SqlCommand();
            //Parameterized Query
            cmdDeletePub.CommandText = "DELETE FROM Publishers " +
                                    "WHERE PublisherID = " + pub.PublisherID;

            cmdDeletePub.Connection = connDB;
            cmdDeletePub.ExecuteNonQuery();
            connDB.Close();


        }

        //LIST
        public static List<Publisher> GetPublisherList()
        {
            List<Publisher> listPublisher = new List<Publisher>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectPublisher = new SqlCommand("SELECT * FROM Publishers", connDB);
            SqlDataReader sqlReader = cmdSelectPublisher.ExecuteReader();
            Publisher pub;
            while (sqlReader.Read())
            {
                pub = new Publisher();
                pub.PublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
                pub.PublisherName = sqlReader["PublisherName"].ToString();
                pub.PublisherWeb = sqlReader["WebAddress"].ToString();
                listPublisher.Add(pub);

            }
            return listPublisher;

        }

        //UPDATE
        public static void UpdatePublisherRecord(Publisher pub)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdatePub = new SqlCommand();
            //Parameterized Query
            cmdUpdatePub.CommandText = "UPDATE Publishers " +
                "SET PublisherID='" + pub.PublisherID + "', PublisherName='" + pub.PublisherName + "', WebAddress='" + pub.PublisherWeb + "' " +
                "WHERE PublisherID =" + pub.PublisherID;

            cmdUpdatePub.Connection = connDB;
            cmdUpdatePub.ExecuteNonQuery();
            connDB.Close();
        }

        //SEARCH
        public static Publisher GetPublisher(int pId)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectPubID = new SqlCommand();

            cmdSelectPubID.CommandText = "SELECT * FROM Publishers " +
                                    "WHERE PublisherID = " + pId;
            cmdSelectPubID.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectPubID.ExecuteReader();
            Publisher pub = new Publisher();
            if (sqlReader.Read())
            {
                pub.PublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
                pub.PublisherName = sqlReader["PublisherName"].ToString();
                pub.PublisherWeb = sqlReader["WebAddress"].ToString();
            }
            else
            {
                pub = null;
            }

            return pub;
        }

        public static Publisher GetPublisherRecordName(string pubName)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectPubName = new SqlCommand();

            cmdSelectPubName.CommandText = "SELECT * FROM Publishers " +
                                    "WHERE PublisherName = '" + pubName + "' ";
            cmdSelectPubName.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectPubName.ExecuteReader();
            Publisher pub = new Publisher();
            if (sqlReader.Read())
            {
                pub.PublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
                pub.PublisherName = sqlReader["PublisherName"].ToString();
                pub.PublisherWeb = sqlReader["WebAddress"].ToString();
            }
            else
            {
                pub = null;
            }

            return pub; 
        }

        public static Publisher GetPublisherRecordWeb(string pubWeb)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectPubWeb = new SqlCommand();

            cmdSelectPubWeb.CommandText = "SELECT * FROM Publishers " +
                                    "WHERE WebAddress = '" + pubWeb + "' ";
            cmdSelectPubWeb.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectPubWeb.ExecuteReader();
            Publisher pub = new Publisher();
            if (sqlReader.Read())
            {
                pub.PublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
                pub.PublisherName = sqlReader["PublisherName"].ToString();
                pub.PublisherWeb = sqlReader["WebAddress"].ToString();
            }
            else
            {
                pub = null;
            }

            return pub;
        }
    }
}
