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

namespace ClinicApp.Forms.Settings
{
    public partial class FormPrintingSettings : Form
    {
        public FormPrintingSettings()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtImage;
        private bool updateAction = false;
        private string id;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (updateAction)
            {
                try
                {

                    cmd = new SqlCommand("Update PrintingSettings set image=@image,address=@address,phone=@phone,facebook=@facebook,whatsApp=@whatsApp,email=@email,directPrint=@directPrint,showBeforePrint=@showBeforePrint,dontShow=@dontShow Where id = '" + id + "'", adoClass.sqlcn);

                    if (pic.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(pic.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@facebook", txtFaceBook.Text);
                    cmd.Parameters.AddWithValue("@whatsApp", txtWhatsApp.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@directPrint", rdoDirectPrint.Checked.ToString());
                    cmd.Parameters.AddWithValue("@showBeforePrint", rdoShowBeforePrint.Checked.ToString());
                    cmd.Parameters.AddWithValue("@dontShow", rdoDontShow.Checked.ToString());

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
            }
            else
            {
                try
                {
                    cmd = new SqlCommand("Insert into PrintingSettings (image,address,phone,facebook,whatsApp,email,directPrint,showBeforePrint,dontShow) values (@image,@address,@phone,@facebook,@whatsApp,@email,@directPrint,@showBeforePrint,@dontShow)", adoClass.sqlcn);

                    if (pic.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(pic.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@facebook", txtFaceBook.Text);
                    cmd.Parameters.AddWithValue("@whatsApp", txtWhatsApp.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@directPrint", rdoDirectPrint.Checked.ToString());
                    cmd.Parameters.AddWithValue("@showBeforePrint", rdoShowBeforePrint.Checked.ToString());
                    cmd.Parameters.AddWithValue("@dontShow", rdoDontShow.Checked.ToString());

                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

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


            ClassLoading loading = new ClassLoading();
            loading.loadSystemOptions();
        }

        private void btnRemovePic_Click(object sender, EventArgs e)
        {
            pic.BackgroundImage = null;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtImage.Text = fileDialog.FileName;
                pic.BackgroundImage = new Bitmap(txtImage.Text);
            }
        }

        private void FormPrintingSettings_Load(object sender, EventArgs e)
        {
            // for hidden images
            txtImage = new TextBox();
            txtImage.Visible = false;



            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("select * from PrintingSettings", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            if (dt.Rows.Count > 0)
            {
                updateAction = true;
                DataRow row = dt.Rows[0];

                id = row["id"].ToString();
                pic.BackgroundImage = Helper.ByteToImage(row["image"]);
                txtAddress.Text = row["address"].ToString();
                txtPhone.Text = row["phone"].ToString();
                txtFaceBook.Text = row["facebook"].ToString();
                txtWhatsApp.Text = row["whatsApp"].ToString();
                txtEmail.Text = row["email"].ToString();

                //bool printToPrinter = false;
                //bool.TryParse(dataTable.Rows[i]["printToPrinter"].ToString(), out printToPrinter);
                //cBoxPrintToPrinter.Checked = printToPrinter;

                bool DirectPrintValue = false;
                bool.TryParse(row["directPrint"].ToString(), out DirectPrintValue);
                rdoDirectPrint.Checked = DirectPrintValue;


                bool ShowBeforePrintValue = false;
                bool.TryParse(row["showBeforePrint"].ToString(), out ShowBeforePrintValue);
                rdoShowBeforePrint.Checked = ShowBeforePrintValue;

                bool DontShowValue = false;
                bool.TryParse(row["dontShow"].ToString(), out DontShowValue);
                rdoDontShow.Checked = DontShowValue;
            }
        }

        
    }
}
