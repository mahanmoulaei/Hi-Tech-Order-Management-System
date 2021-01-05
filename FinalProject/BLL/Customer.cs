using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL;
using System.Data;

namespace FinalProject.BLL
{
    public class Customer
    {
        private int customerID;
        private string customerName;
        private string streetName;
        private string province;
        private string city;
        private string postalCode;
        private string contactName;
        private string contactEmail;
        private int creditLimit;

        public Customer(int customerID, string customerName, string streetName, string province, string city, string postalCode, string contactName, string contactEmail, int creditLimit)
        {
            this.customerID = customerID;
            this.customerName = customerName;
            this.streetName = streetName;
            this.province = province;
            this.city = city;
            this.postalCode = postalCode;
            this.contactName = contactName;
            this.contactEmail = contactEmail;
            this.creditLimit = creditLimit;

        }

        public Customer()
        {
        }

        public int CustomerID { get => customerID; set => customerID = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string CustomerStreet { get => streetName; set => streetName = value; }
        public string CustomerProvince { get => province; set => province = value; }
        public string CustomerCity { get => city; set => city = value; }
        public string CustomerPostal { get => postalCode; set => postalCode = value; }
        public string CustomerContactName { get => contactName; set => contactName = value; }
        public string CustomerContactEmail { get => contactEmail; set => contactEmail = value; }
        public int CustomerCreditLimit { get => creditLimit; set => creditLimit = value; }



        public List<Customer> GetCustomerList()
        {
            return CustomerDB.CustomerGetRecordList();
        }



    }
}
