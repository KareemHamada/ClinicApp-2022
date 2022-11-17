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

namespace ClinicApp.Forms.Settings.Pharmacy
{
    public partial class FormAddTime : Form
    {
        public FormAddTime()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        public string id = "";
        public FormShowTimes refreshForm;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل العدد");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into timesTakeMedication (name,notes) values (@name,@notes)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);

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
                    MessageBox.Show("حدد العدد المراد تعديله");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل العدد");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update timesTakeMedication set name = @name,notes=@notes Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
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
                refreshForm.loadTable("select * from timesTakeMedication");
            }


            txtName.Text = "";
            txtNotes.Text = "";
            id = "";

            this.DialogResult = DialogResult.OK;
        }
    }
}
