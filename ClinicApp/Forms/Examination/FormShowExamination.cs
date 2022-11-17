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
                                Convert.ToDateTime(row["dateTime"]).ToString("dd/MM/yyyy"),
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


            // hide and show buttons
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select examUpdate from Users where id = '" + declarations.userId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];               
                if (row["examUpdate"].ToString() == "False")
                {
                    btnUpdate.Visible = false;
                }
            }
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

        private void dateOfReservations_ValueChanged(object sender, EventArgs e)
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
                "Reservations.id = DoctorExaminationPatient.examinationId " +
                "and DoctorExaminationPatient.dateTime = '" + dateOfReservations.Value + "' ");
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
                "(Clinics.name like '%" + text + "%' " +
                "or patient.name like '%" + text + "%' " +
                "or patient.phone like '%" + text + "%' " +
                "or Doctors.name like '%" + text + "%')");
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
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
                "Reservations.id = DoctorExaminationPatient.examinationId " +
                "and Clinics.name = '" + comboClinic.Text + "' " +
                "and Doctors.name = '" + comboDoctor.Text + "' " +
                "and DoctorExaminationPatient.dateTime = '" + dateOfReservations.Value + "' ");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsTools tbl = new dsTools();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowMainExamintation"].NewRow();
                    dro["patient"] = dgvLoading[4, i].Value;
                    dro["phone"] = dgvLoading[3, i].Value;
                    dro["date"] = dgvLoading[2, i].Value;
                    dro["clinic"] = dgvLoading[1, i].Value;
                    dro["doctor"] = dgvLoading[0, i].Value;

                    tbl.Tables["dtShowMainExamintation"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowMainExamintation.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowMainExamintation"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowMainExamintation.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowMainExamintation"]));

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
