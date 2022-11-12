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

namespace ClinicApp.Forms.Settings.Pharmacy
{
    public partial class FormShowMedicineUnits : Form
    {
        public FormShowMedicineUnits()
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
                                row["notes"],
                                row["name"],
                                row["id"],
                            }
                        ); ;
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
                loadTable("select * from medicineUnit");
            }
            else
            {
                loadTable("select * from medicineUnit where name like '%" + text + "%'");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string id = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                    if (id == "")
                    {
                        MessageBox.Show("حدد الدواء المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from medicineUnit Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select * from medicineUnit");

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormAddMedicineUnit frm = new FormAddMedicineUnit();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtName.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[0].Value.ToString();
                frm.id = txtHidden.Text;
                frm.refreshForm = this;
                frm.Show();
            }
        }

        private void FormShowMedicineUnits_Load(object sender, EventArgs e)
        {
            loadTable("select * from medicineUnit");

            txtHidden = new TextBox();
            txtHidden.Visible = false;

            // hide and show buttons
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select settingDelete,settingUpdate from Users where id = '" + declarations.userId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row["settingDelete"].ToString() == "False")
                {
                    btnDelete.Visible = false;
                }
                if (row["settingUpdate"].ToString() == "False")
                {
                    btnUpdate.Visible = false;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsTools tbl = new dsTools();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowMedicineUnits"].NewRow();
                    dro["name"] = dgvLoading[1, i].Value;
                    dro["notes"] = dgvLoading[0, i].Value;

                    tbl.Tables["dtShowMedicineUnits"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowMedicineUnits.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowMedicineUnits"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowMedicineUnits.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowMedicineUnits"]));

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
