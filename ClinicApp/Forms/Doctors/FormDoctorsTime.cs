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
    public partial class FormDoctorsTime : Form
    {
        public FormDoctorsTime()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        public string id = "";
        public string doctorText = "";
        public FormShowDoctorsTime refreshForm;

        private void FormDoctorsTime_Load(object sender, EventArgs e)
        {
            // for combo doctors
            Helper.fillComboBox(comboDoctor, "Select id,name from Doctors", "name", "id");

            comboDoctor.Text = doctorText;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (comboDoctor.Text == "")
                {
                    MessageBox.Show("اختر الطبيب");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into DoctorDates (saturday,sunday,monday,tuesday,wednesday,thursday,friday,notes,doctorId) values (@saturday,@sunday,@monday,@tuesday,@wednesday,@thursday,@friday,@notes,@doctorId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@saturday", txtSaturday.Text);
                    cmd.Parameters.AddWithValue("@sunday", txtSunday.Text);
                    cmd.Parameters.AddWithValue("@monday", txtMonday.Text);
                    cmd.Parameters.AddWithValue("@tuesday", txtTuesday.Text);
                    cmd.Parameters.AddWithValue("@wednesday", txtWednesday.Text);
                    cmd.Parameters.AddWithValue("@thursday", txtThursday.Text);
                    cmd.Parameters.AddWithValue("@friday", txtFriday.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@doctorId", comboDoctor.SelectedValue);

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
                    MessageBox.Show("حدد الدكتور المراد تعديل المواعيد له");
                    return;
                }
                if (comboDoctor.Text == "")
                {
                    MessageBox.Show("اختر الطبيب");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update DoctorDates set saturday=@saturday,sunday=@sunday,monday=@monday,tuesday=@tuesday,wednesday=@wednesday,thursday=@thursday,friday=@friday,notes=@notes,doctorId=@doctorId Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@saturday", txtSaturday.Text);
                    cmd.Parameters.AddWithValue("@sunday", txtSunday.Text);
                    cmd.Parameters.AddWithValue("@monday", txtMonday.Text);
                    cmd.Parameters.AddWithValue("@tuesday", txtTuesday.Text);
                    cmd.Parameters.AddWithValue("@wednesday", txtWednesday.Text);
                    cmd.Parameters.AddWithValue("@thursday", txtThursday.Text);
                    cmd.Parameters.AddWithValue("@friday", txtFriday.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@doctorId", comboDoctor.SelectedValue);

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
                refreshForm.loadTable("select DoctorDates.id,DoctorDates.saturday,DoctorDates.sunday,DoctorDates.monday,DoctorDates.tuesday,DoctorDates.wednesday,DoctorDates.thursday,DoctorDates.friday,DoctorDates.notes,Doctors.name as doctor,Specializations.name as specialization,Clinics.name as clinic from DoctorDates,Clinics,Specializations,Doctors where DoctorDates.doctorId = Doctors.id and Doctors.clinicsId = Clinics.id and Doctors.specializationId = Specializations.id");
            }

            txtSaturday.Text = "";
            txtSunday.Text = "";
            txtMonday.Text = "";
            txtTuesday.Text = "";
            txtWednesday.Text = "";
            txtThursday.Text = "";
            txtFriday.Text = "";
            txtNotes.Text = "";
            comboDoctor.Text = "";
            id = "";
        }

       
    }
}
