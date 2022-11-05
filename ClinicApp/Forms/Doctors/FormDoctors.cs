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

namespace ClinicApp.Forms.Doctors
{
    public partial class FormDoctors : Form
    {
        public FormDoctors()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtImage;
        public string id = "";
        public FormShowDoctors refreshForm;
        public string clinText = "";
        public string specText = "";
        private void FormDoctors_Load(object sender, EventArgs e)
        {

            // for combo clinics
            Helper.fillComboBox(comboClinic, "Select id,name from Clinics", "name", "id");

            // for combo specialization
            Helper.fillComboBox(comboSpecialization, "Select id,name from Specializations", "name", "id");

            comboClinic.Text = clinText;
            comboSpecialization.Text = specText;
            // for hidden images
            txtImage = new TextBox();
            txtImage.Visible = false;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtImage.Text = fileDialog.FileName;
                picBox.BackgroundImage = new Bitmap(txtImage.Text);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الدكتور");
                    return;
                }
                if (comboClinic.Text == "")
                {
                    MessageBox.Show("اختر العيادة");
                    return;
                }
                if (comboSpecialization.Text == "")
                {
                    MessageBox.Show("اختر التخصص");
                    return;
                }

                try
                {
                    if (picBox.BackgroundImage != null)
                    {

                        cmd = new SqlCommand("Insert into Doctors (name,clinicsId,specializationId,jobDes,notes,address,phone,facebook,whatsApp,gmail,image) values (@name,@clinicsId,@specializationId,@jobDes,@notes,@address,@phone,@facebook,@whatsApp,@gmail,@image)", adoClass.sqlcn);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@clinicsId", comboClinic.SelectedValue);
                        cmd.Parameters.AddWithValue("@specializationId", comboSpecialization.SelectedValue);
                        cmd.Parameters.AddWithValue("@jobDes", txtDes.Text);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@facebook", txtFaceBook.Text);
                        cmd.Parameters.AddWithValue("@whatsApp", txtWhatsApp.Text);
                        cmd.Parameters.AddWithValue("@gmail", txtEmail.Text);


                        cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));

                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into Doctors (name,clinicsId,specializationId,jobDes,notes,address,phone,facebook,whatsApp,gmail) values (@name,@clinicsId,@specializationId,@jobDes,@notes,@address,@phone,@facebook,@whatsApp,@gmail)", adoClass.sqlcn);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@clinicsId", comboClinic.SelectedValue);
                        cmd.Parameters.AddWithValue("@specializationId", comboSpecialization.SelectedValue);
                        cmd.Parameters.AddWithValue("@jobDes", txtDes.Text);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@facebook", txtFaceBook.Text);
                        cmd.Parameters.AddWithValue("@whatsApp", txtWhatsApp.Text);
                        cmd.Parameters.AddWithValue("@gmail", txtEmail.Text);
                    }
                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("تم اضافة الطبيب بنجاح");

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
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الدكتور");
                    return;
                }
                if (comboClinic.Text == "")
                {
                    MessageBox.Show("اختر العيادة");
                    return;
                }
                if (comboSpecialization.Text == "")
                {
                    MessageBox.Show("اختر التخصص");
                    return;
                }

                try
                {
                    if (picBox.BackgroundImage != null)
                    {
                        cmd = new SqlCommand("Update Doctors set name=@name,clinicsId=@clinicsId,specializationId=@specializationId,jobDes=@jobDes,notes=@notes,address=@address,phone=@phone,facebook=@facebook,whatsApp=@whatsApp,gmail=@gmail,image=@image Where id = '" + id + "'", adoClass.sqlcn);


                        ///
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@clinicsId", comboClinic.SelectedValue);
                        cmd.Parameters.AddWithValue("@specializationId", comboSpecialization.SelectedValue);
                        cmd.Parameters.AddWithValue("@jobDes", txtDes.Text);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@facebook", txtFaceBook.Text);
                        cmd.Parameters.AddWithValue("@whatsApp", txtWhatsApp.Text);
                        cmd.Parameters.AddWithValue("@gmail", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                    }
                    else
                    {
                        cmd = new SqlCommand("Update Doctors set name=@name,clinicsId=@clinicsId,specializationId=@specializationId,jobDes=@jobDes,notes=@notes,address=@address,phone=@phone,facebook=@facebook,whatsApp=@whatsApp,gmail=@gmail,image=@image Where id = '" + id + "'", adoClass.sqlcn);


                        ///
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@clinicsId", comboClinic.SelectedValue);
                        cmd.Parameters.AddWithValue("@specializationId", comboSpecialization.SelectedValue);
                        cmd.Parameters.AddWithValue("@jobDes", txtDes.Text);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@facebook", txtFaceBook.Text);
                        cmd.Parameters.AddWithValue("@whatsApp", txtWhatsApp.Text);
                        cmd.Parameters.AddWithValue("@gmail", txtEmail.Text);
                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                        
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
                refreshForm.loadTable("select Doctors.id,Doctors.name,Doctors.jobDes,Doctors.notes,Doctors.address,Doctors.phone,Doctors.facebook,Doctors.whatsApp,Doctors.gmail,Doctors.image,Clinics.name as clinic,Specializations.name as specialization from Doctors LEFT JOIN Clinics on Doctors.clinicsId = Clinics.id LEFT JOIN Specializations on Doctors.specializationId = Specializations.id");

            }
            

            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtName.Text = "";
            comboClinic.Text = "";
            comboSpecialization.Text = "";
            txtDes.Text = "";
            txtNotes.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtFaceBook.Text = "";
            txtWhatsApp.Text = "";
            txtEmail.Text = "";

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

       
    }
}
