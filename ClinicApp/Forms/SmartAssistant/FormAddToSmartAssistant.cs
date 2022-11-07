using ClinicApp.Classes;
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

namespace ClinicApp.Forms.SmartAssistant
{
    public partial class FormAddToSmartAssistant : Form
    {
        public FormAddToSmartAssistant()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        public bool showPDFMode = false;
        public string id = "";
        public FormShowSmartAssistant refreshForm;
        public byte[] pdfContent;
        private TextBox txtPic1;
        private TextBox txtPic2;
        private TextBox txtPic3;
        private TextBox txtPic4;
        private TextBox txtPic5;
        private TextBox txtPic6;
        private TextBox txtPic7;
        private TextBox txtPic8;
        private void btnChoose1_Click(object sender, EventArgs e)
        {
            addImage(txtPic1, pic1);
        }

        private void FormAddToSmartAssistant_Load(object sender, EventArgs e)
        {
            // for hidden pics
            txtPic1 = new TextBox();
            txtPic1.Visible = false;

            txtPic2 = new TextBox();
            txtPic2.Visible = false;

            txtPic3 = new TextBox();
            txtPic3.Visible = false;

            txtPic4 = new TextBox();
            txtPic4.Visible = false;

            txtPic5 = new TextBox();
            txtPic5.Visible = false;

            txtPic6 = new TextBox();
            txtPic6.Visible = false;

            txtPic7 = new TextBox();
            txtPic7.Visible = false;

            txtPic8 = new TextBox();
            txtPic8.Visible = false;
        }

        private void btnRemovePic1_Click(object sender, EventArgs e)
        {
            pic1.BackgroundImage = null;
        }

        private void btnChoose2_Click(object sender, EventArgs e)
        {
            addImage(txtPic2, pic2);
        }


