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

namespace ClinicApp.Forms.Expenses
{
    public partial class FormAddExpenses : Form
    {
        public FormAddExpenses()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;

        public string id = "";
        public string comboExpensesTypeText = "";

        public FormShowExpenses refreshForm;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل المصروف ");
                    return;
                }

                if (comboExpensesType.Text == "")
                {
                    MessageBox.Show("اختر نوع المصروف");
                    return;
                }

                if (txtMoney.Text == "")
                {
                    MessageBox.Show("ادخل المبلغ");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into Expenses (name,expensesTypeId,dateTime,money,notes,userId) values (@name,@expensesTypeId,@dateTime,@money,@notes,@userId)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@expensesTypeId", comboExpensesType.SelectedValue);
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
                    MessageBox.Show("حدد المصروف المراد تعديله");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل المصروف ");
                    return;
                }

                if (comboExpensesType.Text == "")
                {
                    MessageBox.Show("اختر نوع المصروف");
                    return;
                }

                if (txtMoney.Text == "")
                {
                    MessageBox.Show("ادخل المبلغ");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update Expenses set name=@name,expensesTypeId=@expensesTypeId,dateTime=@dateTime,money=@money,notes=@notes,userId=@userId Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@expensesTypeId", comboExpensesType.SelectedValue);
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
                refreshForm.loadTable("select Expenses.id,Expenses.name,Expenses.dateTime,Expenses.money,Expenses.notes,Users.name as userName,ExpensesTypes.name as expensesType from Expenses,Users,ExpensesTypes where Expenses.expensesTypeId = ExpensesTypes.id and Expenses.userId = Users.id");
            }


            txtName.Text = "";
            comboExpensesType.Text = "";
            txtMoney.Text = "";
            txtNotes.Text = "";

            id = "";
        }

        private void FormAddExpenses_Load(object sender, EventArgs e)
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
