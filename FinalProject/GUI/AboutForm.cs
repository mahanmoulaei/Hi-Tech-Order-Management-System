using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.GUI
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            txtAbout.AppendText("Hi-Tech Distribution Inc.");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("5740 Cavendish, Montreal, Quebec");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("H4W-2T8");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("Tel: (514) 709 - 9558");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("Fax: (514) 709 - 9558");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("Developed by: Mahan Moulaei");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("LaSalle College - Fall 2020");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("Teacher: Quang Hoang Cao");
        }

        private void buttonCloseAbout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
