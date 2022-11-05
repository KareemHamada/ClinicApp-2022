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
    public partial class FormShowDoctors : Form
    {
        public FormShowDoctors()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtHidden;

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }
        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select Doctors.id,Doctors.name,Doctors.jobDes,Doctors.notes,Doctors.address,Doctors.phone,Doctors.facebook,Doctors.whatsApp,Doctors.gmail,Doctors.image,Clinics.name as clinic,Specializations.name as specialization from Doctors LEFT JOIN Clinics on Doctors.clinicsId = Clinics.id LEFT JOIN Specializations on Doctors.specializationId = Specializations.id");
            }
            else
            {
                loadTable("select Doctors.id,Doctors.name,Doctors.jobDes,Doctors.notes,Doctors.address,Doctors.phone,Doctors.facebook,Doctors.whatsApp,Doctors.gmail,Doctors.image,Clinics.name as clinic,Specializations.name as specialization from Doctors LEFT JOIN Clinics on Doctors.clinicsId = Clinics.id LEFT JOIN Specializations on Doctors.specializationId = Specializations.id where Doctors.name like '%" + text + "%'");
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
                        MessageBox.Show("حدد الطبيب المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Doctors Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select Doctors.id,Doctors.name,Doctors.jobDes,Doctors.notes,Doctors.address,Doctors.phone,Doctors.facebook,Doctors.whatsApp,Doctors.gmail,Doctors.image,Clinics.name as clinic,Specializations.name as specialization from Doctors LEFT JOIN Clinics on Doctors.clinicsId = Clinics.id LEFT JOIN Specializations on Doctors.specializationId = Specializations.id");

                }
            

                
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormDoctors frm = new FormDoctors();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                frm.id = txtHidden.Text;
                frm.txtName.Text = dgvLoading.CurrentRow.Cells[10].Value.ToString();
                frm.clinText = dgvLoading.CurrentRow.Cells[9].Value.ToString();
                frm.specText = dgvLoading.CurrentRow.Cells[8].Value.ToString();

                frm.txtDes.Text = dgvLoading.CurrentRow.Cells[7].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[6].Value.ToString();
                frm.txtAddress.Text = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                frm.txtPhone.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtFaceBook.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.txtWhatsApp.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtEmail.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                frm.picBox.BackgroundImage = Helper.ByteToImage(dgvLoading.CurrentRow.Cells[0].Value);
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
                    DataRow dro = tbl.Tables["dtShowDoctors"].NewRow();
                    dro["name"] = dgvLoading[10, i].Value;
                    dro["clinic"] = dgvLoading[9, i].Value;
                    dro["specialization"] = dgvLoading[8, i].Value;
                    dro["jobDes"] = dgvLoading[7, i].Value;
                    dro["notes"] = dgvLoading[6, i].Value;
                    dro["address"] = dgvLoading[5, i].Value;
                    dro["phone"] = dgvLoading[4, i].Value;
                    dro["facebook"] = dgvLoading[3, i].Value;
                    dro["whatsApp"] = dgvLoading[2, i].Value;
                    dro["gmail"] = dgvLoading[1, i].Value;

                    tbl.Tables["dtShowDoctors"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowDoctors.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowDoctors"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowDoctors.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowDoctors"]));

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
                                row["image"],
                                row["gmail"],
                                row["whatsApp"],
                                row["facebook"],
                                row["phone"],
                                row["address"],
                                row["notes"],
                                row["jobDes"],
                                row["specialization"],
                                row["clinic"],
                                row["name"],
                                row["id"],
                            }
                        ); ;
                }
            }
        }

        private void FormShowDoctors_Load(object sender, EventArgs e)
        {
            loadTable("select Doctors.id,Doctors.name,Doctors.jobDes,Doctors.notes,Doctors.address,Doctors.phone,Doctors.facebook,Doctors.whatsApp,Doctors.gmail,Doctors.image,Clinics.name as clinic,Specializations.name as specialization from Doctors LEFT JOIN Clinics on Doctors.clinicsId = Clinics.id LEFT JOIN Specializations on Doctors.specializationId = Specializations.id");


            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }
    }
}
