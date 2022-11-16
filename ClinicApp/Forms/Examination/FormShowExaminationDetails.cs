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

namespace ClinicApp.Forms.Examination
{
    public partial class FormShowExaminationDetails : Form
    {
        public FormShowExaminationDetails()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        public string patId; // patient id
        public string reservationId; // reservation Id


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
                            }
                        );
                }
            }
        }

        private void FormShowExaminationDetails_Load(object sender, EventArgs e)
        {
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvAnalysis.Rows.Count > 0)
            {
                MessageBox.Show(dgvAnalysis.Rows.Count.ToString());
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
                reportParameters[3] = new ReportParameter("examinationDate", dateOfBirth.Value.ToString());
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
                MessageBox.Show("لا يوجد عناصر لعرضها");
            }
        }
    }
}
