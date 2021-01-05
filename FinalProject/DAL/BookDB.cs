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
    public static class BookDB
    {
        //SAVE
        public static void SaveBookRecord(Book buk)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdNewBuk = new SqlCommand();
            cmdNewBuk.CommandText = "INSERT INTO Books (ISBN,BookTitle,QOH,UnitPrice,CategoryID,PublisherID)" +
                                    "VALUES (@BookISBN,@BookTitle,@BookQOH,@BookPrice,@BookCategoryID,@BookPublisherID)";

            cmdNewBuk.Parameters.AddWithValue("@BookISBN", buk.BookISBN);
            cmdNewBuk.Parameters.AddWithValue("@BookTitle", buk.BookTitle);
            cmdNewBuk.Parameters.AddWithValue("@BookQOH", buk.BookQOH);
            cmdNewBuk.Parameters.AddWithValue("@BookPrice", buk.BookPrice);
            cmdNewBuk.Parameters.AddWithValue("@BookCategoryID", buk.BookCategoryID);
            cmdNewBuk.Parameters.AddWithValue("@BookPublisherID", buk.BookPublisherID);
            cmdNewBuk.Connection = connDB;
            cmdNewBuk.ExecuteNonQuery();
            connDB.Close();
        }

        //DELETE
        public static void DeleteBookRecord(Book buk)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdDeleteBuk = new SqlCommand();
            //Parameterized Query
            cmdDeleteBuk.CommandText = "DELETE FROM Books " +
                                    "WHERE ISBN = " + buk.BookISBN;

            cmdDeleteBuk.Connection = connDB;
            cmdDeleteBuk.ExecuteNonQuery();
            connDB.Close();


        }

        //LIST
        public static List<Book> GetBookList()
        {
            List<Book> listBook = new List<Book>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectBook = new SqlCommand("SELECT * FROM Books", connDB);
            SqlDataReader sqlReader = cmdSelectBook.ExecuteReader();
            Book buk;
            while (sqlReader.Read())
            {
                buk = new Book();
                buk.BookISBN = Convert.ToInt64(sqlReader["ISBN"]);
                buk.BookTitle = sqlReader["BookTitle"].ToString();
                buk.BookQOH = Convert.ToInt32(sqlReader["QOH"]);
                buk.BookPrice = (float)Convert.ToDecimal(sqlReader["UnitPrice"]);
                buk.BookCategoryID = Convert.ToInt32(sqlReader["CategoryID"]);
                buk.BookPublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
                listBook.Add(buk);

            }
            return listBook;

        }

        //UPDATE
        public static void UpdateBookRecord(Book Buk)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdateBuk = new SqlCommand();
            //Parameterized Query
            cmdUpdateBuk.CommandText = "UPDATE Books " +
                "SET ISBN='" + Buk.BookISBN + "', BookTitle='" + Buk.BookTitle + "', QOH='" + Buk.BookQOH + "', UnitPrice='" + Buk.BookPrice + "', CategoryID='" + Buk.BookCategoryID + "', PublisherID ='" + Buk.BookPublisherID + "' " +
                "WHERE ISBN =" + Buk.BookISBN;

            cmdUpdateBuk.Connection = connDB;
            cmdUpdateBuk.ExecuteNonQuery();
            connDB.Close();
        }

        

        //SEARCH
        public static Book GetBookRecord(long bukISBN)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectBuk = new SqlCommand();

            cmdSelectBuk.CommandText = "SELECT * FROM Books " +
                                    "WHERE ISBN = " + bukISBN;
            cmdSelectBuk.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectBuk.ExecuteReader();
            Book buk = new Book();
            if (sqlReader.Read())
            {
                buk.BookISBN = Convert.ToInt64(sqlReader["ISBN"]);
                buk.BookTitle = sqlReader["BookTitle"].ToString();
                buk.BookQOH = Convert.ToInt32(sqlReader["QOH"]);
                buk.BookPrice = (float)Convert.ToDecimal(sqlReader["UnitPrice"]);
                buk.BookCategoryID = Convert.ToInt32(sqlReader["CategoryID"]);
                buk.BookPublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
            }
            else
            {
                buk = null;
            }

            return buk;
        }

        //SEARCH BY QUANTITY
        public static Book GetBookQuantity(long bukISBN)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectBuk = new SqlCommand();

            cmdSelectBuk.CommandText = "SELECT QOH FROM Books " +
                                    "WHERE ISBN = " + bukISBN;
            cmdSelectBuk.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectBuk.ExecuteReader();
            Book buk = new Book();
            if (sqlReader.Read())
            {
                //buk.BookISBN = Convert.ToInt64(sqlReader["ISBN"]);
                buk.BookQOH = Convert.ToInt32(sqlReader["QOH"]);

            }
            else
            {
                buk = null;
            }

            return buk;
        }


        public static List<Book> GetBookTitleRecordList(string bookTitle)
        {
            List<Book> listBook = new List<Book>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectBook = new SqlCommand("SELECT * FROM Books WHERE BookTitle = '" + bookTitle + "'", connDB);
            SqlDataReader sqlReader = cmdSelectBook.ExecuteReader();
            Book buk;
            while (sqlReader.Read())
            {
                buk = new Book();
                buk.BookISBN = Convert.ToInt64(sqlReader["ISBN"]);
                buk.BookTitle = sqlReader["BookTitle"].ToString();
                buk.BookQOH = Convert.ToInt32(sqlReader["QOH"]);
                buk.BookPrice = (float)Convert.ToDecimal(sqlReader["UnitPrice"]);
                buk.BookCategoryID = Convert.ToInt32(sqlReader["CategoryID"]);
                buk.BookPublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
                listBook.Add(buk);

            }
            return listBook;

        }

        public static List<Book> GetBookCategoryIDRecordList(int categoryID)
        {
            List<Book> listBook = new List<Book>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectBook = new SqlCommand("SELECT * FROM Books WHERE CategoryID = '" + categoryID + "'", connDB);
            SqlDataReader sqlReader = cmdSelectBook.ExecuteReader();
            Book buk;
            while (sqlReader.Read())
            {
                buk = new Book();
                buk.BookISBN = Convert.ToInt64(sqlReader["ISBN"]);
                buk.BookTitle = sqlReader["BookTitle"].ToString();
                buk.BookQOH = Convert.ToInt32(sqlReader["QOH"]);
                buk.BookPrice = (float)Convert.ToDecimal(sqlReader["UnitPrice"]);
                buk.BookCategoryID = Convert.ToInt32(sqlReader["CategoryID"]);
                buk.BookPublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
                listBook.Add(buk);

            }
            return listBook;

        }


        public static List<Book> GetBookPublisherIDRecordList(int publisherID)
        {
            List<Book> listBook = new List<Book>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectBook = new SqlCommand("SELECT * FROM Books WHERE PublisherID = '" + publisherID + "'", connDB);
            SqlDataReader sqlReader = cmdSelectBook.ExecuteReader();
            Book buk;
            while (sqlReader.Read())
            {
                buk = new Book();
                buk.BookISBN = Convert.ToInt64(sqlReader["ISBN"]);
                buk.BookTitle = sqlReader["BookTitle"].ToString();
                buk.BookQOH = Convert.ToInt32(sqlReader["QOH"]);
                buk.BookPrice = (float)Convert.ToDecimal(sqlReader["UnitPrice"]);
                buk.BookCategoryID = Convert.ToInt32(sqlReader["CategoryID"]);
                buk.BookPublisherID = Convert.ToInt32(sqlReader["PublisherID"]);
                listBook.Add(buk);

            }
            return listBook;

        }


    }
}
