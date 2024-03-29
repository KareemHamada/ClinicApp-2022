﻿using ClinicApp.Classes;
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
                                "",
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsTools tbl = new dsTools();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowPatientAnalysis"].NewRow();
                    dro["doctor"] = dgvLoading[0, i].Value;
                    dro["clinic"] = dgvLoading[1, i].Value;
                    dro["dateTime"] = dgvLoading[2, i].Value;
                    dro["notes"] = dgvLoading[3, i].Value;
                    dro["name"] = dgvLoading[5, i].Value;

                    tbl.Tables["dtShowPatientAnalysis"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormPatientAnalysis.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPatientAnalysis"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormPatientAnalysis.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPatientAnalysis"]));

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
