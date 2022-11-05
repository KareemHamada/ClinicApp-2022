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

namespace ClinicApp.Forms.Locations.Doctors
{
    public partial class FormAddDoctorToLoc : Form
    {
        public FormAddDoctorToLoc()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        public string id = "";
        public string txtGovernment = "";
        
        public FormShowDoctorsLoc refreshForm;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الدكتور ");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into DoctorsLocations (doctorName,specializaion,governmentId,address,phone) values (@doctorName,@specializaion,@governmentId,@address,@phone)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@doctorName", txtName.Text);
                    cmd.Parameters.AddWithValue("@specializaion", txtSpecialization.Text);
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
                    MessageBox.Show("حدد الدكتور المراد تعديله");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الدكتور الجديد ");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update DoctorsLocations set doctorName=@doctorName,specializaion=@specializaion,governmentId=@governmentId,address=@address,phone=@phone Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@doctorName", txtName.Text);
                    cmd.Parameters.AddWithValue("@specializaion", txtSpecialization.Text);
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
                refreshForm.loadTable("select DoctorsLocations.id,DoctorsLocations.doctorName,DoctorsLocations.specializaion,DoctorsLocations.address,DoctorsLocations.phone,Governments.name as government from DoctorsLocations,Governments where DoctorsLocations.governmentId = Governments.id");
            }


            txtName.Text = "";
            txtSpecialization.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            comboGovernment.Text = "";
            id = "";
        }
         
        private void FormAddDoctorToLoc_Load(object sender, EventArgs e)
        {
            // for combo government
            Helper.fillComboBox(comboGovernment, "Select id,name from Governments", "name", "id");


            comboGovernment.Text = txtGovernment;
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
