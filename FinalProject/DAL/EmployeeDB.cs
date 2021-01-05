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
    public static class EmployeeDB
    {
        public static void EmpSaveRecord(Employee emp) 
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdNewEmp = new SqlCommand();
            cmdNewEmp.CommandText = "INSERT INTO Employees (EmployeeID,FirstName,LastName,PhoneNumber,Email,JobID)" +
                                    "VALUES (@EmployeeID,@EmployeeFName,@EmployeeLName,@EmployeePhone,@EmployeeEmail,@EmployeeJobID)";

            cmdNewEmp.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
            cmdNewEmp.Parameters.AddWithValue("@EmployeeFName", emp.EmployeeFName);
            cmdNewEmp.Parameters.AddWithValue("@EmployeeLName", emp.EmployeeLName);
            cmdNewEmp.Parameters.AddWithValue("@EmployeePhone", emp.EmployeePhone);
            cmdNewEmp.Parameters.AddWithValue("@EmployeeEmail", emp.EmployeeEmail);
            cmdNewEmp.Parameters.AddWithValue("@EmployeeJobID", emp.EmployeeJobID);
            cmdNewEmp.Connection = connDB;
            cmdNewEmp.ExecuteNonQuery();
            connDB.Close();
        }

        public static Employee EmpGetRecord(int empID)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectEmp = new SqlCommand();

            cmdSelectEmp.CommandText = "SELECT * FROM Employees " +
                                    "WHERE EmployeeID = " + empID;
            cmdSelectEmp.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectEmp.ExecuteReader();
            Employee emp = new Employee();
            if (sqlReader.Read())
            {
                emp.EmployeeID = Convert.ToInt32(sqlReader["EmployeeID"]);
                emp.EmployeeFName = sqlReader["FirstName"].ToString();
                emp.EmployeeLName = sqlReader["LastName"].ToString();
                emp.EmployeePhone = sqlReader["PhoneNumber"].ToString();
                emp.EmployeeEmail = sqlReader["Email"].ToString();
                emp.EmployeeJobID = Convert.ToInt32(sqlReader["JobID"]);
            }
            else
            {
                emp = null;
            }

            return emp;
        }

        public static List<Employee> EmpGetRecordList()
        {
            List<Employee> listEmployee = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectEmp = new SqlCommand("SELECT * FROM Employees", connDB);
            SqlDataReader sqlReader = cmdSelectEmp.ExecuteReader();
            Employee emp;
            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeID = Convert.ToInt32(sqlReader["EmployeeID"]);
                emp.EmployeeFName = sqlReader["FirstName"].ToString();
                emp.EmployeeLName = sqlReader["LastName"].ToString();
                emp.EmployeePhone = sqlReader["PhoneNumber"].ToString();
                emp.EmployeeEmail = sqlReader["Email"].ToString();
                emp.EmployeeJobID = Convert.ToInt32(sqlReader["JobID"]);
                listEmployee.Add(emp);

            }
            return listEmployee;

        }

        public static List<Employee> EmpGetRecordListFirstName(string firstName)
        {
            List<Employee> listEmployee = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectEmp = new SqlCommand("SELECT * FROM Employees WHERE FirstName = '" + firstName + "'", connDB);
            SqlDataReader sqlReader = cmdSelectEmp.ExecuteReader();
            Employee emp;
            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeID = Convert.ToInt32(sqlReader["EmployeeID"]);
                emp.EmployeeFName = sqlReader["FirstName"].ToString();
                emp.EmployeeLName = sqlReader["LastName"].ToString();
                emp.EmployeePhone = sqlReader["PhoneNumber"].ToString();
                emp.EmployeeEmail = sqlReader["Email"].ToString();
                emp.EmployeeJobID = Convert.ToInt32(sqlReader["JobID"]);
                listEmployee.Add(emp);

            }
            return listEmployee;

        }

        public static List<Employee> EmpGetRecordListLastName(string lastName)
        {
            List<Employee> listEmployee = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectEmp = new SqlCommand("SELECT * FROM Employees WHERE LastName = '" + lastName + "'", connDB);
            SqlDataReader sqlReader = cmdSelectEmp.ExecuteReader();
            Employee emp;
            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeID = Convert.ToInt32(sqlReader["EmployeeID"]);
                emp.EmployeeFName = sqlReader["FirstName"].ToString();
                emp.EmployeeLName = sqlReader["LastName"].ToString();
                emp.EmployeePhone = sqlReader["PhoneNumber"].ToString();
                emp.EmployeeEmail = sqlReader["Email"].ToString();
                emp.EmployeeJobID = Convert.ToInt32(sqlReader["JobID"]);
                listEmployee.Add(emp);

            }
            return listEmployee;

        }



        public static void EmpUpdateRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdateEmp = new SqlCommand();
            //Parameterized Query
            cmdUpdateEmp.CommandText = "UPDATE Employees " +
                "SET EmployeeID='" + emp.EmployeeID + "', FirstName='" + emp.EmployeeFName + "' ,LastName='" + emp.EmployeeLName + "', PhoneNumber='" + emp.EmployeePhone + "' ,Email='" + emp.EmployeeEmail + "' ,JobID ='" + emp.EmployeeJobID + "' " +
                "WHERE EmployeeID =" + emp.EmployeeID;

            cmdUpdateEmp.Connection = connDB;
            cmdUpdateEmp.ExecuteNonQuery();
            connDB.Close();
        }

        public static void EmpDeleteRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdDeleteEmp = new SqlCommand();
            //Parameterized Query
            cmdDeleteEmp.CommandText = "DELETE FROM Employees " +
                                    "WHERE EmployeeID = " + emp.EmployeeID;

            cmdDeleteEmp.Connection = connDB;
            cmdDeleteEmp.ExecuteNonQuery();
            connDB.Close();


        }




        


    }

}
