﻿using ClinicApp.Classes;
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
    public partial class FormAddDrug : Form
    {
        public FormAddDrug()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        public string id = "";
        public FormShowDrugs refreshForm;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الدواء");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into Drugs (name,company,notes) values (@name,@company,@notes)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@company", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("تم الاضافة بنجاح");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    adoClass.sqlcn.Close();
                }

            }
            else
            {
                if (id == "")
                {
                    MessageBox.Show("حدد الدواء المراد تعديله");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم الدواء");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update Drugs set name = @name,company=@company,notes=@notes Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@company", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("تم التعديل بنجاح");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    adoClass.sqlcn.Close();
                }

                this.Close();
                refreshForm.loadTable("select * from Drugs");
            }


            txtName.Text = "";
            txtCompany.Text = "";
            txtNotes.Text = "";
            id = "";

            this.DialogResult = DialogResult.OK;
        }
    }
}
