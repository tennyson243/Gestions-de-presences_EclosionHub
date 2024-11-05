using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.incubes
{
    public partial class Carte_Incube : Form
    {
        int id_incube;
        public Carte_Incube(int id)
        {
            InitializeComponent();
            this.id_incube = id;
        }

        Classes.Incubes incubes = new Classes.Incubes();
        private void Carte_Incube_Load(object sender, EventArgs e)
        {
            DataTable table = incubes.getCarteIncube(id_incube);

            if(table.Rows.Count>0)
            {
                Initialiserdonnee(table);
            }
        }

        public void Initialiserdonnee(DataTable data)
        {
            LabelNomIncube.Text = data.Rows[0][0].ToString();

            try
            {
                byte[] couverture = (byte[])data.Rows[0][1];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                pictureBoxPhotoIncube.Image = Image.FromStream(ms);

                byte[] code = (byte[])data.Rows[0][2];
                MemoryStream mss = new MemoryStream(code);
                byte[] QR = mss.ToArray();
                PictureBoxQrCode.Image = Image.FromStream(mss);
            }
            catch
            {

            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            

        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sv = new SaveFileDialog();
                sv.FileName = "CodeQR_" + LabelNomIncube.Text;

                if (PictureBoxQrCode.Image == null)
                {
                    MessageBox.Show("Pas d'image");
                }
                else
                {
                    if (sv.ShowDialog() == DialogResult.OK)
                    {
                        PictureBoxQrCode.Image.Save(sv.FileName + "." + ImageFormat.Jpeg.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
            }
        }
    }
}
