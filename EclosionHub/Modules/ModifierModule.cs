using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Modules
{
    public partial class ModifierModule : Form
    {
        int id;
        public ModifierModule(int identifiant)
        {
            InitializeComponent();
            this.id = identifiant;
        }
        Classes.Module module = new Classes.Module();
        private void ModifierModule_Load(object sender, EventArgs e)
        {

            DataTable bleta = module.getModulesbyid(id);

            if (bleta.Rows.Count > 0)
            {
                initialiserCombobox(bleta);
            }

        }

        public void initialiserCombobox(DataTable table)
        {
            TextBoxid.Text = table.Rows[0][0].ToString();
            TextBoxNomModifier.Text = table.Rows[0][1].ToString();
            DateTimePickerJourModifier.Value = Convert.ToDateTime(table.Rows[0][2]);
            DateTimePickerjOURFinModifier.Value = Convert.ToDateTime(table.Rows[0][3]);
            DateTimePickerDebutModifier.Value = DateTime.Today.Add((TimeSpan)table.Rows[0][4]);
            // Convertir TimeSpan en chaîne de caractères et l'affecter à DateTimePickerDateFin
            DateTimePickerFinModifier.Value = DateTime.Today.Add((TimeSpan)table.Rows[0][5]);
            TextBoxFormateurModifier.Text = table.Rows[0][6].ToString();
            TextboxDescriptionModifier.Text = table.Rows[0][7].ToString();
        }
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxid.Text);
            string nom = TextBoxNomModifier.Text;
            DateTime datejour = DateTimePickerJourModifier.Value.Date; // Ignorez l'heure pour la datejour
            DateTime heureDebut = DateTimePickerDebutModifier.Value;
            DateTime heureFin = DateTimePickerFinModifier.Value;
            DateTime jourFin = DateTimePickerjOURFinModifier.Value;
            string formateur = TextBoxFormateurModifier.Text;
            string description = TextboxDescriptionModifier.Text;
            string statut = "Actif";

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Le nom est obligatoire", "Ajouter un module", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (formateur.Trim().Equals(""))
            {
                MessageBox.Show("Le nom du formateur est obligatoire", "Ajouter un module", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (description.Trim().Equals(""))
            {
                MessageBox.Show("La description est obligatoire", "Ajouter un module", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                TimeSpan heureDebutSpan = heureDebut.TimeOfDay; // Ignorez la date pour l'heureDebut
                TimeSpan heureFinSpan = heureFin.TimeOfDay; // Ignorez la date pour l'heureFin

                // Ajoutez ces messages de débogage pour vérifier les valeurs
                Console.WriteLine($"heureDebutSpan: {heureDebutSpan}");
                Console.WriteLine($"heureFinSpan: {heureFinSpan}");

                if (heureDebutSpan.Ticks >= 0 && heureFinSpan.Ticks >= 0)
                {
                    if (module.ModifierModule(id, nom, datejour, jourFin, heureDebutSpan, heureFinSpan, description, formateur))
                    {
                        MessageBox.Show("Module Ajouté avec Succès", "Ajouter un module", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dashboard.UserControlModule controlModule = new Dashboard.UserControlModule();
                        using (var db = new EclosionHubEntities())
                        {
                            var listModuleConcs = db.VueModuleConcs.ToList();
                            controlModule.DataGridViewTrier.DataSource = listModuleConcs;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Échec d'ajout du module", "Ajouter un module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Les heures de début et de fin doivent être valides.", "Ajouter un module", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
