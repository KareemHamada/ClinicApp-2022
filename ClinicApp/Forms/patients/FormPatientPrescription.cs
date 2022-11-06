using ClinicApp.Classes;
using ClinicApp.Tools;
using Microsoft.Reporting.WinForms;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsTools tbl = new dsTools();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowPatientPrescription"].NewRow();
                    dro["doctor"] = dgvLoading[0, i].Value;
                    dro["clinic"] = dgvLoading[1, i].Value;
                    dro["dateTime"] = dgvLoading[2, i].Value;
                    dro["notes"] = dgvLoading[3, i].Value;
                    dro["dosages"] = dgvLoading[4, i].Value;
                    dro["medicineUnit"] = dgvLoading[5, i].Value;
                    dro["timeTakeMedicine"] = dgvLoading[6, i].Value;
                    dro["medicine"] = dgvLoading[7, i].Value;

                    tbl.Tables["dtShowPatientPrescription"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormPatientPrescription.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPatientPrescription"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormPatientPrescription.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPatientPrescription"]));

                    PrintersClass.PrintToPrinter(report);
                }
                else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
                {
                    rptForm.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("لا يوجد عناصر لعرضها");
            }
        }
    }
}
