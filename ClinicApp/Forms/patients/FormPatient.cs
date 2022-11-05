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

namespace ClinicApp.Forms.patients
{
    public partial class FormPatient : Form
    {
        public FormPatient()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtImage;
        public string id = "";
        public FormShowPatient refreshForm;
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم المريض");
                    return;
                }
                

                try
                {
                    if (picBox.BackgroundImage != null)
                    {

                        cmd = new SqlCommand("Insert into Patient (name,dateOfBirth,type,dateTimeReg,phone,address,companyId,weight,height,lastOperations,notes,image) values (@name,@dateOfBirth,@type,@dateTimeReg,@phone,@address,@companyId,@weight,@height,@lastOperations,@notes,@image)", adoClass.sqlcn);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@type", comboGender.Text);
                        cmd.Parameters.AddWithValue("@dateTimeReg", DateTime.Now);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@companyId", comboCompany.SelectedValue);
                        cmd.Parameters.AddWithValue("@weight", txtWeight.Text);
                        cmd.Parameters.AddWithValue("@height", txtHeight.Text);
                        cmd.Parameters.AddWithValue("@lastOperations", txtPreviousOperations.Text);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));

                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into Patient (name,dateOfBirth,type,dateTimeReg,phone,address,companyId,weight,height,lastOperations,notes) values (@name,@dateOfBirth,@type,@dateTimeReg,@phone,@address,@companyId,@weight,@height,@lastOperations,@notes)", adoClass.sqlcn);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@type", comboGender.Text);
                        cmd.Parameters.AddWithValue("@dateTimeReg", DateTime.Now);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@companyId", comboCompany.SelectedValue);
                        cmd.Parameters.AddWithValue("@weight", txtWeight.Text);
                        cmd.Parameters.AddWithValue("@height", txtHeight.Text);
                        cmd.Parameters.AddWithValue("@lastOperations", txtPreviousOperations.Text);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    }
                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("تمت الاضافة بنجاح");

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
                    MessageBox.Show("ادخل اسم المريض");
                    return;
                }
                
                try
                {
                    if (picBox.BackgroundImage != null)
                    {
                        cmd = new SqlCommand("Update Patient set name=@name,dateOfBirth=@dateOfBirth,type=@type,dateTimeReg=@dateTimeReg,phone=@phone,address=@address,companyId=@companyId,weight=@weight,height=@height,lastOperations=@lastOperations,notes=@notes,image=@image Where id = '" + id + "'", adoClass.sqlcn);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@type", comboGender.Text);
                        cmd.Parameters.AddWithValue("@dateTimeReg", DateTime.Now);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@companyId", comboCompany.SelectedValue);
                        cmd.Parameters.AddWithValue("@weight", txtWeight.Text);
                        cmd.Parameters.AddWithValue("@height", txtHeight.Text);
                        cmd.Parameters.AddWithValue("@lastOperations", txtPreviousOperations.Text);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                    }
                    else
                    {
                        cmd = new SqlCommand("Update Patient set name=@name,dateOfBirth=@dateOfBirth,type=@type,dateTimeReg=@dateTimeReg,phone=@phone,address=@address,companyId=@companyId,weight=@weight,height=@height,lastOperations=@lastOperations,notes=@notes,image=@image Where id = '" + id + "'", adoClass.sqlcn);


                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@type", comboGender.Text);
                        cmd.Parameters.AddWithValue("@dateTimeReg", DateTime.Now);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@companyId", comboCompany.SelectedValue);
                        cmd.Parameters.AddWithValue("@weight", txtWeight.Text);
                        cmd.Parameters.AddWithValue("@height", txtHeight.Text);
                        cmd.Parameters.AddWithValue("@lastOperations", txtPreviousOperations.Text);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);                        
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
                refreshForm.loadTable("select Patient.id,Patient.name,Patient.dateOfBirth,Patient.type,Patient.dateTimeReg,Patient.phone,Patient.address,Patient.weight,Patient.height,Patient.lastOperations,Patient.notes,Patient.image,Company.name as company from Patient LEFT JOIN Company on Patient.companyId = Company.id");

            }


            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtName.Text = "";
            comboGender.Text = "";
            comboCompany.Text = "";
            txtWeight.Text = "";
            txtHeight.Text = "";
            txtNotes.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtPreviousOperations.Text = "";
            
         
            id = "";
        }

        private void FormPatient_Load(object sender, EventArgs e)
        {
            // for combo clinics
            Helper.fillComboBox(comboCompany, "Select id,name from Company", "name", "id");


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

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
