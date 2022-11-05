using ClinicApp.Classes;
using ClinicApp.Forms.Employees;
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

namespace ClinicApp.Forms
{
    public partial class FormEmployee : Form
    {
        public FormEmployee()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtImage;

        public string id = "";
        public string genderText = "";
        public string jobText = "";
        public FormShowEmployees refreshForm;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الموظف");
                    return;
                }
                if (comboGender.Text == "")
                {
                    MessageBox.Show("اختر جنس الموظف");
                    return;
                }
                if (comboSpecialization.Text == "")
                {
                    MessageBox.Show("اختر الوظيفة");
                    return;
                }

                try
                {
                    if (picBox.BackgroundImage != null)
                    {

                        cmd = new SqlCommand("Insert into Employees (name,gender,jobId,age,notes,address,phone,facebook,whatsApp,gmail,image) values (@name,@gender,@jobId,@age,@notes,@address,@phone,@facebook,@whatsApp,@gmail,@image)", adoClass.sqlcn);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@gender", comboGender.Text);
                        cmd.Parameters.AddWithValue("@jobId", comboSpecialization.SelectedValue);
                        cmd.Parameters.AddWithValue("@age", txtAge.Text);
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
                        cmd = new SqlCommand("Insert into Employees (name,gender,jobId,age,notes,address,phone,facebook,whatsApp,gmail) values (@name,@gender,@jobId,@age,@notes,@address,@phone,@facebook,@whatsApp,@gmail)", adoClass.sqlcn);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@gender", comboGender.Text);
                        cmd.Parameters.AddWithValue("@jobId", comboSpecialization.SelectedValue);
                        cmd.Parameters.AddWithValue("@age", txtAge.Text);
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


                    MessageBox.Show("تم اضافة الموظف بنجاح");

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
                    MessageBox.Show("حدد العنصر الموظف تعديلة");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الموظف");
                    return;
                }
                if (comboGender.Text == "")
                {
                    MessageBox.Show("اختر جنس الموظف");
                    return;
                }
                if (comboSpecialization.Text == "")
                {
                    MessageBox.Show("اختر الوظيفة");
                    return;
                }



                try
                {
                    if (picBox.BackgroundImage != null)
                    {
                        cmd = new SqlCommand("Update Employees set name=@name,gender=@gender,jobId=@jobId,age=@age,notes=@notes,address=@address,phone=@phone,facebook=@facebook,whatsApp=@whatsApp,gmail=@gmail,image=@image Where id = '" + id + "'", adoClass.sqlcn);


                        ///
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@gender", comboGender.Text);
                        cmd.Parameters.AddWithValue("@jobId", comboSpecialization.SelectedValue);
                        cmd.Parameters.AddWithValue("@age", txtAge.Text);
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
                        cmd = new SqlCommand("Update Employees set name=@name,gender=@gender,jobId=@jobId,age=@age,notes=@notes,address=@address,phone=@phone,facebook=@facebook,whatsApp=@whatsApp,gmail=@gmail,image=@image Where id = '" + id + "'", adoClass.sqlcn);


                        ///
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@gender", comboGender.Text);
                        cmd.Parameters.AddWithValue("@jobId", comboSpecialization.SelectedValue);
                        cmd.Parameters.AddWithValue("@age", txtAge.Text);
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
                refreshForm.loadTable("select Employees.id,Employees.name,Employees.gender,Employees.age,Employees.notes,Employees.address,Employees.phone,Employees.facebook,Employees.whatsApp,Employees.gmail,Employees.image,Specializations.name as job from Employees LEFT JOIN Specializations on Employees.jobId = Specializations.id");

               
            }
            

            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtName.Text = "";
            comboGender.Text = "";
            comboSpecialization.Text = "";
            txtAge.Text = "";
            txtNotes.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtFaceBook.Text = "";
            txtWhatsApp.Text = "";
            txtEmail.Text = "";
            id = "";
        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboSpecialization, "Select id,name from Specializations", "name", "id");


            // for hidden images
            txtImage = new TextBox();
            txtImage.Visible = false;

            comboGender.Text = genderText;
            comboSpecialization.Text = jobText;
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
    }
}
