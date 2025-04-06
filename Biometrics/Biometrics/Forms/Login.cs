using Biometrics.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biometrics.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txt_idnum.Text);
                string password = txt_password.Text;

                DatabaseClass databaseclass = new DatabaseClass();

                bool verified = databaseclass.verify_login(id, password);

                if (verified)
                {
                    Register register = new Register();
                    this.Hide();
                    register.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect ID and/or Password. Please try again.");
                    txt_idnum.Text = "";
                    txt_password.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Incorrect ID and/or Password. Please try again.", "Login");
                txt_idnum.Text = "";
                txt_password.Text = "";
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Capture capture = new Capture();
            this.Hide();
            capture.Show();
        }
    }
}
