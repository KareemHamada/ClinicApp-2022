using ClinicApp.Classes;
using ClinicApp.Forms.Settings.Analysis;
using ClinicApp.Forms.Settings.Diseases;
using ClinicApp.Forms.Settings.Foods;
using ClinicApp.Forms.Settings.Pharmacy;
using ClinicApp.Forms.Settings.Rays;
using ClinicApp.Forms.Settings.Symptoms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicApp.Forms.Examination
{
    public partial class FormAddExamination : Form
    {
        public FormAddExamination()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private string patId; // patient id
        private string newExaminationID; // reservation Id
        //public byte[] pdfAnalysisContent;

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
                                row["date"],
                                row["reservationNumber"],
                                row["patient"],
                                row["id"],
                                row["patientId"],
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

        private void FormAddExamination_Load(object sender, EventArgs e)
        {
            // hide btn save and show after open new examination
            btnSavePatientVisiting.Visible = false;

            // remove tabs
            tabControl.TabPages.Remove(patientInfoPage);
            tabControl.TabPages.Remove(fastExaminationPage);
            tabControl.TabPages.Remove(DiseasesPage);
            tabControl.TabPages.Remove(SymptomsPage);
            tabControl.TabPages.Remove(ExaminationPage);
            tabControl.TabPages.Remove(PrescriptionPage);
            tabControl.TabPages.Remove(AnalysisPage);
            tabControl.TabPages.Remove(raysPage);
            tabControl.TabPages.Remove(foodPage);
            tabControl.TabPages.Remove(contrastingMedicinePage);
            //tabControl.TabPages.Remove(smartAssistantPage);
            tabControl.TabPages.Remove(printingPage);


            dateOfReservations.Value = DateTime.Now;

            // all combo boxies

            
            // for combo clinics
            Helper.fillComboBox(comboClinic, "Select id,name from Clinics", "name", "id");
            // for combo doctor
            Helper.fillComboBox(comboDoctor, "Select id,name from Doctors", "name", "id");
            // for combo Diseases
            Helper.fillComboBox(comboDiseases, "Select id,name from Diseases", "name", "id");
            Helper.fillComboBox(ComboSymptoms, "Select id,name from Symptoms", "name", "id");

            Helper.fillComboBox(comboMedicine, "Select id,name from Drugs", "name", "id");
            Helper.fillComboBox(comboTimeTakeMedicine, "Select id,name from timesTakeMedication", "name", "id");
            Helper.fillComboBox(comboMedicineUnit, "Select id,name from medicineUnit", "name", "id");
            Helper.fillComboBox(comboDosages, "Select id,name from Dosages", "name", "id");
            Helper.fillComboBox(comboAnalysis, "Select id,name from Analysis", "name", "id");
            // for combo rays
            Helper.fillComboBox(comboRays, "Select id,name from Rays", "name", "id");
            // for combo food
            Helper.fillComboBox(comboRays, "Select id,name from Rays", "name", "id");
            Helper.fillComboBox(comboFood, "Select id,name from Food", "name", "id");
            Helper.fillComboBox(comboContrastingMedicines, "Select id,name from Drugs", "name", "id");
            
            loadTable("select " +
                "Reservations.id," +
                "Reservations.reservationNumber," +
                "Clinics.name as clinic," +
                "Doctors.name as doctor," +
                "patient.name as patient," +
                "Reservations.patientId," +
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

        private void btnOpenExamination_Click(object sender, EventArgs e)
        {
            // show btn save 
            btnSavePatientVisiting.Visible = true;


            if (dgvLoading.Rows.Count > 0)
            {
                tabControl.TabPages.Remove(reservationsPage);
                tabControl.TabPages.Add(patientInfoPage);
                tabControl.TabPages.Add(fastExaminationPage);
                tabControl.TabPages.Add(DiseasesPage);
                tabControl.TabPages.Add(SymptomsPage);
                tabControl.TabPages.Add(ExaminationPage);
                tabControl.TabPages.Add(PrescriptionPage);
                tabControl.TabPages.Add(AnalysisPage);
                tabControl.TabPages.Add(raysPage);
                tabControl.TabPages.Add(foodPage);
                tabControl.TabPages.Add(contrastingMedicinePage);
                //tabControl.TabPages.Add(smartAssistantPage);
                tabControl.TabPages.Add(printingPage);

                btnOpenExamination.Visible = false;


                patId = dgvLoading.CurrentRow.Cells[16].Value.ToString();
                newExaminationID = dgvLoading.CurrentRow.Cells[15].Value.ToString();
                loadPatientData(patId);

                try
                {
                    cmd = new SqlCommand("Update Reservations set bookingStatus=@bookingStatus Where id = '" + newExaminationID + "'", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@bookingStatus", "بالداخل عند الدكتور");


                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("تم فتح الكشف");
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

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


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



        public void loadPatientData(string ppId)
        {
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select Patient.id,Patient.name,Patient.dateOfBirth,Patient.type,Patient.dateTimeReg,Patient.phone,Patient.address,Patient.weight,Patient.height,Patient.lastOperations,Patient.notes,Patient.image,Company.name as company from Patient LEFT JOIN Company on Patient.companyId = Company.id where Patient.id = '" + ppId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtName.Text = row["name"].ToString();
                txtGender.Text = row["type"].ToString();
                dateOfBirth.Text = row["dateOfBirth"].ToString();
                txtAddress.Text = row["address"].ToString();
                txtPhone.Text = row["phone"].ToString();
                txtCompany.Text = row["company"].ToString();
                txtHeight.Text = row["height"].ToString();
                txtWeight.Text = row["weight"].ToString();
                txtPreviousOperations.Text = row["lastOperations"].ToString();
                txtNotes.Text = row["notes"].ToString();
                picBox.BackgroundImage = Helper.ByteToImage(row["image"]);

            }
        }

        private void btnAddDiseases_Click(object sender, EventArgs e)
        {

            //for (int i = 0; i < comboDiseases.Items.Count; i++)
            //{
            //    string value = comboDiseases.GetItemText(comboDiseases.Items[i]);
            //    MessageBox.Show(value);
            //}

            if (comboDiseases.Text != "")
            {
                dgvDiseases.Rows.Add
                        (new object[]
                            {
                                txtNotesDiseases.Text,
                                comboDiseases.Text,
                                Properties.Resources.delete_24,
                                comboDiseases.SelectedValue
                            }
                        );

                txtNotesDiseases.Text = "";
                comboDiseases.Text = "";
            }
        }

        private void dgvDiseases_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDiseases.Rows.Count > 0)
            {
                if (dgvDiseases.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvDiseases.Rows.Remove(dgvDiseases.CurrentRow);
                    }
                }
            }
        }

        private void btnAddSymptoms_Click(object sender, EventArgs e)
        {
            if (ComboSymptoms.Text != "")
            {
                dgvSymptoms.Rows.Add
                        (new object[]
                            {
                                txtSymptomsNotes.Text,
                                ComboSymptoms.Text,
                                Properties.Resources.delete_24,
                                ComboSymptoms.SelectedValue
                            }
                        );

                txtSymptomsNotes.Text = "";
                ComboSymptoms.Text = "";
            }
        }

        private void dgvSymptoms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSymptoms.Rows.Count > 0)
            {
                if (dgvSymptoms.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvSymptoms.Rows.Remove(dgvSymptoms.CurrentRow);
                    }
                }
            }
        }

        private void dgvPrescription_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPrescription.Rows.Count > 0)
            {
                if (dgvPrescription.CurrentCell.ColumnIndex.Equals(5) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvPrescription.Rows.Remove(dgvPrescription.CurrentRow);
                    }
                }
            }
        }

        private void btnAddPrescription_Click(object sender, EventArgs e)
        {
            if (comboMedicine.Text != "")
            {
                dgvPrescription.Rows.Add
                        (new object[]
                            {
                                txtPrescriptionNotes.Text,
                                comboDosages.Text,
                                comboMedicineUnit.Text,
                                comboTimeTakeMedicine.Text,
                                comboMedicine.Text,
                                Properties.Resources.delete_24,
                                comboMedicine.SelectedValue
                            }
                        );

                txtPrescriptionNotes.Text = "";
                comboDosages.Text = "";
                comboMedicineUnit.Text = "";
                comboTimeTakeMedicine.Text = "";
                comboMedicine.Text = "";

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSavePatientVisiting_Click(object sender, EventArgs e)
        {
            btnSavePatientVisiting.Visible = false;

            // get patient id and last examination id
            //DataTable dt = new DataTable();

            //cmd = new SqlCommand("SELECT MAX (id) + 1 FROM ExaminingPatient", adoClass.sqlcn);
            //int newExaminationID = 0;
            //if (cmd.ExecuteScalar().ToString() == "")
            //{
            //    newExaminationID = 1;
            //}
            //else
            //{
            //    newExaminationID = (int)cmd.ExecuteScalar();
            //}


            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }

            // insert into ExaminingPatient
            cmd = new SqlCommand("Insert into ExaminingPatient (bloodPressure,breathing,heartBeats,bodyTemperature,sugar,weight,previousOperations,patientId,reservationId) values (@bloodPressure,@breathing,@heartBeats,@bodyTemperature,@sugar,@weight,@previousOperations,@patientId,@reservationId)", adoClass.sqlcn);

            cmd.Parameters.AddWithValue("@bloodPressure", txtBloodPressure.Text);
            cmd.Parameters.AddWithValue("@breathing", txtBreathing.Text);
            cmd.Parameters.AddWithValue("@heartBeats", txtHeartBeats.Text);
            cmd.Parameters.AddWithValue("@bodyTemperature", txtBodyTemperature.Text);
            cmd.Parameters.AddWithValue("@sugar", txtSugar.Text);
            cmd.Parameters.AddWithValue("@weight", txtExaminationWeight.Text);
            cmd.Parameters.AddWithValue("@previousOperations", txtExaminationPreviousOperations.Text);
            cmd.Parameters.AddWithValue("@patientId", patId);
            cmd.Parameters.AddWithValue("@reservationId", newExaminationID);
            
            cmd.ExecuteNonQuery();


            // insert into DiseasesPage
            if (dgvDiseases.Rows.Count > 0)
            {
                for (int i = 0; i < dgvDiseases.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into DiseasesPatient (diseasesId,notes,patientId,examinationId) values (@diseasesId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@diseasesId", dgvDiseases[3, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvDiseases[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", newExaminationID);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // insert into Symptoms
            if (dgvSymptoms.Rows.Count > 0)
            {
                for (int i = 0; i < dgvSymptoms.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into SymptomsPatient (symptomsId,notes,patientId,examinationId) values (@symptomsId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@symptomsId", dgvSymptoms[3, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvSymptoms[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", newExaminationID);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }


            // insert into Doctor Examination
            cmd = new SqlCommand("Insert into DoctorExaminationPatient (dateTime,doctorExamination,advices,notes,patientId,examinationId) values (@dateTime,@doctorExamination,@advices,@notes,@patientId,@examinationId)", adoClass.sqlcn);

            cmd.Parameters.AddWithValue("@dateTime", dateExamination.Value);
            cmd.Parameters.AddWithValue("@doctorExamination", txtDoctorExamination.Text);
            cmd.Parameters.AddWithValue("@advices", txtDoctorAdvice.Text);
            cmd.Parameters.AddWithValue("@notes", txtExaminationNotes.Text);
            cmd.Parameters.AddWithValue("@patientId", patId);
            cmd.Parameters.AddWithValue("@examinationId", newExaminationID);
      
            cmd.ExecuteNonQuery();


            // insert into Prescription
            if (dgvPrescription.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPrescription.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into PrescriptionPatient (medicineId,timeTakeMedicine,medicineUnit,dosages,notes,patientId,examinationId) values (@medicineId,@timeTakeMedicine,@medicineUnit,@dosages,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@medicineId", dgvPrescription[6, i].Value);
                    cmd.Parameters.AddWithValue("@timeTakeMedicine", dgvPrescription[3, i].Value);
                    cmd.Parameters.AddWithValue("@medicineUnit", dgvPrescription[2, i].Value);
                    cmd.Parameters.AddWithValue("@dosages", dgvPrescription[1, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvPrescription[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", newExaminationID);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // insert into AnalysisPatient
            if (dgvAnalysis.Rows.Count > 0)
            {
                for (int i = 0; i < dgvAnalysis.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into AnalysisPatient (analysisId,notes,patientId,examinationId) values (@analysisId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@analysisId", dgvAnalysis[3, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvAnalysis[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", newExaminationID);

                    //// kareem
                    //if (pdfAnalysisContent != null && pdfAnalysisContent.Length > 0)
                    //{
                    //    cmd.Parameters.AddWithValue("@pdf", Encoding.ASCII.GetBytes(dgvAnalysis[4, i].Value.ToString()));
                    //}
                    //else
                    //{
                    //    cmd.Parameters.Add("@pdf", SqlDbType.VarBinary).Value = DBNull.Value;
                    //}

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // insert into RaysPatient
            if (dgvRays.Rows.Count > 0)
            {
                for (int i = 0; i < dgvRays.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into RaysPatient (raysId,notes,patientId,examinationId) values (@raysId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@raysId", dgvRays[3, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvRays[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", newExaminationID);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }


            // insert into FoodPatient
            if (dgvFood.Rows.Count > 0)
            {
                for (int i = 0; i < dgvFood.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into FoodPatient (foodId,notes,patientId,examinationId) values (@foodId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@foodId", dgvFood[3, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvFood[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", newExaminationID);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }


            // insert into ContrastingMedicines
            if (dgvContrastingMedicines.Rows.Count > 0)
            {
                for (int i = 0; i < dgvContrastingMedicines.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into ContrastingMedicines (medicineId,notes,patientId,examinationId) values (@medicineId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@medicineId", dgvContrastingMedicines[3, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvContrastingMedicines[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", newExaminationID);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }


            try
            {
                cmd = new SqlCommand("Update Reservations set bookingStatus=@bookingStatus Where id = '" + newExaminationID + "'", adoClass.sqlcn);
                cmd.Parameters.AddWithValue("@bookingStatus", "تم الدخول و الخروج للدكتور");


                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            MessageBox.Show("تم الحفظ بنجاح");
        }



        private void dgvAnalysis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAnalysis.Rows.Count > 0)
            {
                if (dgvAnalysis.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvAnalysis.Rows.Remove(dgvAnalysis.CurrentRow);
                    }
                }
            }
        }

        private void btnAddAnalysis_Click(object sender, EventArgs e)
        {
            if (comboAnalysis.Text != "")
            {
                dgvAnalysis.Rows.Add
                        (new object[]
                            {
                                txtAnalysisNotes.Text,
                                comboAnalysis.Text,
                                Properties.Resources.delete_24,
                                comboAnalysis.SelectedValue
                                //pdfAnalysisContent
                            }
                        );

                txtAnalysisNotes.Text = "";
                comboAnalysis.Text = "";


            }
        }

        private void btnAddRays_Click(object sender, EventArgs e)
        {
            if (comboRays.Text != "")
            {
                dgvRays.Rows.Add
                        (new object[]
                            {
                                txtRaysNotes.Text,
                                comboRays.Text,
                                Properties.Resources.delete_24,
                                comboRays.SelectedValue
                            }
                        );

                txtRaysNotes.Text = "";
                comboRays.Text = "";


            }
        }

        private void dgvRays_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRays.Rows.Count > 0)
            {
                if (dgvRays.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvRays.Rows.Remove(dgvRays.CurrentRow);
                    }
                }
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (comboFood.Text != "")
            {
                dgvFood.Rows.Add
                        (new object[]
                            {
                                txtFoodNotes.Text,
                                comboFood.Text,
                                Properties.Resources.delete_24,
                                comboFood.SelectedValue
                            }
                        );

                txtFoodNotes.Text = "";
                comboFood.Text = "";


            }
        }

        private void dgvFood_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFood.Rows.Count > 0)
            {
                if (dgvFood.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvFood.Rows.Remove(dgvFood.CurrentRow);
                    }
                }
            }
        }

        private void btnAddContrastingMedicines_Click(object sender, EventArgs e)
        {
            if (comboContrastingMedicines.Text != "")
            {
                dgvContrastingMedicines.Rows.Add
                        (new object[]
                            {
                                txtContrastingMedicinesNotes.Text,
                                comboContrastingMedicines.Text,
                                Properties.Resources.delete_24,
                                comboContrastingMedicines.SelectedValue
                            }
                        );

                txtContrastingMedicinesNotes.Text = "";
                comboContrastingMedicines.Text = "";


            }
        }

        private void dgvContrastingMedicines_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvContrastingMedicines.Rows.Count > 0)
            {
                if (dgvContrastingMedicines.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvContrastingMedicines.Rows.Remove(dgvContrastingMedicines.CurrentRow);
                    }
                }
            }
        }

        private void btnUploadPdf_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog fileDialog = new OpenFileDialog() { Filter = "PDF Documents(*.pdf)|*.pdf", ValidateNames = true })
            //{
            //    if (fileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        DialogResult dialog = MessageBox.Show("هل متاكد من حفظ ذلك الملف ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //        if (dialog == DialogResult.Yes)
            //        {
            //            string filename = fileDialog.FileName;
            //            pdfAnalysisContent = uploadFile(filename);
            //        }
            //    }
            //}
        }

        private void btnAddToCombo_Click(object sender, EventArgs e)
        {
            FormAddDisease frm = new FormAddDisease();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // for combo Diseases
                Helper.fillComboBox(comboDiseases, "Select id,name from Diseases", "name", "id");
            }    
        }

        private void btnAddToComboSymptoms_Click(object sender, EventArgs e)
        {
            FormAddSymptom frm = new FormAddSymptom();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // for combo Symptoms
                Helper.fillComboBox(ComboSymptoms, "Select id,name from Symptoms", "name", "id");
            }
        }

        private void btnAddTimeTakeMedicine_Click(object sender, EventArgs e)
        {

        }

        private void btnAddMedicineUnit_Click(object sender, EventArgs e)
        {
            // for combo medicineUnit
            FormAddMedicineUnit frm = new FormAddMedicineUnit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Helper.fillComboBox(comboMedicineUnit, "Select id,name from medicineUnit", "name", "id");
            }
        }

        private void btnAddDosages_Click(object sender, EventArgs e)
        {
            // for combo Dosages
            FormAddDosage frm = new FormAddDosage();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Helper.fillComboBox(comboDosages, "Select id,name from Dosages", "name", "id");
            }
        }

        private void btnNewAddAnalysis_Click(object sender, EventArgs e)
        {
            // for combo Analysis
            FormAddAnalysis frm = new FormAddAnalysis();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Helper.fillComboBox(comboAnalysis, "Select id,name from Analysis", "name", "id");
            }
        }

        private void btnAddNewRays_Click(object sender, EventArgs e)
        {
            // for combo rays
            FormAddRay frm = new FormAddRay();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Helper.fillComboBox(comboRays, "Select id,name from Rays", "name", "id");
            }
        }

        private void btnAddNewFood_Click(object sender, EventArgs e)
        {
            // for combo Food
            FormAddFood frm = new FormAddFood();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Helper.fillComboBox(comboFood, "Select id,name from Food", "name", "id");
            }
        }

        private void btnAddNewMedicicne_Click(object sender, EventArgs e)
        {
            // for combo Contrasting Medicines
            FormAddDrug frm = new FormAddDrug();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Helper.fillComboBox(comboContrastingMedicines, "Select id,name from Drugs", "name", "id");
            }
        }

        private void btnAddNewPerceptionMedicine_Click(object sender, EventArgs e)
        {
            // for combo Drug
            FormAddDrug frm = new FormAddDrug();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Helper.fillComboBox(comboMedicine, "Select id,name from Drugs", "name", "id");
            }
        }

        //private byte[] uploadFile(string file)
        //{
        //    FileStream fstream = File.OpenRead(file);
        //    byte[] content = new byte[fstream.Length];
        //    fstream.Read(content, 0, (int)fstream.Length);
        //    fstream.Close();

        //    return content;
        //}
    }
}
