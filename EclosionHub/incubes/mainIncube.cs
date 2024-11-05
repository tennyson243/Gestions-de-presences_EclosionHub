using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.incubes
{
    public partial class mainIncube : Form
    {
        public mainIncube()
        {
            InitializeComponent();
        }

        Classes.Incubes incubes = new Classes.Incubes();
        Classes.codeQR qR = new Classes.codeQR();

        public void actualiser()
        {
            TextBoxNom.Text = "";
            TextBoxPostnom.Text = "";
            TextBoxPrenom.Text = "";
            TextBoxMatricule.Text = "";
            TextBoxTelephone.Text = "";
            TextBoxTelModifier.Text = "";
            PictureBoxaddIncuber.Image = Image.FromFile("../../Resources/logo_eclosionhub.png");
            DataGridViewIncubes.DataSource = incubes.listIncubes();
        }

        private void gunaAdvenceTileButton1_Click(object sender, EventArgs e)
        {
            PanelList.BringToFront();
        }

        private void gunaAdvenceTileButton2_Click(object sender, EventArgs e)
        {
            PanelAdd.BringToFront();
        }

        private void gunaAdvenceTileButton3_Click(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
        }

        private void mainIncube_Load(object sender, EventArgs e)
        {
            DataGridViewIncubes.RowTemplate.Height = 80;
            DataGridViewColumn IM = new DataGridViewColumn();
            DataGridViewIncubes.DataSource = incubes.listIncubes();
            IM = (DataGridViewColumn)DataGridViewIncubes.Columns[0];
            IM.Visible = false;

            DataGridViewImageColumn dgic = new DataGridViewImageColumn();
            dgic = (DataGridViewImageColumn)DataGridViewIncubes.Columns[5];
            dgic.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        public void Initialiserdonnee(DataTable data)
        {


            TextBoxid.Text = data.Rows[0][0].ToString();
            TextBoxNomModifier.Text = data.Rows[0][1].ToString();
            TextBoxPostNomModifier.Text = data.Rows[0][2].ToString();
            TextBoxPrenomModifier.Text = data.Rows[0][3].ToString();
            TextBoxMatriculeModifier.Text = data.Rows[0][4].ToString();

            try
            {
                byte[] couverture = (byte[])data.Rows[0][5];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                PictureBoxIncubeModifier.Image = Image.FromStream(ms);
            }
            catch
            {

            }
            TextBoxTelephone.Text = data.Rows[0][6].ToString();

        }

        private void ButtonParcourirModifier_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (OPf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxIncubeModifier.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void DataGridViewIncubes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewIncubes.CurrentRow.Cells[0].Value);
            Carte_Incube carte = new Carte_Incube(id);
            carte.Show();
        }

        private void DataGridViewIncubes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewIncubes.CurrentRow.Cells[0].Value);
            Carte_Incube carte = new Carte_Incube(id);
            carte.Show();
        }


        private void gunaAdvenceTileButton1_Click_1(object sender, EventArgs e)
        {
            PanelList.BringToFront();
        }

        private void gunaAdvenceTileButton2_Click_1(object sender, EventArgs e)
        {
            PanelAdd.BringToFront();
        }

        private void gunaAdvenceTileButton3_Click_1(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
        }


        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                if (e.Value >= 0 && e.Value < DataGridViewIncubes.RowCount)
                {
                    DataGridViewIncubes.FirstDisplayedScrollingRowIndex = DataGridViewIncubes.Rows[e.Value].Index;
                }
            }
            catch (Exception ex)
            {
                // Gérez l'exception de manière appropriée, par exemple, en affichant un message d'erreur ou en journalisant.
                Console.WriteLine("Erreur lors du défilement : " + ex.Message);
            }
        }

        private void DataGridViewIncubes_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewIncubes.RowCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ajout de lignes : " + ex.Message);
            }
        }

        private void DataGridViewIncubes_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewIncubes.RowCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la suppression de lignes : " + ex.Message);
            }
        }

        private void bunifuTileButton2_Click_1(object sender, EventArgs e)
        {
            PanelAdd.BringToFront();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            PanelList.BringToFront();
        }

        private void ButtonAddParcourir_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (OPf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxaddIncuber.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            string nom = TextBoxNom.Text;
            string postnom = TextBoxPostnom.Text;
            string prenom = TextBoxPrenom.Text;
            string matricule = TextBoxMatricule.Text;
            string tel = TextBoxTelModifier.Text;
            MemoryStream ms = new MemoryStream();
            PictureBoxaddIncuber.Image.Save(ms, PictureBoxaddIncuber.Image.RawFormat);
            Byte[] image = ms.ToArray();
            string des = $"{nom} {postnom} {prenom}";

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Le nom est obligatoire", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (postnom.Trim().Equals(""))
            {
                MessageBox.Show("Le postnom est obligatoire", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (prenom.Trim().Equals(""))
            {
                MessageBox.Show("Le prenom est obligatoire", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (matricule.Trim().Equals(""))
            {
                MessageBox.Show("Le numero d'identite national est obligatoire", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (PictureBoxaddIncuber.Image == Image.FromFile("../../Resources/logo_eclosionhub.png"))
            {
                MessageBox.Show("Veillez choisir une image pour incube", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (incubes.AjouterIncube(nom, postnom, prenom, matricule, image, tel))
                {
                    MessageBox.Show("Incube Ajouter avec Succes", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    QRCoder.QRCodeGenerator QR = new QRCoder.QRCodeGenerator();
                    var Mydata = QR.CreateQrCode(des, QRCoder.QRCodeGenerator.ECCLevel.H);
                    var code = new QRCoder.QRCode(Mydata);
                    pictureBoxCodeQr.Image = code.GetGraphic(200);

                    MemoryStream mt = new MemoryStream();
                    pictureBoxCodeQr.Image.Save(mt, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Byte[] Photo = new byte[mt.Length];
                    mt.Position = 0;
                    mt.Read(Photo, 0, Photo.Length);

                    int id_incube = 0;
                    DataTable sortie = incubes.TopdernierDesignationtIncubes();

                    if (sortie.Rows.Count > 0)
                    {
                        id_incube = Convert.ToInt32(sortie.Rows[0][0]);

                    }

                    if (qR.AjouterCodeQR(id_incube, Photo))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Echec d'ajout code QR", "Ajout CODE QR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    actualiser();

                }
                else
                {
                    MessageBox.Show("Echec d'ajout incube", "Ajout incube", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridViewIncubes_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            PanelModifier.BringToFront();
            int id = Convert.ToInt32(DataGridViewIncubes.CurrentRow.Cells[0].Value);

            DataTable table = incubes.getIncubebyid(id);


            if (table.Rows.Count > 0)
            {
                Initialiserdonnee(table);
            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void gunaButton2_Click_1(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxid.Text);
            string nom = TextBoxPostNomModifier.Text;
            string postnom = TextBoxPostNomModifier.Text;
            string prenom = TextBoxPrenomModifier.Text;
            string matricule = TextBoxMatriculeModifier.Text;
            string tel = TextBoxTelephone.Text;
            MemoryStream ms = new MemoryStream();
            PictureBoxIncubeModifier.Image.Save(ms, PictureBoxaddIncuber.Image.RawFormat);
            Byte[] image = ms.ToArray();

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Le nom est obligatoire", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (postnom.Trim().Equals(""))
            {
                MessageBox.Show("Le postnom est obligatoire", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (prenom.Trim().Equals(""))
            {
                MessageBox.Show("Le prenom est obligatoire", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (matricule.Trim().Equals(""))
            {
                MessageBox.Show("Le numero d'identite national est obligatoire", "Ajout Incube", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (incubes.ModifierIncube(id, nom, postnom, prenom, matricule, image, tel))
                {
                    MessageBox.Show("Incube Modifier avec Succes", "Modifier Incube", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    actualiser();

                }
                else
                {
                    MessageBox.Show("Echec de modification de l'incube", "Modifier incube", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
