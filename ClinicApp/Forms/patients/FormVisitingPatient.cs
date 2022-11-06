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
    public partial class FormVisitingPatient : Form
    {
        public FormVisitingPatient()
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
                                row["phone"],
                                row["patient"],
                                row["id"],
                                row["pId"],

                            }
                        );
                }
            }
        }

        private void FormVisitingPatient_Load(object sender, EventArgs e)
        {
          
            //// for combo doctor
            //Helper.fillComboBox(comboPatient, "Select id,name from patient", "name", "id");

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
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
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
            }
            else
            {
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
                "Reservations.id = DoctorExaminationPatient.examinationId and " +
                "patient.name like '%" + text + "%' ");

                
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsTools tbl = new dsTools();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowVisitingPatient"].NewRow();
                    dro["doctor"] = dgvLoading[4, i].Value;
                    dro["clinic"] = dgvLoading[3, i].Value;
                    dro["dateTime"] = dgvLoading[2, i].Value;
                    dro["phone"] = dgvLoading[1, i].Value;
                    dro["patient"] = dgvLoading[0, i].Value;

                    tbl.Tables["dtShowVisitingPatient"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormVisitingPatient.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowVisitingPatient"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormVisitingPatient.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowVisitingPatient"]));

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
