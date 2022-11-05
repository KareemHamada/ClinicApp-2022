using ClinicApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicApp.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private SqlDataReader dr;

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("ادخل اسم المستخدم");
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("ادخل الباسورد");
                return;
            }


            cmd = new SqlCommand("Select * from Users where name = @name and password = @password", adoClass.sqlcn);
            dr = null;
            cmd.Parameters.AddWithValue("@name", txtUserName.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);

            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        declarations.userId = int.Parse(dr["id"].ToString());
                        declarations.name = dr["name"].ToString();
                        declarations.privilege = dr["privilege"].ToString();
                    }

                    this.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("فشل تسجيل الدخول");
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    return;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }
        }
    }
}
