using ClinicApp.Classes;
using ClinicApp.Forms.Settings.Analysis;
using ClinicApp.Forms.Settings.Diseases;
using ClinicApp.Forms.Settings.Foods;
using ClinicApp.Forms.Settings.Pharmacy;
using ClinicApp.Forms.Settings.Rays;
using ClinicApp.Forms.Settings.Symptoms;
using ClinicApp.Tools;
using Microsoft.Reporting.WinForms;
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
    public partial class FormUpdateExamination : Form
    {
        public FormUpdateExamination()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        public string patId; // patient id
        public string reservationId; // reservation Id
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
            }
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

        public void ExamingPatientFun()
        {
            DataTable dt = new DataTable();
            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select * from ExaminingPatient where reservationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtBloodPressure.Text = row["bloodPressure"].ToString();
                txtBreathing.Text = row["breathing"].ToString();
                txtHeartBeats.Text = row["heartBeats"].ToString();
                txtBodyTemperature.Text = row["bodyTemperature"].ToString();
                txtSugar.Text = row["sugar"].ToString();
                txtExaminationWeight.Text = row["weight"].ToString();
                txtExaminationPreviousOperations.Text = row["previousOperations"].ToString();
            }
        }

        public void loadTablePatientDiseases()
        {
            dgvDiseases.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select Diseases.name as disease,DiseasesPatient.notes as notes,DiseasesPatient.diseasesId from DiseasesPatient,Diseases where DiseasesPatient.diseasesId =  Diseases.id and DiseasesPatient.examinationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    //newItem.DateSaved.ToString("yyyy-MM-dd") //

                    //Console.WriteLine(row["dateTime"]);
                    dgvDiseases.Rows.Add
                        (new object[]
                            {
                                row["notes"],
                                row["disease"],
                                row["diseasesId"],
                            }
                        );
                }
            }
        }

        public void loadTablePatientSymptoms()
        {
            dgvSymptoms.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select Symptoms.name as symptom,SymptomsPatient.notes as notes,SymptomsPatient.symptomsId from SymptomsPatient,Symptoms where SymptomsPatient.symptomsId = Symptoms.id and SymptomsPatient.examinationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dgvSymptoms.Rows.Add
                        (new object[]
                            {
                                row["notes"],
                                row["symptom"],
                                row["symptomsId"],
                            }
                        );
                }
            }
        }

        public void doctorExamingPatientFun()
        {
            DataTable dt = new DataTable();
            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select * from DoctorExaminationPatient where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                dateExamination.Text = row["dateTime"].ToString();
                txtDoctorExamination.Text = row["doctorExamination"].ToString();
                txtDoctorAdvice.Text = row["advices"].ToString();
                txtExaminationNotes.Text = row["notes"].ToString();

            }
        }

        public void loadTablePrescriptionPatient()
        {
            dgvPrescription.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select PrescriptionPatient.medicineId,PrescriptionPatient.timeTakeMedicine,PrescriptionPatient.medicineUnit,PrescriptionPatient.dosages,PrescriptionPatient.notes,Drugs.name as medicine from PrescriptionPatient,Drugs where PrescriptionPatient.medicineId =  Drugs.id and PrescriptionPatient.examinationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    //newItem.DateSaved.ToString("yyyy-MM-dd") //

                    //Console.WriteLine(row["dateTime"]);
                    dgvPrescription.Rows.Add
                        (new object[]
                            {
                                row["notes"],
                                row["dosages"],
                                row["medicineUnit"],
                                row["timeTakeMedicine"],
                                row["medicine"],
                                row["medicineId"],
                            }
                        );
                }
            }
        }

        public void loadTableAnalysisPatient()
        {
            dgvAnalysis.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select Analysis.name as analysis,AnalysisPatient.analysisId,AnalysisPatient.notes as notes from AnalysisPatient,Analysis where AnalysisPatient.analysisId =  Analysis.id and AnalysisPatient.examinationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dgvAnalysis.Rows.Add
                        (new object[]
                            {
                                row["notes"],
                                row["analysis"],
                                row["analysisId"],
                            }
                        );
                }
            }
        }


        public void loadTableRaysPatient()
        {
            dgvRays.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select Rays.name as ray,RaysPatient.raysId,RaysPatient.notes as notes from RaysPatient,Rays where RaysPatient.raysId = Rays.id and  RaysPatient.examinationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dgvRays.Rows.Add
                        (new object[]
                            {
                                row["notes"],
                                row["ray"],
                                row["raysId"],
                            }
                        );
                }
            }
        }


        public void loadTableFoodsPatient()
        {
            dgvFood.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select Food.name as food,FoodPatient.foodId,FoodPatient.notes as notes from FoodPatient,Food where FoodPatient.foodId = Food.id and  FoodPatient.examinationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dgvFood.Rows.Add
                        (new object[]
                            {
                                row["notes"],
                                row["food"],
                                row["foodId"],
                            }
                        );
                }
            }
        }

        public void loadTableContrastingMedicinesPatient()
        {
            dgvContrastingMedicines.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select Drugs.name as medicine,ContrastingMedicines.medicineId,ContrastingMedicines.notes as notes from ContrastingMedicines,Drugs where ContrastingMedicines.medicineId =  Drugs.id and ContrastingMedicines.examinationId = '" + reservationId + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dgvContrastingMedicines.Rows.Add
                        (new object[]
                            {
                                row["notes"],
                                row["medicine"],
                                row["medicineId"],
                            }
                        );
                }
            }
        }

        private void FormUpdateExamination_Load(object sender, EventArgs e)
        {
            // combo boxies
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

            // end of combo boxies



            // reservation
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
                "Reservations.userId = Users.id and Reservations.id = '" + reservationId + "'");

            // patient data
            loadPatientData(patId);

            // examinng patient from bloodPressure, breathing and others.
            ExamingPatientFun();

            // patient Diseases
            loadTablePatientDiseases();


            // patient Symptoms
            loadTablePatientSymptoms();

            // doctorExamingPatient
            doctorExamingPatientFun();


            // Prescription Patient
            loadTablePrescriptionPatient();


            // Analysis Patient
            loadTableAnalysisPatient();

            // Rays Patient
            loadTableRaysPatient();

            // Food Patient
            loadTableFoodsPatient();

            // Contrasting Medicines Patient
            loadTableContrastingMedicinesPatient();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // kareem
        private void btnAddDiseases_Click(object sender, EventArgs e)
        {
            if (comboDiseases.Text != "")
            {
                dgvDiseases.Rows.Add
                        (new object[]
                            {
                                txtNotesDiseases.Text,
                                comboDiseases.Text,
                                comboDiseases.SelectedValue,
                                Properties.Resources.delete_24,
                            }
                        );

                txtNotesDiseases.Text = "";
                comboDiseases.Text = "";
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
                                ComboSymptoms.SelectedValue,
                                Properties.Resources.delete_24
                                 
                            }
                        );

                txtSymptomsNotes.Text = "";
                ComboSymptoms.Text = "";
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
                                comboMedicine.SelectedValue,
                                Properties.Resources.delete_24,
                                
                            }
                        );

                txtPrescriptionNotes.Text = "";
                comboDosages.Text = "";
                comboMedicineUnit.Text = "";
                comboTimeTakeMedicine.Text = "";
                comboMedicine.Text = "";

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
                                comboAnalysis.SelectedValue,
                                Properties.Resources.delete_24,
                                
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
                                comboRays.SelectedValue,
                                Properties.Resources.delete_24,
                            }
                        );

                txtRaysNotes.Text = "";
                comboRays.Text = "";


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
                                comboFood.SelectedValue,
                                Properties.Resources.delete_24,
                                
                            }
                        );

                txtFoodNotes.Text = "";
                comboFood.Text = "";


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
                                comboContrastingMedicines.SelectedValue,
                                Properties.Resources.delete_24,
                            }
                        );

                txtContrastingMedicinesNotes.Text = "";
                comboContrastingMedicines.Text = "";
            }
        }

        private void dgvDiseases_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDiseases.Rows.Count > 0)
            {
                if (dgvDiseases.CurrentCell.ColumnIndex.Equals(3) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvDiseases.Rows.Remove(dgvDiseases.CurrentRow);
                    }
                }
            }
        }

        private void dgvSymptoms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSymptoms.Rows.Count > 0)
            {
                if (dgvSymptoms.CurrentCell.ColumnIndex.Equals(3) && e.RowIndex != -1)
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
                if (dgvPrescription.CurrentCell.ColumnIndex.Equals(6) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvPrescription.Rows.Remove(dgvPrescription.CurrentRow);
                    }
                }
            }
        }

        private void dgvAnalysis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAnalysis.Rows.Count > 0)
            {
                if (dgvAnalysis.CurrentCell.ColumnIndex.Equals(3) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvAnalysis.Rows.Remove(dgvAnalysis.CurrentRow);
                    }
                }
            }
        }

        private void dgvRays_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRays.Rows.Count > 0)
            {
                if (dgvRays.CurrentCell.ColumnIndex.Equals(3) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvRays.Rows.Remove(dgvRays.CurrentRow);
                    }
                }
            }
        }

        private void dgvFood_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFood.Rows.Count > 0)
            {
                if (dgvFood.CurrentCell.ColumnIndex.Equals(3) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvFood.Rows.Remove(dgvFood.CurrentRow);
                    }
                }
            }
        }

        private void dgvContrastingMedicines_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvContrastingMedicines.Rows.Count > 0)
            {
                if (dgvContrastingMedicines.CurrentCell.ColumnIndex.Equals(3) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvContrastingMedicines.Rows.Remove(dgvContrastingMedicines.CurrentRow);
                    }
                }
            }
        }
 
        private void btnSavePatientVisiting_Click(object sender, EventArgs e)
        {
            MessageBox.Show(patId);
            MessageBox.Show(reservationId);
            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }

            // delete ExaminingPatient
            cmd = new SqlCommand("delete from ExaminingPatient Where reservationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();

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
            cmd.Parameters.AddWithValue("@reservationId", reservationId);

            cmd.ExecuteNonQuery();


            // delete DiseasesPage
            cmd = new SqlCommand("delete from DiseasesPatient Where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();

            // insert into DiseasesPage
            if (dgvDiseases.Rows.Count > 0)
            {
                for (int i = 0; i < dgvDiseases.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into DiseasesPatient (diseasesId,notes,patientId,examinationId) values (@diseasesId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@diseasesId", dgvDiseases[2, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvDiseases[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", reservationId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // delete Symptoms
            cmd = new SqlCommand("delete from SymptomsPatient Where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();


            // insert into Symptoms
            if (dgvSymptoms.Rows.Count > 0)
            {
                for (int i = 0; i < dgvSymptoms.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into SymptomsPatient (symptomsId,notes,patientId,examinationId) values (@symptomsId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@symptomsId", dgvSymptoms[2, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvSymptoms[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", reservationId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // delete Doctor Examination
            cmd = new SqlCommand("delete from DoctorExaminationPatient Where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();

            // insert into Doctor Examination
            cmd = new SqlCommand("Insert into DoctorExaminationPatient (dateTime,doctorExamination,advices,notes,patientId,examinationId) values (@dateTime,@doctorExamination,@advices,@notes,@patientId,@examinationId)", adoClass.sqlcn);

            cmd.Parameters.AddWithValue("@dateTime", dateExamination.Value);
            cmd.Parameters.AddWithValue("@doctorExamination", txtDoctorExamination.Text);
            cmd.Parameters.AddWithValue("@advices", txtDoctorAdvice.Text);
            cmd.Parameters.AddWithValue("@notes", txtExaminationNotes.Text);
            cmd.Parameters.AddWithValue("@patientId", patId);
            cmd.Parameters.AddWithValue("@examinationId", reservationId);

            cmd.ExecuteNonQuery();


            // delete Doctor Prescription Patient
            cmd = new SqlCommand("delete from PrescriptionPatient Where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();

            // insert into Prescription
            if (dgvPrescription.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPrescription.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into PrescriptionPatient (medicineId,timeTakeMedicine,medicineUnit,dosages,notes,patientId,examinationId) values (@medicineId,@timeTakeMedicine,@medicineUnit,@dosages,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@medicineId", dgvPrescription[5, i].Value);
                    cmd.Parameters.AddWithValue("@timeTakeMedicine", dgvPrescription[3, i].Value);
                    cmd.Parameters.AddWithValue("@medicineUnit", dgvPrescription[2, i].Value);
                    cmd.Parameters.AddWithValue("@dosages", dgvPrescription[1, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvPrescription[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", reservationId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // delete Analysis Patient
            cmd = new SqlCommand("delete from AnalysisPatient Where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();

            // insert into AnalysisPatient
            if (dgvAnalysis.Rows.Count > 0)
            {
                for (int i = 0; i < dgvAnalysis.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into AnalysisPatient (analysisId,notes,patientId,examinationId) values (@analysisId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@analysisId", dgvAnalysis[2, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvAnalysis[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", reservationId);

                    //if (pdfAnalysisContent != null && pdfAnalysisContent.Length > 0)
                    //{
                    //    cmd.Parameters.AddWithValue("@pdf", pdfAnalysisContent);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.Add("@pdf", SqlDbType.VarBinary).Value = DBNull.Value;
                    //}

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // delete RaysPatient
            cmd = new SqlCommand("delete from RaysPatient Where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();

            // insert into RaysPatient
            if (dgvRays.Rows.Count > 0)
            {
                for (int i = 0; i < dgvRays.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into RaysPatient (raysId,notes,patientId,examinationId) values (@raysId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@raysId", dgvRays[2, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvRays[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", reservationId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // delete FoodPatient
            cmd = new SqlCommand("delete from FoodPatient Where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();

            // insert into FoodPatient
            if (dgvFood.Rows.Count > 0)
            {
                for (int i = 0; i < dgvFood.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into FoodPatient (foodId,notes,patientId,examinationId) values (@foodId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@foodId", dgvFood[2, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvFood[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", reservationId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            // delete ContrastingMedicines
            cmd = new SqlCommand("delete from ContrastingMedicines Where examinationId = '" + reservationId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();

            // insert into ContrastingMedicines
            if (dgvContrastingMedicines.Rows.Count > 0)
            {
                for (int i = 0; i < dgvContrastingMedicines.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into ContrastingMedicines (medicineId,notes,patientId,examinationId) values (@medicineId,@notes,@patientId,@examinationId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@medicineId", dgvContrastingMedicines[2, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvContrastingMedicines[0, i].Value);
                    cmd.Parameters.AddWithValue("@patientId", patId);
                    cmd.Parameters.AddWithValue("@examinationId", reservationId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            
            adoClass.sqlcn.Close();

            MessageBox.Show("تم التعديل بنجاح");
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

        private void btnAddNewPerceptionMedicine_Click(object sender, EventArgs e)
        {
            // for combo Drug
            FormAddDrug frm = new FormAddDrug();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Helper.fillComboBox(comboMedicine, "Select id,name from Drugs", "name", "id");
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (chbPrintPerception.Checked)
            {
                if (dgvPrescription.Rows.Count > 0)
                {
                    dsTools tbl = new dsTools();
                    for (int i = 0; i < dgvPrescription.Rows.Count; i++)
                    {
                        DataRow dro = tbl.Tables["dtShowPerceptionMedicine"].NewRow();
                        dro["medicine"] = dgvPrescription[4, i].Value;
                        dro["timesTakeMedicine"] = dgvPrescription[3, i].Value;
                        dro["unit"] = dgvPrescription[2, i].Value;
                        dro["dosage"] = dgvPrescription[1, i].Value;
                        dro["notes"] = dgvPrescription[0, i].Value;
                        tbl.Tables["dtShowPerceptionMedicine"].Rows.Add(dro);
                    }

                    FormReports rptForm = new FormReports();
                    rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportShowExaminationPerception.rdlc";
                    rptForm.mainReport.LocalReport.DataSources.Clear();
                    rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPerceptionMedicine"]));

                    ReportParameter[] reportParameters = new ReportParameter[13];
                    reportParameters[0] = new ReportParameter("doctor", dgvLoading[10, 0].Value.ToString());
                    reportParameters[1] = new ReportParameter("specialization", dgvLoading[11, 0].Value.ToString());
                    reportParameters[2] = new ReportParameter("patient", dgvLoading[14, 0].Value.ToString());
                    reportParameters[3] = new ReportParameter("examinationDate", dateExamination.Value.ToString());
                    reportParameters[4] = new ReportParameter("notes", txtExaminationNotes.Text);
                    reportParameters[5] = new ReportParameter("examination", txtDoctorExamination.Text);
                    reportParameters[6] = new ReportParameter("advise", txtDoctorAdvice.Text);
                    reportParameters[7] = new ReportParameter("address", declarations.systemOptions["address"].ToString());
                    reportParameters[8] = new ReportParameter("phone", declarations.systemOptions["phone"].ToString());
                    reportParameters[9] = new ReportParameter("whatsApp", declarations.systemOptions["whatsApp"].ToString());
                    reportParameters[10] = new ReportParameter("gmail", declarations.systemOptions["email"].ToString());
                    reportParameters[11] = new ReportParameter("facebook", declarations.systemOptions["facebook"].ToString());
                    reportParameters[12] = new ReportParameter("user", declarations.name);

                    if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                    {
                        LocalReport report = new LocalReport();
                        string path = Application.StartupPath + @"\Reports\ReportShowExaminationPerception.rdlc";
                        report.ReportPath = path;
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowPerceptionMedicine"]));
                        report.SetParameters(reportParameters);
                        PrintersClass.PrintToPrinter(report);
                    }
                    else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
                    {
                        rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                        rptForm.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("لا يوجد عناصر لطباعتها في الروشتة العلاجية");
                }
            }
            if (chbPrintAnalysis.Checked)
            {
                if (dgvAnalysis.Rows.Count > 0)
                {
                    dsTools tbl = new dsTools();
                    for (int i = 0; i < dgvAnalysis.Rows.Count; i++)
                    {
                        DataRow dro = tbl.Tables["dtShowAnalysis"].NewRow();
                        dro["name"] = dgvAnalysis[1, i].Value;
                        dro["notes"] = dgvAnalysis[0, i].Value;
                        tbl.Tables["dtShowAnalysis"].Rows.Add(dro);
                    }

                    FormReports rptForm = new FormReports();
                    rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportShowExamination.rdlc";
                    rptForm.mainReport.LocalReport.DataSources.Clear();
                    rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowAnalysis"]));

                    ReportParameter[] reportParameters = new ReportParameter[13];
                    reportParameters[0] = new ReportParameter("doctor", dgvLoading[10, 0].Value.ToString());
                    reportParameters[1] = new ReportParameter("specialization", dgvLoading[11, 0].Value.ToString());
                    reportParameters[2] = new ReportParameter("patient", dgvLoading[14, 0].Value.ToString());
                    reportParameters[3] = new ReportParameter("examinationDate", dateExamination.Value.ToString());
                    reportParameters[4] = new ReportParameter("notes", txtExaminationNotes.Text);
                    reportParameters[5] = new ReportParameter("examination", txtDoctorExamination.Text);
                    reportParameters[6] = new ReportParameter("advise", txtDoctorAdvice.Text);
                    reportParameters[7] = new ReportParameter("address", declarations.systemOptions["address"].ToString());
                    reportParameters[8] = new ReportParameter("phone", declarations.systemOptions["phone"].ToString());
                    reportParameters[9] = new ReportParameter("whatsApp", declarations.systemOptions["whatsApp"].ToString());
                    reportParameters[10] = new ReportParameter("gmail", declarations.systemOptions["email"].ToString());
                    reportParameters[11] = new ReportParameter("facebook", declarations.systemOptions["facebook"].ToString());
                    reportParameters[12] = new ReportParameter("user", declarations.name);

                    if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                    {
                        LocalReport report = new LocalReport();
                        string path = Application.StartupPath + @"\Reports\ReportShowExamination.rdlc";
                        report.ReportPath = path;
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowAnalysis"]));
                        report.SetParameters(reportParameters);
                        PrintersClass.PrintToPrinter(report);
                    }
                    else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
                    {
                        rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                        rptForm.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("لا يوجد عناصر لطباعتها في التحاليل");
                }
            }
            if (chbPrintRays.Checked)
            {
                if (dgvRays.Rows.Count > 0)
                {
                    dsTools tbl = new dsTools();
                    for (int i = 0; i < dgvRays.Rows.Count; i++)
                    {
                        DataRow dro = tbl.Tables["dtShowRays"].NewRow();
                        dro["name"] = dgvRays[1, i].Value;
                        dro["notes"] = dgvRays[0, i].Value;
                        tbl.Tables["dtShowRays"].Rows.Add(dro);
                    }

                    FormReports rptForm = new FormReports();
                    rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportShowExaminationRays.rdlc";
                    rptForm.mainReport.LocalReport.DataSources.Clear();
                    rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowRays"]));

                    ReportParameter[] reportParameters = new ReportParameter[13];
                    reportParameters[0] = new ReportParameter("doctor", dgvLoading[10, 0].Value.ToString());
                    reportParameters[1] = new ReportParameter("specialization", dgvLoading[11, 0].Value.ToString());
                    reportParameters[2] = new ReportParameter("patient", dgvLoading[14, 0].Value.ToString());
                    reportParameters[3] = new ReportParameter("examinationDate", dateExamination.Value.ToString());
                    reportParameters[4] = new ReportParameter("notes", txtExaminationNotes.Text);
                    reportParameters[5] = new ReportParameter("examination", txtDoctorExamination.Text);
                    reportParameters[6] = new ReportParameter("advise", txtDoctorAdvice.Text);
                    reportParameters[7] = new ReportParameter("address", declarations.systemOptions["address"].ToString());
                    reportParameters[8] = new ReportParameter("phone", declarations.systemOptions["phone"].ToString());
                    reportParameters[9] = new ReportParameter("whatsApp", declarations.systemOptions["whatsApp"].ToString());
                    reportParameters[10] = new ReportParameter("gmail", declarations.systemOptions["email"].ToString());
                    reportParameters[11] = new ReportParameter("facebook", declarations.systemOptions["facebook"].ToString());
                    reportParameters[12] = new ReportParameter("user", declarations.name);

                    if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                    {
                        LocalReport report = new LocalReport();
                        string path = Application.StartupPath + @"\Reports\ReportShowExaminationRays.rdlc";
                        report.ReportPath = path;
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowRays"]));
                        report.SetParameters(reportParameters);
                        PrintersClass.PrintToPrinter(report);
                    }
                    else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
                    {
                        rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                        rptForm.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("لا يوجد عناصر لطباعتها في الاشعة");
                }
            }
            if (chbPrintFoods.Checked)
            {
                if (dgvFood.Rows.Count > 0)
                {
                    dsTools tbl = new dsTools();
                    for (int i = 0; i < dgvFood.Rows.Count; i++)
                    {
                        DataRow dro = tbl.Tables["dtShowFoods"].NewRow();
                        dro["name"] = dgvFood[1, i].Value;
                        dro["notes"] = dgvFood[0, i].Value;
                        tbl.Tables["dtShowFoods"].Rows.Add(dro);
                    }

                    FormReports rptForm = new FormReports();
                    rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportShowExaminationFoods.rdlc";
                    rptForm.mainReport.LocalReport.DataSources.Clear();
                    rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowFoods"]));

                    ReportParameter[] reportParameters = new ReportParameter[13];
                    reportParameters[0] = new ReportParameter("doctor", dgvLoading[10, 0].Value.ToString());
                    reportParameters[1] = new ReportParameter("specialization", dgvLoading[11, 0].Value.ToString());
                    reportParameters[2] = new ReportParameter("patient", dgvLoading[14, 0].Value.ToString());
                    reportParameters[3] = new ReportParameter("examinationDate", dateExamination.Value.ToString());
                    reportParameters[4] = new ReportParameter("notes", txtExaminationNotes.Text);
                    reportParameters[5] = new ReportParameter("examination", txtDoctorExamination.Text);
                    reportParameters[6] = new ReportParameter("advise", txtDoctorAdvice.Text);
                    reportParameters[7] = new ReportParameter("address", declarations.systemOptions["address"].ToString());
                    reportParameters[8] = new ReportParameter("phone", declarations.systemOptions["phone"].ToString());
                    reportParameters[9] = new ReportParameter("whatsApp", declarations.systemOptions["whatsApp"].ToString());
                    reportParameters[10] = new ReportParameter("gmail", declarations.systemOptions["email"].ToString());
                    reportParameters[11] = new ReportParameter("facebook", declarations.systemOptions["facebook"].ToString());
                    reportParameters[12] = new ReportParameter("user", declarations.name);

                    if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                    {
                        LocalReport report = new LocalReport();
                        string path = Application.StartupPath + @"\Reports\ReportShowExaminationFoods.rdlc";
                        report.ReportPath = path;
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowFoods"]));
                        report.SetParameters(reportParameters);
                        PrintersClass.PrintToPrinter(report);
                    }
                    else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
                    {
                        rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                        rptForm.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("لا يوجد عناصر لطباعتها في الاكل الممنوع");
                }
            }
            if (chbPrintContrastingMedicines.Checked)
            {
                if (dgvContrastingMedicines.Rows.Count > 0)
                {
                    dsTools tbl = new dsTools();
                    for (int i = 0; i < dgvContrastingMedicines.Rows.Count; i++)
                    {
                        DataRow dro = tbl.Tables["dtShowDrugs"].NewRow();
                        dro["name"] = dgvContrastingMedicines[1, i].Value;
                        dro["notes"] = dgvContrastingMedicines[0, i].Value;
                        tbl.Tables["dtShowDrugs"].Rows.Add(dro);
                    }

                    FormReports rptForm = new FormReports();
                    rptForm.mainReport.LocalReport.ReportEmbeddedResource = "ClinicApp.Reports.ReportShowExaminationMedicine.rdlc";
                    rptForm.mainReport.LocalReport.DataSources.Clear();
                    rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowDrugs"]));

                    ReportParameter[] reportParameters = new ReportParameter[13];
                    reportParameters[0] = new ReportParameter("doctor", dgvLoading[10, 0].Value.ToString());
                    reportParameters[1] = new ReportParameter("specialization", dgvLoading[11, 0].Value.ToString());
                    reportParameters[2] = new ReportParameter("patient", dgvLoading[14, 0].Value.ToString());
                    reportParameters[3] = new ReportParameter("examinationDate", dateExamination.Value.ToString());
                    reportParameters[4] = new ReportParameter("notes", txtExaminationNotes.Text);
                    reportParameters[5] = new ReportParameter("examination", txtDoctorExamination.Text);
                    reportParameters[6] = new ReportParameter("advise", txtDoctorAdvice.Text);
                    reportParameters[7] = new ReportParameter("address", declarations.systemOptions["address"].ToString());
                    reportParameters[8] = new ReportParameter("phone", declarations.systemOptions["phone"].ToString());
                    reportParameters[9] = new ReportParameter("whatsApp", declarations.systemOptions["whatsApp"].ToString());
                    reportParameters[10] = new ReportParameter("gmail", declarations.systemOptions["email"].ToString());
                    reportParameters[11] = new ReportParameter("facebook", declarations.systemOptions["facebook"].ToString());
                    reportParameters[12] = new ReportParameter("user", declarations.name);

                    if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                    {
                        LocalReport report = new LocalReport();
                        string path = Application.StartupPath + @"\Reports\ReportShowExaminationMedicine.rdlc";
                        report.ReportPath = path;
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowDrugs"]));
                        report.SetParameters(reportParameters);
                        PrintersClass.PrintToPrinter(report);
                    }
                    else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
                    {
                        rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                        rptForm.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("لا يوجد عناصر لطباعتها في الادوية المتعارضة");
                }
            }
        }


        //private byte[] uploadFile(string file)
        //{
        //FileStream fstream = File.OpenRead(file);
        //byte[] content = new byte[fstream.Length];
        //fstream.Read(content, 0, (int)fstream.Length);
        //fstream.Close();

        //return content;
        //}
    }
}
