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

namespace ClinicApp.Forms.SmartAssistant
{
    public partial class FormShowSmartAssistant : Form
    {
        public FormShowSmartAssistant()
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
                                row["nameEnglish"],
                                row["nameArabic"],
                                row["id"],
                            }
                        ); ;
                }
            }
        }
        private void FormShowSmartAssistant_Load(object sender, EventArgs e)
        {
            loadTable("select id,nameArabic,nameEnglish from SmartAssistant");

            txtHidden = new TextBox();
            txtHidden.Visible = false;


            //dgvLoading.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            


        }
       
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select id,nameArabic,nameEnglish from SmartAssistant");
            }
            else
            {
                loadTable("select id,nameArabic,nameEnglish from SmartAssistant " +
                    "where " +
                    "nameArabic like '%" + text + "%' " +
                    "or nameArabic like '%" + text + "%' " +
                    "or nameEnglish like '%" + text + "%'");

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
                        MessageBox.Show("حدد المرض المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from SmartAssistant Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select id,nameArabic,nameEnglish from SmartAssistant");

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FormAddToSmartAssistant frm = new FormAddToSmartAssistant();
            txtHidden.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select * from SmartAssistant where id = '" + txtHidden.Text + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                frm.txtNameArabic.Text = row["nameArabic"].ToString();
                frm.txtNameEnglish.Text = row["nameEnglish"].ToString();
                frm.txtDefination.Text = row["defination"].ToString();
                frm.txtSymptoms.Text = row["symptom"].ToString();
                frm.txtMedicine.Text = row["medicine"].ToString();
                frm.txtReasons.Text = row["reasons"].ToString();
                frm.txtNotes.Text = row["notes"].ToString();
                frm.txtTypes.Text = row["types"].ToString();
                frm.pic1.BackgroundImage = Helper.ByteToImage(row["image1"]);
                frm.pic2.BackgroundImage = Helper.ByteToImage(row["image2"]);
                frm.pic3.BackgroundImage = Helper.ByteToImage(row["image3"]);
                frm.pic4.BackgroundImage = Helper.ByteToImage(row["image4"]);
                frm.pic5.BackgroundImage = Helper.ByteToImage(row["image5"]);
                frm.pic6.BackgroundImage = Helper.ByteToImage(row["image6"]);
                frm.pic7.BackgroundImage = Helper.ByteToImage(row["image7"]);
                frm.pic8.BackgroundImage = Helper.ByteToImage(row["image8"]);
                frm.id = txtHidden.Text;
                frm.refreshForm = this;
                frm.Show();
                
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            FormAddToSmartAssistant frm = new FormAddToSmartAssistant();
            txtHidden.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select * from SmartAssistant where id = '" + txtHidden.Text + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                frm.txtNameArabic.Text = row["nameArabic"].ToString();
                frm.txtNameEnglish.Text = row["nameEnglish"].ToString();
                frm.txtDefination.Text = row["defination"].ToString();
                frm.txtSymptoms.Text = row["symptom"].ToString();
                frm.txtMedicine.Text = row["medicine"].ToString();
                frm.txtReasons.Text = row["reasons"].ToString();
                frm.txtNotes.Text = row["notes"].ToString();
                frm.txtTypes.Text = row["types"].ToString();
                frm.pic1.BackgroundImage = Helper.ByteToImage(row["image1"]);
                frm.pic2.BackgroundImage = Helper.ByteToImage(row["image2"]);
                frm.pic3.BackgroundImage = Helper.ByteToImage(row["image3"]);
                frm.pic4.BackgroundImage = Helper.ByteToImage(row["image4"]);
                frm.pic5.BackgroundImage = Helper.ByteToImage(row["image5"]);
                frm.pic6.BackgroundImage = Helper.ByteToImage(row["image6"]);
                frm.pic7.BackgroundImage = Helper.ByteToImage(row["image7"]);
                frm.pic8.BackgroundImage = Helper.ByteToImage(row["image8"]);


                frm.id = txtHidden.Text;

                frm.refreshForm = this;
                frm.btnAdd.Visible = false;
                frm.btnDeletePdf.Visible = false;

                frm.btnChoose1.Visible = false;
                frm.btnChoose2.Visible = false;
                frm.btnChoose3.Visible = false;
                frm.btnChoose4.Visible = false;
                frm.btnChoose5.Visible = false;
                frm.btnChoose6.Visible = false;
                frm.btnChoose7.Visible = false;
                frm.btnChoose8.Visible = false;


                frm.btnRemovePic1.Visible = false;
                frm.btnRemovePic2.Visible = false;
                frm.btnRemovePic3.Visible = false;
                frm.btnRemovePic4.Visible = false;
                frm.btnRemovePic5.Visible = false;
                frm.btnRemovePic6.Visible = false;
                frm.btnRemovePic7.Visible = false;
                frm.btnRemovePic8.Visible = false;

                frm.btnAddPdf.Text = "عرض PDF";
                frm.showPDFMode = true;
                frm.Show();

            }
        }
    }
}
