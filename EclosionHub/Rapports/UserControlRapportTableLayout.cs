using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace EclosionHub.Rapports
{
    public partial class UserControlRapportTableLayout : UserControl
    {
        private UserControlObservationsIncubes observationsIncubes;
        private UserControlRapportPresence rapportPresence;
        private RapportParticipationParModule RapportParticipation;
        private UserControlRapportParSessionModule RapportParSessionModule;
        private Classes.Module module;
        private Classes.Incubes incubes;
        private Classes.Projet entreprise;
        public UserControlRapportTableLayout()
        {
            InitializeComponent();
            observationsIncubes = new UserControlObservationsIncubes();
            module = new Classes.Module();
            incubes = new Classes.Incubes();
            entreprise = new Classes.Projet();
        }

        private void  InitializeRapportObservationIncubeAsync()
        {
            if (tabPageObservationIncube == null)
            {
                // Log ou gérer le cas où les éléments nécessaires ne sont pas initialisés
                return;
            }

            // Initialisation des classes
            observationsIncubes = new UserControlObservationsIncubes();
            tabPageObservationIncube.SuspendLayout();
            tabPageObservationIncube.Controls.Clear();

            // Configuration des ComboBox
            ConfigureComboBox(observationsIncubes.ComboBoxTrierModule, module.listModulesEnCoursOuTerminer(), "Nom", "id");

            // Récupération des données depuis la base de données
            using (EclosionHubEntities db = new EclosionHubEntities())
            {
                // Utilisez Invoke pour effectuer des opérations d'interface utilisateur sur le thread principal
                observationsIncubes.DataGridViewTrier.DataSource = db.VueObservationsIncubes.ToList();
                DisableComboBoxes(observationsIncubes.ComboBoxTrierModule);
                ClearComboBoxSelections(observationsIncubes.ComboBoxTrierModule);
                observationsIncubes.DataGridViewTrier.ClearSelection();
            }

            observationsIncubes.Dock = DockStyle.Fill;

            // Ajouter rapportPresence à tabPageRapportTrier
            tabPageObservationIncube.Controls.Add(observationsIncubes);

            // Reprendre le layout
            tabPageObservationIncube.ResumeLayout();
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
            if (tabPageRapportTrier == null)
            {
                // Log ou gérer le cas où les éléments nécessaires ne sont pas initialisés
                return;
            }
            // Suspendre le layout pour améliorer les performances lors de l'ajout de contrôles
          
            tabPageRapportTrier.SuspendLayout();
            tabPageRapportTrier.Controls.Clear();

            // Configuration des ComboBox
            rapportPresence = new UserControlRapportPresence();
            ConfigureComboBox(rapportPresence.ComboBoxTrierIncubes, incubes.listIncubes(), "Nom", "id");
            ConfigureComboBox(rapportPresence.ComboboxTrierProjet, entreprise.listEntreprise(), "Nom", "id");
            ConfigureComboBox(rapportPresence.ComboboxTrierModule, module.listModulesEnCoursOuTerminer(), "Nom", "id");

            // Récupération des données depuis la base de données
            using (var db = new EclosionHubEntities())
            {
                var listProjetConcs = await Task.Run(() => db.RapportPresenceParModules.ToListAsync());

                    rapportPresence.DataGridViewTrier.DataSource = listProjetConcs;
                    DisableComboBoxes(rapportPresence.ComboBoxTrierIncubes, rapportPresence.ComboboxTrierProjet, rapportPresence.ComboboxTrierModule);
                    ClearComboBoxSelections(rapportPresence.ComboBoxTrierIncubes, rapportPresence.ComboboxTrierProjet, rapportPresence.ComboboxTrierModule);
                    rapportPresence.DataGridViewTrier.ClearSelection();
          

            }

            rapportPresence.Dock = DockStyle.Fill;
            // Ajouter rapportPresence à tabPageRapportTrier
            tabPageRapportTrier.Controls.Add(rapportPresence);
            // Reprendre le layout
            tabPageRapportTrier.ResumeLayout();

        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            if (Observations.TabPages.Contains(tabPageObservationIncube))
            {
                Observations.SelectedTab = tabPageObservationIncube;
            }
            InitializeRapportObservationIncubeAsync();
        }
        private void UserControlRapportTableLayout_Load(object sender, EventArgs e)
        {

        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            if (Observations.TabPages.Contains(tabPageRapportTrier))
            {
                Observations.SelectedTab = tabPageRapportTrier;
            }
            _ = InitializeRapportDePresenceAsync();
        }

        private async Task InitializeRapportDeParticipationParModuleAsync()
        {
            if (tabPageParticipationModule == null)
            {
                // Log ou gérer le cas où les éléments nécessaires ne sont pas initialisés
                return;
            }
            // Suspendre le layout pour améliorer les performances lors de l'ajout de contrôles

            tabPageParticipationModule.SuspendLayout();
            tabPageParticipationModule.Controls.Clear();

            // Configuration des ComboBox
            RapportParticipation = new RapportParticipationParModule();
            ConfigureComboBox(RapportParticipation.ComboBoxTrierModule, module.listModulesEnCoursOuTerminer(), "Nom", "id");

            // Récupération des données depuis la base de données
            using (var db = new EclosionHubEntities())
            {
                var listProjetConcs = await Task.Run(() => db.RapportParticipationTotalParModules.ToListAsync());

                RapportParticipation.DataGridViewTrier.DataSource = listProjetConcs;
                DisableComboBoxes(RapportParticipation.ComboBoxTrierModule);
                ClearComboBoxSelections(RapportParticipation.ComboBoxTrierModule);
                RapportParticipation.DataGridViewTrier.ClearSelection();


            }

            RapportParticipation.Dock = DockStyle.Fill;
            // Ajouter rapportPresence à tabPageRapportTrier
            tabPageParticipationModule.Controls.Add(RapportParticipation);
            // Reprendre le layout
            tabPageParticipationModule.ResumeLayout();

        }

        private void gunaGradientButton3_Click(object sender, EventArgs e)
        {

            if (Observations.TabPages.Contains(tabPageParticipationModule))
            {
                Observations.SelectedTab = tabPageParticipationModule;
            }

            _ = InitializeRapportDeParticipationParModuleAsync();
        }

        private async Task InitializeRapportDeSessionParModuleAsync()
        {
            if (tabPageSessionModule == null)
            {
                // Log ou gérer le cas où les éléments nécessaires ne sont pas initialisés
                return;
            }
            // Suspendre le layout pour améliorer les performances lors de l'ajout de contrôles

            tabPageSessionModule.SuspendLayout();
            tabPageSessionModule.Controls.Clear();

            // Configuration des ComboBox
            RapportParSessionModule = new UserControlRapportParSessionModule();
            ConfigureComboBox(RapportParSessionModule.ComboBoxTrierModule, module.listModulesEnCoursOuTerminer(), "Nom", "id");

            // Récupération des données depuis la base de données
            using (var db = new EclosionHubEntities())
            {
                var listProjetConcs = await Task.Run(() => db.RapportPresenceParSessionParModules.ToListAsync());

                RapportParSessionModule.DataGridViewTrier.DataSource = listProjetConcs;
                DisableComboBoxes(RapportParSessionModule.ComboBoxTrierModule);
                ClearComboBoxSelections(RapportParSessionModule.ComboBoxTrierModule);
                RapportParSessionModule.DataGridViewTrier.ClearSelection();


            }

            RapportParSessionModule.Dock = DockStyle.Fill;
            // Ajouter rapportPresence à tabPageRapportTrier
            tabPageSessionModule.Controls.Add(RapportParSessionModule);
            // Reprendre le layout
            tabPageSessionModule.ResumeLayout();

        }
        private void gunaGradientButton4_Click(object sender, EventArgs e)
        {
            if (Observations.TabPages.Contains(tabPageSessionModule))
            {
                Observations.SelectedTab = tabPageSessionModule;
            }

            _ = InitializeRapportDeSessionParModuleAsync();
        }
    }
}
