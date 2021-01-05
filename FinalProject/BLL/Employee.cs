using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL;

namespace FinalProject.BLL
{
    public class Employee
    {
        private int empID;
        private string empFName;
        private string empLName;
        private string empPhone;
        private string empEmail;
        private int empJobID;

        public Employee(int empID, string empFName, string empLName, string empPhone, string empEmail, int empJobID)
        {
            this.empID = empID;
            this.empFName = empFName;
            this.empLName = empLName;
            this.empPhone = empPhone;
            this.empEmail = empEmail;
            this.empJobID = empJobID;

        }

        public Employee()
        {
        }

        public int EmployeeID { get => empID; set => empID = value; }
        public string EmployeeFName { get => empFName; set => empFName = value; }
        public string EmployeeLName { get => empLName; set => empLName = value; }
        public string EmployeePhone { get => empPhone; set => empPhone = value; }
        public string EmployeeEmail { get => empEmail; set => empEmail = value; }
        public int EmployeeJobID { get => empJobID; set => empJobID = value; }

        public void EmpSaveEmployee(Employee emp) 
        {
            EmployeeDB.EmpSaveRecord(emp);
        }

        public List<Employee> GetEmployeeList()
        {
            return EmployeeDB.EmpGetRecordList();
        }
        
       
        public Employee GetEmployee(int eId)
        {
            return EmployeeDB.EmpGetRecord(eId);
        }
       
        public List<Employee> GetEmployeeList(string name)
        {
            return EmployeeDB.EmpGetRecordList();
        }

        public void UpdateEmployee(Employee emp)
        {
            EmployeeDB.EmpUpdateRecord(emp);
        }

        public void DeleteEmployee(Employee emp)
        {
            EmployeeDB.EmpDeleteRecord(emp);
        }


        public List<Employee> GetEmployeeListF(string firstName)
        {
            return EmployeeDB.EmpGetRecordListFirstName(firstName);
        }

        public List<Employee> GetEmployeeListL(string lastName)
        {
            return EmployeeDB.EmpGetRecordListLastName(lastName);
        }
    }
}
