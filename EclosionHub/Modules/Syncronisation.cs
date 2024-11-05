using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Modules
{
    public partial class Syncronisation : Form
    {
        int idmodule;
        public Syncronisation(int id)
        {
            InitializeComponent();
            this.idmodule = id;
        }


        Classes.RequetteRapport requette = new Classes.RequetteRapport();
        Classes.Module module = new Classes.Module();
        Classes.Presence presence = new Classes.Presence();
        BDD.Connecteur connexion = new BDD.Connecteur();
       private void Syncronisation_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = connexion.getconnexion())
            {
                connexion.Openconnection();
                string query = @"
                WITH Dates AS (
                    SELECT
                        Id AS ModuleId,
                        Nom AS NomModule,
                        Date_debut AS SessionDate,
                        DATEDIFF(DAY, Date_debut, Date_fin) + 1 AS SessionDuration
                    FROM
                        dbo.Modules
                )
                SELECT
                    ModuleId,
                    NomModule,
                    'Jour ' + CAST(ROW_NUMBER() OVER (PARTITION BY ModuleId ORDER BY SessionDate) AS NVARCHAR(MAX)) AS J_Session,
                    CONVERT(VARCHAR, DATEADD(DAY, RN - 1, SessionDate), 103) AS Date_Seance
                FROM
                    Dates
                CROSS APPLY (
                    SELECT TOP (SessionDuration) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RN
                    FROM master.dbo.spt_values
                ) AS Numbers
                WHERE ModuleId = @idmodule
                ORDER BY
                    ModuleId, SessionDate, J_Session";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@idmodule", idmodule);

                    DataTable sessionsTable = new DataTable();

                    // Remplacez "requette" par votre objet de requête ou command
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        sessionsTable.Load(dr);

                        // Assurez-vous de remplacer "comboBoxSessionModule" par le nom correct de votre ComboBox
                        comboBoxSessionModule.Items.Clear();

                        foreach (DataRow row in sessionsTable.Rows)
                        {
                            // Assurez-vous que l'index 2 existe dans votre résultat de requête
                            // Vous devrez peut-être ajuster cela en fonction de votre schéma de base de données
                            string sessionValue = row.IsNull("J_Session") ? string.Empty : row["J_Session"].ToString();
                            comboBoxSessionModule.Items.Add(sessionValue);
                        }
                    }
                    else
                    {
                        // Gérer le cas où aucune session n'est récupérée
                        // Peut-être afficher un message à l'utilisateur, désactiver des fonctionnalités, etc.
                    }

                    dr.Close(); // N'oubliez pas de fermer le DataReader
                }
                connexion.closeconnection();
            }
            DataTable table = requette.GetSessionJourParModule(idmodule);
            if(table.Rows.Count>0)
            {
                TextBoxID.Text = table.Rows[0][0].ToString();
                LabelNomModule.Text= table.Rows[0][1].ToString();
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if(RadioButtonTousLesIncubes.Checked && comboBoxSessionModule.SelectedItem != null)
            {
                string pre = "Oui";
                TimeSpan heureArrive = TimeSpan.Zero;
                TimeSpan heureSortie = TimeSpan.Zero;
                DateTime dateSession = DateTime.MinValue;

                // Récupérer les données de session
                DataTable sessionTable = requette.GetSessionJourParModule(idmodule);
                string jourSession = comboBoxSessionModule.SelectedItem.ToString();

                if (sessionTable.Rows.Count > 0)
                {
                    heureArrive = sessionTable.Rows[0].Field<TimeSpan?>("Heure_debut") ?? TimeSpan.Zero;
                    heureSortie = sessionTable.Rows[0].Field<TimeSpan?>("Heure_fin") ?? TimeSpan.Zero;
                }

                // Récupérer la date de session
                DataTable dateTable = requette.GetDateSessionJourParModule(idmodule, jourSession);

                if (dateTable.Rows.Count > 0 && dateTable.Columns.Contains("Date_Seance"))
                {
                    if (dateTable.Rows[0]["Date_Seance"] != DBNull.Value)
                    {
                        dateSession = DateTime.ParseExact(
                            dateTable.Rows[0].Field<string>("Date_Seance"),
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None
                        );
                    }
                }

                System.Data.DataTable tebla = requette.CheckModuleASyncroniser(idmodule, dateSession);
                if (tebla.Rows.Count > 0)
                {
                    MessageBox.Show("Cette section de cette module a déjà été enregistrée. Si vous souhaitez modifier quelque chose dans cette section, veuillez vous rendre dans la partie gestion de présence.", "Synchronisation des incubes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    BDD.Connecteur connecteur = new BDD.Connecteur();

                    if (connecteur.Openconnection())
                    {
                        using (SqlCommand command = new SqlCommand("SELECT Id FROM Incubes", connecteur.getconnexion()))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int incubeId = reader.GetInt32(0);

                                    if (requette.AjouterPresenceSyncroniser(incubeId, idmodule, pre, heureArrive, heureSortie, dateSession))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("Échec de la synchronisation", "Synchronisation des incubes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }

                                }
                            }
                            MessageBox.Show("Synchronisation effectuée avec succès", "Synchronisation des incubes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        connecteur.closeconnection();
                    }
                    else
                    {
                        MessageBox.Show("La connexion à la base de données n'est pas ouverte.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if(RadioButtonTousSauf.Checked && comboBoxSessionModule.SelectedItem != null)
            {
                // Assurez-vous d'appeler ceci avant d'accéder aux valeurs de la colonne "ID".

                // Récupérer la liste des incubes dans la DataGridViewPanier
                List<int> incubesDansPanier = new List<int>();
                if (DataGridViewPanier.Rows.Count > 0)
                {
                    int idColumnIndex = 0; // Remplacez 0 par l'index correct de votre colonne "Id"

                    foreach (DataGridViewRow row in DataGridViewPanier.Rows)
                    {
                        if (row.Cells[idColumnIndex].Value != null && int.TryParse(row.Cells[idColumnIndex].Value.ToString(), out int incubId))
                        {
                            incubesDansPanier.Add(incubId);
                        }
                    }

                    DataTable sessionTable = requette.GetSessionJourParModule(idmodule);
                    string jourSession = comboBoxSessionModule.SelectedItem.ToString();
                    DataTable dateTable = requette.GetDateSessionJourParModule(idmodule, jourSession);
                    DateTime dateSession = DateTime.MinValue;
                    TimeSpan arriver = TimeSpan.Zero;
                    TimeSpan sortrie = TimeSpan.Zero;

                    if (sessionTable.Rows.Count > 0)
                    {
                        arriver = sessionTable.Rows[0].Field<TimeSpan?>("Heure_debut") ?? TimeSpan.Zero;
                        sortrie = sessionTable.Rows[0].Field<TimeSpan?>("Heure_fin") ?? TimeSpan.Zero;
                    }

                    if (dateTable.Rows.Count > 0 && dateTable.Columns.Contains("Date_Seance"))
                    {
                        if (dateTable.Rows[0]["Date_Seance"] != DBNull.Value)
                        {
                            dateSession = DateTime.ParseExact(
                                dateTable.Rows[0].Field<string>("Date_Seance"),
                                "dd/MM/yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None
                            );
                        }
                    }

                    // Récupérer tous les incubes de la base de données
                    DataTable tousIncubes = requette.GetTousIncubes();

                    // Préparer la requête SQL d'insertion
                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append("INSERT INTO Presence (Incube, module, present, heure_arrive, heure_sortie, Date_Scan) VALUES ");

                    // Ajouter les valeurs pour chaque incubé
                    foreach (DataRow row in tousIncubes.Rows)
                    {
                        int incubeId = Convert.ToInt32(row["id"]);
                        string pre = incubesDansPanier.Contains(incubeId) ? "Non" : "Oui";
                        TimeSpan heureArrive = incubesDansPanier.Contains(incubeId) ? TimeSpan.Zero : arriver;
                        TimeSpan heureSortie = incubesDansPanier.Contains(incubeId) ? TimeSpan.Zero : sortrie;

                        // Ajouter les valeurs à la requête SQL avec des paramètres
                        queryBuilder.AppendFormat("(@Incube{0}, @Module{0}, @Present{0}, @HeureArrive{0}, @HeureSortie{0}, @DateSession{0}), ", incubeId);
                    }

                    // Supprimer la virgule en trop à la fin de la requête
                    queryBuilder.Length -= 2;

                    // Exécuter la requête SQL avec des paramètres
                    foreach (DataRow row in tousIncubes.Rows)
                    {
                        int incubeId = Convert.ToInt32(row["id"]);
                        string pre = incubesDansPanier.Contains(incubeId) ? "Non" : "Oui";
                        TimeSpan heureArrive = incubesDansPanier.Contains(incubeId) ? TimeSpan.Zero : arriver;
                        TimeSpan heureSortie = incubesDansPanier.Contains(incubeId) ? TimeSpan.Zero : sortrie;

                        // Ajouter les paramètres

                        if (requette.AjouterPresenceSyncroniser(incubeId, idmodule, pre, heureArrive, heureSortie, dateSession))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Échec de la synchronisation", "Synchronisation des incubes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // Afficher un message de succès
                    MessageBox.Show("Synchronisation effectuée avec succès pour tous les incubes, avec des absences pour ceux dans le panier.", "Synchronisation des incubes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une option de synchronisation et spécifier le jour de la session.", "Synchronisation des incubes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool colonnesPanierInitialisees = false;

        public void InitialiserColonnesPanier()
        {
            // Vérifie si les colonnes ont déjà été initialisées
            if (!colonnesPanierInitialisees)
            {
                // Colonnes pour le panier
                DataGridViewTextBoxColumn colonneIDIncubes = new DataGridViewTextBoxColumn
                {
                    HeaderText = "Id"
                };

                DataGridViewTextBoxColumn colonneNomIncubes = new DataGridViewTextBoxColumn
                {
                    HeaderText = "Incubes"
                };

                DataGridViewTextBoxColumn colonneProjetIncubes = new DataGridViewTextBoxColumn
                {
                    HeaderText = "Projets"
                };

                // Ajout des colonnes à la DataGridView
                DataGridViewPanier.Columns.AddRange(colonneIDIncubes, colonneNomIncubes, colonneProjetIncubes);

                // Marque les colonnes comme initialisées
                colonnesPanierInitialisees = true;
            }
        }

        private void RadioButtonTousSauf_CheckedChanged(object sender, EventArgs e)
        {
            // Initialisation des colonnes du panier (ne sera exécuté qu'une fois)
            InitialiserColonnesPanier();

            // Chargement des entreprises dans la ComboBox
            Classes.Projet entreprise = new Classes.Projet();
            ComboBoxIncubes.DataSource = entreprise.listEntreprise();
            ComboBoxIncubes.DisplayMember = "Nom";
            ComboBoxIncubes.ValueMember = "id";

            // Réinitialisation de la sélection et activation de la ComboBox et de la DataGridView
            ComboBoxIncubes.SelectedItem = null;
            ComboBoxIncubes.Enabled = true;
            DataGridViewPanier.Enabled = true;

            labelModuleJour.Text = "Vous êtes sur le point de synchroniser tous les incubes, sauf ceux dans le panier. Les incubes dans le panier auront seront absent pour le jour du session selectionner";
        }

        private void ComboBoxIncubes_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (ComboBoxIncubes.SelectedItem != null)
            {
                DataRowView selectedModule = (DataRowView)ComboBoxIncubes.SelectedItem;

                // Récupérer la valeur de l'ID à partir de l'objet sélectionné
                string nomProjet = Convert.ToString(selectedModule["Nom"]);

                // Utiliser le nom du projet pour obtenir les données du module
                System.Data.DataTable bleta = requette.GetIncubesEtProjetParProjet(nomProjet);

                if (bleta.Rows.Count > 0)
                {
                    int id = Convert.ToInt32(bleta.Rows[0][0]);
                    string incu = bleta.Rows[0][1].ToString();
                    string proje = bleta.Rows[0][2].ToString();
                    DataGridViewPanier.Rows.Add(id,incu, proje);
                }
            }
            else
            {
                for (int j = DataGridViewPanier.Rows.Count - 1; j >= 0; j--)
                {
                    DataGridViewRow removerows = DataGridViewPanier.Rows[j];
                    DataGridViewPanier.Rows.Remove(removerows);
                }
            }
        }

        private void DataGridViewPanier_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous supprimez ce projet de la Liste des incubes a exclures??", "Liste", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowindex = DataGridViewPanier.CurrentCell.RowIndex;
                DataGridViewPanier.Rows.RemoveAt(rowindex);
            }
        }

        private void bunifuVScrollBar2_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewPanier.FirstDisplayedScrollingRowIndex = DataGridViewPanier.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewPanier_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewPanier.RowCount + 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewPanier_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewPanier.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void ButtonRafrechir_Click(object sender, EventArgs e)
        {
            for (int j = DataGridViewPanier.Rows.Count - 1; j >= 0; j--)
            {
                DataGridViewRow removerows = DataGridViewPanier.Rows[j];
                DataGridViewPanier.Rows.Remove(removerows);
            }
        }

        private void RadioButtonTousLesIncubes_CheckedChanged(object sender, EventArgs e)
        {
            // Parcourir les lignes de la DataGridView en sens inverse
            for (int j = DataGridViewPanier.Rows.Count - 1; j >= 0; j--)
            {
                DataGridViewRow removerows = DataGridViewPanier.Rows[j];
                DataGridViewPanier.Rows.Remove(removerows);
            }

            // Réinitialiser la sélection et désactiver la ComboBox et la DataGridView
            ComboBoxIncubes.SelectedItem = null;
            ComboBoxIncubes.Enabled = false;
            DataGridViewPanier.Enabled = false;
            labelModuleJour.Text = "Vous êtes sur le point de synchroniser tous les incubes à ce module. Tous les incubes auront 100% de satisfaction de participation à ce module et seront présents pour le jour de cette session.";
        }

    }
}
