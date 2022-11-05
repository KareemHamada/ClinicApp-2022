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

namespace ClinicApp.Forms.Settings.Specializations
{
    public partial class FormAddSpecialization : Form
    {
        public FormAddSpecialization()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        public string id = "";
        public FormShowSpecializations refreshForm;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم التخصص");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into Specializations (name) values (@name)", adoClass.sqlcn);
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
                    MessageBox.Show("حدد التخصص المراد تعديله");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم التخصص");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update Specializations set name = @name Where id = '" + id + "'", adoClass.sqlcn);

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
                refreshForm.loadTable("select * from Specializations");
            }


            txtName.Text = "";
            id = "";
        }
    }
}
