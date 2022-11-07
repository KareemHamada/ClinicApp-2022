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
    public partial class FormPatientRays : Form
    {
        public FormPatientRays()
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
                                Convert.ToDateTime(row["dateTime"]).ToString("dd/MM/yyyy"),
                                row["notes"],
                                row["rays"],
                                row["id"],

                            }
                        );
                }
            }
        }
        private void FormPatientRays_Load(object sender, EventArgs e)
        {
            // for combo clinics
            Helper.fillComboBox(comboPatient, "Select id,name from patient", "name", "id");
        }

        private void comboPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPatient.Text != "")
            {
                loadTable("Select " +
                "Rays.name as rays," +
                "RaysPatient.notes," +
                "RaysPatient.id," +
                "Clinics.name as clinic," +
                "Doctors.name as doctor," +
                "Reservations.date as dateTime " +
                "from RaysPatient,Reservations,Clinics,Doctors,Rays " +
                "where RaysPatient.patientId = '" + comboPatient.SelectedValue + "' " +
                "and RaysPatient.examinationId = Reservations.id " +
                "and RaysPatient.raysId = Rays.id " +
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
                    DataRow dro = tbl.Tables["dtShowPatientRays"].NewRow();
                    dro["doctor"] = dgvLoading[0, i].Value;
                    dro["clinic"] = dgvLoading[1, i].Value;
                    dro["dateTime"] = dgvLoading[2, i].Value;
                    dro["notes"] = dgvLoading[3, i].Value;
                    dro["name"] = dgvLoading[4, i].Value;

                    tbl.Tables["dtShowPatientRays"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormPatientRays.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPatientRays"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormPatientRays.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPatientRays"]));

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
