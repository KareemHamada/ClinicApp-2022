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

namespace ClinicApp.Forms.Users
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        public string id = "";
        public string doctorText = "";
        public FormShowUsers refreshForm;

        private void FormUsers_Load(object sender, EventArgs e)
        {
            

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم المستخدم");
                    return;
                }
                if (comboPriv.Text == "")
                {
                    MessageBox.Show("اختر الصلاحية");
                    return;
                }
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("ادخل الباسورد");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into Users (name,privilege,password,notes) values (@name,@privilege,@password,@notes)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@privilege", comboPriv.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("تم اضافة المستخدم بنجاح");

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
            else
            {
                if (id == "")
                {
                    MessageBox.Show("حدد العنصر المراد تعديلة");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم المستخدم");
                    return;
                }
                if (comboPriv.Text == "")
                {
                    MessageBox.Show("اختر الصلاحية");
                    return;
                }
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("ادخل الباسورد");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Update Users set name=@name,privilege=@privilege,password=@password,notes=@notes Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@privilege", comboPriv.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);



                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("تم التعديل بنجاح");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    adoClass.sqlcn.Close();
                }

                this.Close();
                refreshForm.loadTable("select * from Users");
            }

            txtName.Text = "";
            comboPriv.Text = "";
            txtPassword.Text = "";
            txtNotes.Text = "";

            id = "";
        }
    }
}
