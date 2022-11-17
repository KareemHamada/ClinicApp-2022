using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicApp.Classes
{
    class adoClass
    {
        public static SqlConnection sqlcn;

        public static void setConnection()
        {
            //try
            //{
            //    sqlcn = new SqlConnection("Data Source=DESKTOP-KE662S4;Initial Catalog=Clinic;Integrated Security=True");
            //}
            //catch (Exception ex) 
            //{
            //    MessageBox.Show(ex.Message);
            //}

            try
            {

                StreamReader sr = new StreamReader(Application.StartupPath + "\\Serial\\serial.txt");
                string txt = sr.ReadLine();
                string ds = sr.ReadLine();

                sr.Close();
                txt = Regex.Replace(txt, @"\s+", "");
                ds = Regex.Replace(ds, @"\s+", "");


                sqlcn = new SqlConnection("Data Source=" + ds + ";Initial Catalog=Clinic;Integrated Security=True");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
