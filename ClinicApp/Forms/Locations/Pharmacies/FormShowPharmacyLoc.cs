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

namespace ClinicApp.Forms.Locations.Pharmacies
{
    public partial class FormShowPharmacyLoc : Form
    {
        public FormShowPharmacyLoc()
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

        private void FormShowPharmacyLoc_Load(object sender, EventArgs e)
        {
            loadTable("select PharmaciesLocations.id,PharmaciesLocations.name,PharmaciesLocations.address,PharmaciesLocations.phone,Governments.name as government from PharmaciesLocations,Governments where PharmaciesLocations.governmentId = Governments.id");

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
                loadTable("select PharmaciesLocations.id,PharmaciesLocations.name,PharmaciesLocations.address,PharmaciesLocations.phone,Governments.name as government from PharmaciesLocations,Governments where PharmaciesLocations.governmentId = Governments.id");
            }
            else
            {
                

                loadTable("select PharmaciesLocations.id," +
                    "PharmaciesLocations.name," +
                    "PharmaciesLocations.address," +
                    "PharmaciesLocations.phone," +
                    "Governments.name as government" +
                    " from PharmaciesLocations,Governments" +
                    " where " +
                    "PharmaciesLocations.governmentId = Governments.id " +
                    "and(PharmaciesLocations.name like '%" + text + "%' " +
                    "or PharmaciesLocations.address like '%" + text + "%' " +
                    "or PharmaciesLocations.phone like '%" + text + "%' " +
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
                        MessageBox.Show("حددالصيدلية المراد حذفها");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from PharmaciesLocations Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select PharmaciesLocations.id,PharmaciesLocations.name,PharmaciesLocations.address,PharmaciesLocations.phone,Governments.name as government from PharmaciesLocations,Governments where PharmaciesLocations.governmentId = Governments.id");

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                FormAddPharmacyLoc frm = new FormAddPharmacyLoc();
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
    }
}
