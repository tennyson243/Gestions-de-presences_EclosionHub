using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Rapports
{
    public partial class UserControlButtonDeRapport : UserControl
    {
        private UserControlRapportTableLayout userLayout;
        private UserControlObservationsIncubes observationsIncubes;
        private UserControlButtonDeRapport buttonDeRapport;
        private UserControlRapportPresence rapportPresence;
        private Dashboard.NewDashboard dashboard; // Ajout de la déclaration de la variable dashboard
        private Classes.Module module;
        private Classes.Incubes incubes;
        private Classes.Projet entreprise;

        public UserControlButtonDeRapport()
        {
            InitializeComponent();

            // Initialisation des objets
            userLayout = new UserControlRapportTableLayout();
            buttonDeRapport = this;
            observationsIncubes = new UserControlObservationsIncubes();
            module = new Classes.Module();
            incubes = new Classes.Incubes();
            entreprise = new Classes.Projet();
        }

        private Dashboard.NewDashboard GetDashboardInstance()
        {
            if (dashboard == null)
            {
                // Créer une nouvelle instance si elle n'existe pas encore
                dashboard = new Dashboard.NewDashboard();
            }

            return dashboard;
        }

        private async Task InitializeRapportObservationIncubeAsync()
        {
            if (userLayout == null || userLayout.tabPageObservationIncube == null)
            {
                // Log ou gérer le cas où les éléments nécessaires ne sont pas initialisés
                return;
            }
            // Suspendre le layout pour améliorer les performances lors de l'ajout de contrôles
            GetDashboardInstance();
            userLayout.tabPageRapportTrier.SuspendLayout();
            userLayout.tabPageRapportTrier.Controls.Clear();
            dashboard.tabPageRapport.SuspendLayout();
            dashboard.tabPageRapport.Controls.Clear();

            // Initialisation des classes
            observationsIncubes = new UserControlObservationsIncubes();
            userLayout.tabPageObservationIncube.SuspendLayout();
            userLayout.tabPageObservationIncube.Controls.Clear();

            // Configuration des ComboBox
            ConfigureComboBox(observationsIncubes.ComboBoxTrierModule, module.listModulesEnCoursOuTerminer(), "Nom", "id");

            // Récupération des données depuis la base de données
            //using (var db = new EclosionHubEntities())
            //{
            //    var listProjetConcs = await Task.Run(() => db.VueObservationsIncubes.ToListAsync());
            //    // Utilisez Invoke pour effectuer des opérations d'interface utilisateur sur le thread principal
            //    userLayout.BeginInvoke((MethodInvoker)delegate
            //    {
            //        observationsIncubes.DataGridViewTrier.DataSource = listProjetConcs;
            //        DisableComboBoxes(observationsIncubes.ComboBoxTrierModule);
            //        ClearComboBoxSelections(observationsIncubes.ComboBoxTrierModule);
            //        observationsIncubes.DataGridViewTrier.ClearSelection();
            //    });
            //}

            observationsIncubes.Dock = DockStyle.Fill;

            // Ajouter rapportPresence à tabPageRapportTrier
            userLayout.tabPageRapportTrier.Controls.Add(observationsIncubes);

            // Reprendre le layout
            userLayout.tabPageRapportTrier.ResumeLayout();

            GetDashboardInstance(); // Assurez-vous d'obtenir une instance après la suspension du layout

            dashboard.tabPageRapport.Controls.Add(buttonDeRapport);
            buttonDeRapport.Dock = DockStyle.Top; // Positionne en haut
            dashboard.tabPageRapport.Controls.Add(userLayout);
            // Création du userControlRapport
            userLayout.Dock = DockStyle.Fill; // Remplit le centre

            dashboard.tabPageRapport.ResumeLayout();
        }

        private void ConfigureComboBox(ComboBox comboBox, object dataSource, string displayMember, string valueMember)
        {
            comboBox.DataSource = dataSource;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
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

        private async Task InitializeRapportDePresenceAsync()
        {
            if (userLayout == null || userLayout.tabPageRapportTrier == null)
            {
                // Log ou gérer le cas où les éléments nécessaires ne sont pas initialisés
                return;
            }
            // Suspendre le layout pour améliorer les performances lors de l'ajout de contrôles
            GetDashboardInstance();
            userLayout.tabPageRapportTrier.SuspendLayout();
            userLayout.tabPageRapportTrier.Controls.Clear();
            dashboard.tabPageRapport.SuspendLayout();
            dashboard.tabPageRapport.Controls.Clear();

            // Configuration des ComboBox
            rapportPresence = new UserControlRapportPresence();
            ConfigureComboBox(rapportPresence.ComboBoxTrierIncubes, incubes.listIncubes(), "Nom", "id");
            ConfigureComboBox(rapportPresence.ComboboxTrierProjet, entreprise.listEntreprise(), "Nom", "id");
            ConfigureComboBox(rapportPresence.ComboboxTrierModule, module.listModulesEnCoursOuTerminer(), "Nom", "id");

            // Récupération des données depuis la base de données
            using (var db = new EclosionHubEntities())
            {
                var listProjetConcs = await Task.Run(() => db.RapportPresenceParModules.ToListAsync());

                userLayout.BeginInvoke((MethodInvoker)delegate
                {
                    rapportPresence.DataGridViewTrier.DataSource = listProjetConcs;
                    DisableComboBoxes(rapportPresence.ComboBoxTrierIncubes, rapportPresence.ComboboxTrierProjet, rapportPresence.ComboboxTrierModule);
                    ClearComboBoxSelections(rapportPresence.ComboBoxTrierIncubes, rapportPresence.ComboboxTrierProjet, rapportPresence.ComboboxTrierModule);
                    rapportPresence.DataGridViewTrier.ClearSelection();
                });

            }

            rapportPresence.Dock = DockStyle.Fill;

            // Ajouter rapportPresence à tabPageRapportTrier
            userLayout.tabPageRapportTrier.Controls.Add(rapportPresence);

            // Reprendre le layout
            userLayout.tabPageRapportTrier.ResumeLayout();

            GetDashboardInstance(); // Assurez-vous d'obtenir une instance après la suspension du layout

            dashboard.tabPageRapport.Controls.Add(buttonDeRapport);
            buttonDeRapport.Dock = DockStyle.Top; // Positionne en haut
            dashboard.tabPageRapport.Controls.Add(userLayout);
            // Création du userControlRapport
            userLayout.Dock = DockStyle.Fill; // Remplit le centre

            dashboard.tabPageRapport.ResumeLayout();

        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            _ = InitializeRapportDePresenceAsync();
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            _ = InitializeRapportObservationIncubeAsync();
        }
    }
}



