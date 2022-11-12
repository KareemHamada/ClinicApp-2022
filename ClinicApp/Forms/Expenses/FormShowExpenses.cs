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

namespace ClinicApp.Forms.Expenses
{
    public partial class FormShowExpenses : Form
    {
        public FormShowExpenses()
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
                                row["userName"],
                                row["notes"],
                                row["dateTime"],
                                row["money"],
                                row["expensesType"],
                                row["name"],
                                row["id"],
                            }
                        ); ;
                }
            }
        }
        private void FormShowExpenses_Load(object sender, EventArgs e)
        {
            // for combo ExpensesType
            Helper.fillComboBox(comboExpensesType, "Select id,name from ExpensesTypes", "name", "id");

            loadTable("select Expenses.id,Expenses.name,Expenses.dateTime,Expenses.money,Expenses.notes,Users.name as userName,ExpensesTypes.name as expensesType from Expenses,Users,ExpensesTypes where Expenses.expensesTypeId = ExpensesTypes.id and Expenses.userId = Users.id");

            txtHidden = new TextBox();
            txtHidden.Visible = false;


            // hide and show buttons
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select expenseDelete,expenseUpdate from Users where id = '" + declarations.userId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row["expenseDelete"].ToString() == "False")
                {
                    btnDelete.Visible = false;
                }
                if (row["expenseUpdate"].ToString() == "False")
                {
                    btnUpdate.Visible = false;
                }
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
                        MessageBox.Show(" حدد المصروف المراد حذفه ");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Expenses Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select Expenses.id,Expenses.name,Expenses.dateTime,Expenses.money,Expenses.notes,Users.name as userName,ExpensesTypes.name as expensesType from Expenses,Users,ExpensesTypes where Expenses.expensesTypeId = ExpensesTypes.id and Expenses.userId = Users.id");

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormAddExpenses frm = new FormAddExpenses();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[6].Value.ToString();
                frm.txtName.Text = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                frm.comboExpensesTypeText = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtMoney.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.dtpDateTime.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                
                frm.id = txtHidden.Text;
                frm.refreshForm = this;
                frm.Show();
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
                loadTable("select Expenses.id,Expenses.name,Expenses.dateTime,Expenses.money,Expenses.notes,Users.name as userName,ExpensesTypes.name as expensesType from Expenses,Users,ExpensesTypes where Expenses.expensesTypeId = ExpensesTypes.id and Expenses.userId = Users.id");
            }
            else
            {
                loadTable("select Expenses.id,Expenses.name,Expenses.dateTime,Expenses.money,Expenses.notes,Users.name as userName,ExpensesTypes.name as expensesType from Expenses,Users,ExpensesTypes where Expenses.expensesTypeId = ExpensesTypes.id and Expenses.userId = Users.id and (Expenses.name like '%" + text + "%' or ExpensesTypes.name like '%" + text + "%')");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //loadTable("select Expenses.id,Expenses.name,Expenses.dateTime,Expenses.money,Expenses.notes,Users.name as userName,ExpensesTypes.name as expensesType from Expenses,Users,ExpensesTypes where Expenses.expensesTypeId = ExpensesTypes.id and Expenses.userId = Users.id and ExpensesTypes.name like '%" + comboExpensesType.Text + "%'");

            loadTable("select Expenses.id,Expenses.name,Expenses.dateTime,Expenses.money,Expenses.notes,Users.name as userName,ExpensesTypes.name as expensesType from Expenses,Users,ExpensesTypes where Expenses.expensesTypeId = ExpensesTypes.id and Expenses.userId = Users.id and ExpensesTypes.name = '" + comboExpensesType.Text + "' and Expenses.dateTime >= '" + dtpDateTimeFrom.Value + "' and Expenses.dateTime <= '" + dtpDateTimeTo.Value +"'");
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select Expenses.id,Expenses.name,Expenses.dateTime,Expenses.money,Expenses.notes,Users.name as userName,ExpensesTypes.name as expensesType from Expenses,Users,ExpensesTypes where Expenses.expensesTypeId = ExpensesTypes.id and Expenses.userId = Users.id");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsTools tbl = new dsTools();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtExpenses"].NewRow();
                    dro["name"] = dgvLoading[5, i].Value;
                    dro["expensesType"] = dgvLoading[4, i].Value;                    
                    dro["money"] = dgvLoading[3, i].Value;
                    dro["dateTime"] = dgvLoading[2, i].Value;
                    dro["notes"] = dgvLoading[1, i].Value;
                    dro["user"] = dgvLoading[0, i].Value;
                    tbl.Tables["dtExpenses"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowExpenses.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtExpenses"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowExpenses.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtExpenses"]));

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
