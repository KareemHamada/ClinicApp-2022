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

namespace ClinicApp.Forms.Employees
{
    public partial class FormShowEmployees : Form
    {
        public FormShowEmployees()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtHidden;
        private TextBox txtImage;
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
                                row["age"],
                                row["job"],
                                row["gender"],
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
                loadTable("select Employees.id,Employees.name,Employees.gender,Employees.age,Employees.notes,Employees.address,Employees.phone,Employees.facebook,Employees.whatsApp,Employees.gmail,Employees.image,Specializations.name as job from Employees LEFT JOIN Specializations on Employees.jobId = Specializations.id");
            }
            else
            {
                loadTable("select Employees.id,Employees.name,Employees.gender,Employees.age,Employees.notes,Employees.address,Employees.phone,Employees.facebook,Employees.whatsApp,Employees.gmail,Employees.image,Specializations.name as job from Employees LEFT JOIN Specializations on Employees.jobId = Specializations.id where Employees.name like '%" + text + "%'");


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
                        MessageBox.Show("حدد الموظف المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Employees Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select Employees.id,Employees.name,Employees.gender,Employees.age,Employees.notes,Employees.address,Employees.phone,Employees.facebook,Employees.whatsApp,Employees.gmail,Employees.image,Specializations.name as job from Employees LEFT JOIN Specializations on Employees.jobId = Specializations.id");
                }
            }
                    
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormEmployee frm = new FormEmployee();

                txtHidden.Text = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                frm.txtName.Text = dgvLoading.CurrentRow.Cells[10].Value.ToString();
                frm.genderText = dgvLoading.CurrentRow.Cells[9].Value.ToString();
                frm.jobText = dgvLoading.CurrentRow.Cells[8].Value.ToString();
                frm.txtAge.Text = dgvLoading.CurrentRow.Cells[7].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[6].Value.ToString();
                frm.txtAddress.Text = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                frm.txtPhone.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtFaceBook.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.txtWhatsApp.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtEmail.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                frm.picBox.BackgroundImage = Helper.ByteToImage(dgvLoading.CurrentRow.Cells[0].Value);

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
                    DataRow dro = tbl.Tables["dtFormShowEmployees"].NewRow();

                    dro["name"] = dgvLoading[10, i].Value;
                    dro["gender"] = dgvLoading[9, i].Value;
                    dro["job"] = dgvLoading[8, i].Value;
                    dro["age"] = dgvLoading[7, i].Value;
                    dro["notes"] = dgvLoading[6, i].Value;
                    dro["address"] = dgvLoading[5, i].Value;
                    dro["phone"] = dgvLoading[4, i].Value;
                    dro["facebook"] = dgvLoading[3, i].Value;
                    dro["whatsApp"] = dgvLoading[2, i].Value;
                    dro["gmail"] = dgvLoading[1, i].Value;

                    tbl.Tables["dtFormShowEmployees"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowEmployees.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtFormShowEmployees"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowEmployees.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtFormShowEmployees"]));

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

        private void FormShowEmployees_Load(object sender, EventArgs e)
        {
            loadTable("select Employees.id,Employees.name,Employees.gender,Employees.age,Employees.notes,Employees.address,Employees.phone,Employees.facebook,Employees.whatsApp,Employees.gmail,Employees.image,Specializations.name as job from Employees LEFT JOIN Specializations on Employees.jobId = Specializations.id");


            txtHidden = new TextBox();
            txtHidden.Visible = false;


            // hide and show buttons
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select employeeDelete,employeeUpdate from Users where id = '" + declarations.userId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                // for doctor
                if (row["employeeDelete"].ToString() == "False")
                {
                    btnDelete.Visible = false;
                }
                if (row["employeeUpdate"].ToString() == "False")
                {
                    btnUpdate.Visible = false;
                }
            }
        }
    }
}
