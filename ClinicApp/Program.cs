using ClinicApp.Classes;
using ClinicApp.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClinicApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //adoClass.setConnection();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain());



            if (File.Exists(Application.StartupPath + "\\Serial\\serial.txt") == false)
            {
                Application.Run(new FormEnterSerialNumber());

            }
            else
            {
                adoClass.setConnection();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new FormMain());

                try
                {
                    ClassSerialNumber sn = new ClassSerialNumber();
                    string serialNumber = Regex.Replace(sn.GetSerialNumber(@"C:"), @"\s+", "");

                    StreamReader sr = new StreamReader(Application.StartupPath + "\\serial\\serial.txt");
                    string txt = sr.ReadLine();

                    sr.Close();
                    txt = Regex.Replace(txt, @"\s+", "");

                    if (txt == serialNumber)
                    {

                        FormStartUp frmStartUP = new FormStartUp();
                        if (frmStartUP.ShowDialog() == DialogResult.OK)
                        {
                            FormLogin frmLogin = new FormLogin();
                            if (frmLogin.ShowDialog() == DialogResult.OK)
                            {
                                Application.Run(new FormMain());
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("خطا في تشغيل البرنامج الرجاء الاتصال علي الشركة");
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //MessageBox.Show("خطا في تشغيل البرنامج الرجاء الاتصال علي الشركة");
                    Application.Exit();
                }
            }

        }
    }
}
