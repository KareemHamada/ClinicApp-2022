using ClinicApp.Classes;
using ClinicApp.Tools;
using Microsoft.Reporting.WinForms;
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
    public partial class FormShowReservations : Form
    {
        public FormShowReservations()
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
                    //newItem.DateSaved.ToString("yyyy-MM-dd") //

                    
                    //Console.WriteLine(row["dateTime"]);
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                                row["userName"],
                                row["notes"],
                                row["moneyAfterDiscount"],
                                row["discount"],
                                row["money"],
                                row["bookingStatus"],
                                row["bookingType"],
                                row["visitingType"],
                                row["phoneReservationPerson"],
                                row["reservationPerson"],
                                row["doctor"],
                                row["clinic"],
                                Convert.ToDateTime(row["date"]).ToString("dd/MM/yyyy"),
                                row["reservationNumber"],
                                row["patient"],
                                row["id"],
                            }
                        );
                        
                }
                foreach (DataGridViewRow s in dgvLoading.Rows)
                {
                    if (s.Cells[5].Value.ToString() == "تم الدخول و الخروج للدكتور")
                    {
                        s.DefaultCellStyle.BackColor = Color.LawnGreen;
                    }
                    else if (s.Cells[5].Value.ToString() == "بالداخل عند الدكتور")
                    {
                        s.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (s.Cells[5].Value.ToString() == "تم الغاء الحجز")
                    {
                        s.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
        private void FormShowReservations_Load(object sender, EventArgs e)
        {
            dateOfReservations.Value = DateTime.Now;
            // for combo clinics
            Helper.fillComboBox(comboClinic, "Select id,name from Clinics", "name", "id");
            // for combo doctor
            Helper.fillComboBox(comboDoctor, "Select id,name from Doctors", "name", "id");

            loadTable("select " +
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

            txtHidden = new TextBox();
            txtHidden.Visible = false;


            // hide and show buttons
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select resDelete,resUpdate from Users where id = '" + declarations.userId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                // for doctor
                if (row["resDelete"].ToString() == "False")
                {
                    btnDelete.Visible = false;
                }
                if (row["resUpdate"].ToString() == "False")
                {
                    btnUpdate.Visible = false;
                }
            }
        }

        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    search(txtSearch.Text);
        //}

        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select " +
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
            else
            {
                loadTable("select " +
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
                "Reservations.userId = Users.id " +
                "and (Clinics.name like '%" + text + "%' " +
                "or Doctors.name like '%" + text + "%' " +
                "or patient.name like '%" + text + "%' " +
                "or BookingType.name like '%" + text + "%' " +
                "or bookingStatus like '%" + text + "%' " +
                "or phoneReservationPerson like '%" + text + "%' " +
                "or date like '%" + text + "%' " +
                "or VisitingType.name like '%" + text + "%' " +
                "or Users.name like '%" + text + "%')");
            }
        }

    

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select " +
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            loadTable("select " +
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
                "Reservations.userId = Users.id " +
                "and Clinics.name ='" + comboClinic.Text + "' " +
                "and Doctors.name  ='" + comboDoctor.Text + "' " +
                "and Reservations.date = '" + dateOfReservations.Value + "' ");


        }

        private void dateOfReservations_ValueChanged(object sender, EventArgs e)
        {
            loadTable("select " +
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
                "Reservations.userId = Users.id " +
                "and Reservations.date = '" + dateOfReservations.Value + "' ");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string id = dgvLoading.CurrentRow.Cells[15].Value.ToString();
                    if (id == "")
                    {
                        MessageBox.Show("حدد الشركة المراد حذفها");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Reservations Where id = '" + id + "'", adoClass.sqlcn);

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

                    loadTable("select " +
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

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {

                FormAddReservations frm = new FormAddReservations();
                txtHidden.Text = dgvLoading.CurrentRow.Cells[15].Value.ToString();
                frm.patientText = dgvLoading.CurrentRow.Cells[14].Value.ToString();
                frm.txtReservationNumber.Text = dgvLoading.CurrentRow.Cells[13].Value.ToString();
                frm.dateOfReservations.Text = dgvLoading.CurrentRow.Cells[12].Value.ToString();
                frm.clinicText = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                frm.doctorText = dgvLoading.CurrentRow.Cells[10].Value.ToString();
                frm.txtBookingPersonName.Text = dgvLoading.CurrentRow.Cells[9].Value.ToString();
                frm.txtPhone.Text = dgvLoading.CurrentRow.Cells[8].Value.ToString();
                frm.visitingTypeText = dgvLoading.CurrentRow.Cells[7].Value.ToString();
                frm.bookingTypeText = dgvLoading.CurrentRow.Cells[6].Value.ToString();
                frm.bookingStatusText = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                frm.txtMoney.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                frm.txtDiscount.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();
                frm.txtMoneyAfterDiscount.Text = dgvLoading.CurrentRow.Cells[2].Value.ToString();
                frm.txtNotes.Text = dgvLoading.CurrentRow.Cells[1].Value.ToString();
                frm.Text = dgvLoading.CurrentRow.Cells[0].Value.ToString();
                frm.id = txtHidden.Text;
                frm.refreshForm = this;
                frm.Show();

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsTools tbl = new dsTools();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowReservations"].NewRow();

                    dro["userName"] = dgvLoading[0, i].Value;
                    dro["notes"] = dgvLoading[1, i].Value;
                    dro["moneyAfterDiscount"] = dgvLoading[2, i].Value;
                    dro["discount"] = dgvLoading[3, i].Value;
                    dro["money"] = dgvLoading[4, i].Value;
                    dro["bookingStatus"] = dgvLoading[5, i].Value;
                    dro["bookingType"] = dgvLoading[6, i].Value;
                    dro["visitingType"] = dgvLoading[7, i].Value;
                    dro["phoneReservationPerson"] = dgvLoading[8, i].Value;
                    dro["reservationPerson"] = dgvLoading[9, i].Value;
                    dro["doctor"] = dgvLoading[10, i].Value;
                    dro["clinic"] = dgvLoading[11, i].Value;
                    dro["date"] = dgvLoading[12, i].Value;
                    dro["reservationNumber"] = dgvLoading[13, i].Value;
                    dro["patient"] = dgvLoading[14, i].Value;

                    tbl.Tables["dtShowReservations"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportFormShowReservations.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowReservations"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportFormShowReservations.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowReservations"]));

                    PrintersClass.PrintToPrinter(report);
                }
                else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
                {
                    rptForm.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("لا يوجد عناصر لعرضها");
            }
        }
    }
}
