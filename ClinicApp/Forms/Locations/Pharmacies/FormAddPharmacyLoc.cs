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

namespace ClinicApp.Forms.Locations.Pharmacies
{
    public partial class FormAddPharmacyLoc : Form
    {
        public FormAddPharmacyLoc()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        public string id = "";
        public string txtGovernment = "";

        public FormShowPharmacyLoc refreshForm;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الصيدلية ");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into PharmaciesLocations (name,governmentId,address,phone) values (@name,@governmentId,@address,@phone)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@governmentId", comboGovernment.SelectedValue);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);

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
                    MessageBox.Show("حدد الصيدلية المراد تعديلها");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الصيدلية الجديد ");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update PharmaciesLocations set name=@name,governmentId=@governmentId,address=@address,phone=@phone Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@governmentId", comboGovernment.SelectedValue);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);

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
                refreshForm.loadTable("select PharmaciesLocations.id,PharmaciesLocations.name,PharmaciesLocations.address,PharmaciesLocations.phone,Governments.name as government from PharmaciesLocations,Governments where PharmaciesLocations.governmentId = Governments.id");
            }


            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            comboGovernment.Text = "";
            id = "";
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void FormAddPharmacyLoc_Load(object sender, EventArgs e)
        {
            // for combo government
            Helper.fillComboBox(comboGovernment, "Select id,name from Governments", "name", "id");


            comboGovernment.Text = txtGovernment;
        }
    }
}
