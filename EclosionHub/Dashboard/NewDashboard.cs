using EclosionHub.incubes;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.SqlClient;

namespace EclosionHub.Dashboard
{
    public partial class NewDashboard : Form
    {
        Classes.Incubes incu = new Classes.Incubes();
        Classes.RequetteRapport requetteRapport = new Classes.RequetteRapport();
        Classes.Module mdl = new Classes.Module();
        private Rapports.UserControlRapportTableLayout userControlRapport;
        private Rapports.UserControlRapportPresence rapportPresence;
        private Rapports.UserControlRapportTableLayout userLayout;
        private UserControlPresencecs controlPresencecs;
        private Rapports.UserControlObservationEligibiliteBourse eligibiliteBourse;
        BDD.Connecteur connecteur = new BDD.Connecteur();
        Classes.Presence presence = new Classes.Presence();
        public NewDashboard()
        {
            InitializeComponent();
            userLayout = new Rapports.UserControlRapportTableLayout();
            userControlRapport = new Rapports.UserControlRapportTableLayout();
            controlPresencecs = new UserControlPresencecs();
            eligibiliteBourse = new Rapports.UserControlObservationEligibiliteBourse();
        }

        private void moveImageBox(object sender)
        {
            Guna2Button b = (Guna2Button)sender;
            imgSlide.Location = new Point(b.Location.X + 118, b.Location.Y - 30);
            imgSlide.SendToBack();
        }

        private bool liveMainLaunched = false;
        private async void IniatialiserUsercontroleAsync()
        {

            flowLayoutPanelStock.SuspendLayout();
            flowLayoutPanelStock.Controls.Clear();

            DataTable table = await Task.Run(() => incu.LireLesitems());

            if (table != null && table.Rows.Count > 0)
            {
                UserControlIncubes[] listItems = new UserControlIncubes[table.Rows.Count];

                foreach (DataRow row in table.Rows)
                {
                    UserControlIncubes item = new UserControlIncubes();
                    item.LabelNomIncube.Text = row["Nom_Incube"].ToString();
                    item.LabelPostNomIncube.Text = row["postnom"].ToString();
                    item.LabelPrenomIncube.Text = row["prenom"].ToString();
                    item.LabelProjetIncube.Text = row["projet"].ToString();
                    item.LabelParticipation.Text = row["P_Participation_Total"].ToString();

                    byte[] couverture = (byte[])row["Photo"];
                    using (MemoryStream ms = new MemoryStream(couverture))
                    {
                        item.PictureBoxIncube.Image = Image.FromStream(ms);
                    }

                    item.OnSelected += (ss, ee) =>
                    {
                        UserControlIncubes controle = (UserControlIncubes)ss;
                        int id = Convert.ToInt32(row["Id"]);
                        incubes.IncubeMoreDetails moreDetails = new IncubeMoreDetails(id);
                        moreDetails.Show();
                        // GRID.Rows.Add(new object[] { row["Nature"].ToString(), controle.NUDQ.Value });
                        // calculeTotal();
                    };

                    flowLayoutPanelStock.Controls.Add(item);
                }
            }

            flowLayoutPanelStock.ResumeLayout();
            checklive();


        }

        public void checklive()
        {
            DataTable bleta = requetteRapport.GetRapportParticipationTousModule();
            if (bleta.Rows.Count > 0)
            {
                string mod = bleta.Rows[0][1].ToString();
                string jour = bleta.Rows[0][2].ToString();
                labelModuleJour.Text = $"{jour} du module {mod}";
            }
            else
            {
                labelModuleJour.Text = "Aucune Session d'aucun Module n'est Disponible Aujourd'hui";
                ButtonLiveDash.Enabled = false;
            }
        }
        private async Task IniatialiserProjetAsync()
        {
            tabPageProjet.SuspendLayout();
            tabPageProjet.Controls.Clear();

            Classes.Projet entreprise = new Classes.Projet();
            UserControlProjet controlProjet = new UserControlProjet();

            using (var db = new EclosionHubEntities())
            {
                var listProjetConcs = await db.VueListProjetConcs.ToListAsync();

                controlProjet.DataGridViewTrier.DataSource = listProjetConcs;
                controlProjet.ComboBoxTrierModule.Enabled = false;
                controlProjet.ComboBoxTrierModule.SelectedItem = null;
                controlProjet.DataGridViewTrier.ClearSelection();
            }
            controlProjet.Dock = DockStyle.Fill;

            // Ajouter controlProjet à tabPageProjet
            tabPageProjet.Controls.Add(controlProjet);

            tabPageProjet.ResumeLayout();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            _ = IniatialiserProjetAsync();
        }

