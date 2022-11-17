using ClinicApp.Classes;
using ClinicApp.Forms.Companies;
using ClinicApp.Forms.Doctors;
using ClinicApp.Forms.Employees;
using ClinicApp.Forms.Examination;
using ClinicApp.Forms.Expenses;
using ClinicApp.Forms.Governments;
using ClinicApp.Forms.Income;
using ClinicApp.Forms.Locations.Analysis;
using ClinicApp.Forms.Locations.Doctors;
using ClinicApp.Forms.Locations.Hospitals;
using ClinicApp.Forms.Locations.Pharmacies;
using ClinicApp.Forms.Locations.Rays;
using ClinicApp.Forms.patients;
using ClinicApp.Forms.Reservations;
using ClinicApp.Forms.Settings;
using ClinicApp.Forms.Settings.Analysis;
using ClinicApp.Forms.Settings.BookingType;
using ClinicApp.Forms.Settings.Clinic;
using ClinicApp.Forms.Settings.Diseases;
using ClinicApp.Forms.Settings.Foods;
using ClinicApp.Forms.Settings.Pharmacy;
using ClinicApp.Forms.Settings.Rays;
using ClinicApp.Forms.Settings.Specializations;
using ClinicApp.Forms.Settings.Symptoms;
using ClinicApp.Forms.SmartAssistant;
using ClinicApp.Forms.Users;
using ClinicApp.Settings.Clinic;
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

