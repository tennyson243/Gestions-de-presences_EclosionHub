using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Speech.Synthesis;
using System.Windows.Forms;
using ZXing;

namespace EclosionHub.Live
{
    public partial class LiveMain : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        BDD.Connecteur connexion = new BDD.Connecteur();
        Classes.Presence presence = new Classes.Presence();
        SpeechSynthesizer voice;
        int id_module;
        public LiveMain(int id)
        {
            InitializeComponent();
            this.id_module = id;
            connexion.getconnexion();
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            PictureBoxCheck.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void LiveMain_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
            {
                ComboBoxDevice.Items.Add(Device.Name);
            }
            ComboBoxDevice.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();

            DataTable afficher = presence.AfficherPresence(id_module);
            if (afficher.Rows.Count > 0)
            {
                DataGridViewLive.DataSource = afficher;
            }

            voice = new SpeechSynthesizer();
        }

        public void Initialiserdonnee(DataTable data)
        {

            LabelNomIncube.Text = data.Rows[0][1].ToString();
            LabelProjetincube.Text = data.Rows[0][3].ToString();
            LabelHeureArrive.Text = data.Rows[0][4].ToString();
            LabelHeureDepart.Text = data.Rows[0][5].ToString();

            try
            {
                byte[] couverture = (byte[])data.Rows[0][2];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                PictureBoxIncuber.Image = Image.FromStream(ms);
            }
            catch
            {

            }

            DataTable afficher = presence.AfficherPresence(id_module);
            if (afficher.Rows.Count > 0)
            {
                DataGridViewLive.DataSource = afficher;
            }

        }
        private void LiveMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FinalFrame.IsRunning == true)
                FinalFrame.Stop();
        }

        private void ButtonLiveDash_Click_1(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[ComboBoxDevice.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (PictureBoxCheck.Image != null)
                {
                    BarcodeReader reader = new BarcodeReader();
                    Result result = reader.Decode((Bitmap)PictureBoxCheck.Image);

                    if (result != null)
                    {
                        string decode = result.ToString().Trim();
                        string nom = decode;

                        DataTable table1 = new DataTable();

                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        using (SqlCommand cmd = new SqlCommand("select id from Incubes where CONCAT(Incubes.Nom,' ',Incubes.Postnom,' ',Incubes.Prenom)=@Nom", connexion.getconnexion()))
                        {
                            cmd.Parameters.Add("@Nom", SqlDbType.VarChar).Value = nom;

                            adapter.SelectCommand = cmd;
                            adapter.Fill(table1);

                            if (table1.Rows.Count > 0)
                            {
                                int idIncube = Convert.ToInt32(table1.Rows[0][0]);
                                if (decode != null)
                                {
                                    DataTable pre = presence.CheckPresence(idIncube, id_module);

                                    if (pre.Rows.Count > 0)
                                    {
                                        string preset = (pre.Rows[0]["Present"] == DBNull.Value) ? string.Empty : Convert.ToString(pre.Rows[0]["Present"]);

                                        if (String.Equals(preset, "Oui", StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (presence.ModifierHeureDepart(idIncube, id_module))
                                            {
                                                DataTable inc = presence.GetLive(nom);

                                                if (inc.Rows.Count > 0)
                                                {
                                                    Initialiserdonnee(inc);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Echec d'initialisation des donnees", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Echec de Modifcation de l'heure de depart", "Heure de Sortie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            if (presence.ModifierHeureArrive(idIncube, id_module))
                                            {
                                                DataTable inc = presence.GetLive(nom);

                                                if (inc.Rows.Count > 0)
                                                {
                                                    Initialiserdonnee(inc);
                                                    voice.SelectVoiceByHints(VoiceGender.Female);
                                                    string projet = Convert.ToString(inc.Rows[0][3].ToString());
                                                    voice.SpeakAsync(projet);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Echec d'initialisation des donnees", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Echec de Modifcation de l'heure d'arriver", "Heure d'arriver", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