        private void NewDashboard_Load(object sender, EventArgs e)
        {
            IniatialiserUsercontroleAsync();
            CheckStatutModule();
        }

        // Autres méthodes restantes inchangées...


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            moveImageBox(sender);
            Pages.SelectedIndex = 0;
        }

        private void guna2Button2_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
            Pages.SelectedIndex = 1;

        }

        private void guna2Button3_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
            Pages.SelectedIndex = 2;

        }

        private void guna2Button4_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
            Pages.SelectedIndex = 3;

        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            moveImageBox(sender);
            _ = IniatialiserPresencetAsync();
            Pages.SelectedIndex = 4;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            incubes.mainIncube mainin = new mainIncube();
            mainin.Show();
        }

        private async Task IniatialiserModulesAsync()
        {
            tabPageModules.SuspendLayout();
            tabPageModules.Controls.Clear();

            UserControlModule controlModule = new UserControlModule();

            using (var db = new EclosionHubEntities())
            {
                var listModuleConcs = await db.VueModuleConcs.ToListAsync();
                controlModule.DataGridViewTrier.DataSource = listModuleConcs;
                controlModule.ComboBoxTrierModule.Enabled = false;
                controlModule.ComboBoxTrierModule.SelectedItem = null;
                controlModule.DataGridViewTrier.ClearSelection();

            }
            controlModule.Dock = DockStyle.Fill;

            // Ajouter controlProjet à tabPageProjet
            tabPageModules.Controls.Add(controlModule);

            tabPageModules.ResumeLayout();
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            _ = IniatialiserModulesAsync();
        }

        private async Task InitializeRapportDePresenceAsync()
        {
            if (userLayout == null || userLayout.tabPageRapportTrier == null)
            {
                // Log ou gérer le cas où les éléments nécessaires ne sont pas initialisés
                return;
            }
            // Suspendre le layout pour améliorer les performances lors de l'ajout de contrôles
            userLayout.tabPageRapportTrier.SuspendLayout();
            userLayout.tabPageRapportTrier.Controls.Clear();
            tabPageRapport.SuspendLayout();
            tabPageRapport.Controls.Clear();

            // Initialisation des classes
            Classes.Module module = new Classes.Module();
            Classes.Incubes incubes = new Classes.Incubes();
            Classes.Projet entreprise = new Classes.Projet();

            // Configuration des ComboBox
            rapportPresence = new Rapports.UserControlRapportPresence();
            ConfigureComboBox(rapportPresence.ComboBoxTrierIncubes, incubes.listIncubes(), "Nom", "id");
            ConfigureComboBox(rapportPresence.ComboboxTrierProjet, entreprise.listEntreprise(), "Nom", "id");
            ConfigureComboBox(rapportPresence.ComboboxTrierModule, module.listModulesEnCoursOuTerminer(), "Nom", "id");

            // Récupération des données depuis la base de données
            using (var db = new EclosionHubEntities())
            {
                var listProjetConcs = await db.RapportPresenceParModules.ToListAsync();

                rapportPresence.DataGridViewTrier.DataSource = listProjetConcs;
                DisableComboBoxes(rapportPresence.ComboBoxTrierIncubes, rapportPresence.ComboboxTrierProjet, rapportPresence.ComboboxTrierModule);
                ClearComboBoxSelections(rapportPresence.ComboBoxTrierIncubes, rapportPresence.ComboboxTrierProjet, rapportPresence.ComboboxTrierModule);
                rapportPresence.DataGridViewTrier.ClearSelection();
            }

            rapportPresence.Dock = DockStyle.Fill;

            // Ajouter rapportPresence à tabPageRapportTrier
            userLayout.tabPageRapportTrier.Controls.Add(rapportPresence);

            // Reprendre le layout
            userLayout.tabPageRapportTrier.ResumeLayout();

            // Positionne en haut
            tabPageRapport.Controls.Add(userLayout);
            // Création du userControlRapport
            userLayout.Dock = DockStyle.Fill; // Remplit le centre

            tabPageRapport.ResumeLayout();
        }

        private void ConfigureComboBox(ComboBox comboBox, object dataSource, string displayMember, string valueMember)
        {
            comboBox.DataSource = dataSource;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
        }

        private void ConfigureComboBoxDisplayMember(ComboBox comboBox, object dataSource, string displayMember)
        {
            comboBox.DataSource = dataSource;
            comboBox.DisplayMember = displayMember;
        }
        private void DisableComboBoxes(params ComboBox[] comboBoxes)
        {
            foreach (var comboBox in comboBoxes)
            {
                comboBox.Enabled = false;
            }
        }

        private void ClearComboBoxSelections(params ComboBox[] comboBoxes)
        {
            foreach (var comboBox in comboBoxes)
            {
                comboBox.SelectedItem = null;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            _ = InitializeRapportDePresenceAsync();
            //if (userLayout != null && this.Controls.Contains(userLayout))
            //{
            //    userLayout.tabPageRapportTrier.SuspendLayout();
            //    userLayout.tabPageRapportTrier.Controls.Clear();

            //    // Reste du code...
            //    tabPageRapport.SuspendLayout();
            //    tabPageRapport.Controls.Clear();

            //    // Création du boutonDeRapport
            //    buttonDeRapport.Dock = DockStyle.Top; // Positionne en haut

            //    // Création du userControlRapport
            //    userControlRapport.Dock = DockStyle.Fill; // Remplit le centre

            //    // Ajout des contrôles à tabPageRapport
            //    tabPageRapport.Controls.Add(buttonDeRapport);
            //    tabPageRapport.Controls.Add(userControlRapport);

            //    // Reprendre le layout
            //    tabPageRapport.ResumeLayout();
            //}
            //else
            //{
            //    // Log ou gérer le cas où userLayout n'est pas initialisé ou ajouté
            //}

        }

        private async Task IniatialiserPresencetAsync()
        {
            tabPageObservation.SuspendLayout();
            tabPageObservation.Controls.Clear();

            Classes.Presence presence = new Classes.Presence();

            ConfigureComboBoxDisplayMember(controlPresencecs.ComboboxDate, presence.GetGroupeDate(), "Date");
            ConfigureComboBoxDisplayMember(controlPresencecs.ComboBoxJustification, presence.GetGroupeJustification(), "Justification");
            ConfigureComboBoxDisplayMember(controlPresencecs.ComboBoxTrierIncube, presence.GetGroupeincubeConcatener(), "Incube");
            ConfigureComboBoxDisplayMember(controlPresencecs.ComboBoxTrierParProjet, presence.GetGroupeincubeConcatener(), "Nom");
            using (var db = new EclosionHubEntities())
            {
                var listProjetConcs = await db.VuePresences.ToListAsync();

                controlPresencecs.DataGridViewTrier.DataSource = listProjetConcs;
                DisableComboBoxes(controlPresencecs.ComboboxDate, controlPresencecs.ComboBoxJustification, controlPresencecs.ComboBoxTrierIncube, controlPresencecs.ComboBoxTrierParProjet);
                ClearComboBoxSelections(controlPresencecs.ComboboxDate, controlPresencecs.ComboBoxJustification, controlPresencecs.ComboBoxTrierIncube, controlPresencecs.ComboBoxTrierParProjet);
                controlPresencecs.DataGridViewTrier.ClearSelection();
            }
            controlPresencecs.Dock = DockStyle.Fill;

            // Ajouter controlProjet à tabPageProjet
            tabPageObservation.Controls.Add(controlPresencecs);

            tabPageObservation.ResumeLayout();
        }

        private void imgSlide_Click(object sender, EventArgs e)
        {

        }

        private void CheckStatutModule()
        {
            DataTable ModuleTerminer = requetteRapport.GetModulesTerminer();
            if(ModuleTerminer.Rows.Count>0)
            {
                foreach (DataRow row in ModuleTerminer.Rows)
                {
                    int moduleId = Convert.ToInt32(row["ModuleId"]);
                    string statut = "Terminé";
                    if (mdl.UpdateModule(moduleId, statut))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Échec de mise a jour Module", "Mise a jour Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
           
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            moveImageBox(sender);
            _ = IniatialiserEligibiliteAsync();
            Pages.SelectedIndex = 5;
        }

        private async Task IniatialiserEligibiliteAsync()
        {
            tabPageEligibilite.SuspendLayout();
            tabPageEligibilite.Controls.Clear();

            Classes.Presence presence = new Classes.Presence();
            Classes.Incubes incubes = new Classes.Incubes();
            Classes.Projet entreprise = new Classes.Projet();

            ConfigureComboBox(eligibiliteBourse.ComboBoxTrierIncubes, incubes.listIncubes(), "Incube","id");
            ConfigureComboBox(eligibiliteBourse.ComboboxTrierProjet, entreprise.listEntreprise(), "Nom","id");
            using (var db = new EclosionHubEntities())
            {
                var listProjetConcs = await db.Eligibilites.ToListAsync();

                eligibiliteBourse.DataGridViewTrier.DataSource = listProjetConcs;
                DisableComboBoxes(eligibiliteBourse.ComboBoxTrierIncubes, eligibiliteBourse.ComboboxTrierProjet);
                ClearComboBoxSelections(eligibiliteBourse.ComboBoxTrierIncubes, eligibiliteBourse.ComboboxTrierProjet);
                eligibiliteBourse.DataGridViewTrier.ClearSelection();
            }
            eligibiliteBourse.Dock = DockStyle.Fill;

            // Ajouter controlProjet à tabPageProjet
            tabPageEligibilite.Controls.Add(eligibiliteBourse);

            tabPageEligibilite.ResumeLayout();
        }

        private void NewDashboard_Shown(object sender, EventArgs e)
        {
            this.Enabled = false;
            Login.Login_Form pn = new Login.Login_Form(this);
            pn.Show();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Login.Controle controle = new Login.Controle();
            controle.Show();
        }

        private void ButtonLiveDash_Click(object sender, EventArgs e)
        {
            DataTable bleta = requetteRapport.GetRapportParticipationTousModule();
            if (bleta.Rows.Count > 0)
            {

                    int moduleId = Convert.ToInt32(bleta.Rows[0][0]);
                    string pre = "Non";
                    TimeSpan heureArrive = TimeSpan.Zero;
                    TimeSpan heureSortie = TimeSpan.Zero;

                    connecteur.Openconnection();

                    using (SqlCommand command = new SqlCommand("SELECT Id FROM Incubes", connecteur.getconnexion()))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int incubeId = reader.GetInt32(0);

                                System.Data.DataTable bletaa = presence.CheckModule(incubeId, moduleId);
                                if (bletaa.Rows.Count > 0)
                                {
                                    DateTime date_scan = Convert.ToDateTime(bletaa.Rows[0][2]);
                                    if (date_scan.Date < DateTime.Today)
                                    {
                                        if (presence.AjouterPresence(incubeId, moduleId, pre, heureArrive, heureSortie))
                                        {
                                            if (!liveMainLaunched)
                                            {
                                                Live.LiveMain live = new Live.LiveMain(moduleId);
                                                live.Show();
                                                liveMainLaunched = true;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Echec de lancement du live", "Lancer le live", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        if (!liveMainLaunched)
                                        {
                                            Live.LiveMain live = new Live.LiveMain(moduleId);
                                            live.Show();
                                            liveMainLaunched = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (presence.AjouterPresence(incubeId, moduleId, pre, heureArrive, heureSortie))
                                    {
                                        if (!liveMainLaunched)
                                        {
                                            Live.LiveMain live = new Live.LiveMain(moduleId);
                                            live.Show();
                                            liveMainLaunched = true;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Echec de lancement du live", "Lancer le live", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }

                    connecteur.closeconnection();
            }
            else
            {
                labelModuleJour.Text = "Aucune Session d'aucun Module n'est Disponible Aujourd'hui";
                ButtonLiveDash.Enabled = false;
            }
        }
    }
}