        private void addImage(TextBox tBox,PictureBox pBox)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                tBox.Text = fileDialog.FileName;
                pBox.BackgroundImage = new Bitmap(tBox.Text);
            }
        }

        private void btnRemovePic2_Click(object sender, EventArgs e)
        {
            pic2.BackgroundImage = null;
        }

        private void btnChoose3_Click(object sender, EventArgs e)
        {
            addImage(txtPic3, pic3);
        }

        private void btnChoose4_Click(object sender, EventArgs e)
        {
            addImage(txtPic4, pic4);
        }

        private void btnChoose5_Click(object sender, EventArgs e)
        {
            addImage(txtPic5, pic5);
        }

        private void btnChoose6_Click(object sender, EventArgs e)
        {
            addImage(txtPic6, pic6);
        }

        private void btnChoose7_Click(object sender, EventArgs e)
        {
            addImage(txtPic7, pic7);
        }

        private void btnChoose8_Click(object sender, EventArgs e)
        {
            addImage(txtPic8, pic8);
        }

        private void btnRemovePic3_Click(object sender, EventArgs e)
        {
            pic3.BackgroundImage = null;
        }

        private void btnRemovePic4_Click(object sender, EventArgs e)
        {
            pic4.BackgroundImage = null;
        }

        private void btnRemovePic5_Click(object sender, EventArgs e)
        {
            pic5.BackgroundImage = null;
        }

        private void btnRemovePic6_Click(object sender, EventArgs e)
        {
            pic6.BackgroundImage = null;
        }

        private void btnRemovePic7_Click(object sender, EventArgs e)
        {
            pic7.BackgroundImage = null;
        }

        private void btnRemovePic8_Click(object sender, EventArgs e)
        {
            pic8.BackgroundImage = null;
        }

        private void btnAddPdf_Click(object sender, EventArgs e)
        {
            if(showPDFMode)
            {
                DataTable dt = new DataTable();

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                cmd = new SqlCommand("select pdf from SmartAssistant where id = '" + id + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                adoClass.sqlcn.Close();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (row[0].ToString() == "")
                    {
                        MessageBox.Show("لا يوجد ملف محفوظ من قبل");
                    }
                    else
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "PDF Documents(*.pdf)|*.pdf", ValidateNames = true })
                        {
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                DialogResult dialog = MessageBox.Show("هل متاكد من حفظ ذلك الملف ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (dialog == DialogResult.Yes)
                                {
                                    string filename = saveFileDialog.FileName;
                                    downLoadFile(filename, row[0]);
                                }
                            }
                        }
                    }


                }

                
            }
            else
            {
                using (OpenFileDialog fileDialog = new OpenFileDialog() { Filter = "PDF Documents(*.pdf)|*.pdf", ValidateNames = true })
                {
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        DialogResult dialog = MessageBox.Show("هل متاكد من حفظ ذلك الملف ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialog == DialogResult.Yes)
                        {
                            string filename = fileDialog.FileName;
                            pdfContent = uploadFile(filename);
                        }
                    }
                }
            }
 
            
        }

        private void downLoadFile(string file,object r)
        {
            byte[] fileData = (byte[])r;
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(fileData);
                    bw.Close();

                    MessageBox.Show("تم الحفظ");
                }
            }
        }

        private byte[] uploadFile(string file)
        {
            FileStream fstream = File.OpenRead(file);
            byte[] content = new byte[fstream.Length];
            fstream.Read(content,0,(int)fstream.Length);
            fstream.Close();

            return content;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                if (txtNameArabic.Text == "" && txtNameEnglish.Text == "")
                {
                    MessageBox.Show("ادخل اسم المرض ");
                    return;
                }

                try
                {
                    cmd = new SqlCommand("Insert into SmartAssistant (nameArabic,nameEnglish,image1,image2,image3,image4,image5,image6,image7,image8,defination,symptom,medicine,reasons,notes,types,pdf) values (@nameArabic,@nameEnglish,@image1,@image2,@image3,@image4,@image5,@image6,@image7,@image8,@defination,@symptom,@medicine,@reasons,@notes,@types,@pdf)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@nameArabic", txtNameArabic.Text);
                    cmd.Parameters.AddWithValue("@nameEnglish", txtNameEnglish.Text);
                    cmd.Parameters.AddWithValue("@defination", txtDefination.Text);
                    cmd.Parameters.AddWithValue("@symptom", txtSymptoms.Text);
                    cmd.Parameters.AddWithValue("@medicine", txtMedicine.Text);
                    cmd.Parameters.AddWithValue("@reasons", txtReasons.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@types", txtTypes.Text);

                    // for image 1
                    if (pic1.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image1", Helper.ImageTOByte(pic1.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image1", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 2
                    if (pic2.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image2", Helper.ImageTOByte(pic2.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image2", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 3
                    if (pic3.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image3", Helper.ImageTOByte(pic3.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image3", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 4
                    if (pic4.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image4", Helper.ImageTOByte(pic4.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image4", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 5
                    if (pic5.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image5", Helper.ImageTOByte(pic5.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image5", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 6
                    if (pic6.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image6", Helper.ImageTOByte(pic6.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image6", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 7
                    if (pic7.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image7", Helper.ImageTOByte(pic7.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image7", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 8
                    if (pic8.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image8", Helper.ImageTOByte(pic8.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image8", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    if (pdfContent != null && pdfContent.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@pdf", pdfContent);
                    }
                    else
                    {
                        cmd.Parameters.Add("@pdf", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

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
            else
            {
                if (id == "")
                {
                    MessageBox.Show("حدد الطعام المراد تعديله");
                    return;
                }
                if (txtNameArabic.Text == "" && txtNameEnglish.Text == "")
                {
                    MessageBox.Show("ادخل اسم المرض ");
                    return;
                }


                try
                {

                    cmd = new SqlCommand("Update SmartAssistant set nameArabic=@nameArabic,nameEnglish=@nameEnglish,image1=@image1,image2=@image2,image3=@image3,image4=@image4,image5=@image5,image6=@image6,image7=@image7,image8=@image8,defination=@defination,symptom=@symptom,medicine=@medicine,reasons=@reasons,notes=@notes,types=@types,pdf=@pdf Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@nameArabic", txtNameArabic.Text);
                    cmd.Parameters.AddWithValue("@nameEnglish", txtNameEnglish.Text);
                    cmd.Parameters.AddWithValue("@defination", txtDefination.Text);
                    cmd.Parameters.AddWithValue("@symptom", txtSymptoms.Text);
                    cmd.Parameters.AddWithValue("@medicine", txtMedicine.Text);
                    cmd.Parameters.AddWithValue("@reasons", txtReasons.Text);
                    cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@types", txtTypes.Text);

                    // for image 1
                    if (pic1.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image1", Helper.ImageTOByte(pic1.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image1", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 2
                    if (pic2.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image2", Helper.ImageTOByte(pic2.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image2", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 3
                    if (pic3.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image3", Helper.ImageTOByte(pic3.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image3", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 4
                    if (pic4.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image4", Helper.ImageTOByte(pic4.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image4", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 5
                    if (pic5.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image5", Helper.ImageTOByte(pic5.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image5", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 6
                    if (pic6.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image6", Helper.ImageTOByte(pic6.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image6", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 7
                    if (pic7.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image7", Helper.ImageTOByte(pic7.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image7", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // for image 8
                    if (pic8.BackgroundImage != null)
                    {
                        cmd.Parameters.AddWithValue("@image8", Helper.ImageTOByte(pic8.BackgroundImage));
                    }
                    else
                    {
                        cmd.Parameters.Add("@image8", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    if (pdfContent != null && pdfContent.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@pdf", pdfContent);
                    }
                    else
                    {
                        cmd.Parameters.Add("@pdf", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

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

                this.Close();
                refreshForm.loadTable("select id,nameArabic,nameEnglish from SmartAssistant");
            }


            txtNameArabic.Text = "";
            txtNameEnglish.Text = "";
            txtDefination.Text = "";
            txtSymptoms.Text = "";
            txtMedicine.Text = "";
            txtReasons.Text = "";
            txtNotes.Text = "";
            txtTypes.Text = "";
            pic1.BackgroundImage = null;
            pic2.BackgroundImage = null;
            pic3.BackgroundImage = null;
            pic4.BackgroundImage = null;
            pic5.BackgroundImage = null;
            pic6.BackgroundImage = null;
            pic7.BackgroundImage = null;
            pic8.BackgroundImage = null;
            pdfContent = null;

            id = "";
        }

        private void btnDeletePdf_Click(object sender, EventArgs e)
        {
            pdfContent = null;
        }
    }
}