namespace ClinicApp.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        //private void btnSettings_Click(object sender, EventArgs e)
        //{
        //    FormSettings frm = new FormSettings();
        //    frm.Show();
        //}



        //private void btnEmployee_Click(object sender, EventArgs e)
        //{
        //    FormEmployee frm = new FormEmployee();
        //    frm.Show();
        //}

        //private void btnPatient_Click(object sender, EventArgs e)
        //{
        //    //FormPatients frm = new FormPatients();
        //    //frm.Show();
        //}

   

        private void btnShowDoctors_Click(object sender, EventArgs e)
        {
            FormShowDoctors frm = new FormShowDoctors();
            frm.Show();
        }

        private void btnAddDoctorTime_Click(object sender, EventArgs e)
        {
            FormDoctorsTime frm = new FormDoctorsTime();
            frm.Show();
        }

        private void btnDoctorsTime_Click(object sender, EventArgs e)
        {
            FormShowDoctorsTime frm = new FormShowDoctorsTime();
            frm.Show();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            FormEmployee frm = new FormEmployee();
            frm.Show();
        }

        private void btnShowEmployees_Click(object sender, EventArgs e)
        {
            FormShowEmployees frm = new FormShowEmployees();
            frm.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            FormUsers frm = new FormUsers();
            frm.Show();
        }

        private void btnShowUsers_Click(object sender, EventArgs e)
        {
            FormShowUsers frm = new FormShowUsers();
            frm.Show();
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            FormCompanies frm = new FormCompanies();
            frm.Show();
        }

        private void btnShowCompanies_Click(object sender, EventArgs e)
        {
            FormShowCompanies frm = new FormShowCompanies();
            frm.Show();
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            FormPatient frm = new FormPatient();
            frm.Show();
        }

        private void btnShowPatient_Click(object sender, EventArgs e)
        {
            FormShowPatient frm = new FormShowPatient();
            frm.Show();
        }

        private void btnAddClinic_Click(object sender, EventArgs e)
        {
            FormAddClinic frm = new FormAddClinic();
            frm.Show();
        }

        private void btnShowClinics_Click(object sender, EventArgs e)
        {
            FormShowClinic frm = new FormShowClinic();
            frm.Show();
        }

        private void btnAddSpecialization_Click(object sender, EventArgs e)
        {
            FormAddSpecialization frm = new FormAddSpecialization();
            frm.Show();
        }

        private void btnShowSpecializations_Click(object sender, EventArgs e)
        {
            FormShowSpecializations frm = new FormShowSpecializations();
            frm.Show();
        }

        private void btnAddDrug_Click(object sender, EventArgs e)
        {
            FormAddDrug frm = new FormAddDrug();
            frm.Show();
        }

        private void btnShowDrugs_Click(object sender, EventArgs e)
        {
            FormShowDrugs frm = new FormShowDrugs();
            frm.Show();
        }

        private void btnAddMedicineUnit_Click(object sender, EventArgs e)
        {
            FormAddMedicineUnit frm = new FormAddMedicineUnit();
            frm.Show();
        }

        private void btnShowMedicineUnits_Click(object sender, EventArgs e)
        {
            FormShowMedicineUnits frm = new FormShowMedicineUnits();
            frm.Show();
        }

        private void btnAddDosage_Click(object sender, EventArgs e)
        {
            FormAddDosage frm = new FormAddDosage();
            frm.Show();
        }

        private void btnShowDosages_Click(object sender, EventArgs e)
        {
            FormShowDosages frm = new FormShowDosages();
            frm.Show();
        }

        private void btnAddReservations_Click(object sender, EventArgs e)
        {
            FormAddReservations frm = new FormAddReservations();
            frm.Show();
            
        }

        private void btnShowReservations_Click(object sender, EventArgs e)
        {
            FormShowReservations frm = new FormShowReservations();
            frm.Show();
        }

        private void btnAddExamination_Click(object sender, EventArgs e)
        {
            FormAddExamination frm = new FormAddExamination();
            frm.Show();
        }

        private void btnAddDisease_Click(object sender, EventArgs e)
        {
            FormAddDisease frm = new FormAddDisease();
            frm.Show();
        }

        private void btnShowDiseases_Click(object sender, EventArgs e)
        {
            FormShowDiseases frm = new FormShowDiseases();
            frm.Show();
        }

        private void btnShowSymptom_Click(object sender, EventArgs e)
        {
            FormShowSymptoms frm = new FormShowSymptoms();
            frm.Show();
        }

        private void btnAddSymptom_Click(object sender, EventArgs e)
        {
            FormAddSymptom frm = new FormAddSymptom();
            frm.Show();
        }

        private void btnAddAnalysis_Click(object sender, EventArgs e)
        {
            FormAddAnalysis frm = new FormAddAnalysis();
            frm.Show();
        }

        private void btnShowAnalysis_Click(object sender, EventArgs e)
        {
            FormShowAnalysis frm = new FormShowAnalysis();
            frm.Show();
        }

        private void btnAddRay_Click(object sender, EventArgs e)
        {
            FormAddRay frm = new FormAddRay();
            frm.Show();
        }

        private void btnShowRays_Click(object sender, EventArgs e)
        {
            FormShowRays frm = new FormShowRays();
            frm.Show();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            FormAddFood frm = new FormAddFood();
            frm.Show();
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            FormShowFoods frm = new FormShowFoods();
            frm.Show();
        }

        private void btnAddBookingType_Click(object sender, EventArgs e)
        {
            FormAddBookingType frm = new FormAddBookingType();
            frm.Show();
        }

        private void btnShowBookingType_Click(object sender, EventArgs e)
        {
            FormShowBookingType frm = new FormShowBookingType();
            frm.Show();
        }

        private void btnShowExamination_Click(object sender, EventArgs e)
        {
            FormShowExamination frm = new FormShowExamination();
            frm.Show();
        }

        private void btnAddExpensesType_Click(object sender, EventArgs e)
        {
            FormAddExpensesType frm = new FormAddExpensesType();
            frm.Show();
        }

        private void btnShowExpensesTypes_Click(object sender, EventArgs e)
        {
            FormShowExpensesTypes frm = new FormShowExpensesTypes();
            frm.Show();
            
        }

        private void btnAddExpenses_Click(object sender, EventArgs e)
        {
            FormAddExpenses frm = new FormAddExpenses();
            frm.Show();
        }

        private void btnShowExpenses_Click(object sender, EventArgs e)
        {
            FormShowExpenses frm = new FormShowExpenses();
            frm.Show();
        }

        private void btnAddGovernment_Click(object sender, EventArgs e)
        {
            FormAddGovernment frm = new FormAddGovernment();
            frm.Show();
        }

        private void btnShowGovernment_Click(object sender, EventArgs e)
        {
            FormShowGovernments frm = new FormShowGovernments();
            frm.Show();
        }

        private void btnAddDoctorToLoc_Click(object sender, EventArgs e)
        {
            FormAddDoctorToLoc frm = new FormAddDoctorToLoc();
            frm.Show();
        }

        private void btnShowDoctorsLoc_Click(object sender, EventArgs e)
        {
            FormShowDoctorsLoc frm = new FormShowDoctorsLoc();
            frm.Show();
        }

        private void btnAddHospitalsLoc_Click(object sender, EventArgs e)
        {
            FormAddHospitalsLoc frm = new FormAddHospitalsLoc();
            frm.Show();
        }

        private void btnShowHospitalsLoc_Click(object sender, EventArgs e)
        {
            FormShowHospitalsLoc frm = new FormShowHospitalsLoc();
            frm.Show();
        }

        private void btnAddPharmacyLoc_Click(object sender, EventArgs e)
        {
            FormAddPharmacyLoc frm = new FormAddPharmacyLoc();
            frm.Show();
        }

        private void btnShowPharmacyLoc_Click(object sender, EventArgs e)
        {
            FormShowPharmacyLoc frm = new FormShowPharmacyLoc();
            frm.Show();
        }

        private void btnAddAnalysisLoc_Click(object sender, EventArgs e)
        {
            FormAddAnalysisLoc frm = new FormAddAnalysisLoc();
            frm.Show();
        }

        private void btnShowAnalysisLoc_Click(object sender, EventArgs e)
        {
            FormShowAnalysisLoc frm = new FormShowAnalysisLoc();
            frm.Show();
        }

        private void btnAddRaysLoc_Click(object sender, EventArgs e)
        {
            FormAddRaysLoc frm = new FormAddRaysLoc();
            frm.Show();
        }

        private void btnShowRaysLoc_Click(object sender, EventArgs e)
        {
            FormShowRaysLoc frm = new FormShowRaysLoc();
            frm.Show();
        }

        private void btnAddToSmartAssistant_Click(object sender, EventArgs e)
        {
            FormAddToSmartAssistant frm = new FormAddToSmartAssistant();
            frm.Show();
        }

        private void btnShowSmartAssistant_Click(object sender, EventArgs e)
        {
            FormShowSmartAssistant frm = new FormShowSmartAssistant();
            frm.Show();
        }

        private void btnPrintingSettings_Click(object sender, EventArgs e)
        {
            FormPrintingSettings frm = new FormPrintingSettings();
            frm.Show();
        }

        private void btnVisitingPatient_Click(object sender, EventArgs e)
        {
            FormVisitingPatient frm = new FormVisitingPatient();
            frm.Show();
        }

        private void btnPatientPerception_Click(object sender, EventArgs e)
        {
            FormPatientPrescription frm = new FormPatientPrescription();
            frm.Show();
        }

        private void btnPatientAnalysis_Click(object sender, EventArgs e)
        {
            FormPatientAnalysis frm = new FormPatientAnalysis();
            frm.Show();
        }

        private void btnPatientRays_Click(object sender, EventArgs e)
        {
            FormPatientRays frm = new FormPatientRays();
            frm.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select * from Users where id = '" + declarations.userId  + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                // for doctor
                if(row["doctorHome"].ToString() == "False")
                {
                    btnHomeDoctors.Visible = false;
                }
                if (row["doctorAdd"].ToString() == "False")
                {
                    btnAddDoctor.Visible = false;
                    btnAddDoctorTime.Visible = false;
                }
                if (row["doctorShow"].ToString() == "False")
                {
                    btnShowDoctors.Visible = false;
                    btnDoctorsTime.Visible = false;
                }

                // for employee
                if (row["employeeHome"].ToString() == "False")
                {
                    btnHomeEmployees.Visible = false;
                }
                if (row["employeeAdd"].ToString() == "False")
                {
                    btnAddEmployee.Visible = false;
                }
                if (row["employeeShow"].ToString() == "False")
                {
                    btnShowEmployees.Visible = false;
                }
                // for Users
                if (row["userHome"].ToString() == "False")
                {
                    btnHomeUsers.Visible = false;
                }
                if (row["userAdd"].ToString() == "False")
                {
                    btnUsers.Visible = false;
                }
                if (row["userShow"].ToString() == "False")
                {
                    btnShowUsers.Visible = false;
                }
                // for patient
                if (row["patientHome"].ToString() == "False")
                {
                    btnHomePatients.Visible = false;
                }
                if (row["patientAdd"].ToString() == "False")
                {
                    btnAddPatient.Visible = false;
                }
                if (row["patientShow"].ToString() == "False")
                {
                    btnShowPatient.Visible = false;
                    btnVisitingPatient.Visible = false;
                    btnPatientPerception.Visible = false;
                    btnPatientAnalysis.Visible = false;
                    btnPatientRays.Visible = false;
                }


                // for reservation
                if (row["resHome"].ToString() == "False")
                {
                    btnHomeReservations.Visible = false;
                }
                if (row["resAdd"].ToString() == "False")
                {
                    btnAddReservations.Visible = false;
                }
                if (row["resShow"].ToString() == "False")
                {
                    btnShowReservations.Visible = false;
                }

                // for examination
                if (row["examHome"].ToString() == "False")
                {
                    btnHomeExamination.Visible = false;
                }
                if (row["examAdd"].ToString() == "False")
                {
                    btnAddExamination.Visible = false;
                }
                if (row["examShow"].ToString() == "False")
                {
                    btnShowExamination.Visible = false;
                }

                // for expense
                if (row["expenseHome"].ToString() == "False")
                {
                    btnHomeExpenses.Visible = false;
                }
                if (row["expenseAdd"].ToString() == "False")
                {
                    btnAddExpenses.Visible = false;
                    btnAddExpensesType.Visible = false;
                }
                if (row["expenseShow"].ToString() == "False")
                {
                    btnShowExpenses.Visible = false;
                    btnShowExpensesTypes.Visible = false;
                }

                // for company
                if (row["companyHome"].ToString() == "False")
                {
                    btnHomeCompany.Visible = false;
                }
                if (row["companyAdd"].ToString() == "False")
                {
                    btnCompany.Visible = false;
                }
                if (row["companyShow"].ToString() == "False")
                {
                    btnShowCompanies.Visible = false;
                }
                // for smart assistant
                if (row["smartHome"].ToString() == "False")
                {
                    btnHomeSmartAssistant.Visible = false;
                }
                if (row["smartAdd"].ToString() == "False")
                {
                    btnAddToSmartAssistant.Visible = false;
                }
                if (row["smartShow"].ToString() == "False")
                {
                    btnShowSmartAssistant.Visible = false;
                }
                // for locations
                if (row["locationHome"].ToString() == "False")
                {
                    btnHomeLocations.Visible = false;
                }
                if (row["locationAdd"].ToString() == "False")
                {
                    btnAddDoctorToLoc.Visible = false;
                    btnAddHospitalsLoc.Visible = false;
                    btnAddPharmacyLoc.Visible = false;
                    btnAddAnalysisLoc.Visible = false;
                    btnAddRaysLoc.Visible = false;
                }
                if (row["locationShow"].ToString() == "False")
                {
                    btnShowDoctorsLoc.Visible = false;
                    btnShowHospitalsLoc.Visible = false;
                    btnShowPharmacyLoc.Visible = false;
                    btnShowAnalysisLoc.Visible = false;
                    btnShowRaysLoc.Visible = false;
                }

                if (row["settingHome"].ToString() == "False")
                {
                    btnHomeSettings.Visible = false;
                }
                if (row["settingAdd"].ToString() == "False")
                {
                    btnAddClinic.Visible = false;
                    btnAddSpecialization.Visible = false;
                    btnAddDrug.Visible = false;
                    btnAddMedicineUnit.Visible = false;
                    btnAddDosage.Visible = false;
                    btnAddDisease.Visible = false;
                    btnAddSymptom.Visible = false;
                    btnAddAnalysis.Visible = false;
                    btnAddRay.Visible = false;
                    btnAddFood.Visible = false;
                    btnAddBookingType.Visible = false;
                    btnAddGovernment.Visible = false;
                    btnPrintingSettings.Visible = false;
                    btnAddVisitingType.Visible = false;
                }
                if (row["settingShow"].ToString() == "False")
                {
                    btnShowClinics.Visible = false;
                    btnShowSpecializations.Visible = false;
                    btnShowDrugs.Visible = false;
                    btnShowMedicineUnits.Visible = false;
                    btnShowDosages.Visible = false;
                    btnShowDiseases.Visible = false;
                    btnShowSymptom.Visible = false;
                    btnShowAnalysis.Visible = false;
                    btnShowRays.Visible = false;
                    btnShowFood.Visible = false;
                    btnShowBookingType.Visible = false;
                    btnShowGovernment.Visible = false;
                    btnShowVisitingType.Visible = false;
                }
            }
        
    }

        private void btnAddDoctor_Click(object sender, EventArgs e)
        {
            FormDoctors frm = new FormDoctors();
            frm.Show();
        }


        private void btnAddVisitingType_Click(object sender, EventArgs e)
        {
            FormAddIncomeType frm = new FormAddIncomeType();
            frm.Show();
        }

        private void btnShowVisitingType_Click(object sender, EventArgs e)
        {

            FormShowIncomesTypes frm = new FormShowIncomesTypes();
            frm.Show();
        }
    }
}
