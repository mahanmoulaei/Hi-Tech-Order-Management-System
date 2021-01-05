using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL;

namespace FinalProject.BLL
{
    public class Book
    {
        private Int64 bukISBN;
        private string bukTitle;
        private int bukQOH;
        private float bukUnitPrice;
        private int bukCategoryID;
        private int bukPublisherID;
        public Book(Int64 bukISBN, string bukTitle, int bukQOH, float bukUnitPrice, int bukCategoryID, int bukPublisherID)
        {
            this.bukISBN = bukISBN;
            this.bukTitle = bukTitle;
            this.bukQOH = bukQOH;
            this.bukUnitPrice = bukUnitPrice;
            this.bukCategoryID = bukCategoryID;
            this.bukPublisherID = bukPublisherID;

        }

        public Book()
        {
        }

        public Int64 BookISBN { get => bukISBN; set => bukISBN = value; }
        public string BookTitle { get => bukTitle; set => bukTitle = value; }
        public int BookQOH { get => bukQOH; set => bukQOH = value; }
        public float BookPrice { get => bukUnitPrice; set => bukUnitPrice = value; }
        public int BookCategoryID { get => bukCategoryID; set => bukCategoryID = value; }
        public int BookPublisherID { get => bukPublisherID; set => bukPublisherID = value; }

        //SAVE
        public void SaveBook(Book buk)
        {
            BookDB.SaveBookRecord(buk);
        }

        //DELETE
        public void DeleteBook(Book buk)
        {
            BookDB.DeleteBookRecord(buk);
        }

        //LIST
        public List<Book> GetBookList()
        {
            return BookDB.GetBookList();
        }

        public List<Book> GetBookList(string name)
        {
            return BookDB.GetBookList();
        }

        //UPDATE
        public void UpdateBook(Book buk)
        {
            BookDB.UpdateBookRecord(buk);
        }

        //SEARCH
        public Book GetBookRecord(long bukISBN)
        {
            return BookDB.GetBookRecord(bukISBN);
        }

        public List<Book> GetBookTitleRecordList(string bookTitle)
        {
            return BookDB.GetBookTitleRecordList(bookTitle);
        }

        public List<Book> GetBookCategoryIDRecordList(int categoryID)
        {
            return BookDB.GetBookCategoryIDRecordList(categoryID); 
        }

        public List<Book> GetBookPublisherIDRecordList(int publisherID)
        {
            return BookDB.GetBookPublisherIDRecordList(publisherID); 
        }

        //SEARCH BY QUANTITY
        public Book GetBookQuantity(long bukISBN)
        {
            return BookDB.GetBookQuantity(bukISBN);
        }
    }
}
