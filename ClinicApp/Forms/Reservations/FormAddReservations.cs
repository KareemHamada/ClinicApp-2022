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

namespace ClinicApp.Forms.Reservations
{
    public partial class FormAddReservations : Form
    {
        public FormAddReservations()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private string newID = "";
        public string id = "";
        public FormShowReservations refreshForm;
        public string clinicText = "";
        public string doctorText = "";
        public string bookingTypeText = "";
        public string bookingStatusText = "";
        public string patientText = "";
        public string visitingTypeText = "";

        private void FormAddReservations_Load(object sender, EventArgs e)
        {
            // for combo clinics
            Helper.fillComboBox(comboClinic, "Select id,name from Clinics", "name", "id");
            // for combo doctor
            Helper.fillComboBox(comboDoctor, "Select id,name from Doctors", "name", "id");
            // for combo patient
            Helper.fillComboBox(comboPatient, "Select id,name from patient", "name", "id");
            // for combo booking type
            Helper.fillComboBox(comboBookingType, "Select id,name from BookingType", "name", "id");
            // for combo booking status 
            //Helper.fillComboBox(comboBookingStatus, "Select id,name from BookingStatus", "name", "id");
            // for combo visiting Type 
            Helper.fillComboBox(comboVisitingType, "Select id,name from VisitingType", "name", "id");


            dateOfReservations.Value = DateTime.Now;


            comboClinic.Text =  clinicText;
            comboDoctor.Text = doctorText;
            comboBookingType.Text = bookingTypeText;
            comboBookingStatus.Text = bookingStatusText;
            comboPatient.Text = patientText;
            comboVisitingType.Text = visitingTypeText;

    }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (comboClinic.Text == "")
                {
                    MessageBox.Show("اختر العيادة");
                    return;
                }
                if (comboDoctor.Text == "")
                {
                    MessageBox.Show("اختر الدكتور");
                    return;
                }
                if (comboPatient.Text == "")
                {
                    MessageBox.Show("اختر المريض");
                    return;
                }
                if (comboBookingType.Text == "")
                {
                    MessageBox.Show("اختر نوع الحجز");
                    return;
                }
                if (comboBookingStatus.Text == "")
                {
                    MessageBox.Show("اختر حالة الحجز");
                    return;
                }
                if (comboVisitingType.Text == "")
                {
                    MessageBox.Show("اختر نوع الزيارة");
                    return;
                }
                if (txtMoney.Text == "")
                {
                    MessageBox.Show("ادخل المبلغ");
                    return;
                }

                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT MAX (reservationNumber) +1 FROM Reservations where clinicId = '" + comboClinic.SelectedValue + "'and doctorId = '" + comboDoctor.SelectedValue + "' and date = '" +  dateOfReservations.Value + "'", adoClass.sqlcn);

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }
                    int newID = 0;
                    if (cmd.ExecuteScalar().ToString() == "")
                    {
                        newID = 1;
                    }
                    else
                    {
                        newID = (int)cmd.ExecuteScalar();
                    }
                    

