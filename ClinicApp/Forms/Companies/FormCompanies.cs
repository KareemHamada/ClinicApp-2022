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

namespace ClinicApp.Forms.Companies
{
    public partial class FormCompanies : Form
    {
        public FormCompanies()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        public string id = "";
        public FormShowCompanies refreshForm;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الشركة");
                    return;
                }
                if (txtCompanyPay.Text == "")
                {
                    MessageBox.Show("ادخل نسبة تحمل الشركة");
                    return;
                }
             

                try
                {
                    cmd = new SqlCommand("Insert into Company (name,cPay,pPay,notes) values (@name,@cPay,@pPay,@notes)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@cPay", txtCompanyPay.Text);
                    cmd.Parameters.AddWithValue("@pPay", txtPatientPay.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("تم اضافة الشركة بنجاح");

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
                    MessageBox.Show("حدد الشركة المراد تعديلها");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الشركة");
                    return;
                }
                if (txtCompanyPay.Text == "")
                {
                    MessageBox.Show("ادخل نسبة تحمل الشركة");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Update Company set name=@name,cPay=@cPay,pPay=@pPay,notes=@notes Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@cPay", txtCompanyPay.Text);
                    cmd.Parameters.AddWithValue("@pPay", txtPatientPay.Text);
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
                refreshForm.loadTable("select * from Company");
            }

            txtName.Text = "";
            txtCompanyPay.Text = "0";
            txtPatientPay.Text = "0";
            txtNotes.Text = "";

            id = "";
        }

        private void txtCompanyPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtCompanyPay_TextChanged(object sender, EventArgs e)
        {
            if (txtCompanyPay.Text == "")
            {
                txtCompanyPay.Text = "0";
            }


            double companyPay = double.Parse(txtCompanyPay.Text);
            if (companyPay > 100)
            {
                MessageBox.Show("ادخل نسبة اقل من او يساوي %100");
                txtCompanyPay.Text = "0";
                txtPatientPay.Text = "0";
                return;
            }
            double patientPay = 100 - companyPay;

            txtPatientPay.Text = patientPay.ToString();
        }

        private void FormCompanies_Load(object sender, EventArgs e)
        {

        }
    }
}
