using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicApp.Classes
{
    public class ClassLoading
    {
        private SqlCommand cmd;
        private SqlDataReader dr;


        public void loadSystemOptions()
        {
            cmd = new SqlCommand("Select Top 1 * from PrintingSettings", adoClass.sqlcn);
            dr = null;
            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                dr = cmd.ExecuteReader();
                declarations.systemOptions = new Dictionary<string, object>();
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        declarations.systemOptions.Add(dr.GetName(i), dr[dr.GetName(i)]);
                    }
                }
                adoClass.sqlcn.Close();
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
}
