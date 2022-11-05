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

namespace ClinicApp.Forms.Users
{
    public partial class FormShowUsers : Form
    {
        public FormShowUsers()
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
                                row["privilege"],
                                row["password"],
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
                loadTable("select * from Users");
            }
            else
            {
                loadTable("select * from Users where name like '%" + text + "%'");
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
                        MessageBox.Show("حدد المستخدم المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Users Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select * from Users");
                }



            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormUsers frm = new FormUsers();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtName.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.txtPassword.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.comboPriv.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[0].Value.ToString();
                frm.id = txtHidden.Text;
                frm.refreshForm = this;
                frm.Show();

            }

            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void FormShowUsers_Load(object sender, EventArgs e)
        {
            loadTable("select * from Users");

            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }
    }
}
