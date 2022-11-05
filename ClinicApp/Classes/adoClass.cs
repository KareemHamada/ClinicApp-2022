using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicApp.Classes
{
    class adoClass
    {
        public static SqlConnection sqlcn;

        public static void setConnection()
        {
            try
            {
                sqlcn = new SqlConnection("Data Source=DESKTOP-KE662S4;Initial Catalog=Clinic;Integrated Security=True");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
