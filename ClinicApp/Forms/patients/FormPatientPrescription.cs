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
    public partial class FormPatientPrescription : Form
    {
        public FormPatientPrescription()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        //private TextBox txtHidden;

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

                    dgvLoading.Rows.Add
                        (new object[]
                            {
                                row["doctor"],
                                row["clinic"],
                                row["dateTime"],
                                row["notes"],
                                row["dosages"],
                                row["medicineUnit"],
                                row["timeTakeMedicine"],
                                row["medicine"],
                                row["id"],

                            }
                        );
                }
            }
        }
        private void FormPatientPrescription_Load(object sender, EventArgs e)
        {
            // for combo clinics
            Helper.fillComboBox(comboPatient, "Select id,name from patient", "name", "id");

            

            //loadTable("Select " +
            //    "Drugs.name as medicine," +
            //    "PrescriptionPatient.timeTakeMedicine," +
            //    "PrescriptionPatient.medicineUnit," +
            //    "PrescriptionPatient.dosages," +
            //    "PrescriptionPatient.notes," +
            //    "PrescriptionPatient.id," +
            //    "Clinics.name as clinic," +
            //    "Doctors.name as doctor," +
            //    "Reservations.date as dateTime" +
            //    " from PrescriptionPatient p" +
            //    " inner join Reservations r on r.id = p.clinicId" +
            //    " inner join Clinics c on c.id = r.clinicId" +
            //    " inner join Doctors d on d.id = r.doctorId" +
            //    " inner join Drugs dr on dr.id = p.medicineId" +
            //    " inner join Reservations r on " +
            //    "where p.patientId = '" + comboPatient.SelectedValue + "' " +
            //    "and p.medicineId = dr.id" +
            //    "and p.reservationId = r.id " +
            //    "and r.doctorId = d.id " +
            //    "and r.clinicId = c.id"
            //    );

        }

        private void comboPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboPatient.Text != "")
            {
                loadTable("Select " +
                "Drugs.name as medicine," +
                "PrescriptionPatient.timeTakeMedicine," +
                "PrescriptionPatient.medicineUnit," +
                "PrescriptionPatient.dosages," +
                "PrescriptionPatient.notes," +
                "PrescriptionPatient.id," +
                "Clinics.name as clinic," +
                "Doctors.name as doctor," +
                "Reservations.date as dateTime " +
                "from PrescriptionPatient,Reservations,Clinics,Doctors,Drugs " +
                "where PrescriptionPatient.patientId = '" + comboPatient.SelectedValue + "' " +
                "and PrescriptionPatient.examinationId = Reservations.id " +
                "and PrescriptionPatient.medicineId = Drugs.id " +
                "and Reservations.doctorId = Doctors.id " +
                "and Reservations.clinicId = Clinics.id"
                );
            }
        }
    }
}
