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
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        public string id = "";
        public string doctorText = "";
        public FormShowUsers refreshForm;



        private void FormUsers_Load(object sender, EventArgs e)
        {
            

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (id == "")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم المستخدم");
                    return;
                }
                if (comboPriv.Text == "")
                {
                    MessageBox.Show("اختر الصلاحية");
                    return;
                }
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("ادخل الباسورد");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into Users (name,privilege,password,notes," +
                        "doctorAdd,doctorDelete,doctorUpdate,doctorShow,doctorHome," +
                        "employeeAdd,employeeDelete,employeeUpdate,employeeShow,employeeHome," +
                        "userAdd,userDelete,userUpdate,userShow,userHome," +
                        "patientAdd,patientDelete,patientUpdate,patientShow,patientHome," +
                        "resAdd,resDelete,resUpdate,resShow,resHome," +
                        "examAdd,examDelete,examUpdate,examShow,examHome," +
                        "expenseAdd,expenseDelete,expenseUpdate,expenseShow,expenseHome," +
                        "incomeAdd,incomeDelete,incomeUpdate,incomeShow,incomeHome," +
                        "companyAdd,companyDelete,companyUpdate,companyShow,companyHome," +
                        "smartAdd,smartDelete,smartUpdate,smartShow,smartHome," +
                        "locationAdd,locationDelete,locationUpdate,locationShow,locationHome," +
                        "settingAdd,settingDelete,settingUpdate,settingShow,settingHome" +
                        ") values (@name,@privilege,@password,@notes," +
                        "@doctorAdd,@doctorDelete,@doctorUpdate,@doctorShow,@doctorHome," +
                    "@employeeAdd,@employeeDelete,@employeeUpdate,@employeeShow,@employeeHome," +
                        "@userAdd,@userDelete,@userUpdate,@userShow,@userHome," +
                        "@patientAdd,@patientDelete,@patientUpdate,@patientShow,@patientHome," +
                        "@resAdd,@resDelete,@resUpdate,@resShow,@resHome," +
                        "@examAdd,@examDelete,@examUpdate,@examShow,@examHome," +
                        "@expenseAdd,@expenseDelete,@expenseUpdate,@expenseShow,@expenseHome," +
                        "@incomeAdd,@incomeDelete,@incomeUpdate,@incomeShow,@incomeHome," +
                        "@companyAdd,@companyDelete,@companyUpdate,@companyShow,@companyHome," +
                        "@smartAdd,@smartDelete,@smartUpdate,@smartShow,@smartHome," +
                        "@locationAdd,@locationDelete,@locationUpdate,@locationShow,@locationHome," +
                        "@settingAdd,@settingDelete,@settingUpdate,@settingShow,@settingHome" +
                        ")", adoClass.sqlcn);



                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@privilege", comboPriv.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);

                    // doctor priviledge
                    cmd.Parameters.AddWithValue("@doctorAdd", chbAddDoctor.Checked.ToString());
                    cmd.Parameters.AddWithValue("@doctorDelete", chbDeleteDoctor.Checked.ToString());
                    cmd.Parameters.AddWithValue("@doctorUpdate", chbUpdateDoctor.Checked.ToString());
                    cmd.Parameters.AddWithValue("@doctorShow", chbShowDoctor.Checked.ToString());
                    cmd.Parameters.AddWithValue("@doctorHome", chbHomeDoctor.Checked.ToString());
                    
                    // employee priviledge
                    cmd.Parameters.AddWithValue("@employeeAdd", chbAddEmployee.Checked.ToString());
                    cmd.Parameters.AddWithValue("@employeeDelete", chbDeleteEmployee.Checked.ToString());
                    cmd.Parameters.AddWithValue("@employeeUpdate", chbUpdateEmployee.Checked.ToString());
                    cmd.Parameters.AddWithValue("@employeeShow", chbShowEmployee.Checked.ToString());
                    cmd.Parameters.AddWithValue("@employeeHome", chbHomeEmployee.Checked.ToString());

                    // user priviledge
                    cmd.Parameters.AddWithValue("@userAdd", chbAddUser.Checked.ToString());
                    cmd.Parameters.AddWithValue("@userDelete", chbDeleteUser.Checked.ToString());
                    cmd.Parameters.AddWithValue("@userUpdate", chbUpdateUser.Checked.ToString());
                    cmd.Parameters.AddWithValue("@userShow", chbShowUser.Checked.ToString());
                    cmd.Parameters.AddWithValue("@userHome", chbHomeUser.Checked.ToString());

                    // patient priviledge
                    cmd.Parameters.AddWithValue("@patientAdd", chbAddPatient.Checked.ToString());
                    cmd.Parameters.AddWithValue("@patientDelete", chbDeletePatient.Checked.ToString());
                    cmd.Parameters.AddWithValue("@patientUpdate", chbUpdatePatient.Checked.ToString());
                    cmd.Parameters.AddWithValue("@patientShow", chbShowPatient.Checked.ToString());
                    cmd.Parameters.AddWithValue("@patientHome", chbHomePatient.Checked.ToString());


                    // reservation priviledge
                    cmd.Parameters.AddWithValue("@resAdd", chbAddRes.Checked.ToString());
                    cmd.Parameters.AddWithValue("@resDelete", chbDeleteRes.Checked.ToString());
                    cmd.Parameters.AddWithValue("@resUpdate", chbUpdateRes.Checked.ToString());
                    cmd.Parameters.AddWithValue("@resShow", chbShowRes.Checked.ToString());
                    cmd.Parameters.AddWithValue("@resHome", chbHomeRes.Checked.ToString());

                    // Examination priviledge
                    cmd.Parameters.AddWithValue("@examAdd", chbAddExam.Checked.ToString());
                    cmd.Parameters.AddWithValue("@examDelete", chbDeleteExam.Checked.ToString());
                    cmd.Parameters.AddWithValue("@examUpdate", chbUpdateExam.Checked.ToString());
                    cmd.Parameters.AddWithValue("@examShow", chbShowExam.Checked.ToString());
                    cmd.Parameters.AddWithValue("@examHome", chbHomeExam.Checked.ToString());


                    // expenses priviledge
                    cmd.Parameters.AddWithValue("@expenseAdd", chbAddExpense.Checked.ToString());
                    cmd.Parameters.AddWithValue("@expenseDelete", chbDeleteExpense.Checked.ToString());
                    cmd.Parameters.AddWithValue("@expenseUpdate", chbUpdateExpense.Checked.ToString());
                    cmd.Parameters.AddWithValue("@expenseShow", chbShowExpense.Checked.ToString());
                    cmd.Parameters.AddWithValue("@expenseHome", chbHomeExpense.Checked.ToString());


                    // income priviledge
                    cmd.Parameters.AddWithValue("@incomeAdd", chbAddIncome.Checked.ToString());
                    cmd.Parameters.AddWithValue("@incomeDelete", chbDeleteIncome.Checked.ToString());
                    cmd.Parameters.AddWithValue("@incomeUpdate", chbUpdateIncome.Checked.ToString());
                    cmd.Parameters.AddWithValue("@incomeShow", chbShowIncome.Checked.ToString());
                    cmd.Parameters.AddWithValue("@incomeHome", chbHomeIncome.Checked.ToString());

                    // company priviledge
                    cmd.Parameters.AddWithValue("@companyAdd", chbAddCompany.Checked.ToString());
                    cmd.Parameters.AddWithValue("@companyDelete", chbDeleteCompany.Checked.ToString());
                    cmd.Parameters.AddWithValue("@companyUpdate", chbUpdateCompany.Checked.ToString());
                    cmd.Parameters.AddWithValue("@companyShow", chbShowCompany.Checked.ToString());
                    cmd.Parameters.AddWithValue("@companyHome", chbHomeCompany.Checked.ToString());

                    // smart assistant priviledge
                    cmd.Parameters.AddWithValue("@smartAdd", chbAddSmartAssistant.Checked.ToString());
                    cmd.Parameters.AddWithValue("@smartDelete", chbDeleteSmartAssistant.Checked.ToString());
                    cmd.Parameters.AddWithValue("@smartUpdate", chbUpdateSmartAssistant.Checked.ToString());
                    cmd.Parameters.AddWithValue("@smartShow", chbShowSmartAssistant.Checked.ToString());
                    cmd.Parameters.AddWithValue("@smartHome", chbHomeSmartAssistant.Checked.ToString());

                    
                    // locations priviledge
                    cmd.Parameters.AddWithValue("@locationAdd", chbAddLoc.Checked.ToString());
                    cmd.Parameters.AddWithValue("@locationDelete", chbDeleteLoc.Checked.ToString());
                    cmd.Parameters.AddWithValue("@locationUpdate", chbUpdateLoc.Checked.ToString());
                    cmd.Parameters.AddWithValue("@locationShow", chbShowLoc.Checked.ToString());
                    cmd.Parameters.AddWithValue("@locationHome", chbHomeLoc.Checked.ToString());



                    
                    // settings priviledge
                    cmd.Parameters.AddWithValue("@settingAdd", chbAddSetting.Checked.ToString());
                    cmd.Parameters.AddWithValue("@settingDelete", chbDeleteSetting.Checked.ToString());
                    cmd.Parameters.AddWithValue("@settingUpdate", chbUpdateSetting.Checked.ToString());
                    cmd.Parameters.AddWithValue("@settingShow", chbShowSetting.Checked.ToString());
                    cmd.Parameters.AddWithValue("@settingHome", chbHomeSetting.Checked.ToString());

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("تم اضافة المستخدم بنجاح");

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
                    MessageBox.Show("حدد العنصر المراد تعديلة");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("ادخل اسم المستخدم");
                    return;
                }
                if (comboPriv.Text == "")
                {
                    MessageBox.Show("اختر الصلاحية");
                    return;
                }
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("ادخل الباسورد");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Update Users set name=@name,privilege=@privilege,password=@password,notes=@notes Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@privilege", comboPriv.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
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
                refreshForm.loadTable("select * from Users");
            }

            txtName.Text = "";
            comboPriv.Text = "";
            txtPassword.Text = "";
            txtNotes.Text = "";

            id = "";


            //foreach (CheckBox tb in this.Controls.OfType<CheckBox>())
            //{
            //    tb.Checked = false;
            //}

            //foreach (Control cBox in Controls)
            //{
            //    if (cBox is CheckBox)
            //    {
            //        ((CheckBox)cBox).Checked = false;
            //    }
            //}
        }

 
    }
}