                    cmd = new SqlCommand("Insert into Reservations (clinicId,doctorId,patientId,bookingTypeId,bookingStatus,phoneReservationPerson,reservationPerson,date,visitTypeId,money,discount,moneyAfterDiscount,notes,reservationNumber,userId) values (@clinicId,@doctorId,@patientId,@bookingTypeId,@bookingStatus,@phoneReservationPerson,@reservationPerson,@date,@visitTypeId,@money,@discount,@moneyAfterDiscount,@notes,@reservationNumber,@userId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@clinicId", comboClinic.SelectedValue);
                    cmd.Parameters.AddWithValue("@doctorId", comboDoctor.SelectedValue);
                    cmd.Parameters.AddWithValue("@patientId", comboPatient.SelectedValue);
                    cmd.Parameters.AddWithValue("@bookingTypeId", comboBookingType.SelectedValue);
                    cmd.Parameters.AddWithValue("@bookingStatus", comboBookingStatus.Text);
                    cmd.Parameters.AddWithValue("@phoneReservationPerson", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@reservationPerson", txtBookingPersonName.Text);
                    cmd.Parameters.AddWithValue("@date", dateOfReservations.Value);
                    cmd.Parameters.AddWithValue("@visitTypeId", comboVisitingType.SelectedValue);
                    cmd.Parameters.AddWithValue("@money", txtMoney.Text);
                    cmd.Parameters.AddWithValue("@discount", txtDiscount.Text);
                    cmd.Parameters.AddWithValue("@moneyAfterDiscount", txtMoneyAfterDiscount.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@reservationNumber", newID);
                    cmd.Parameters.AddWithValue("@userId", declarations.userId);

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
                    MessageBox.Show("حدد الحجز المراد تعديله");
                    return;
                }
                if (comboClinic.Text == "")
                {
                    MessageBox.Show("اختر العيادة");
                    return;
                }
                if (comboDoctor.Text == "")
                {
                    MessageBox.Show("اختر الدكتور");
                    return;
                }
                if (comboPatient.Text == "")
                {
                    MessageBox.Show("اختر المريض");
                    return;
                }
                if (comboBookingType.Text == "")
                {
                    MessageBox.Show("اختر نوع الحجز");
                    return;
                }
                if (comboBookingStatus.Text == "")
                {
                    MessageBox.Show("اختر حالة الحجز");
                    return;
                }
                if (comboVisitingType.Text == "")
                {
                    MessageBox.Show("اختر نوع الزيارة");
                    return;
                }
                if (txtMoney.Text == "")
                {
                    MessageBox.Show("ادخل المبلغ");
                    return;
                }


                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT MAX (reservationNumber) +1 FROM Reservations where clinicId = '" + comboClinic.SelectedValue + "'and doctorId = '" + comboDoctor.SelectedValue + "' and date = '" + dateOfReservations.Value + "'", adoClass.sqlcn);

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }
                    int newID = 0;
                    if (cmd.ExecuteScalar().ToString() == "")
                    {
                        newID = 1;
                    }
                    else
                    {
                        newID = (int)cmd.ExecuteScalar();
                    }

                    cmd = new SqlCommand("Update Reservations set clinicId=@clinicId,doctorId=@doctorId,patientId=@patientId,bookingTypeId=@bookingTypeId,bookingStatus=@bookingStatus,phoneReservationPerson=@phoneReservationPerson,reservationPerson=@reservationPerson,date=@date,visitTypeId=@visitTypeId,money=@money,discount=@discount,moneyAfterDiscount=@moneyAfterDiscount,notes=@notes,reservationNumber=@reservationNumber,userId=@userId Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@clinicId", comboClinic.SelectedValue);
                    cmd.Parameters.AddWithValue("@doctorId", comboDoctor.SelectedValue);
                    cmd.Parameters.AddWithValue("@patientId", comboPatient.SelectedValue);
                    cmd.Parameters.AddWithValue("@bookingTypeId", comboBookingType.SelectedValue);
                    cmd.Parameters.AddWithValue("@bookingStatus", comboBookingStatus.Text);
                    cmd.Parameters.AddWithValue("@phoneReservationPerson", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@reservationPerson", txtBookingPersonName.Text);
                    cmd.Parameters.AddWithValue("@date", dateOfReservations.Value);
                    cmd.Parameters.AddWithValue("@visitTypeId", comboVisitingType.SelectedValue);
                    cmd.Parameters.AddWithValue("@money", txtMoney.Text);
                    cmd.Parameters.AddWithValue("@discount", txtDiscount.Text);
                    cmd.Parameters.AddWithValue("@moneyAfterDiscount", txtMoneyAfterDiscount.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@reservationNumber", newID);
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
                refreshForm.loadTable("select " +
                "Reservations.id," +
                "Reservations.reservationNumber," +
                "Clinics.name as clinic," +
                "Doctors.name as doctor," +
                "patient.name as patient," +
                "BookingType.name as bookingType," +
                "Reservations.bookingStatus," +
                "Reservations.phoneReservationPerson," +
                "Reservations.reservationPerson," +
                "Reservations.date," +
                "VisitingType.name as visitingType," +
                "Reservations.money," +
                "Reservations.discount," +
                "Reservations.moneyAfterDiscount," +
                "Reservations.notes," +
                "Users.name as userName" +
                " from Reservations," +
                "Clinics," +
                "Doctors," +
                "patient," +
                "BookingType," +
                "VisitingType," +
                "Users " +
                "where " +
                "Reservations.clinicId = Clinics.id and " +
                "Reservations.doctorId = Doctors.id and " +
                "Reservations.patientId = patient.id and " +
                "Reservations.bookingTypeId = BookingType.id and " +
                "Reservations.visitTypeId = VisitingType.id and " +
                "Reservations.userId = Users.id");
            }

            comboClinic.Text = "";
            comboDoctor.Text = "";
            comboPatient.Text = "";
            comboBookingType.Text = "";
            comboBookingStatus.Text = "";
            txtPhone.Text = "";
            txtBookingPersonName.Text = "";
            comboVisitingType.Text = "";
            txtMoney.Text = "";
            txtDiscount.Text = "";
            txtMoneyAfterDiscount.Text = "";
            txtNotes.Text = "";
            checkBoxPatient.Checked = false;

            id = "";
        }



