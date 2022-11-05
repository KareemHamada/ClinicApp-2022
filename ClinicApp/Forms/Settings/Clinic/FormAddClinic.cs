using ClinicApp.Classes;
using ClinicApp.Forms.Settings.Clinic;
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

namespace ClinicApp.Settings.Clinic
{
    public partial class FormAddClinic : Form
    {
        public FormAddClinic()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        public string id = "";
        public FormShowClinic refreshForm;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم العيادة");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into Clinics (name) values (@name)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("تم الاضافة بنجاح");

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
                    MessageBox.Show("حدد العيادة المراد تعديلها");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم العيادة");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update Clinics set name = @name Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);

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
                refreshForm.loadTable("select * from Clinics");
            }
            

            txtName.Text = "";
            id = "";
        }
    }
}
