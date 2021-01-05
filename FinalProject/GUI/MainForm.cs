using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FinalProject.BLL;
using FinalProject.VALIDATION;
using FinalProject.DAL;
using System.Data.SqlClient;
using System.Xml;
//using EnitityFramework.edmx;

namespace FinalProject.GUI
{
    /// <summary>
    /// Hi-Tech Company Management System
    /// 
    /// Fall 2020
    /// 
    /// LaSalle College
    /// 
    /// Programmed and Developed By :
    ///       Mahan Moulaei
    ///       
    /// </summary>
    public partial class MainForm : Form
    {
        
        static SqlDataAdapter SDA;
        static DataSet dsCustomer;
        static DataTable dtCustomer;
        //SqlCommandBuilder sqlBuilder;
        Customer cus = new Customer();
        //static DataSet dsCustomer = new DataSet("CustomerDB");
        //static DataTable dtCustomer = new DataTable("Customers");


        public MainForm()
        {
            InitializeComponent();
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }

        private void MainPageExit_Click(object sender, EventArgs e)
        {
            //MainForm.Exit();
            if (MessageBox.Show("Do You Want To Exit The Application?", "Quit The App", MessageBoxButtons.YesNo).ToString() == "Yes")
            {
                Application.Exit();
            }
        }






        private void buttonTest_Click(object sender, EventArgs e)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            MessageBox.Show(connDB.State.ToString(), "Database Connection");
        }


