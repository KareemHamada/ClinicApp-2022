using ClinicApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicApp.Forms
{
    public partial class FormStartUp : Form
    {
        public FormStartUp()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value == 10)
            {
                ClassLoading loading = new ClassLoading();
                loading.loadSystemOptions();
            }

            if (progressBar.Value == 20)
            {
                Close();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                progressBar.Value++;
                progressBar.Refresh();
            }
        }
    }
}
