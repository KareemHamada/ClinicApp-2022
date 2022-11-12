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

namespace ClinicApp.Forms.Doctors
{
    public partial class FormShowDoctorsTime : Form
    {
        public FormShowDoctorsTime()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtHidden;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }
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
                                row["notes"],
                                row["friday"],
                                row["thursday"],
                                row["wednesday"],
                                row["tuesday"],
                                row["monday"],
                                row["sunday"],
                                row["saturday"],
                                row["specialization"],
                                row["clinic"],
                                row["doctor"],
                                row["id"],
                            }
                        ); ;
                }
            }
        }
        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select DoctorDates.id,DoctorDates.saturday,DoctorDates.sunday,DoctorDates.monday,DoctorDates.tuesday,DoctorDates.wednesday,DoctorDates.thursday,DoctorDates.friday,DoctorDates.notes,Doctors.name as doctor,Specializations.name as specialization,Clinics.name as clinic from DoctorDates,Clinics,Specializations,Doctors where DoctorDates.doctorId = Doctors.id and Doctors.clinicsId = Clinics.id and Doctors.specializationId = Specializations.id");
            }
            else
            {
                loadTable("select DoctorDates.id,DoctorDates.saturday,DoctorDates.sunday,DoctorDates.monday,DoctorDates.tuesday,DoctorDates.wednesday,DoctorDates.thursday,DoctorDates.friday,DoctorDates.notes,Doctors.name as doctor,Specializations.name as specialization,Clinics.name as clinic from DoctorDates,Clinics,Specializations,Doctors where DoctorDates.doctorId = Doctors.id and Doctors.clinicsId = Clinics.id and Doctors.specializationId = Specializations.id and Doctors.name like '%" + text + "%'");


            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                    string id = txtHidden.Text;
                    if (id == "")
                    {
                        MessageBox.Show("حدد الطبيب المراد حذف مواعيدة");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from DoctorDates Where id = '" + id + "'", adoClass.sqlcn);

                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تم الحذف بنجاح");

                    }
                    catch
                    {
                        MessageBox.Show("خطا في الحذف");
                    }
                    finally
                    {
                        adoClass.sqlcn.Close();
                    }

                    loadTable("select DoctorDates.id,DoctorDates.saturday,DoctorDates.sunday,DoctorDates.monday,DoctorDates.tuesday,DoctorDates.wednesday,DoctorDates.thursday,DoctorDates.friday,DoctorDates.notes,Doctors.name as doctor,Specializations.name as specialization,Clinics.name as clinic from DoctorDates,Clinics,Specializations,Doctors where DoctorDates.doctorId = Doctors.id and Doctors.clinicsId = Clinics.id and Doctors.specializationId = Specializations.id");
                }
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormDoctorsTime frm = new FormDoctorsTime();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                frm.doctorText = dgvLoading.CurrentRow.Cells[10].Value.ToString();
                frm.txtSaturday.Text = dgvLoading.CurrentRow.Cells[7].Value.ToString();
                frm.txtSunday.Text = dgvLoading.CurrentRow.Cells[6].Value.ToString();
                frm.txtMonday.Text = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                frm.txtTuesday.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtWednesday.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.txtThursday.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtFriday.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[0].Value.ToString();
                frm.id = txtHidden.Text;
                frm.refreshForm = this;
                frm.Show();

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsTools tbl = new dsTools();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowDoctorsTime"].NewRow();
                    dro["doctor"] = dgvLoading[10, i].Value;
                    dro["clinic"] = dgvLoading[9, i].Value;
                    dro["specialization"] = dgvLoading[8, i].Value;
                    dro["saturday"] = dgvLoading[7, i].Value;
                    dro["sunday"] = dgvLoading[6, i].Value;
                    dro["monday"] = dgvLoading[5, i].Value;
                    dro["tuesday"] = dgvLoading[4, i].Value;
                    dro["wednesday"] = dgvLoading[3, i].Value;
                    dro["thursday"] = dgvLoading[2, i].Value;
                    dro["friday"] = dgvLoading[1, i].Value;
                    dro["notes"] = dgvLoading[0, i].Value;

                    tbl.Tables["dtShowDoctorsTime"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowDoctorsTime.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowDoctorsTime"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowDoctorsTime.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowDoctorsTime"]));

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

        private void FormShowDoctorsTime_Load(object sender, EventArgs e)
        {
            loadTable("select DoctorDates.id," +
                "DoctorDates.saturday," +
                "DoctorDates.sunday," +
                "DoctorDates.monday," +
                "DoctorDates.tuesday," +
                "DoctorDates.wednesday," +
                "DoctorDates.thursday," +
                "DoctorDates.friday," +
                "DoctorDates.notes," +
                "Doctors.name as doctor," +
                "Specializations.name as specialization," +
                "Clinics.name as clinic " +
                "from DoctorDates,Clinics,Specializations,Doctors " +
                "where DoctorDates.doctorId = Doctors.id" +
                " and Doctors.clinicsId = Clinics.id" +
                " and Doctors.specializationId = Specializations.id");

            txtHidden = new TextBox();
            txtHidden.Visible = false;


            // hide and show buttons
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select doctorDelete,doctorUpdate from Users where id = '" + declarations.userId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                // for doctor
                if (row["doctorDelete"].ToString() == "False")
                {
                    btnDelete.Visible = false;
                }
                if (row["doctorUpdate"].ToString() == "False")
                {
                    btnUpdate.Visible = false;
                }
            }
        }
    }
}
