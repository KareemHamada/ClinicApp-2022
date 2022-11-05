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

namespace ClinicApp.Forms.Locations.Analysis
{
    public partial class FormShowAnalysisLoc : Form
    {
        public FormShowAnalysisLoc()
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
                                row["phone"],
                                row["address"],
                                row["government"],
                                row["name"],
                                row["id"],
                            }
                        ); ;
                }
            }
        }
        private void FormShowAnalysisLoc_Load(object sender, EventArgs e)
        {
            loadTable("select AnalysisLocation.id,AnalysisLocation.name,AnalysisLocation.address,AnalysisLocation.phone,Governments.name as government from AnalysisLocation,Governments where AnalysisLocation.governmentId = Governments.id");

            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }
        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select AnalysisLocation.id,AnalysisLocation.name,AnalysisLocation.address,AnalysisLocation.phone,Governments.name as government from AnalysisLocation,Governments where AnalysisLocation.governmentId = Governments.id");
            }
            else
            {
                

                loadTable("select AnalysisLocation.id," +
                    "AnalysisLocation.name," +
                    "AnalysisLocation.address," +
                    "AnalysisLocation.phone," +
                    "Governments.name as government" +
                    " from AnalysisLocation,Governments" +
                    " where " +
                    "AnalysisLocation.governmentId = Governments.id " +
                    "and(AnalysisLocation.name like '%" + text + "%' " +
                    "or AnalysisLocation.address like '%" + text + "%' " +
                    "or AnalysisLocation.phone like '%" + text + "%' " +
                    "or Governments.name like '%" + text + "%')");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string id = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                    if (id == "")
                    {
                        MessageBox.Show("حدد مركز التحليل المراد حذفها");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from AnalysisLocation Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select AnalysisLocation.id,AnalysisLocation.name,AnalysisLocation.address,AnalysisLocation.phone,Governments.name as government from AnalysisLocation,Governments where AnalysisLocation.governmentId = Governments.id");

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormAddAnalysisLoc frm = new FormAddAnalysisLoc();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtName.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.txtGovernment = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtAddress.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                frm.txtPhone.Text = dgvLoading.CurrentRow.Cells[0].Value.ToString();
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
                    DataRow dro = tbl.Tables["dtShowAnalysisLoc"].NewRow();
                    dro["name"] = dgvLoading[3, i].Value;
                    dro["government"] = dgvLoading[2, i].Value;
                    dro["address"] = dgvLoading[1, i].Value;
                    dro["phone"] = dgvLoading[0, i].Value;

                    tbl.Tables["dtShowAnalysisLoc"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowAnalysisLoc.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowAnalysisLoc"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowAnalysisLoc.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowAnalysisLoc"]));

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
