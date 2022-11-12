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

namespace ClinicApp.Forms.Income
{
    public partial class FormAddIncome : Form
    {
        public FormAddIncome()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        public string id = "";
        public string comboIncomeTypeText = "";

        public FormShowIncomes refreshForm;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {

                if (comboIncomesType.Text == "")
                {
                    MessageBox.Show("اختر نوع الايراد");
                    return;
                }

                if (txtMoney.Text == "")
                {
                    MessageBox.Show("ادخل المبلغ");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into Income (patientId,VisitingTypeId,dateTime,money,notes,userId) values (@name,@expensesTypeId,@dateTime,@money,@notes,@userId)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@patientId", comboPatient.SelectedValue);
                    cmd.Parameters.AddWithValue("@VisitingTypeId", comboIncomesType.SelectedValue);
                    cmd.Parameters.AddWithValue("@dateTime", dtpDateTime.Value);
                    cmd.Parameters.AddWithValue("@money", txtMoney.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@userId", declarations.userId);

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
                    MessageBox.Show("حدد الايراد المراد تعديله");
                    return;
                }
                

                if (comboIncomesType.Text == "")
                {
                    MessageBox.Show("اختر نوع الايراد");
                    return;
                }

                if (txtMoney.Text == "")
                {
                    MessageBox.Show("ادخل المبلغ");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update Income set patientId=@patientId,VisitingTypeId=@VisitingTypeId,dateTime=@dateTime,money=@money,notes=@notes,userId=@userId Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@patientId", comboPatient.SelectedValue);
                    cmd.Parameters.AddWithValue("@VisitingTypeId", comboIncomesType.SelectedValue);
                    cmd.Parameters.AddWithValue("@dateTime", dtpDateTime.Value);
                    cmd.Parameters.AddWithValue("@money", txtMoney.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@userId", declarations.userId);

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
                refreshForm.loadTable("select Income.id,patient.name as patient,Income.dateTime,Income.money,Income.notes,Users.name as userName,VisitingType.name as visitingType from Income,Users,VisitingType,patient where Income.VisitingTypeId = VisitingType.id and Income.userId = Users.id and and Income.patient.id = patient.id");
            }


            comboIncomesType.Text = "";
            comboPatient.Text = "";
            txtMoney.Text = "";
            txtNotes.Text = "";

            id = "";
        }

        private void FormAddIncome_Load(object sender, EventArgs e)
        {
            // for combo ExpensesType
            Helper.fillComboBox(comboExpensesType, "Select id,name from ExpensesTypes", "name", "id");
            dtpDateTime.Value = DateTime.Now;


            comboExpensesType.Text = comboExpensesTypeText;
        }

        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
