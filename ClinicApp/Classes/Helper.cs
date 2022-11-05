using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicApp.Classes
{
    class Helper
    {
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Byte[] ImageTOByte(Image img)
        {
            img = ResizeImage(img, 64, 64);
            Byte[] bResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                bResult = ms.ToArray();
            }

            return bResult;
        }


        public static Image ByteToImage(object bObj)
        {
            Image image = null;
            try
            {
                Byte[] myImg = (Byte[])bObj;

                using (MemoryStream ms = new MemoryStream(myImg, 0, myImg.Length))
                {
                    ms.Write(myImg, 0, myImg.Length);
                    image = Image.FromStream(ms, true);
                }

                return image;
            }
            catch
            {
                return image;
            }

        }


        public static void fillComboBox(ComboBox name, string statement, string displayMember, string valueMember)
        {
            SqlCommand cmd = new SqlCommand(statement, adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            DataRow itemRow = table1.NewRow();
            itemRow[1] = "";
            table1.Rows.InsertAt(itemRow, 0);

            name.DataSource = table1;
            name.DisplayMember = displayMember;
            name.ValueMember = valueMember;

        }
    }
}
