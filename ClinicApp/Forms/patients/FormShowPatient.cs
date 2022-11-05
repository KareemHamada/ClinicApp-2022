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
    public partial class FormShowPatient : Form
    {
        public FormShowPatient()
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
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                                row["image"],
                                row["notes"],
                                row["lastOperations"],
                                row["weight"],
                                row["height"],
                                row["company"],
                                row["phone"],
                                row["address"],
                                row["dateTimeReg"],
                                row["dateOfBirth"],
                                row["type"],
                                row["name"],
                                row["id"],
                            }
                        ); ;
                }
            }
        }

        private void FormShowPatient_Load(object sender, EventArgs e)
        {
            loadTable("select Patient.id,Patient.name,Patient.dateOfBirth,Patient.type,Patient.dateTimeReg,Patient.phone,Patient.address,Patient.weight,Patient.height,Patient.lastOperations,Patient.notes,Patient.image,Company.name as company from Patient LEFT JOIN Company on Patient.companyId = Company.id");


            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {


                    txtHidden.Text = dgvLoading.CurrentRow.Cells[12].Value.ToString();

                    string id = txtHidden.Text;
                    if (id == "")
                    {
                        MessageBox.Show("حدد المريض المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Patient Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select Patient.id,Patient.name,Patient.dateOfBirth,Patient.type,Patient.dateTimeReg,Patient.phone,Patient.address,Patient.weight,Patient.height,Patient.lastOperations,Patient.notes,Patient.image,Company.name as company from Patient LEFT JOIN Company on Patient.companyId = Company.id");
                    id = "";

                }



            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select Patient.id,Patient.name,Patient.dateOfBirth,Patient.type,Patient.dateTimeReg,Patient.phone,Patient.address,Patient.weight,Patient.height,Patient.lastOperations,Patient.notes,Patient.image,Company.name as company from Patient LEFT JOIN Company on Patient.companyId = Company.id");
            }
            else
            {
                loadTable("select Patient.id,Patient.name,Patient.dateOfBirth,Patient.type,Patient.dateTimeReg,Patient.phone,Patient.address,Patient.weight,Patient.height,Patient.lastOperations,Patient.notes,Patient.image,Company.name as company from Patient LEFT JOIN Company on Patient.companyId = Company.id  where Patient.name like '%" + text + "%'");

               
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormPatient frm = new FormPatient();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[12].Value.ToString();
                frm.id = txtHidden.Text;
                frm.txtName.Text = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                frm.comboGender.Text = dgvLoading.CurrentRow.Cells[10].Value.ToString();
                frm.dateOfBirth.Text = dgvLoading.CurrentRow.Cells[9].Value.ToString();

                frm.txtAddress.Text = dgvLoading.CurrentRow.Cells[7].Value.ToString();
                frm.txtPhone.Text = dgvLoading.CurrentRow.Cells[6].Value.ToString();
                frm.comboCompany.Text = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                frm.txtHeight.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtWeight.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.txtPreviousOperations.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
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
                    DataRow dro = tbl.Tables["dtShowPatient"].NewRow();

                    dro["name"] = dgvLoading[11, i].Value;
                    dro["type"] = dgvLoading[10, i].Value;
                    dro["dateOfBirth"] = dgvLoading[9, i].Value;
                    dro["dateTimeReg"] = dgvLoading[8, i].Value;
                    dro["address"] = dgvLoading[7, i].Value;
                    dro["phone"] = dgvLoading[6, i].Value;
                    dro["company"] = dgvLoading[5, i].Value;
                    dro["height"] = dgvLoading[4, i].Value;
                    dro["weight"] = dgvLoading[3, i].Value;
                    dro["lastOperations"] = dgvLoading[2, i].Value;
                    dro["notes"] = dgvLoading[1, i].Value;

                    tbl.Tables["dtShowPatient"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowPatient.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPatient"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowPatient.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPatient"]));

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
