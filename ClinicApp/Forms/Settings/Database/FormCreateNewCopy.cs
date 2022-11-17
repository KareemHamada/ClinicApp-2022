using ClinicApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicApp.Forms.Settings.Database
{
    public partial class FormCreateNewCopy : Form
    {
        public FormCreateNewCopy()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            //string database = adoClass.sqlcn.Database.ToString();
            //StreamReader sr = new StreamReader(Application.StartupPath + "\\Serial\\serial.txt");
            //string txt = sr.ReadLine();
            //string ds = sr.ReadLine();

            //sr.Close();
            //txt = Regex.Replace(txt, @"\s+", "");
            //ds = Regex.Replace(ds, @"\s+", "");

            if (txtBackup.Text == "")
            {
                MessageBox.Show("اختار المسار");
            }
            else
            {
                SqlCommand cmd;

                string fileName = txtBackup.Text + "\\Clinic" + DateTime.Now.ToShortDateString().Replace("/", "-") + " - " + DateTime.Now.ToLongTimeString().Replace(":", "-");
                string strQuery = "Backup Database Clinic to Disk='" + fileName + ".bak'";
                cmd = new SqlCommand(strQuery, adoClass.sqlcn);
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                cmd.ExecuteNonQuery();
                adoClass.sqlcn.Close();
                MessageBox.Show("تم الحفظ بنجاح", "انشاء نسخة احتياطبة", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = dlg.SelectedPath;
                //btnBackup.Enabled = true;
            }
        }
    }
}
