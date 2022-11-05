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

namespace ClinicApp.Forms.Governments
{
    public partial class FormAddGovernment : Form
    {
        public FormAddGovernment()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        public string id = "";
        public FormShowGovernments refreshForm;
        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل المحافظة ");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into Governments (name) values (@name)", adoClass.sqlcn);
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
                    MessageBox.Show("حدد المحافظة المراد تعديلها");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم المحافظة الجديد");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update Governments set name = @name Where id = '" + id + "'", adoClass.sqlcn);

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
                refreshForm.loadTable("select * from Governments");
            }


            txtName.Text = "";
            id = "";
        }
    }
}
