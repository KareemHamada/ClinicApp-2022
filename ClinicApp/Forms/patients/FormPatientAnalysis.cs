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
    public partial class FormPatientAnalysis : Form
    {
        public FormPatientAnalysis()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
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
                                row["result"],
                                row["analysis"],
                                row["id"],

                            }
                        );
                }
            }
        }
        private void FormPatientAnalysis_Load(object sender, EventArgs e)
        {
            // for combo clinics
            Helper.fillComboBox(comboPatient, "Select id,name from patient", "name", "id");
        }

        private void comboPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPatient.Text != "")
            {
                loadTable("Select " +
                "Analysis.name as analysis," +
                "AnalysisPatient.notes," +
                "PrescriptionPatient.medicineUnit," +
                "AnalysisPatient.id," +
                "Clinics.name as clinic," +
                "Doctors.name as doctor," +
                "Reservations.date as dateTime " +
                "from AnalysisPatient,Reservations,Clinics,Doctors,Analysis " +
                "where AnalysisPatient.patientId = '" + comboPatient.SelectedValue + "' " +
                "and AnalysisPatient.examinationId = Reservations.id " +
                "and AnalysisPatient.analysisId = Analysis.id " +
                "and Reservations.doctorId = Doctors.id " +
                "and Reservations.clinicId = Clinics.id"
                );
            }
        }
    }
}
