using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;

namespace WindowsForms
{
    public partial class FormPersonalInfo : Form
    {
        public FormPersonalInfo()
        {
            InitializeComponent();
            dataGrid.DataSource = UsersBL.GetAllUsers();

        }

        private void btPersonalCabinet_Click(object sender, EventArgs e)
        {
            FormRegister fr = new FormRegister(false, LoginBL.CurrentUser.Id);
            fr.ShowDialog();
            dataGrid.DataSource = UsersBL.GetAllUsers();
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