        ////////////////////////////
        ///   Employee Section   ///
        ////////////////////////////
        private void EmpListButton_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            List<Employee> listEmp = new List<Employee>();
            listEmp = emp.GetEmployeeList();
            listViewEmployee.Items.Clear();
            if (listEmp != null)
            {
                foreach (Employee anEmp in listEmp)
                {
                    ListViewItem empItem = new ListViewItem(anEmp.EmployeeID.ToString());
                    empItem.SubItems.Add(anEmp.EmployeeFName);
                    empItem.SubItems.Add(anEmp.EmployeeLName);
                    empItem.SubItems.Add(anEmp.EmployeePhone);
                    empItem.SubItems.Add(anEmp.EmployeeEmail);
                    empItem.SubItems.Add(anEmp.EmployeeJobID.ToString());
                    listViewEmployee.Items.Add(empItem);
                }

            }
            else
            {

            }
        }

        private void EmpSaveButton_Click(object sender, EventArgs e)
        {
            //Check If The Enetered Employee ID is 3 Digit
            string tempId = textBoxEmployeeID.Text.Trim();
            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid Employee ID!", "Error");
                textBoxEmployeeID.Clear();
                textBoxEmployeeID.Focus();
                return;
            }
            //Check Duplicate Employee ID
            Employee empExist = new Employee();
            empExist = empExist.GetEmployee(Convert.ToInt32(tempId));
            if (empExist != null)
            {
                MessageBox.Show("This Employee ID Already Exists!", "Duplicate Employee ID");
                textBoxEmployeeID.Clear();
                textBoxEmployeeID.Focus();
                return;
            }

            //Check Valid First Name
            string tempFname = textBoxEmpFirstName.Text.Trim();
            if (!Validator.IsValidName(tempFname))
            {
                MessageBox.Show("Invalid First Name!", "Error");
                textBoxEmpFirstName.Clear();
                textBoxEmpFirstName.Focus();
                return;
            }
            //Check Valid Last Name
            string tempLname = textBoxEmpLastName.Text.Trim();
            if (!Validator.IsValidName(tempLname))
            {
                MessageBox.Show("Invalid Last Name!", "Error");
                textBoxEmpLastName.Clear();
                textBoxEmpLastName.Focus();
                return;
            }

           
            string tempPhone2 = textBoxEmpPhone.Text.Trim();
            if (Validator.IsEmpty(tempPhone2))
            {
                MessageBox.Show("Phone Number Cannot Be Empty!", "Error");
                textBoxEmpPhone.Clear();
                textBoxEmpPhone.Focus();
                return;
            }

            //Check If Email Address is not Empty
            string tempEmail = textBoxEmpEmail.Text.Trim();
            if (Validator.IsEmpty(tempEmail))
            {
                MessageBox.Show("Email Address Cannot Be Empty!", "Error");
                textBoxEmpEmail.Clear();
                textBoxEmpEmail.Focus();
                return;
            }

            if (!Validator.IsValidEmail(textBoxEmpEmail.Text, "E-mail"))
            {
                //MessageBox.Show("Invalid Contact Email Address!", "Error");
                textBoxEmpEmail.Clear();
                textBoxEmpEmail.Focus();
                return;
            }

            //Check If The Enetered Job ID is 1 Digit
            string tempJob = textBoxEmpJobID.Text.Trim();
            if (!Validator.IsValidId(tempJob, 1))
            {
                MessageBox.Show("Invalid Job ID!", "Error");
                //textBoxEmpJobID.Clear();
                textBoxEmpJobID.Focus();
                return;
            }
            //Check If Job ID Exist
            Job jobExist = new Job();
            jobExist = jobExist.GetJob(Convert.ToInt32(tempJob));
            if (jobExist == null)
            {
                MessageBox.Show("This Job ID Does Not Exist!", "Invalid Job ID");
                //textBoxEmpJobID.Clear();
                textBoxEmpJobID.Focus();
                return;
            }
            //Check If Job ID is not Empty
            string tempJobID = textBoxEmpJobID.Text.Trim();
            if (Validator.IsEmpty(tempJobID))
            {
                MessageBox.Show("Job ID Cannot Be Empty!", "Error");
                //textBoxEmpJobID.Clear();
                textBoxEmpJobID.Focus();
                return;
            }

            Employee emp = new Employee();
            //Valid data
            emp.EmployeeID = Convert.ToInt32(textBoxEmployeeID.Text);
            emp.EmployeeFName = textBoxEmpFirstName.Text.Trim();
            emp.EmployeeLName = textBoxEmpLastName.Text.Trim();
            emp.EmployeePhone = textBoxEmpPhone.Text.Trim();
            emp.EmployeeEmail = textBoxEmpEmail.Text.Trim();
            emp.EmployeeJobID = Convert.ToInt32(textBoxEmpJobID.Text);
            emp.EmpSaveEmployee(emp);
            textBoxEmployeeID.Clear();
            textBoxEmpFirstName.Clear();
            textBoxEmpLastName.Clear();
            textBoxEmpPhone.Clear();
            textBoxEmpEmail.Clear();
            //textBoxEmpJobID.Clear();
            MessageBox.Show("Employee Info SAVED Successfully!", "Confirmation");

        }

        private void EmpDeleteButton_Click(object sender, EventArgs e)
        {
            string tempId = textBoxEmployeeID.Text.Trim();
            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid Employee ID", "Error");
                textBoxEmployeeID.Clear();
                textBoxEmployeeID.Focus();
                return;
            }

            //Check If User ID Exist
            User usrExist = new User();
            usrExist = usrExist.GetUser(Convert.ToInt32(tempId));
            if (usrExist != null)
            {
                MessageBox.Show("Because The User ID Of This Employee Exists, You Cannot Delete This Employee Info!", "User ID Of This Employee Exists");
                textBoxEmployeeID.Clear();
                textBoxEmployeeID.Focus();
                return;
            }

            Employee emp = new Employee();
            if (emp != null)
            {
                emp.EmployeeID = Convert.ToInt32(textBoxEmployeeID.Text);
                emp.EmployeeFName = textBoxEmpFirstName.Text.Trim();
                emp.EmployeeLName = textBoxEmpLastName.Text.Trim();
                emp.EmployeePhone = textBoxEmpPhone.Text.Trim();
                emp.EmployeeEmail = textBoxEmpEmail.Text.Trim();
                //emp.EmployeeJobID = Convert.ToInt32(textBoxEmpJobID.Text);

            }
            else
            {
                MessageBox.Show("Employee Not Found!", "Invalid Employee ID");
                textBoxEmpSearch.Clear();
                textBoxEmpSearch.Focus();
            }
            emp.DeleteEmployee(emp);
            textBoxEmployeeID.Clear();
            textBoxEmpFirstName.Clear();
            textBoxEmpLastName.Clear();
            textBoxEmpPhone.Clear();
            textBoxEmpEmail.Clear();
            textBoxEmpJobID.SelectedIndex = -1;
            MessageBox.Show("Employee Info DELETED Successfully!", "Confirmation");

        }

        private void ClearEmpButton_Click(object sender, EventArgs e)
        {
            textBoxEmployeeID.ReadOnly = false;
            textBoxEmployeeID.Clear();
            textBoxEmpFirstName.Clear();
            textBoxEmpLastName.Clear();
            textBoxEmpPhone.Clear();
            textBoxEmpEmail.Clear();
            textBoxEmpJobID.SelectedIndex = -1;
            textBoxEmpSearch.Clear();
        }

        private void EmpUpdateButton_Click(object sender, EventArgs e)
        {


            //Check Valid First Name
            string tempFname = textBoxEmpFirstName.Text.Trim();
            if (!Validator.IsValidName(tempFname))
            {
                MessageBox.Show("Invalid First Name!", "Error");
                textBoxEmpFirstName.Clear();
                textBoxEmpFirstName.Focus();
                return;
            }
            //Check Valid Last Name
            string tempLname = textBoxEmpLastName.Text.Trim();
            if (!Validator.IsValidName(tempLname))
            {
                MessageBox.Show("Invalid Last Name!", "Error");
                textBoxEmpLastName.Clear();
                textBoxEmpLastName.Focus();
                return;
            }

           
            string tempPhone2 = textBoxEmpPhone.Text.Trim();
            if (Validator.IsEmpty(tempPhone2))
            {
                MessageBox.Show("Phone Number Cannot Be Empty!", "Error");
                textBoxEmpPhone.Clear();
                textBoxEmpPhone.Focus();
                return;
            }

            //Check If Email Address is not Empty
            string tempEmail = textBoxEmpEmail.Text.Trim();
            if (Validator.IsEmpty(tempEmail))
            {
                MessageBox.Show("Email Address Cannot Be Empty!", "Error");
                textBoxEmpEmail.Clear();
                textBoxEmpEmail.Focus();
                return;
            }

            //Check If The Enetered Job ID is 1 Digit
            string tempJob = textBoxEmpJobID.Text.Trim();
            if (!Validator.IsValidId(tempJob, 1))
            {
                MessageBox.Show("Invalid Job ID!", "Error");
                //textBoxEmpJobID.Clear();
                textBoxEmpJobID.Focus();
                return;
            }
            //Check If Job ID Exist
            Job jobExist = new Job();
            jobExist = jobExist.GetJob(Convert.ToInt32(tempJob));
            if (jobExist == null)
            {
                MessageBox.Show("This Job ID Does Not Exist!", "Invalid Job ID");
                //textBoxEmpJobID.Clear();
                textBoxEmpJobID.Focus();
                return;
            }
            //Check If Job ID is not Empty
            string tempJobID = textBoxEmpJobID.Text.Trim();
            if (Validator.IsEmpty(tempJobID))
            {
                MessageBox.Show("Job ID Cannot Be Empty!", "Error");
                //textBoxEmpJobID.Clear();
                textBoxEmpJobID.Focus();
                return;
            }

            Employee emp = new Employee();
            if (emp != null)
            {
                emp.EmployeeID = Convert.ToInt32(textBoxEmployeeID.Text);
                emp.EmployeeFName = textBoxEmpFirstName.Text.Trim();
                emp.EmployeeLName = textBoxEmpLastName.Text.Trim();
                emp.EmployeePhone = textBoxEmpPhone.Text.Trim();
                emp.EmployeeEmail = textBoxEmpEmail.Text.Trim();
                emp.EmployeeJobID = Convert.ToInt32(textBoxEmpJobID.Text);

            }
            else
            {
                MessageBox.Show("Employee Not Found !", "Invalid Employee ID");
                textBoxEmpSearch.Clear();
                textBoxEmpSearch.Focus();
            }
            //emp = emp.GetEmployee(Convert.ToInt32(textBoxInput.Text.Trim()));
            emp.UpdateEmployee(emp);
            MessageBox.Show("Employee Info UPDATED Successfully", "Confirmation");
        }

        private void EmpSearchButton_Click(object sender, EventArgs e)
        {

            
            listViewEmployee.Items.Clear();
            int indexSelected = EmpComboBoxOption.SelectedIndex;
            switch (indexSelected)
            {
                case 0:
                    // perform search operation by Employee ID
                    //data validation
                    //validate the input data, not valid, enter again
                    textBoxEmployeeID.ReadOnly = true;
                    string tempId = textBoxEmpSearch.Text.Trim();
                    if (!Validator.IsValidId(tempId, 3))
                    {
                        MessageBox.Show("Invalid Employee ID!", "Error");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                        return;
                    }
                    Employee emp = new Employee();
                    emp = emp.GetEmployee(Convert.ToInt32(textBoxEmpSearch.Text.Trim()));
                    if (emp != null)
                    {
                        textBoxEmployeeID.Text = emp.EmployeeID.ToString();
                        textBoxEmpFirstName.Text = emp.EmployeeFName;
                        textBoxEmpLastName.Text = emp.EmployeeLName;
                        textBoxEmpPhone.Text = emp.EmployeePhone;
                        textBoxEmpEmail.Text = emp.EmployeeEmail;
                        textBoxEmpJobID.Text = emp.EmployeeJobID.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Employee Not Found!", "Invalid Employee ID");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                    }
                    break;

                case 1:
                    //Search by First Name
                    string tempFname = textBoxEmpSearch.Text.Trim();
                    if (Validator.IsEmpty(tempFname))
                    {
                        MessageBox.Show("Search Input Cannot Be Empty!", "Error");
                        textBoxPublisherID.Clear();
                        textBoxPublisherID.Focus();
                        return;
                    }
                    if (!Validator.IsValidName(tempFname))
                    {
                        MessageBox.Show("Invalid First Name", "Error");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                        return;
                    }

                    Employee empF = new Employee();
                    List<Employee> listEmp = new List<Employee>();
                    listEmp = empF.GetEmployeeListF(tempFname);
                    listViewEmployee.Items.Clear();
                    if (listEmp != null)
                    {
                        foreach (Employee anEmp in listEmp)
                        {
                            ListViewItem empItemF = new ListViewItem(anEmp.EmployeeID.ToString());
                            empItemF.SubItems.Add(anEmp.EmployeeFName);
                            empItemF.SubItems.Add(anEmp.EmployeeLName);
                            empItemF.SubItems.Add(anEmp.EmployeePhone);
                            empItemF.SubItems.Add(anEmp.EmployeeEmail);
                            empItemF.SubItems.Add(anEmp.EmployeeJobID.ToString());
                            listViewEmployee.Items.Add(empItemF);
                        }
                        if (listViewEmployee.Items.Count == 0)
                        {
                            MessageBox.Show("Employee Not Found!", "Invalid Employee First Name");
                            textBoxEmpSearch.Clear();
                            textBoxEmpSearch.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Employee Not Found!", "Invalid Employee First Name");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                    }
                    break;

                case 2:
                    //Search by Last Name
                    string tempLname = textBoxEmpSearch.Text.Trim();
                    if (Validator.IsEmpty(tempLname))
                    {
                        MessageBox.Show("Search Input Cannot Be Empty!", "Error");
                        textBoxPublisherID.Clear();
                        textBoxPublisherID.Focus();
                        return;
                    }
                    if (!Validator.IsValidName(tempLname))
                    {
                        MessageBox.Show("Invalid First Name", "Error");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                        return;
                    }

                    Employee empL = new Employee();
                    List<Employee> listEmpL = new List<Employee>();
                    listEmpL = empL.GetEmployeeListL(tempLname);
                    listViewEmployee.Items.Clear();
                    if (listEmpL != null)
                    {
                        foreach (Employee anEmpL in listEmpL)
                        {
                            ListViewItem empItemL = new ListViewItem(anEmpL.EmployeeID.ToString());
                            empItemL.SubItems.Add(anEmpL.EmployeeFName);
                            empItemL.SubItems.Add(anEmpL.EmployeeLName);
                            empItemL.SubItems.Add(anEmpL.EmployeePhone);
                            empItemL.SubItems.Add(anEmpL.EmployeeEmail);
                            empItemL.SubItems.Add(anEmpL.EmployeeJobID.ToString());
                            listViewEmployee.Items.Add(empItemL);
                        }
                        if (listViewEmployee.Items.Count == 0)
                        {
                            MessageBox.Show("Employee Not Found!", "Invalid Employee Last Name");
                            textBoxEmpSearch.Clear();
                            textBoxEmpSearch.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Employee Not Found!", "Invalid Employee First Name");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                    }
                    break;
            }
        }


        ////////////////////////
        ///   User Section   ///
        ////////////////////////
        private void UserListButton_Click(object sender, EventArgs e)
        {
            User usr = new User();
            List<User> listUser = new List<User>();
            listUser = usr.GetUserList();
            listViewUser.Items.Clear();
            if (listUser != null)
            {
                foreach (User anUsr in listUser)
                {
                    ListViewItem usrItem = new ListViewItem(anUsr.UserID.ToString());
                    usrItem.SubItems.Add(anUsr.UserPassword);
                    usrItem.SubItems.Add(anUsr.UserEmployeeID.ToString());
                    usrItem.SubItems.Add(anUsr.UserComment);
                    listViewUser.Items.Add(usrItem);
                }

            }
            else
            {

            }
        }

        private void UserSaveButton_Click(object sender, EventArgs e)
        {
            //Check If The Enetered User ID is 3 Digit
            string tempId = textBoxUserID.Text.Trim();
            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid User ID!", "Error");
                textBoxUserID.Clear();
                textBoxUserID.Focus();
                return;
            }

            //Check Duplicate User ID
            User usrExist = new User();
            usrExist = usrExist.GetUser(Convert.ToInt32(tempId));
            if (usrExist != null)
            {
                MessageBox.Show("This User ID Already Exists!", "Duplicate User ID");
                textBoxUserID.Clear();
                textBoxUserID.Focus();
                return;
            }

            //Check If Password is not Empty
            string tempPass = textBoxUserPass.Text.Trim();
            if (Validator.IsEmpty(tempPass))
            {
                MessageBox.Show("Password Cannot Be Empty!", "Error");
                textBoxUserPass.Clear();
                textBoxUserPass.Focus();
                return;
            }

            //Check If The Enetered Employee ID is 3 Digit
            string tempEmpId = textBoxUserEmpID.Text.Trim();
            if (!Validator.IsValidId(tempEmpId, 3))
            {
                MessageBox.Show("Invalid Employee ID!", "Error");
                textBoxUserEmpID.Clear();
                textBoxUserEmpID.Focus();
                return;
            }

            //Check If Employee ID Exist
            Employee empIdExist = new Employee();
            empIdExist = empIdExist.GetEmployee(Convert.ToInt32(tempEmpId));
            if (empIdExist == null)
            {
                MessageBox.Show("This Employee ID Does Not Exist!", "Invalid Employee ID");
                textBoxUserEmpID.Clear();
                textBoxUserEmpID.Focus();
                return;
            }
            if (textBoxUserEmpID.Text != textBoxUserID.Text)
            {
                MessageBox.Show("The User ID and The Employee ID MUST MATCH!", "Invalid Entry");
                textBoxUserID.Clear();
                textBoxUserID.Focus();
                return;
            }
            else
            {
                User usr = new User();
                //Valid data
                usr.UserID = Convert.ToInt32(textBoxUserID.Text);
                usr.UserPassword = textBoxUserPass.Text.Trim();
                usr.UserEmployeeID = Convert.ToInt32(textBoxUserEmpID.Text);
                usr.UserComment = textBoxUserComment.Text;
                usr.SaveUser(usr);
                textBoxUserID.Clear();
                textBoxUserPass.Clear();
                textBoxUserEmpID.Clear();
                textBoxUserComment.Clear();
                MessageBox.Show("User Info SAVED Successfully!", "Confirmation");
            }


        }

        private void UserDeleteButton_Click(object sender, EventArgs e)
        {
            string tempId = textBoxUserID.Text.Trim();
            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid User ID", "Error");
                textBoxEmployeeID.Clear();
                textBoxEmployeeID.Focus();
                return;
            }

            User usr = new User();
            if (usr != null)
            {
                usr.UserID = Convert.ToInt32(textBoxUserID.Text);
                usr.UserPassword = textBoxUserPass.Text.Trim();
                usr.UserEmployeeID = Convert.ToInt32(textBoxUserEmpID.Text);
                usr.UserComment = textBoxUserComment.Text;

            }
            else
            {
                MessageBox.Show("User Not Found!", "Invalid User ID");
                textBoxUserSearch.Clear();
                textBoxUserSearch.Focus();
            }
            usr.DeleteUser(usr);
            textBoxUserID.Clear();
            textBoxUserPass.Clear();
            textBoxUserEmpID.Clear();
            textBoxUserComment.Clear();
            MessageBox.Show("User Info DELETED Successfully!", "Confirmation");
        }

        private void UserSearchButton_Click(object sender, EventArgs e)
        {
            
            listViewUser.Items.Clear();
            int indexSelected = UserComboBoxOption.SelectedIndex;
            switch (indexSelected)
            {
                case 0:
                    //Search by User/Employee ID
                    textBoxUserID.ReadOnly = true;
                    textBoxUserEmpID.ReadOnly = true;
                    string tempId = textBoxUserSearch.Text.Trim();
                    if (!Validator.IsValidId(tempId, 3))
                    {
                        MessageBox.Show("Invalid User ID!", "Error");
                        textBoxUserSearch.Clear();
                        textBoxUserSearch.Focus();
                        return;
                    }

                    User usr = new User();
                    usr = usr.GetUser(Convert.ToInt32(textBoxUserSearch.Text.Trim()));
                    if (usr != null)
                    {
                        textBoxUserID.Text = usr.UserID.ToString();
                        textBoxUserPass.Text = usr.UserPassword;
                        textBoxUserEmpID.Text = usr.UserEmployeeID.ToString();
                        textBoxUserComment.Text = usr.UserComment;
                    }
                    else
                    {
                        MessageBox.Show("User Not Found!", "Invalid User ID");
                        textBoxUserSearch.Clear();
                        textBoxUserSearch.Focus();
                    }
                    break;

                case 1:
                    string tempPass = textBoxUserSearch.Text.Trim();
                    if (Validator.IsEmpty(tempPass))
                    {
                        MessageBox.Show("Search Input Cannot Be Empty!", "Error");
                        textBoxPublisherID.Clear();
                        textBoxPublisherID.Focus();
                        return;
                    }
                    User empP = new User();
                    List<User> listUser = new List<User>();
                    listUser = empP.GetUserListPass(tempPass);
                    listViewUser.Items.Clear();
                    if (listUser != null)
                    {
                        foreach (User anUser in listUser)
                        {
                            ListViewItem usrItemP = new ListViewItem(anUser.UserID.ToString());
                            usrItemP.SubItems.Add(anUser.UserPassword);
                            usrItemP.SubItems.Add(anUser.UserEmployeeID.ToString());
                            usrItemP.SubItems.Add(anUser.UserComment);
                            listViewUser.Items.Add(usrItemP);
                        }
                        if (listViewUser.Items.Count == 0)
                        {
                            MessageBox.Show("Password Not Found!", "Invalid User Password");
                            textBoxUserSearch.Clear();
                            textBoxUserSearch.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Password Not Found!", "Invalid User Password");
                        textBoxUserSearch.Clear();
                        textBoxUserSearch.Focus();
                    }
                    break;



            }

        }

        private void ClearUserButton_Click(object sender, EventArgs e)
        {
            textBoxUserID.ReadOnly = false;
            textBoxUserEmpID.ReadOnly = false;
            textBoxUserID.Clear();
            textBoxUserPass.Clear();
            textBoxUserEmpID.Clear();
            textBoxUserComment.Clear();
            textBoxUserSearch.Clear();
        }

        private void UserUpdateButton_Click(object sender, EventArgs e)
        {
            //Check If The Enetered User ID is 3 Digit
            string tempId = textBoxUserID.Text.Trim();
            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid User ID!", "Error");
                textBoxUserID.Clear();
                textBoxUserID.Focus();
                return;
            }

            //Check Duplicate User ID
            //User usrExist = new User();
            //usrExist = usrExist.GetUser(Convert.ToInt32(tempId));
            //if (usrExist != null)
            //{
            //    MessageBox.Show("This User ID Already Exists!", "Duplicate User ID");
            //    textBoxUserID.Clear();
            //    textBoxUserID.Focus();
            //    return;
            //}

            //Check If Password is not Empty
            string tempPass = textBoxUserPass.Text.Trim();
            if (Validator.IsEmpty(tempPass))
            {
                MessageBox.Show("Password Cannot Be Empty!", "Error");
                textBoxUserPass.Clear();
                textBoxUserPass.Focus();
                return;
            }

            //Check If The Enetered Employee ID is 3 Digit
            string tempEmpId = textBoxUserEmpID.Text.Trim();
            if (!Validator.IsValidId(tempEmpId, 3))
            {
                MessageBox.Show("Invalid Employee ID!", "Error");
                textBoxUserEmpID.Clear();
                textBoxUserEmpID.Focus();
                return;
            }

            //Check If Employee ID Exist
            Employee empIdExist = new Employee();
            empIdExist = empIdExist.GetEmployee(Convert.ToInt32(tempEmpId));
            if (empIdExist == null)
            {
                MessageBox.Show("This Employee ID Does Not Exist!", "Invalid Employee ID");
                textBoxUserEmpID.Clear();
                textBoxUserEmpID.Focus();
                return;
            }
            if (textBoxUserEmpID.Text != textBoxUserID.Text)
            {
                MessageBox.Show("The User ID and The Employee ID MUST MATCH!", "Invalid Entry");
                textBoxUserID.Clear();
                textBoxUserID.Focus();
                return;
            }
            else
            {
                User usr = new User();
                if (usr != null)
                {
                    usr.UserID = Convert.ToInt32(textBoxUserID.Text);
                    usr.UserPassword = textBoxUserPass.Text.Trim();
                    usr.UserEmployeeID = Convert.ToInt32(textBoxUserEmpID.Text);
                    usr.UserComment = textBoxUserComment.Text;

                }
                else
                {
                    MessageBox.Show("User Not Found !", "Invalid User");
                    textBoxEmpSearch.Clear();
                    textBoxEmpSearch.Focus();
                }
                usr.UpdateUser(usr);
                MessageBox.Show("User Info UPDATED Successfully", "Confirmation");
            }

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }


        //////////////////////////////
        ///  Login/Logout Section  ///
        //////////////////////////////
        private void LoginLogoutButton_Click_1(object sender, EventArgs e)
        {
            int login = tabControl1.SelectedIndex;
            switch (login)
            {
                //MIS MANAGER
                case 0:
                    if (panel1.Visible == false)
                    {
                        panel1.Visible = true;
                        Forgot.Visible = true;

                    }
                    else
                    {
                        string tempUID = textBoxUserName.Text.Trim();
                        if (Validator.IsEmpty(tempUID))
                        {
                            MessageBox.Show("User ID Cannot Be Empty!", "Error");
                            textBoxUserName.Clear();
                            textBoxUserName.Focus();
                            return;
                        }

                        if (!Validator.IsValidId(tempUID, 3))
                        {
                            MessageBox.Show("Invalid User ID!", "Error");
                            textBoxUserName.Clear();
                            textBoxUserName.Focus();
                            return;
                        }

                        string tempUPASS = textBoxUserPassword.Text.Trim();
                        if (Validator.IsEmpty(tempUPASS))
                        {
                            MessageBox.Show("Password Cannot Be Empty!", "Error");
                            textBoxUserPassword.Clear();
                            textBoxUserPassword.Focus();
                            return;
                        }

                        SqlConnection connDB = UtilityDB.ConnectDB();
                        SqlDataAdapter loginSDA = new SqlDataAdapter("Select Count(*) From UserAccounts WHERE UserID = '" + textBoxUserName.Text + "' and Password = '" + textBoxUserPassword.Text + "'", connDB);
                        DataTable loginDT = new DataTable();
                        loginSDA.Fill(loginDT);
                        SqlDataAdapter roleSDA = new SqlDataAdapter("Select JobID From Employees WHERE EmployeeID = '" + textBoxUserName.Text + "'", connDB);
                        DataTable roleDT = new DataTable();
                        roleSDA.Fill(roleDT);
                        if (loginDT.Rows[0][0].ToString() == "1" && roleDT.Rows[0][0].ToString() == "1")
                        {


                            panel1.Visible = false;
                            Forgot.Visible = false;
                            textBoxUserName.Clear();
                            textBoxUserPassword.Clear();


                        }
                        else
                        {
                            MessageBox.Show("The UserID and The Password Do Not Match! Or You Might Not Have The Role of MIS Manager!", "Invalid Data", MessageBoxButtons.OK);
                            textBoxUserName.Clear();
                            textBoxUserPassword.Clear();
                        }
                    }
                    break;

                //SALES MANAGER
                case 1:
                    if (panel2.Visible == false)
                    {
                        panel2.Visible = true;
                        Forgot.Visible = true;

                    }
                    else
                    {
                        string tempUID = textBoxUserName.Text.Trim();
                        if (Validator.IsEmpty(tempUID))
                        {
                            MessageBox.Show("User ID Cannot Be Empty!", "Error");
                            textBoxUserName.Clear();
                            textBoxUserName.Focus();
                            return;
                        }

                        if (!Validator.IsValidId(tempUID, 3))
                        {
                            MessageBox.Show("Invalid User ID!", "Error");
                            textBoxUserName.Clear();
                            textBoxUserName.Focus();
                            return;
                        }

                        string tempUPASS = textBoxUserPassword.Text.Trim();
                        if (Validator.IsEmpty(tempUPASS))
                        {
                            MessageBox.Show("Password Cannot Be Empty!", "Error");
                            textBoxUserPassword.Clear();
                            textBoxUserPassword.Focus();
                            return;
                        }
                        SqlConnection connDB = UtilityDB.ConnectDB();
                        SqlDataAdapter loginSDA = new SqlDataAdapter("Select Count(*) From UserAccounts WHERE UserID = '" + textBoxUserName.Text + "' and Password = '" + textBoxUserPassword.Text + "'", connDB);
                        DataTable loginDT = new DataTable();
                        loginSDA.Fill(loginDT);
                        SqlDataAdapter roleSDA = new SqlDataAdapter("Select JobID From Employees WHERE EmployeeID = '" + textBoxUserName.Text + "'", connDB);
                        DataTable roleDT = new DataTable();
                        roleSDA.Fill(roleDT);
                        if (loginDT.Rows[0][0].ToString() == "1" && roleDT.Rows[0][0].ToString() == "2")
                        {


                            panel2.Visible = false;
                            Forgot.Visible = false;
                            textBoxUserName.Clear();
                            textBoxUserPassword.Clear();


                        }
                        else
                        {
                            MessageBox.Show("The UserID and The Password Do Not Match! Or You Might Not Have The Role of Sale Manager!", "Invalid Data", MessageBoxButtons.OK);
                            textBoxUserName.Clear();
                            textBoxUserPassword.Clear();
                        }
                    }
                    break;

                //INVENTORY CONTROLLER
                case 2:
                    if (panel3.Visible == false)
                    {
                        panel3.Visible = true;
                        Forgot.Visible = true;

                    }
                    else
                    {
                        string tempUID = textBoxUserName.Text.Trim();
                        if (Validator.IsEmpty(tempUID))
                        {
                            MessageBox.Show("User ID Cannot Be Empty!", "Error");
                            textBoxUserName.Clear();
                            textBoxUserName.Focus();
                            return;
                        }

                        if (!Validator.IsValidId(tempUID, 3))
                        {
                            MessageBox.Show("Invalid User ID!", "Error");
                            textBoxUserName.Clear();
                            textBoxUserName.Focus();
                            return;
                        }

                        string tempUPASS = textBoxUserPassword.Text.Trim();
                        if (Validator.IsEmpty(tempUPASS))
                        {
                            MessageBox.Show("Password Cannot Be Empty!", "Error");
                            textBoxUserPassword.Clear();
                            textBoxUserPassword.Focus();
                            return;
                        }
                        SqlConnection connDB = UtilityDB.ConnectDB();
                        SqlDataAdapter loginSDA = new SqlDataAdapter("Select Count(*) From UserAccounts WHERE UserID = '" + textBoxUserName.Text + "' and Password = '" + textBoxUserPassword.Text + "'", connDB);
                        DataTable loginDT = new DataTable();
                        loginSDA.Fill(loginDT);
                        SqlDataAdapter roleSDA = new SqlDataAdapter("Select JobID From Employees WHERE EmployeeID = '" + textBoxUserName.Text + "'", connDB);
                        DataTable roleDT = new DataTable();
                        roleSDA.Fill(roleDT);
                        if (loginDT.Rows[0][0].ToString() == "1" && roleDT.Rows[0][0].ToString() == "3")
                        {


                            panel3.Visible = false;
                            Forgot.Visible = false;
                            textBoxUserName.Clear();
                            textBoxUserPassword.Clear();


                        }
                        else
                        {
                            MessageBox.Show("The UserID and The Password Do Not Match! Or You Might Not Have The Role of Inventory Controller!", "Invalid Data", MessageBoxButtons.OK);
                            textBoxUserName.Clear();
                            textBoxUserPassword.Clear();
                        }
                    }
                    break;

                //ORDER CLERK
                case 3:
                    if (panel4.Visible == false)
                    {
                        panel4.Visible = true;
                        Forgot.Visible = true;

                    }
                    else
                    {
                        string tempUID = textBoxUserName.Text.Trim();
                        if (Validator.IsEmpty(tempUID))
                        {
                            MessageBox.Show("User ID Cannot Be Empty!", "Error");
                            textBoxUserName.Clear();
                            textBoxUserName.Focus();
                            return;
                        }

                        if (!Validator.IsValidId(tempUID, 3))
                        {
                            MessageBox.Show("Invalid User ID!", "Error");
                            textBoxUserName.Clear();
                            textBoxUserName.Focus();
                            return;
                        }

                        string tempUPASS = textBoxUserPassword.Text.Trim();
                        if (Validator.IsEmpty(tempUPASS))
                        {
                            MessageBox.Show("Password Cannot Be Empty!", "Error");
                            textBoxUserPassword.Clear();
                            textBoxUserPassword.Focus();
                            return;
                        }
                        SqlConnection connDB = UtilityDB.ConnectDB();
                        SqlDataAdapter loginSDA = new SqlDataAdapter("Select Count(*) From UserAccounts WHERE UserID = '" + textBoxUserName.Text + "' and Password = '" + textBoxUserPassword.Text + "'", connDB);
                        DataTable loginDT = new DataTable();
                        loginSDA.Fill(loginDT);
                        SqlDataAdapter roleSDA = new SqlDataAdapter("Select JobID From Employees WHERE EmployeeID = '" + textBoxUserName.Text + "'", connDB);
                        DataTable roleDT = new DataTable();
                        roleSDA.Fill(roleDT);
                        if (loginDT.Rows[0][0].ToString() == "1" && roleDT.Rows[0][0].ToString() == "4")
                        {


                            panel4.Visible = false;
                            Forgot.Visible = false;
                            textBoxUserName.Clear();
                            textBoxUserPassword.Clear();


                        }
                        else
                        {
                            MessageBox.Show("The UserID and The Password Do Not Match! Or You Might Not Have The Role of Order Clerk!", "Invalid Data", MessageBoxButtons.OK);
                            textBoxUserName.Clear();
                            textBoxUserPassword.Clear();
                        }
                    }
                    break;
            }

        }

        


        ////////////////////////
        ///   Book Section   ///
        ////////////////////////
        private void buttonListBook_Click(object sender, EventArgs e)
        {
            Book buk = new Book();
            List<Book> listBuk = new List<Book>();
            listBuk = buk.GetBookList();
            listViewBook.Items.Clear();
            if (listBuk != null)
            {
                foreach (Book anBuk in listBuk)
                {
                    ListViewItem bukItem = new ListViewItem(anBuk.BookISBN.ToString());
                    bukItem.SubItems.Add(anBuk.BookTitle);
                    bukItem.SubItems.Add(anBuk.BookQOH.ToString());
                    bukItem.SubItems.Add(anBuk.BookPrice.ToString());
                    bukItem.SubItems.Add(anBuk.BookCategoryID.ToString());
                    bukItem.SubItems.Add(anBuk.BookPublisherID.ToString());
                    listViewBook.Items.Add(bukItem);
                }

            }
            else
            {

            }
        }

        private void buttonSaveBook_Click(object sender, EventArgs e)
        {
            //Check If The Enetered ISBN is 13 Digit
            string tempId = textBoxISBN.Text.Trim();
            if (!Validator.IsValidId(tempId, 13))
            {
                MessageBox.Show("Invalid ISBN!", "Error");
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }
            //Check Duplicate ISBN
            Book bukExist = new Book();
            bukExist = bukExist.GetBookRecord(Convert.ToInt64(tempId));
            if (bukExist != null)
            {
                MessageBox.Show("This ISBN Already Exists!", "Duplicate ISBN");
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }

            //Check If Book Title is not Empty
            string tempBookTitle = textBoxBookTitle.Text.Trim();
            if (Validator.IsEmpty(tempBookTitle))
            {
                MessageBox.Show("Book Title Cannot Be Empty!", "Error");
                textBoxBookTitle.Clear();
                textBoxBookTitle.Focus();
                return;
            }

            //Check If QOH is not Empty
            string tempQOH = textBoxQOH.Text.Trim();
            if (Validator.IsEmpty(tempQOH))
            {
                MessageBox.Show("Quantity On Hand Cannot Be Empty!", "Error");
                textBoxQOH.Clear();
                textBoxQOH.Focus();
                return;
            }

            //Check If Price is not Empty
            string tempPrice = textBoxBookPrice.Text.Trim();
            if (Validator.IsEmpty(tempPrice))
            {
                MessageBox.Show("Book Price Cannot Be Empty!", "Error");
                textBoxBookPrice.Clear();
                textBoxBookPrice.Focus();
                return;
            }

            //Check If Category ID is not Empty
            string tempCID = textBoxCategoryID.Text.Trim();
            if (Validator.IsEmpty(tempCID))
            {
                MessageBox.Show("Category ID Cannot Be Empty!", "Error");
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }
            //Check If Category ID Exist
            Category catExist = new Category();
            catExist = catExist.GetCategoryRecordID(Convert.ToInt32(tempCID));
            if (catExist == null)
            {
                MessageBox.Show("This Category Does Not Exists!", "Error");
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }

            //Check If Publisher ID is not Empty
            string tempPublisherID = textBoxPublisherID.Text.Trim();
            if (Validator.IsEmpty(tempPublisherID))
            {
                MessageBox.Show("Publisher ID Cannot Be Empty!", "Error");
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }
            //Check If Publisher ID Exist
            Publisher publisherExist = new Publisher();
            publisherExist = publisherExist.GetPublisher(Convert.ToInt32(tempPublisherID));
            if (publisherExist == null)
            {
                MessageBox.Show("This Publisher ID Does Not Exist!", "Invalid Publisher ID");
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }

            Book buk = new Book();
            //Valid data
            buk.BookISBN = Convert.ToInt64(textBoxISBN.Text);
            buk.BookTitle = textBoxBookTitle.Text.Trim();
            buk.BookQOH = Convert.ToInt32(textBoxQOH.Text);
            buk.BookPrice = (float)Convert.ToDecimal(textBoxBookPrice.Text);
            buk.BookCategoryID = Convert.ToInt32(textBoxCategoryID.Text);
            buk.BookPublisherID = Convert.ToInt32(textBoxPublisherID.Text);
            buk.SaveBook(buk);
            textBoxISBN.Clear();
            textBoxBookTitle.Clear();
            textBoxQOH.Clear();
            textBoxBookPrice.Clear();
            textBoxCategoryID.Clear();
            textBoxPublisherID.Clear();
            MessageBox.Show("Book Info SAVED Successfully!", "Confirmation");
        }

        private void buttonDeleteBook_Click(object sender, EventArgs e)
        {
            string tempId = textBoxISBN.Text.Trim();
            if (!Validator.IsValidId(tempId, 13))
            {
                MessageBox.Show("Invalid ISBN", "Error");
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }

            Book buk = new Book();
            if (buk != null)
            {
                buk.BookISBN = Convert.ToInt64(textBoxISBN.Text);
                buk.BookTitle = textBoxBookTitle.Text.Trim();
                buk.BookQOH = Convert.ToInt32(textBoxQOH.Text);
                buk.BookPrice = (float)Convert.ToDecimal(textBoxBookPrice.Text);
                buk.BookCategoryID = Convert.ToInt32(textBoxCategoryID.Text);
                buk.BookPublisherID = Convert.ToInt32(textBoxPublisherID.Text);


            }
            else
            {
                MessageBox.Show("Book Not Found!", "Invalid Book Info");
                textBoxSearchBook.Clear();
                textBoxSearchBook.Focus();
            }
            buk.DeleteBook(buk);
            textBoxISBN.Clear();
            textBoxBookTitle.Clear();
            textBoxQOH.Clear();
            textBoxBookPrice.Clear();
            textBoxCategoryID.Clear();
            textBoxPublisherID.Clear();
            MessageBox.Show("Book Info DELETED Successfully!", "Confirmation");
        }

        private void buttonUpdateBook_Click(object sender, EventArgs e)
        {


            //Check If Book Title is not Empty
            string tempBookTitle = textBoxBookTitle.Text.Trim();
            if (Validator.IsEmpty(tempBookTitle))
            {
                MessageBox.Show("Book Title Cannot Be Empty!", "Error");
                textBoxBookTitle.Clear();
                textBoxBookTitle.Focus();
                return;
            }

            //Check If QOH is not Empty
            string tempQOH = textBoxQOH.Text.Trim();
            if (Validator.IsEmpty(tempQOH))
            {
                MessageBox.Show("Quantity On Hand Cannot Be Empty!", "Error");
                textBoxQOH.Clear();
                textBoxQOH.Focus();
                return;
            }

            //Check If Price is not Empty
            string tempPrice = textBoxBookPrice.Text.Trim();
            if (Validator.IsEmpty(tempPrice))
            {
                MessageBox.Show("Book Price Cannot Be Empty!", "Error");
                textBoxBookPrice.Clear();
                textBoxBookPrice.Focus();
                return;
            }

            //Check If Category ID is not Empty
            string tempCID = textBoxCategoryID.Text.Trim();
            if (Validator.IsEmpty(tempCID))
            {
                MessageBox.Show("Category ID Cannot Be Empty!", "Error");
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }
            //Check If Category ID Exist
            Category catExist = new Category();
            catExist = catExist.GetCategoryRecordID(Convert.ToInt32(tempCID));
            if (catExist == null)
            {
                MessageBox.Show("This Category Does Not Exists!", "Error");
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }

            //Check If Publisher ID is not Empty
            string tempPublisherID = textBoxPublisherID.Text.Trim();
            if (Validator.IsEmpty(tempPublisherID))
            {
                MessageBox.Show("Publisher ID Cannot Be Empty!", "Error");
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }
            //Check If Publisher ID Exist
            Publisher publisherExist = new Publisher();
            publisherExist = publisherExist.GetPublisher(Convert.ToInt32(tempPublisherID));
            if (publisherExist == null)
            {
                MessageBox.Show("This Publisher ID Does Not Exist!", "Invalid Publisher ID");
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }

            Book buk = new Book();
            if (buk != null)
            {
                buk.BookISBN = Convert.ToInt64(textBoxISBN.Text);
                buk.BookTitle = textBoxBookTitle.Text.Trim();
                buk.BookQOH = Convert.ToInt32(textBoxQOH.Text);
                buk.BookPrice = (float)Convert.ToDecimal(textBoxBookPrice.Text);
                buk.BookCategoryID = Convert.ToInt32(textBoxCategoryID.Text);
                buk.BookPublisherID = Convert.ToInt32(textBoxPublisherID.Text);

            }
            else
            {
                MessageBox.Show("Book Not Found !", "Invalid Book Info");
                textBoxSearchBook.Clear();
                textBoxSearchBook.Focus();
            }
            //emp = emp.GetEmployee(Convert.ToInt32(textBoxInput.Text.Trim()));
            buk.UpdateBook(buk);
            MessageBox.Show("Book Info UPDATED Successfully", "Confirmation");
        }

        private void buttonBookClear_Click(object sender, EventArgs e)
        {
            textBoxISBN.ReadOnly = false;
            textBoxISBN.Clear();
            textBoxBookTitle.Clear();
            textBoxQOH.Clear();
            textBoxBookPrice.Clear();
            textBoxCategoryID.Clear();
            textBoxPublisherID.Clear();
            textBoxSearchBook.Clear();
        }

        private void buttonSearchBook_Click(object sender, EventArgs e)
        {
            textBoxISBN.ReadOnly = true;
            listViewBook.Items.Clear();
            int indexSelected = comboBoxSearchBook.SelectedIndex;
            switch (indexSelected)
            {
                case 0:
                    //Search by ISBN
                    string tempId = textBoxSearchBook.Text.Trim();

                    if (!Validator.IsValidId(tempId, 13))
                    {
                        MessageBox.Show("Invalid ISBN!", "Error");
                        textBoxSearchBook.Clear();
                        textBoxSearchBook.Focus();
                        return;
                    }

                    Book buk = new Book();
                    buk = buk.GetBookRecord(Convert.ToInt64(textBoxSearchBook.Text.Trim()));
                    if (buk != null)
                    {
                        textBoxISBN.Text = buk.BookISBN.ToString();
                        textBoxBookTitle.Text = buk.BookTitle;
                        textBoxQOH.Text = buk.BookQOH.ToString();
                        textBoxBookPrice.Text = buk.BookPrice.ToString();
                        textBoxCategoryID.Text = buk.BookCategoryID.ToString();
                        textBoxPublisherID.Text = buk.BookPublisherID.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Book Not Found!", "Invalid Book ISBN");
                        textBoxSearchBook.Clear();
                        textBoxSearchBook.Focus();
                    }
                    break;

                case 1:
                    string tempTitle = textBoxSearchBook.Text.Trim();
                    if (Validator.IsEmpty(tempTitle))
                    {
                        MessageBox.Show("Search Input Cannot Be Empty!", "Error");
                        textBoxPublisherID.Clear();
                        textBoxPublisherID.Focus();
                        return;
                    }
                    Book bukT = new Book();
                    List<Book> listBook = new List<Book>();
                    listBook = bukT.GetBookTitleRecordList(tempTitle);
                    listViewBook.Items.Clear();
                    if (listBook != null)
                    {
                        foreach (Book anBook in listBook)
                        {
                            ListViewItem bukItem = new ListViewItem(anBook.BookISBN.ToString());
                            bukItem.SubItems.Add(anBook.BookTitle);
                            bukItem.SubItems.Add(anBook.BookQOH.ToString());
                            bukItem.SubItems.Add(anBook.BookPrice.ToString());
                            bukItem.SubItems.Add(anBook.BookCategoryID.ToString());
                            bukItem.SubItems.Add(anBook.BookPublisherID.ToString());
                            listViewBook.Items.Add(bukItem);
                        }
                        if (listViewBook.Items.Count == 0)
                        {
                            MessageBox.Show("Book Not Found!", "Invalid Book Title");
                            textBoxSearchBook.Clear();
                            textBoxSearchBook.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Book Not Found!", "Invalid Book Title");
                        textBoxSearchBook.Clear();
                        textBoxSearchBook.Focus();
                    }
                    break;

                case 2:
                    string tempCatID = textBoxSearchBook.Text.Trim();
                    if (Validator.IsEmpty(tempCatID))
                    {
                        MessageBox.Show("Search Input Cannot Be Empty!", "Error");
                        textBoxPublisherID.Clear();
                        textBoxPublisherID.Focus();
                        return;
                    }
                    Book bukCatID = new Book();
                    List<Book> listBookCatID = new List<Book>();
                    listBookCatID = bukCatID.GetBookCategoryIDRecordList(Convert.ToInt32(tempCatID));
                    listViewBook.Items.Clear();
                    if (listBookCatID != null)
                    {
                        foreach (Book anBook in listBookCatID)
                        {
                            ListViewItem bukItem = new ListViewItem(anBook.BookISBN.ToString());
                            bukItem.SubItems.Add(anBook.BookTitle);
                            bukItem.SubItems.Add(anBook.BookQOH.ToString());
                            bukItem.SubItems.Add(anBook.BookPrice.ToString());
                            bukItem.SubItems.Add(anBook.BookCategoryID.ToString());
                            bukItem.SubItems.Add(anBook.BookPublisherID.ToString());
                            listViewBook.Items.Add(bukItem);
                        }
                        if (listViewBook.Items.Count == 0)
                        {
                            MessageBox.Show("Book Not Found!", "Invalid Book Category ID");
                            textBoxSearchBook.Clear();
                            textBoxSearchBook.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Book Not Found!", "Invalid Book Category ID");
                        textBoxSearchBook.Clear();
                        textBoxSearchBook.Focus();
                    }
                    break;

                case 3:
                    string tempPubID = textBoxSearchBook.Text.Trim();
                    if (Validator.IsEmpty(tempPubID))
                    {
                        MessageBox.Show("Search Input Cannot Be Empty!", "Error");
                        textBoxPublisherID.Clear();
                        textBoxPublisherID.Focus();
                        return;
                    }
                    Book bukPubID = new Book();
                    List<Book> listBookPubID = new List<Book>();
                    listBookPubID = bukPubID.GetBookPublisherIDRecordList(Convert.ToInt32(tempPubID));
                    listViewBook.Items.Clear();
                    if (listBookPubID != null)
                    {
                        foreach (Book anBook in listBookPubID)
                        {
                            ListViewItem bukItem = new ListViewItem(anBook.BookISBN.ToString());
                            bukItem.SubItems.Add(anBook.BookTitle);
                            bukItem.SubItems.Add(anBook.BookQOH.ToString());
                            bukItem.SubItems.Add(anBook.BookPrice.ToString());
                            bukItem.SubItems.Add(anBook.BookCategoryID.ToString());
                            bukItem.SubItems.Add(anBook.BookPublisherID.ToString());
                            listViewBook.Items.Add(bukItem);
                        }
                        if (listViewBook.Items.Count == 0)
                        {
                            MessageBox.Show("Book Not Found!", "Invalid Book Publisher ID");
                            textBoxSearchBook.Clear();
                            textBoxSearchBook.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Book Not Found!", "Invalid Book Publisher ID");
                        textBoxSearchBook.Clear();
                        textBoxSearchBook.Focus();
                    }
                    break;
            }
        }


        ////////////////////////
        /// Category Section ///
        ////////////////////////
        private void buttonListCat_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            List<Category> listCat = new List<Category>();
            listCat = cat.GetCategoryList();
            listViewCat.Items.Clear();
            if (listCat != null)
            {
                foreach (Category anCat in listCat)
                {
                    ListViewItem catItem = new ListViewItem(anCat.CategoryID.ToString());
                    catItem.SubItems.Add(anCat.CategoryName);
                    listViewCat.Items.Add(catItem);
                }

            }
            else
            {

            }
        }

        private void buttonClearCat_Click(object sender, EventArgs e)
        {
            textBoxCatID.ReadOnly = false;
            textBoxCatID.Clear();
            textBoxCatName.Clear();
            textBoxSearchCat.Clear();
        }

        private void buttonSaveCat_Click(object sender, EventArgs e)
        {
            //Check If Category ID is not Empty
            string tempId = textBoxCatID.Text.Trim();
            if (Validator.IsEmpty(tempId))
            {
                MessageBox.Show("Category ID Cannot Be Empty!", "Error");
                textBoxCatID.Clear();
                textBoxCatID.Focus();
                return;
            }

            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid Category ID", "Error");
                textBoxCatID.Clear();
                textBoxCatID.Focus();
                return;
            }

            //Check Duplicate Category ID
            Category catExist = new Category();
            catExist = catExist.GetCategoryRecordID(Convert.ToInt32(tempId));
            if (catExist != null)
            {
                MessageBox.Show("This Category ID Already Exists!", "Duplicate Category ID");
                textBoxCatID.Clear();
                textBoxCatID.Focus();
                return;
            }

            //Check If Category Name is not Empty
            string tempCatName = textBoxCatName.Text.Trim();
            if (Validator.IsEmpty(tempCatName))
            {
                MessageBox.Show("Category Name Cannot Be Empty!", "Error");
                textBoxCatName.Clear();
                textBoxCatName.Focus();
                return;
            }

            Category cat = new Category();
            //Valid data
            cat.CategoryID = Convert.ToInt32(textBoxCatID.Text);
            cat.CategoryName = textBoxCatName.Text.Trim();
            cat.SaveCategory(cat);
            textBoxCatID.Clear();
            textBoxCatName.Clear();
            MessageBox.Show("Category Info SAVED Successfully!", "Confirmation");
        }

        private void buttonDeleteCat_Click(object sender, EventArgs e)
        {
            string tempId = textBoxCatID.Text.Trim();
            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid Category ID", "Error");
                textBoxCatID.Clear();
                textBoxCatID.Focus();
                textBoxCatName.Clear();
                return;
            }

            Category cat = new Category();
            if (cat != null)
            {
                cat.CategoryID = Convert.ToInt32(textBoxCatID.Text);
                cat.CategoryName = textBoxCatName.Text.Trim();

            }
            else
            {
                MessageBox.Show("Category Not Found!", "Invalid Category Info");
                textBoxSearchCat.Clear();
                textBoxSearchCat.Focus();
            }
            cat.DeleteCategory(cat);
            textBoxCatID.Clear();
            textBoxCatName.Clear();
            MessageBox.Show("Category Info DELETED Successfully!", "Confirmation");
        }

        private void buttonUpdateCat_Click(object sender, EventArgs e)
        {
            //Check If Category Name is not Empty
            string tempCatName = textBoxCatName.Text.Trim();
            if (Validator.IsEmpty(tempCatName))
            {
                MessageBox.Show("Category Name Cannot Be Empty!", "Error");
                textBoxCatName.Clear();
                textBoxCatName.Focus();
                return;
            }


            Category cat = new Category();
            if (cat != null)
            {
                cat.CategoryID = Convert.ToInt32(textBoxCatID.Text);
                cat.CategoryName = textBoxCatName.Text.Trim();

            }
            else
            {
                MessageBox.Show("Category Not Found !", "Invalid Category Info");
                textBoxSearchCat.Clear();
                textBoxSearchCat.Focus();
            }

            cat.UpdateCategory(cat);
            MessageBox.Show("Category Info UPDATED Successfully", "Confirmation");
        }

        private void buttonSearchCat_Click(object sender, EventArgs e)
        {
            textBoxCatID.ReadOnly = true;
            listViewCat.Items.Clear();
            int indexSelected = comboBoxSearchCat.SelectedIndex;
            switch (indexSelected)
            {
                case 0:
                    //Search by Category ID
                    string tempId = textBoxSearchCat.Text.Trim();

                    if (!Validator.IsValidId(tempId, 3))
                    {
                        MessageBox.Show("Invalid Category ID!", "Error");
                        textBoxSearchCat.Clear();
                        textBoxSearchCat.Focus();
                        return;
                    }

                    Category cat = new Category();
                    cat = cat.GetCategoryRecordID(Convert.ToInt32(textBoxSearchCat.Text.Trim()));
                    if (cat != null)
                    {
                        textBoxCatID.Text = cat.CategoryID.ToString();
                        textBoxCatName.Text = cat.CategoryName;
                    }
                    else
                    {
                        MessageBox.Show("Category Not Found!", "Invalid Category ID");
                        textBoxSearchCat.Clear();
                        textBoxSearchCat.Focus();
                    }
                    break;

                case 1:
                    //Search by Category Name
                    string tempName = textBoxSearchCat.Text.Trim();

                    if (Validator.IsEmpty(tempName))
                    {
                        MessageBox.Show("Invalid Category Name!", "Error");
                        textBoxSearchCat.Clear();
                        textBoxSearchCat.Focus();
                        return;
                    }

                    Category anCat = new Category();
                    anCat = anCat.GetCategoryRecordName(textBoxSearchCat.Text.Trim());
                    if (anCat != null)
                    {
                        textBoxCatID.Text = anCat.CategoryID.ToString();
                        textBoxCatName.Text = anCat.CategoryName;
                    }
                    else
                    {
                        MessageBox.Show("Category Not Found!", "Invalid Category Name");
                        textBoxSearchCat.Clear();
                        textBoxSearchCat.Focus();
                    }
                    break;
            }
        }

        /////////////////////////
        /// Publisher Section ///
        /////////////////////////
        private void buttonPubList_Click(object sender, EventArgs e)
        {
            Publisher pub = new Publisher();
            List<Publisher> listPub = new List<Publisher>();
            listPub = pub.GetPublisherList();
            listViewPub.Items.Clear();
            if (listPub != null)
            {
                foreach (Publisher anPub in listPub)
                {
                    ListViewItem pubItem = new ListViewItem(anPub.PublisherID.ToString());
                    pubItem.SubItems.Add(anPub.PublisherName);
                    pubItem.SubItems.Add(anPub.PublisherWeb);
                    listViewPub.Items.Add(pubItem);
                }

            }
            else
            {

            }
        }

        private void buttonPubSave_Click(object sender, EventArgs e)
        {
            //Check If Publisher ID is not Empty
            string tempId = textBoxPubID.Text.Trim();
            if (Validator.IsEmpty(tempId))
            {
                MessageBox.Show("Publisher ID Cannot Be Empty!", "Error");
                textBoxPubID.Clear();
                textBoxPubID.Focus();
                return;
            }

            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid Publisher ID", "Error");
                textBoxPubID.Clear();
                textBoxPubID.Focus();
                return;
            }

            //Check Duplicate Publisher ID
            Publisher pubExist = new Publisher();
            pubExist = pubExist.GetPublisher(Convert.ToInt32(tempId));
            if (pubExist != null)
            {
                MessageBox.Show("This Publisher ID Already Exists!", "Duplicate Publisher ID");
                textBoxPubID.Clear();
                textBoxPubID.Focus();
                return;
            }

            //Check If Publisher Name is not Empty
            string tempPubName = textBoxPubName.Text.Trim();
            if (Validator.IsEmpty(tempPubName))
            {
                MessageBox.Show("Publisher Name Cannot Be Empty!", "Error");
                textBoxPubName.Clear();
                textBoxPubName.Focus();
                return;
            }

            //Check If Publisher Web is not Empty
            string tempPubWeb = textBoxPubWeb.Text.Trim();
            if (Validator.IsEmpty(tempPubWeb))
            {
                MessageBox.Show("Publisher Web Cannot Be Empty!", "Error");
                textBoxPubWeb.Clear();
                textBoxPubWeb.Focus();
                return;
            }

            Publisher pub = new Publisher();
            //Valid data
            pub.PublisherID = Convert.ToInt32(textBoxPubID.Text);
            pub.PublisherName = textBoxPubName.Text.Trim();
            pub.PublisherWeb = textBoxPubWeb.Text.Trim();
            pub.SavePublisher(pub);
            textBoxPubID.Clear();
            textBoxPubName.Clear();
            textBoxPubWeb.Clear();
            MessageBox.Show("Publisher Info SAVED Successfully!", "Confirmation");
        }

        private void buttonPubUpdate_Click(object sender, EventArgs e)
        {
            //Check If Publisher Name is not Empty
            string tempPubName = textBoxPubName.Text.Trim();
            if (Validator.IsEmpty(tempPubName))
            {
                MessageBox.Show("Publisher Name Cannot Be Empty!", "Error");
                textBoxPubName.Clear();
                textBoxPubName.Focus();
                return;
            }

            //Check If Publisher Web is not Empty
            string tempPubWeb = textBoxPubWeb.Text.Trim();
            if (Validator.IsEmpty(tempPubWeb))
            {
                MessageBox.Show("Publisher Web Cannot Be Empty!", "Error");
                textBoxPubWeb.Clear();
                textBoxPubWeb.Focus();
                return;
            }


            Publisher pub = new Publisher();
            if (pub != null)
            {
                pub.PublisherID = Convert.ToInt32(textBoxPubID.Text);
                pub.PublisherName = textBoxPubName.Text.Trim();
                pub.PublisherWeb = textBoxPubWeb.Text.Trim();

            }
            else
            {
                MessageBox.Show("Publisher Not Found !", "Invalid Publisher Info");
                textBoxPubSearch.Clear();
                textBoxPubSearch.Focus();
            }

            pub.UpdatePublisher(pub);
            MessageBox.Show("Publisher Info UPDATED Successfully", "Confirmation");
        }

        private void buttonPubClear_Click(object sender, EventArgs e)
        {
            textBoxPubID.ReadOnly = false;
            textBoxPubID.Clear();
            textBoxPubName.Clear();
            textBoxPubWeb.Clear();
            textBoxPubSearch.Clear();
        }

        private void buttonPubSearch_Click(object sender, EventArgs e)
        {
            textBoxPubID.ReadOnly = true;
            listViewPub.Items.Clear();
            int indexSelected = comboBoxPub.SelectedIndex;
            switch (indexSelected)
            {
                case 0:
                    //Search by Publisher ID
                    string tempId = textBoxPubSearch.Text.Trim();

                    if (!Validator.IsValidId(tempId, 3))
                    {
                        MessageBox.Show("Invalid Publisher ID!", "Error");
                        textBoxPubSearch.Clear();
                        textBoxPubSearch.Focus();
                        return;
                    }

                    Publisher pub = new Publisher();
                    pub = pub.GetPublisher(Convert.ToInt32(textBoxPubSearch.Text.Trim()));
                    if (pub != null)
                    {
                        textBoxPubID.Text = pub.PublisherID.ToString();
                        textBoxPubName.Text = pub.PublisherName;
                        textBoxPubWeb.Text = pub.PublisherWeb;
                    }
                    else
                    {
                        MessageBox.Show("Publisher Not Found!", "Invalid Publisher ID");
                        textBoxPubSearch.Clear();
                        textBoxPubSearch.Focus();
                    }
                    break;

                case 1:
                    //Search by Publisher Name
                    string tempName = textBoxPubSearch.Text.Trim();

                    if (Validator.IsEmpty(tempName))
                    {
                        MessageBox.Show("Invalid Publisher Name!", "Error");
                        textBoxPubSearch.Clear();
                        textBoxPubSearch.Focus();
                        return;
                    }

                    Publisher anPub = new Publisher();
                    anPub = anPub.GetPublisherRecordName(textBoxPubSearch.Text.Trim());
                    if (anPub != null)
                    {
                        textBoxPubID.Text = anPub.PublisherID.ToString();
                        textBoxPubName.Text = anPub.PublisherName;
                        textBoxPubWeb.Text = anPub.PublisherWeb;
                    }
                    else
                    {
                        MessageBox.Show("Publisher Not Found!", "Invalid Publisher Name");
                        textBoxPubSearch.Clear();
                        textBoxPubSearch.Focus();
                    }
                    break;

                case 2:
                    //Search by Publisher Name
                    string tempWeb = textBoxPubSearch.Text.Trim();

                    if (Validator.IsEmpty(tempWeb))
                    {
                        MessageBox.Show("Invalid Publisher Name!", "Error");
                        textBoxPubSearch.Clear();
                        textBoxPubSearch.Focus();
                        return;
                    }

                    Publisher anPub2 = new Publisher();
                    anPub2 = anPub2.GetPublisherRecordWeb(textBoxPubSearch.Text.Trim());
                    if (anPub2 != null)
                    {
                        textBoxPubID.Text = anPub2.PublisherID.ToString();
                        textBoxPubName.Text = anPub2.PublisherName;
                        textBoxPubWeb.Text = anPub2.PublisherWeb;
                    }
                    else
                    {
                        MessageBox.Show("Publisher Not Found!", "Invalid Publisher Web");
                        textBoxPubSearch.Clear();
                        textBoxPubSearch.Focus();
                    }
                    break;
            }
        }

        private void buttonPubDelete_Click(object sender, EventArgs e)
        {
            string tempId = textBoxPubID.Text.Trim();
            if (!Validator.IsValidId(tempId, 3))
            {
                MessageBox.Show("Invalid Publisher ID", "Error");
                textBoxPubID.Clear();
                textBoxPubID.Focus();
                textBoxPubName.Clear();
                textBoxPubWeb.Clear();
                return;
            }

            Publisher pub = new Publisher();
            if (pub != null)
            {
                pub.PublisherID = Convert.ToInt32(textBoxPubID.Text);
                pub.PublisherName = textBoxPubName.Text.Trim();
                pub.PublisherWeb = textBoxPubWeb.Text.Trim();

            }
            else
            {
                MessageBox.Show("Publisher Not Found!", "Invalid Publisher Info");
                textBoxPubSearch.Clear();
                textBoxPubSearch.Focus();
            }
            pub.DeletePublisher(pub);
            textBoxPubID.Clear();
            textBoxPubName.Clear();
            textBoxPubWeb.Clear();
            MessageBox.Show("Publisher Info DELETED Successfully!", "Confirmation");
        }


        
        //////////////////
        /// Form Load ////
        //////////////////
        private void MainForm_Load(object sender, EventArgs e)
        {

            dsCustomer = new DataSet("CustomerDB");
            dtCustomer = new DataTable("Customers");
            dsCustomer.Tables.Add(dtCustomer);

            dtCustomer.Columns.Add("CustomerID", typeof(Int32));
            dtCustomer.Columns.Add("CustomerName", typeof(string));
            dtCustomer.Columns.Add("StreetName", typeof(string));
            dtCustomer.Columns.Add("Province", typeof(string));
            dtCustomer.Columns.Add("City", typeof(string));
            dtCustomer.Columns.Add("PostalCode", typeof(string));
            dtCustomer.Columns.Add("ContactName", typeof(string));
            dtCustomer.Columns.Add("ContactEmail", typeof(string));
            dtCustomer.Columns.Add("CreditLimit", typeof(Int32));
            dtCustomer.PrimaryKey = new DataColumn[] { dtCustomer.Columns["CustomerID"] };
            dtCustomer.Columns["CustomerID"].AutoIncrement = true;
            dtCustomer.Columns["CustomerID"].AutoIncrementStep = 1;
            dtCustomer.Columns["CustomerID"].AutoIncrementSeed = 111111;

            SDA = new SqlDataAdapter("SELECT * FROM Customers", UtilityDB.ConnectDB());
            SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(SDA);



            //ORDER CLERK CUSTOMER ID 
            Customer cuscus = new Customer();
            List<Customer> listCusCus = new List<Customer>();
            listCusCus = cuscus.GetCustomerList();
            if (listCusCus != null)
            {
                foreach (Customer anCusCus in listCusCus)
                {
                    comboBoxCustomerID.Items.Add(anCusCus.CustomerID.ToString() + " " + anCusCus.CustomerName);
                }

            }
            
            
            //ORDER CLERK EMPLOYEE ID 
            Employee empemp = new Employee();
            List<Employee> listEmpEmp = new List<Employee>();
            listEmpEmp = empemp.GetEmployeeList();
            if (listEmpEmp != null)
            {
                foreach (Employee anempemp in listEmpEmp)
                {
                    comboBoxEmployeeID.Items.Add(anempemp.EmployeeID.ToString() + " " + anempemp.EmployeeFName + " " + anempemp.EmployeeLName);
                }

            }

            //ORDER CLERK ISBN 
            Book bukbuk = new Book();
            List<Book> listBukBukp = new List<Book>();
            listBukBukp = bukbuk.GetBookList();
            if (listBukBukp != null)
            {
                foreach (Book anBukBuk in listBukBukp)
                {
                    comboBoxISBN.Items.Add(anBukBuk.BookISBN.ToString());
                }

            }
                
            
            //ORDER CLERK DATES RESET
            OrderDate.ResetText();
            RequiredDate.ResetText();
            ShippingDate.ResetText();
            }

            

        /////////////////////////
        /// Customer Section ////
        /////////////////////////
        private void buttonCusListDS_Click(object sender, EventArgs e)
        {
            dataGridViewCustomerDS.Visible = true;
            dataGridViewCustomerDB.Visible = false;
            SDA.Fill(dsCustomer.Tables["Customers"]);
            dataGridViewCustomerDS.DataSource = dsCustomer.Tables["Customers"];
            
        }

        private void buttonCusClear_Click(object sender, EventArgs e)
        {
            textBoxCusID.ReadOnly = false;
            textBoxCusID.Clear();
            textBoxCusName.Clear();
            textBoxCusStreet.Clear();
            textBoxCusProvince.Clear();
            textBoxCusCity.Clear();
            maskedTextBoxCusPostal.Clear();
            textBoxCusContactName.Clear();
            textBoxCusContactEmail.Clear();
            textBoxCusCreditLimit.Clear();
            textBoxCusSearch.Clear();
        }

        private void buttonCusSave_Click(object sender, EventArgs e)
        {
            //Check Customer ID
            string tempId = textBoxCusID.Text.Trim();
            if (Validator.IsEmpty(tempId))
            {
                MessageBox.Show("Customer ID Cannot Be Empty!", "Error");
                textBoxCusID.Clear();
                textBoxCusID.Focus();
                return;
            }

            if (!Validator.IsValidId(tempId, 7))
            {
                MessageBox.Show("Invalid Customer ID", "Error");
                textBoxCusID.Clear();
                textBoxCusID.Focus();
                return;
            }

            int cusId = Convert.ToInt32(textBoxCusID.Text);
            DataRow drCustomer = dtCustomer.Rows.Find(cusId);
            if (drCustomer != null)
            {
                MessageBox.Show("This Customer ID Already Exists!", "Duplicate Customer ID");
                textBoxCusID.Clear();
                textBoxCusID.Focus();
                return;
            }

            //Check Customer Name
            string tempName = textBoxCusName.Text.Trim();
            if (Validator.IsEmpty(tempName))
            {
                MessageBox.Show("Customer Name Cannot Be Empty!", "Error");
                textBoxCusName.Clear();
                textBoxCusName.Focus();
                return;
            }

            //Check Customer Street
            string tempCustomerStreet = textBoxCusStreet.Text.Trim();
            if (Validator.IsEmpty(tempCustomerStreet))
            {
                MessageBox.Show("Customer Street Cannot Be Empty!", "Error");
                textBoxCusStreet.Clear();
                textBoxCusStreet.Focus();
                return;
            }

            //Check Customer Province
            string tempProvince = textBoxCusProvince.Text.Trim();
            if (Validator.IsEmpty(tempProvince))
            {
                MessageBox.Show("Province Cannot Be Empty!", "Error");
                textBoxCusProvince.Clear();
                textBoxCusProvince.Focus();
                return;
            }

            //Check Customer City
            string tempCity = textBoxCusCity.Text.Trim();
            if (Validator.IsEmpty(tempCity))
            {
                MessageBox.Show("City Cannot Be Empty!", "Error");
                textBoxCusCity.Clear();
                textBoxCusCity.Focus();
                return;
            }

            //Check Customer Postal
            string tempPostal = maskedTextBoxCusPostal.Text.Trim();
            if (Validator.IsEmpty(tempPostal))
            {
                MessageBox.Show("Postal Code Cannot Be Empty!", "Error");
                maskedTextBoxCusPostal.Clear();
                maskedTextBoxCusPostal.Focus();
                return;
            }

            //Check Customer Contact Name
            string tempContactName = textBoxCusContactName.Text.Trim();
            if (Validator.IsEmpty(tempContactName))
            {
                MessageBox.Show("Contact Name Cannot Be Empty!", "Error");
                textBoxCusContactName.Clear();
                textBoxCusContactName.Focus();
                return;
            }

            //Check Customer Contact Email
            string tempContactEmail = textBoxCusContactEmail.Text.Trim();
            if (Validator.IsEmpty(tempContactEmail))
            {
                MessageBox.Show("Contact Email Cannot Be Empty!", "Error");
                textBoxCusContactEmail.Clear();
                textBoxCusContactEmail.Focus();
                return;
            }

            if (!Validator.IsValidEmail(textBoxCusContactEmail.Text, "E-mail"))
            {
                //MessageBox.Show("Invalid Contact Email Address!", "Error");
                textBoxCusContactEmail.Clear();
                textBoxCusContactEmail.Focus();
                return;
            }

            //Check Credit Limit
            string tempCreditLimit = textBoxCusCreditLimit.Text.Trim();
            if (Validator.IsEmpty(tempCreditLimit))
            {
                MessageBox.Show("Credit Limit Cannot Be Empty!", "Error");
                textBoxCusCreditLimit.Clear();
                textBoxCusCreditLimit.Focus();
                return;
            }

            DataRow dr = dtCustomer.NewRow();
            dr["CustomerID"] = Convert.ToInt32(textBoxCusID.Text.Trim());
            dr["CustomerName"] = textBoxCusName.Text.Trim();
            dr["StreetName"] = textBoxCusStreet.Text.Trim();
            dr["Province"] = textBoxCusProvince.Text.Trim();
            dr["City"] = textBoxCusCity.Text.Trim();
            dr["PostalCode"] = maskedTextBoxCusPostal.Text.Trim();
            dr["ContactName"] = textBoxCusContactName.Text.Trim();
            dr["ContactEmail"] = textBoxCusContactEmail.Text.Trim();
            dr["CreditLimit"] = Convert.ToInt64(textBoxCusCreditLimit.Text.Trim());

            dsCustomer.Tables["Customers"].Rows.Add(dr);
            


            MessageBox.Show("Customer Info Successfully SAVED In DataSet!", "Confirmation");

            textBoxCusID.Clear();
            textBoxCusName.Clear();
            textBoxCusStreet.Clear();
            textBoxCusProvince.Clear();
            textBoxCusCity.Clear();
            maskedTextBoxCusPostal.Clear();
            textBoxCusContactName.Clear();
            textBoxCusContactEmail.Clear();
            textBoxCusCreditLimit.Clear();
        }



        private void buttonCusListDB_Click(object sender, EventArgs e)
        {
            dataGridViewCustomerDB.Refresh();
            dataGridViewCustomerDB.Visible = true;
            dataGridViewCustomerDS.Visible = false;

            Customer cus = new Customer();
            dataGridViewCustomerDB.DataSource = cus.GetCustomerList();
        }

        private void buttonCusUpdateDB_Click(object sender, EventArgs e)
        {
            SDA.Update(dsCustomer.Tables["Customers"]);
            MessageBox.Show("Customer Database Has Been Successfully UPDATED!", "Update Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void buttonCusDelete_Click(object sender, EventArgs e)
        {
            //Check Customer ID
            string tempId = textBoxCusID.Text.Trim();
            if (Validator.IsEmpty(tempId))
            {
                MessageBox.Show("Customer ID Cannot Be Empty!", "Error");
                textBoxCusID.Clear();
                textBoxCusID.Focus();
                return;
            }

            if (!Validator.IsValidId(tempId, 7))
            {
                MessageBox.Show("Invalid Customer ID", "Error");
                textBoxCusID.Clear();
                textBoxCusID.Focus();
                return;
            }

            if ((MessageBox.Show("Do You Want To Delete This Customer?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
            {
                int cusId = Convert.ToInt32(textBoxCusID.Text);
                DataRow drCustomer = dtCustomer.Rows.Find(cusId);
                drCustomer.Delete();
                //dsCustomer.Tables["Customers"].Rows.Find(cusId).Delete();
                MessageBox.Show("Customer Successfully Deleted!", "Delete", MessageBoxButtons.OK);

                textBoxCusID.ReadOnly = false;
                textBoxCusID.Clear();
                textBoxCusName.Clear();
                textBoxCusStreet.Clear();
                textBoxCusProvince.Clear();
                textBoxCusCity.Clear();
                maskedTextBoxCusPostal.Clear();
                textBoxCusContactName.Clear();
                textBoxCusContactEmail.Clear();
                textBoxCusCreditLimit.Clear();
                textBoxCusSearch.Clear();
            }
        }

        private void buttonCusUpdate_Click(object sender, EventArgs e)
        {

            //Check Customer Name
            string tempName = textBoxCusName.Text.Trim();
            if (Validator.IsEmpty(tempName))
            {
                MessageBox.Show("Customer Name Cannot Be Empty!", "Error");
                textBoxCusName.Clear();
                textBoxCusName.Focus();
                return;
            }

            //Check Customer Street
            string tempCustomerStreet = textBoxCusStreet.Text.Trim();
            if (Validator.IsEmpty(tempCustomerStreet))
            {
                MessageBox.Show("Customer Street Cannot Be Empty!", "Error");
                textBoxCusStreet.Clear();
                textBoxCusStreet.Focus();
                return;
            }

            //Check Customer Province
            string tempProvince = textBoxCusProvince.Text.Trim();
            if (Validator.IsEmpty(tempProvince))
            {
                MessageBox.Show("Province Cannot Be Empty!", "Error");
                textBoxCusProvince.Clear();
                textBoxCusProvince.Focus();
                return;
            }

            //Check Customer City
            string tempCity = textBoxCusCity.Text.Trim();
            if (Validator.IsEmpty(tempCity))
            {
                MessageBox.Show("City Cannot Be Empty!", "Error");
                textBoxCusCity.Clear();
                textBoxCusCity.Focus();
                return;
            }

            //Check Customer Postal
            string tempPostal = maskedTextBoxCusPostal.Text.Trim();
            if (Validator.IsEmpty(tempPostal))
            {
                MessageBox.Show("Postal Code Cannot Be Empty!", "Error");
                maskedTextBoxCusPostal.Clear();
                maskedTextBoxCusPostal.Focus();
                return;
            }

            //Check Customer Contact Name
            string tempContactName = textBoxCusContactName.Text.Trim();
            if (Validator.IsEmpty(tempContactName))
            {
                MessageBox.Show("Contact Name Cannot Be Empty!", "Error");
                textBoxCusContactName.Clear();
                textBoxCusContactName.Focus();
                return;
            }

            //Check Customer Contact Email
            string tempContactEmail = textBoxCusContactEmail.Text.Trim();
            if (Validator.IsEmpty(tempContactEmail))
            {
                MessageBox.Show("Contact Email Cannot Be Empty!", "Error");
                textBoxCusContactEmail.Clear();
                textBoxCusContactEmail.Focus();
                return;
            }

            if (!Validator.IsValidEmail(textBoxCusContactEmail.Text, "E-mail"))
            {
                //MessageBox.Show("Invalid Contact Email Address!", "Error");
                textBoxCusContactEmail.Clear();
                textBoxCusContactEmail.Focus();
                return;
            }

            //Check Credit Limit
            string tempCreditLimit = textBoxCusCreditLimit.Text.Trim();
            if (Validator.IsEmpty(tempCreditLimit))
            {
                MessageBox.Show("Credit Limit Cannot Be Empty!", "Error");
                textBoxCusCreditLimit.Clear();
                textBoxCusCreditLimit.Focus();
                return;
            }

            int cusId = Convert.ToInt32(textBoxCusID.Text);
            DataRow drCustomer = dtCustomer.Rows.Find(cusId);
            if (drCustomer != null)
            {
                drCustomer["CustomerID"] = Convert.ToInt32(textBoxCusID.Text.Trim());
                drCustomer["CustomerName"] = textBoxCusName.Text.Trim();
                drCustomer["StreetName"] = textBoxCusStreet.Text.Trim();
                drCustomer["Province"] = textBoxCusProvince.Text.Trim();
                drCustomer["City"] = textBoxCusCity.Text.Trim();
                drCustomer["PostalCode"] = maskedTextBoxCusPostal.Text.Trim();
                drCustomer["ContactName"] = textBoxCusContactName.Text.Trim();
                drCustomer["ContactEmail"] = textBoxCusContactEmail.Text.Trim();
                drCustomer["CreditLimit"] = Convert.ToInt64(textBoxCusCreditLimit.Text.Trim());

                MessageBox.Show(drCustomer.RowState.ToString());

            }
            else
            {
                MessageBox.Show("Customer ID Not Found!", "Invalid Customer");
            }
        }

        private void buttonCusSearch_Click(object sender, EventArgs e)
        {
            textBoxCusID.ReadOnly = true;
            int indexSelected = comboBoxCusSearch.SelectedIndex;
            switch (indexSelected)
            {
                case 0:
                    //Search by Customer ID
                    string tempId = textBoxCusSearch.Text.Trim();
                    if (Validator.IsEmpty(tempId))
                    {
                        MessageBox.Show("Search Box Cannot Be Empty!", "Error");
                        textBoxPubSearch.Clear();
                        textBoxPubSearch.Focus();
                        return;
                    }
                    int searchId = Convert.ToInt32(textBoxCusSearch.Text);
                    DataRow drCustomer = dtCustomer.Rows.Find(searchId);
                    if (drCustomer != null)
                    {
                        textBoxCusID.Text = drCustomer["CustomerID"].ToString();
                        textBoxCusName.Text = drCustomer["CustomerName"].ToString();
                        textBoxCusStreet.Text = drCustomer["StreetName"].ToString();
                        textBoxCusProvince.Text = drCustomer["Province"].ToString();
                        textBoxCusCity.Text = drCustomer["City"].ToString();
                        maskedTextBoxCusPostal.Text = drCustomer["PostalCode"].ToString();
                        textBoxCusContactName.Text = drCustomer["ContactName"].ToString();
                        textBoxCusContactEmail.Text = drCustomer["ContactEmail"].ToString();
                        textBoxCusCreditLimit.Text = drCustomer["CreditLimit"].ToString();
                    }

                    else
                    {
                        MessageBox.Show("Customer Not Found", "Wrong ID");
                        textBoxCusID.ReadOnly = false;
                        textBoxCusID.Clear();
                        textBoxCusName.Clear();
                        textBoxCusStreet.Clear();
                        textBoxCusProvince.Clear();
                        textBoxCusCity.Clear();
                        maskedTextBoxCusPostal.Clear();
                        textBoxCusContactName.Clear();
                        textBoxCusContactEmail.Clear();
                        textBoxCusCreditLimit.Clear();
                        textBoxCusSearch.Clear();
                    }
                    break;

                //case 1:
                //    //Search by Customer Name
                //    string tempName = textBoxCusSearch.Text.Trim();
                //    if (Validator.IsEmpty(tempName))
                //    {
                //        MessageBox.Show("Customer Name Cannot Be Empty!", "Error");
                //        textBoxPubSearch.Clear();
                //        textBoxPubSearch.Focus();
                //        return;
                //    }
                //    string searchName = Convert.ToString(textBoxCusSearch.Text);
                //    DataRow drCustomerName = dtCustomer.Rows.Find(searchName);
                //    if (drCustomerName != null)
                //    {
                //        textBoxCusID.Text = drCustomerName["CustomerID"].ToString();
                //        textBoxCusName.Text = drCustomerName["CustomerName"].ToString();
                //        textBoxCusStreet.Text = drCustomerName["StreetName"].ToString();
                //        textBoxCusProvince.Text = drCustomerName["Province"].ToString();
                //        textBoxCusCity.Text = drCustomerName["City"].ToString();
                //        maskedTextBoxCusPostal.Text = drCustomerName["PostalCode"].ToString();
                //        textBoxCusContactName.Text = drCustomerName["ContactName"].ToString();
                //        textBoxCusContactEmail.Text = drCustomerName["ContactEmail"].ToString();
                //        textBoxCusCreditLimit.Text = drCustomerName["CreditLimit"].ToString();
                //    }

                //    else
                //    {
                //        MessageBox.Show("Customer Not Found", "Wrong ID");
                //        textBoxCusID.ReadOnly = false;
                //        textBoxCusID.Clear();
                //        textBoxCusName.Clear();
                //        textBoxCusStreet.Clear();
                //        textBoxCusProvince.Clear();
                //        textBoxCusCity.Clear();
                //        maskedTextBoxCusPostal.Clear();
                //        textBoxCusContactName.Clear();
                //        textBoxCusContactEmail.Clear();
                //        textBoxCusCreditLimit.Clear();
                //        textBoxCusSearch.Clear();
                //    }
                //    break;
            }
        }

        private void Forgot_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If You Forgot Your USER ID or Your PASSWORD \nContact The Adminstration at 'support@hitech.com'.", "Support");
        }

        private void buttonAboutUS_Click(object sender, EventArgs e)
        {
            Form About = new AboutForm();
            About.ShowDialog();
        }










        /////////////////////////////////////////////
        //////////////                 //////////////
        ////////////                     ////////////
        ////////      ENTITY FRAMEWORK      /////////
        ////////////                     ////////////
        //////////////                 //////////////
        /////////////////////////////////////////////

        Entities dbEntities = new Entities();

        //////////////////////
        /// Order Section ////
        //////////////////////


        //LIST ORDERS
        private void buttonOrders_Click(object sender, EventArgs e)
        {
            //var listOrders = (from ord in dbEntities.Orders select ord).ToList<Order>();
            var listOrders = (from Orders in dbEntities.Orders
                              from OrderLine in dbEntities.OrderLines.Where(x => x.OrderID == Orders.OrderID)
                              select new
                              {
                                  OrderID = Orders.OrderID,
                                  OrderDate = Orders.OrderDate,
                                  OrderType = Orders.OrderType,
                                  RequiredDate = Orders.RequiredDate,
                                  ShippingDate = Orders.ShippingDate,
                                  OrderStatus = Orders.OrderStatus,
                                  CustomerID = Orders.CustomerID,
                                  EmployeeID = Orders.EmployeeID,
                                  ISBN = OrderLine.ISBN,
                                  Quantity = OrderLine.QuantityOrdered
                              });


            listViewOrders.Items.Clear();
            foreach (var anOrd in listOrders)
            {
                ListViewItem ordItem = new ListViewItem(anOrd.OrderID.ToString());
                ordItem.SubItems.Add(anOrd.OrderDate);
                ordItem.SubItems.Add(anOrd.OrderType);
                ordItem.SubItems.Add(anOrd.RequiredDate);
                ordItem.SubItems.Add(anOrd.ShippingDate);
                ordItem.SubItems.Add(anOrd.OrderStatus);
                ordItem.SubItems.Add(anOrd.CustomerID.ToString());
                ordItem.SubItems.Add(anOrd.EmployeeID.ToString());
                ordItem.SubItems.Add(anOrd.ISBN.ToString());
                ordItem.SubItems.Add(anOrd.Quantity.ToString());
                listViewOrders.Items.Add(ordItem);
                }



        }

        //CLEAR TEXT BOXES
        private void buttonOrderClear_Click(object sender, EventArgs e)
        {
            textBoxOrderID.ReadOnly = false;
            textBoxOrderID.Clear();
            comboBoxOrderBy.SelectedIndex = -1;
            comboBoxOrderStatus.SelectedIndex = -1;
            comboBoxCustomerID.SelectedIndex = -1;
            comboBoxEmployeeID.SelectedIndex = -1;
            comboBoxISBN.SelectedIndex = -1;
            textBoxQuantityOrdered.Clear();
            OrderDate.ResetText();
            RequiredDate.ResetText();
            ShippingDate.ResetText();
            textBoxOrderSearch.Clear();
            comboBoxISBN.Enabled = true;
        }

        //LIST ORDERS
        private void buttonSaveOrder_Click(object sender, EventArgs e)
        {

            //Check If The Enetered Order ID is 7 Digit
            string tempId = textBoxOrderID.Text.Trim();
            if (!Validator.IsValidId(tempId, 7))
            {
                MessageBox.Show("Invalid Order ID!", "Error");
                textBoxOrderID.Clear();
                textBoxOrderID.Focus();
                return;
            }
            //Check Duplicate Order ID
            int tempId32 = Convert.ToInt32(textBoxOrderID.Text);
            if (dbEntities.Orders.Any(o => o.OrderID == tempId32))
            {
                MessageBox.Show("This Order ID Already Exists!", "Duplicate Order ID");
                textBoxOrderID.Clear();
                textBoxOrderID.Focus();
                return;
            }

            //Check If Order By Is Selected
            if (comboBoxOrderBy.SelectedIndex == -1)
            {
                MessageBox.Show("Order By Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Order Status Is Selected
            if (comboBoxOrderStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Order Status Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Order Customer ID Is Selected
            if (comboBoxCustomerID.SelectedIndex == -1)
            {
                MessageBox.Show("Customer ID Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Order Employee ID Is Selected
            if (comboBoxEmployeeID.SelectedIndex == -1)
            {
                MessageBox.Show("Employee ID Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Order ISBN Is Selected
            if (comboBoxISBN.SelectedIndex == -1)
            {
                MessageBox.Show("ISBN Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Quantity Is Not Empty
            string tempQuantity = textBoxQuantityOrdered.Text.Trim();
            if (Validator.IsEmpty(tempQuantity))
            {
                MessageBox.Show("Quantity Cannot Be Empty!", "Error");
                textBoxQuantityOrdered.Clear();
                textBoxQuantityOrdered.Focus();
                return;
            }

            //Check If Quantity Is Available In Inventory
            Book bukQuantity = new Book();
            bukQuantity = bukQuantity.GetBookQuantity(Convert.ToInt64(comboBoxISBN.Text));
            string QOH = bukQuantity.BookQOH.ToString();
            if (Convert.ToInt32(QOH) < Convert.ToInt32(textBoxQuantityOrdered.Text))
            {
                MessageBox.Show("The Quantity Of Selected Book In Inventory Is " + QOH + ", Which Is Less Than Then Entered Quantity!", "Invalid Employee ID");
                textBoxUserEmpID.Clear();
                textBoxUserEmpID.Focus();
                return;
            }




            //Valid data
            Order ord = new Order();
            ord.OrderID = Convert.ToInt32(textBoxOrderID.Text);
            ord.OrderDate = OrderDate.Text.Trim();
            ord.OrderType = comboBoxOrderBy.Text.ToString();
            ord.RequiredDate = RequiredDate.Text.Trim();
            ord.ShippingDate = ShippingDate.Text.Trim();
            ord.OrderStatus = comboBoxOrderStatus.Text.ToString();
            ord.CustomerID = Convert.ToInt32(comboBoxCustomerID.Text.Substring(0, 7));
            ord.EmployeeID = Convert.ToInt32(comboBoxEmployeeID.Text.Substring(0, 3));
            

            OrderLine ordli = new OrderLine();
            ordli.OrderID = Convert.ToInt32(textBoxOrderID.Text);
            ordli.ISBN = Convert.ToInt64(comboBoxISBN.Text);
            ordli.QuantityOrdered = Convert.ToInt32(textBoxQuantityOrdered.Text);

            dbEntities.Orders.Add(ord);
            dbEntities.OrderLines.Add(ordli);
            dbEntities.SaveChanges();
            MessageBox.Show("Order Info SAVED Successfully! \nYou Need To Contact An Inventory Controller \nTo Deducte " + Convert.ToInt32(textBoxQuantityOrdered.Text) + " Numbers From QOH of The Book  \nWith The ISBN Of " + Convert.ToInt64(comboBoxISBN.Text), "Confirmation");
            textBoxOrderID.Clear();
            comboBoxOrderBy.SelectedIndex = -1;
            comboBoxOrderStatus.SelectedIndex = -1;
            comboBoxCustomerID.SelectedIndex = -1;
            comboBoxEmployeeID.SelectedIndex = -1;
            comboBoxISBN.SelectedIndex = -1;
            textBoxQuantityOrdered.Clear();
            OrderDate.ResetText();
            RequiredDate.ResetText();
            ShippingDate.ResetText();
            
        }

        private void buttonSearchOrder_Click(object sender, EventArgs e)
        {
            textBoxOrderID.ReadOnly = true;
            comboBoxISBN.Enabled = false;
            int indexSelected = comboBoxOrderSearch.SelectedIndex;
            switch (indexSelected)
            {
                case 0:
                    //Search by Order ID
                    string tempId = textBoxOrderSearch.Text.Trim();
                    if (Validator.IsEmpty(tempId))
                    {
                        MessageBox.Show("Search Box Cannot Be Empty!", "Error");
                        textBoxOrderSearch.Clear();
                        textBoxOrderSearch.Focus();
                        return;
                    }

                    int ordID = Convert.ToInt32(textBoxOrderSearch.Text);
                    var listOrders = (from Orders in dbEntities.Orders
                                          //from OrderLine in dbEntities.OrderLines.Where(x => x.OrderID == Orders.OrderID)
                                      join OrderLine in dbEntities.OrderLines
                                      on Orders.OrderID equals OrderLine.OrderID
                                      where Orders.OrderID == ordID
                                      select new
                                      {
                                          OrderID = Orders.OrderID,
                                          OrderDate = Orders.OrderDate,
                                          OrderType = Orders.OrderType,
                                          RequiredDate = Orders.RequiredDate,
                                          ShippingDate = Orders.ShippingDate,
                                          OrderStatus = Orders.OrderStatus,
                                          CustomerID = Orders.CustomerID,
                                          EmployeeID = Orders.EmployeeID,
                                          ISBN = OrderLine.ISBN,
                                          Quantity = OrderLine.QuantityOrdered
                                      });

                    Order ord = dbEntities.Orders.Find(ordID);
                    
                    if (ord != null)
                    {
                        OrderLine ordl = dbEntities.OrderLines.SingleOrDefault(m => m.OrderID == ordID);

                        textBoxOrderID.Text = ord.OrderID.ToString();
                        comboBoxOrderBy.Text = ord.OrderType.ToString();
                        comboBoxOrderStatus.Text = ord.OrderStatus.ToString();
                        comboBoxCustomerID.SelectedIndex = comboBoxCustomerID.FindString(ord.CustomerID.ToString());
                        comboBoxEmployeeID.SelectedIndex = comboBoxEmployeeID.FindString(ord.EmployeeID.ToString());
                        comboBoxISBN.Text = ordl.ISBN.ToString();
                        textBoxQuantityOrdered.Text = ordl.QuantityOrdered.ToString();
                        OrderDate.Text = ord.OrderDate.ToString();
                        RequiredDate.Text = ord.RequiredDate.ToString();
                        ShippingDate.Text = ord.ShippingDate.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Order Not Found", "Invalid ID");
                        textBoxOrderID.ReadOnly = false;
                        textBoxOrderID.Clear();
                        comboBoxOrderBy.SelectedIndex = -1;
                        comboBoxOrderStatus.SelectedIndex = -1;
                        comboBoxCustomerID.SelectedIndex = -1;
                        comboBoxEmployeeID.SelectedIndex = -1;
                        comboBoxISBN.Enabled = true;
                        comboBoxISBN.SelectedIndex = -1;             
                        textBoxQuantityOrdered.Clear();
                        OrderDate.ResetText();
                        RequiredDate.ResetText();
                        ShippingDate.ResetText();
                        textBoxOrderSearch.Clear();
                    }
                    break;
            }
        }

        private void buttonUpdateOrder_Click(object sender, EventArgs e)
        {


            //Check If Order By Is Selected
            if (comboBoxOrderBy.SelectedIndex == -1)
            {
                MessageBox.Show("Order By Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Order Status Is Selected
            if (comboBoxOrderStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Order Status Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Order Customer ID Is Selected
            if (comboBoxCustomerID.SelectedIndex == -1)
            {
                MessageBox.Show("Customer ID Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Order Employee ID Is Selected
            if (comboBoxEmployeeID.SelectedIndex == -1)
            {
                MessageBox.Show("Employee ID Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Order ISBN Is Selected
            if (comboBoxISBN.SelectedIndex == -1)
            {
                MessageBox.Show("ISBN Field Cannot Be Empty!", "Error");
                return;
            }

            //Check If Quantity Is Not Empty
            string tempQuantity = textBoxQuantityOrdered.Text.Trim();
            if (Validator.IsEmpty(tempQuantity))
            {
                MessageBox.Show("Quantity Cannot Be Empty!", "Error");
                textBoxQuantityOrdered.Clear();
                textBoxQuantityOrdered.Focus();
                return;
            }

            //Check If Quantity Is Available In Inventory
            //Book bukQuantity = new Book();
            //bukQuantity = bukQuantity.GetBookQuantity(Convert.ToInt64(comboBoxISBN.Text));
            //string QOH = bukQuantity.BookQOH.ToString();
            //if (Convert.ToInt32(QOH) < Convert.ToInt32(textBoxQuantityOrdered.Text))
            //{
            //    MessageBox.Show("The Quantity Of Selected Book In Inventory Is " + QOH + ", Which Is Less Than Then Entered Quantity!", "Invalid Employee ID");
            //    textBoxUserEmpID.Clear();
            //    textBoxUserEmpID.Focus();
            //    return;
            //}




            //Valid data
            int temp = Convert.ToInt32(textBoxOrderID.Text);
            Order ord = new Order();
            
            ord = dbEntities.Orders.Find(temp);
            if (ord != null)
            {
                //ord.OrderID = Convert.ToInt32(textBoxOrderID.Text);
                ord.OrderDate = OrderDate.Text.Trim();
                ord.OrderType = comboBoxOrderBy.Text.ToString();
                ord.RequiredDate = RequiredDate.Text.Trim();
                ord.ShippingDate = ShippingDate.Text.Trim();
                ord.OrderStatus = comboBoxOrderStatus.Text.ToString();
                ord.CustomerID = Convert.ToInt32(comboBoxCustomerID.Text.Substring(0, 7));
                ord.EmployeeID = Convert.ToInt32(comboBoxEmployeeID.Text.Substring(0, 3));


                OrderLine ordli = new OrderLine();
                ordli = dbEntities.OrderLines.SingleOrDefault(m => m.OrderID == temp);
                //ordli.OrderID = Convert.ToInt32(textBoxOrderID.Text);
                //ordli.ISBN = Convert.ToInt64(comboBoxISBN.Text);
                ordli.QuantityOrdered = Convert.ToInt32(textBoxQuantityOrdered.Text);

                //dbEntities.Orders.Add(ord);
                //dbEntities.OrderLines.Add(ordli);
                dbEntities.SaveChanges();
                
                MessageBox.Show("Order Info Updated Successfully! \nYou Need To Contact An Inventory Controller \nTo Validate " + Convert.ToInt32(textBoxQuantityOrdered.Text) + " Numbers From QOH of The Book  \nWith The ISBN Of " + Convert.ToInt64(comboBoxISBN.Text), "Confirmation");
            }
            else 
            {
                MessageBox.Show("Order Info Not Found!", "Invalid");
            }
        }

        private void buttonOrderClerkReload_Click(object sender, EventArgs e)
        {
            //ORDER CLERK CUSTOMER ID 
            Customer cuscus = new Customer();
            List<Customer> listCusCus = new List<Customer>();
            listCusCus = cuscus.GetCustomerList();
            if (listCusCus != null)
            {
                comboBoxCustomerID.Items.Clear();
                foreach (Customer anCusCus in listCusCus)
                {
                    comboBoxCustomerID.Items.Add(anCusCus.CustomerID.ToString() + " " + anCusCus.CustomerName);
                }

            }


            //ORDER CLERK EMPLOYEE ID 
            Employee empemp = new Employee();
            List<Employee> listEmpEmp = new List<Employee>();
            listEmpEmp = empemp.GetEmployeeList();
            if (listEmpEmp != null)
            {
                comboBoxEmployeeID.Items.Clear();
                foreach (Employee anempemp in listEmpEmp)
                {
                    comboBoxEmployeeID.Items.Add(anempemp.EmployeeID.ToString() + " " + anempemp.EmployeeFName + " " + anempemp.EmployeeLName);
                }

            }

            //ORDER CLERK ISBN 
            Book bukbuk = new Book();
            List<Book> listBukBukp = new List<Book>();
            listBukBukp = bukbuk.GetBookList();
            if (listBukBukp != null)
            {
                comboBoxISBN.Items.Clear();
                foreach (Book anBukBuk in listBukBukp)
                {
                    comboBoxISBN.Items.Add(anBukBuk.BookISBN.ToString());
                }

            }
        }
    }


}
