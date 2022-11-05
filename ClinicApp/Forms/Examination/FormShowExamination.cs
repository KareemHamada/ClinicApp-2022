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

namespace ClinicApp.Forms.Examination
{
    public partial class FormShowExamination : Form
    {
        public FormShowExamination()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtHidden;

        public void loadTable(string query)
        {
            dgvLoading.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand(query, adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    //newItem.DateSaved.ToString("yyyy-MM-dd") //

                    //Console.WriteLine(row["dateTime"]);
                    dgvLoading.Rows.Add
                        (new object[]
                            {

                                row["doctor"],
                                row["clinic"],
                                row["dateTime"],
                                row["phone"],
                                row["patient"],
                                row["id"],
                                row["pId"],
                                
                            }
                        );
                }
            }
        }

        private void FormShowExamination_Load(object sender, EventArgs e)
        {
            dateOfReservations.Value = DateTime.Now;
            // for combo clinics
            Helper.fillComboBox(comboClinic, "Select id,name from Clinics", "name", "id");
            // for combo doctor
            Helper.fillComboBox(comboDoctor, "Select id,name from Doctors", "name", "id");

            loadTable("select " +
                "Reservations.id," +
                "Clinics.name as clinic," +
                "Doctors.name as doctor," +
                "patient.name as patient," +
                "patient.id as pId," +
                "patient.phone as phone," +
                "DoctorExaminationPatient.dateTime as dateTime" +
                " from Reservations," +
                "Clinics," +
                "Doctors," +
                "patient," +
                "DoctorExaminationPatient " +
                "where " +
                "Reservations.clinicId = Clinics.id and " +
                "Reservations.doctorId = Doctors.id and " +
                "Reservations.patientId = patient.id and " +
                "Reservations.id = DoctorExaminationPatient.examinationId");

            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void btnShowExaminationDetails_Click(object sender, EventArgs e)
        {
            if(dgvLoading.Rows.Count > 0)
            {
                FormShowExaminationDetails frm = new FormShowExaminationDetails();
                frm.patId = dgvLoading.CurrentRow.Cells[6].Value.ToString(); ;
                frm.reservationId = dgvLoading.CurrentRow.Cells[5].Value.ToString(); ;
                frm.Show();
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormUpdateExamination frm = new FormUpdateExamination();
                frm.patId = dgvLoading.CurrentRow.Cells[6].Value.ToString(); ;
                frm.reservationId = dgvLoading.CurrentRow.Cells[5].Value.ToString(); ;
                frm.Show();
            }
        }
    }
}