        private void dateOfReservations_ValueChanged(object sender, EventArgs e)
        {
            //dateOfReservations.CustomFormat = "dd/MM/yyyy";
            search(comboClinic.SelectedValue, comboDoctor.SelectedValue, dateOfReservations.Value);

            if (comboClinic.Text == "" || comboDoctor.Text == "")
            {
                txtReservationNumber.Text = "";
            }
        }
        void search(object clinic, object doctor, object date)
        {
            //if (comboClinic.SelectedItem == null)
            //{
            //    return;
            //}
            //if (comboDoctor.SelectedItem == null)
            //{
            //    return;
            //}
           
            if (clinic == null || doctor == null || date == null)
            {
                txtReservationNumber.Text = "";
            }

            else
            {
                SqlCommand cmd = new SqlCommand("SELECT MAX (reservationNumber) +1 FROM Reservations where clinicId = '" + clinic + "'and doctorId = '" + doctor + "' and date = '" + date + "'", adoClass.sqlcn);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                if (cmd.ExecuteScalar().ToString() == "")
                {
                    txtReservationNumber.Text = "1";
                }
                else
                {
                    txtReservationNumber.Text = ((int)cmd.ExecuteScalar()).ToString();
                }
            }
           
            

        }

        private void comboClinic_SelectedValueChanged(object sender, EventArgs e)
        {

            search(comboClinic.SelectedValue, comboDoctor.SelectedValue, dateOfReservations.Value);

            if(comboClinic.Text == "" || comboDoctor.Text == "")
            {
                txtReservationNumber.Text = "";
            }

        }

        private void comboDoctor_SelectedValueChanged(object sender, EventArgs e)
        {
            search(comboClinic.SelectedValue, comboDoctor.SelectedValue, dateOfReservations.Value);
            if (comboClinic.Text == "" || comboDoctor.Text == "")
            {
                txtReservationNumber.Text = "";
            }
        }

        private void checkBoxPatient_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBoxPatient.Checked)
            {
                DataTable dt = new DataTable();

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                cmd = new SqlCommand("select name,phone from patient where id = '" + comboPatient.SelectedValue + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                adoClass.sqlcn.Close();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtBookingPersonName.Text = row["name"].ToString();
                    txtPhone.Text = row["phone"].ToString();
                }
            }
            else
            {
                txtBookingPersonName.Text = "";
                txtPhone.Text = "";
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtMoneyAfterDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtMoney_TextChanged(object sender, EventArgs e)
        {
            //$total = $total - ($total * ($discount_amount / 100));

            double money = 0;
            double discount = 0;
            double moneyAfterDiscount = 0;


            if (txtMoney.Text == "")
            {
                txtMoney.Text = "0";
            }
            
            money = double.Parse(txtMoney.Text);

            moneyAfterDiscount = money - (money * (discount / 100));
            txtMoneyAfterDiscount.Text = moneyAfterDiscount.ToString();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscount.Text == "")
            {
                txtDiscount.Text = "0";
            }

            double discount = 0;
            discount = double.Parse(txtDiscount.Text);
            if (discount > 100)
            {
                MessageBox.Show("ادخل نسبة اقل من او يساوي %100");
                txtDiscount.Text = "0";
                return;
            }

            double money = 0;
            double moneyAfterDiscount = 0;


            if (txtMoney.Text == "")
            {
                txtMoney.Text = "0";
            }

            money = double.Parse(txtMoney.Text);

            moneyAfterDiscount = money - (money * (discount / 100));
            txtMoneyAfterDiscount.Text = moneyAfterDiscount.ToString();

        }
    }
}
