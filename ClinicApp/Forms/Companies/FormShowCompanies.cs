using ClinicApp.Classes;
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

namespace ClinicApp.Forms.Companies
{
    public partial class FormShowCompanies : Form
    {
        public FormShowCompanies()
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
                                row["pPay"],
                                row["cPay"],
                                row["name"],
                                row["id"],
                            }
                        ); ;
                }
            }
        }

        private void FormShowCompanies_Load(object sender, EventArgs e)
        {
            loadTable("select * from Company");

            txtHidden = new TextBox();
            txtHidden.Visible = false;


            // hide and show buttons
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select companyDelete,companyUpdate from Users where id = '" + declarations.userId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row["companyDelete"].ToString() == "False")
                {
                    btnDelete.Visible = false;
                }
                if (row["companyUpdate"].ToString() == "False")
                {
                    btnUpdate.Visible = false;
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
                loadTable("select * from Company");
            }
            else
            {
                loadTable("select * from Company where name like '%" + text + "%'");
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
                        MessageBox.Show("حدد الشركة المراد حذفها");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Company Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select * from Company");
                }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormCompanies frm = new FormCompanies();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtName.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.txtCompanyPay.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtPatientPay.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[0].Value.ToString();
                frm.id = txtHidden.Text;
               
                frm.refreshForm = this;
                frm.Show();

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
