using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BL;




namespace WindowsForms
{
    public partial class FormLogin : Form
    {
              
        
        SecurityDTO Security = null;

        public FormLogin()
        {
            InitializeComponent();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            LoginBL.SetCurrentUser(tbLogin.Text);
            if (LoginBL.CurrentUser != null)
            {
                if (LoginBL.ChekPasswordForCurrentUser(tbPassword.Text))
                {
                    // MessageBox.Show("Welcome " + LoginBL.CurrentUser.FirstName + " " + LoginBL.CurrentUser.LastName + " !");
                    FormPersonalInfo pf = new FormPersonalInfo();
                    pf.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Password is not correct!");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("User not found ! Buy bUy");
                Close();
            }


           
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister fr = new FormRegister(true, 0);
            fr.ShowDialog();
        }
    }
}
