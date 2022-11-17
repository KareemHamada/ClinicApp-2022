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

namespace ClinicApp.Forms.Settings.Database
{
    public partial class FormRestoreCopy : Form
    {
        public FormRestoreCopy()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = dlg.FileName;
                //btnBackup.Enabled = true;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            
            if(txtBackup.Text == string.Empty)
            {
                MessageBox.Show("اختار الملف المراد استرجاعه");
            }
            else
            {
                string database = adoClass.sqlcn.Database.ToString();
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                try
                {
                    string str1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                    SqlCommand cmd1 = new SqlCommand(str1,adoClass.sqlcn);
                    cmd1.ExecuteNonQuery();

                    string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + txtBackup.Text + "' WITH REPLACE;";
                    SqlCommand cmd2 = new SqlCommand(str2, adoClass.sqlcn);
                    cmd2.ExecuteNonQuery();

                    string str3 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                    SqlCommand cmd3 = new SqlCommand(str3, adoClass.sqlcn);
                    cmd3.ExecuteNonQuery();


                    MessageBox.Show("تم استرجاع النسخة الاحتياطية بنجاح");

                    adoClass.sqlcn.Close();

                }
                catch
                {

                }
            }
        }
    }
}
