using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BLL;
using FinalProject.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace FinalProject.DAL
{
    public static class CustomerDB
    {
            public static SqlDataAdapter SDA = new SqlDataAdapter();
            public static SqlConnection connDB = UtilityDB.ConnectDB();

            public static List<Customer> CustomerGetRecordList()
            {
                List<Customer> listCustomer = new List<Customer>();
                using (SqlConnection connDB = UtilityDB.ConnectDB())
                //SqlConnection connDB = UtilityDB.ConnectDB();
                {
                    Customer cus;
                    SqlCommand cmdSelectCust = new SqlCommand("SELECT * FROM Customers", connDB);
                    SqlDataReader sqlReader = cmdSelectCust.ExecuteReader();
                    
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {

                            cus = new Customer();
                            cus.CustomerID = Convert.ToInt32(sqlReader["CustomerID"]);
                            cus.CustomerName = sqlReader["CustomerName"].ToString();
                            cus.CustomerStreet = sqlReader["StreetName"].ToString();
                            cus.CustomerProvince = sqlReader["Province"].ToString();
                            cus.CustomerCity = sqlReader["City"].ToString();
                            cus.CustomerPostal = sqlReader["PostalCode"].ToString();
                            cus.CustomerContactName = sqlReader["ContactName"].ToString();
                            cus.CustomerContactEmail = sqlReader["ContactEmail"].ToString();
                            cus.CustomerCreditLimit = Convert.ToInt32(sqlReader["CreditLimit"]);
                            listCustomer.Add(cus);

                        }
                    }
                    else
                    {
                        listCustomer = null;
                    }
                    
                }
                return listCustomer;
        }

    }
}
