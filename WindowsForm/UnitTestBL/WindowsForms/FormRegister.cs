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
    public partial class FormRegister : Form
    {


        bool IsNewUser;
        SecurityDTO Security = null;
        int UserId;

        public FormRegister(bool isNew, int userId)
        {
            IsNewUser = isNew;
            UserId = userId;
            
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
            if (IsNewUser)
                LoginBL.CurrentUser = new UserDTO();

            LoginBL.CurrentUser.Mail = tbLogin.Text;
            LoginBL.CurrentUser.FirstName = tbFirstName.Text;
            LoginBL.CurrentUser.LastName = tbLastName.Text;
            LoginBL.CurrentUser.RoleId = 4;
            if (rbMale.Checked)
                LoginBL.CurrentUser.Gender = "Male";
            else
                LoginBL.CurrentUser.Gender = "Female";
            LoginBL.CurrentUser.Address = tbAdress.Text;
            LoginBL.CurrentUser.Tel = tbTel.Text;
            LoginBL.CurrentUser.BankCard = tbBankCard.Text;


            SecurityDTO sd = new SecurityDTO();
          

            sd.KeyWord = tbKeyWord.Text;
            sd.UserId = LoginBL.CurrentUser.Id;
            if (tbPassword.Text != tbReenterPassword.Text)
            {
                MessageBox.Show("Paswwords not equel!");
                return;
            }

            sd.Password = tbPassword.Text;


            if (IsNewUser == false)
            {
                LoginBL.UpdateUser(LoginBL.CurrentUser, sd, pbImage.ImageLocation);
            }

            else
            {
                LoginBL.CreateNewUser(LoginBL.CurrentUser, sd, pbImage.ImageLocation);
            }
            Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            if (IsNewUser == false)
            {

                LoginBL.SetCurrentUser(UserId);
                Text = "Edit usrers info";

                tbLogin.Text = LoginBL.CurrentUser.Mail;
                tbFirstName.Text = LoginBL.CurrentUser.FirstName;
                tbLastName.Text = LoginBL.CurrentUser.LastName;
                // LoginBL.CurrentUser.RoleId = 4;
                string gen = LoginBL.CurrentUser.Gender.Replace(" ", "");
                if (gen == "Male")
                {
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                }
                else
                {
                    rbMale.Checked = false;
                    rbFemale.Checked = true;
                }
                tbAdress.Text = LoginBL.CurrentUser.Address;
                tbTel.Text =  LoginBL.CurrentUser.Tel;
                tbBankCard.Text = LoginBL.CurrentUser.BankCard;

                SecurityDTO sd = UsersBL.GetSecurityForUser(LoginBL.CurrentUser);

                tbPassword.Text = sd.Password;
                tbKeyWord.Text = sd.KeyWord;
                tbReenterPassword.Text = sd.Password;

                pbImage.Image = LoginBL.GetImageBinaryFromDb(LoginBL.CurrentUser.Id.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogImages = new OpenFileDialog();
            //For any other formats
            dialogImages.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (dialogImages.ShowDialog() == DialogResult.OK)
            {
                pbImage.ImageLocation = dialogImages.FileName;

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
