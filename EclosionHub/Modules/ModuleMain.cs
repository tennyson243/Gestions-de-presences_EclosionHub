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
    public partial class ModuleMain : Form
    {
        public ModuleMain()
        {
            InitializeComponent();
        }

        Classes.Module module = new Classes.Module();

        private void ModuleMain_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Today;

            // Initialiser DateTimePickerDebut avec l'heure actuelle ou 8h si c'est après 8h
            DateTimePickerFin.Value = currentDate.AddHours(DateTime.Now.Hour >= 8 ? 0 : 8 - DateTime.Now.Hour);

            // Initialiser DateTimePickerFin avec l'heure actuelle + 4h30 ou 12h30 si c'est après 12h30
            DateTimePickerDebut.Value = currentDate.AddHours(DateTime.Now.Hour >= 12 || (DateTime.Now.Hour == 12 && DateTime.Now.Minute >= 30) ? 4.5 : 12.5);
            
        }
  


        private void gunaAdvenceTileButton1_Click(object sender, EventArgs e)
        {
            List_des_modules liste = new List_des_modules();
            liste.Show();
        }

        private void gunaAdvenceTileButton2_Click(object sender, EventArgs e)
        {
            PanelAdd.BringToFront();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            string nom = TextBoxNom.Text;
            DateTime Debut = DateTimePickerDateDebut.Value.Date; // Ignorez l'heure pour la datejour
            DateTime heureDebut = DateTimePickerFin.Value;
            DateTime heureFin = DateTimePickerDebut.Value;
            DateTime Fin = DateTimePickerFiin.Value;
            string formateur = TextBoxFormateur.Text;
            string description = TextBoxDescription.Text;
            string statut = "Non Commencer";

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
            else if (Debut < DateTime.Today || Fin < DateTime.Today)
            {
                MessageBox.Show("La date doit être égale ou postérieure à la date d'aujourd'hui.", "Ajouter un module", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                TimeSpan heureDebutSpan = heureDebut.TimeOfDay; // Ignorez la date pour l'heureDebut
                TimeSpan heureFinSpan = heureFin.TimeOfDay; // Ignorez la date pour l'heureFin

                if (heureDebutSpan.Ticks >= 0 && heureFinSpan.Ticks >= 0)
                {
                    if (module.AjouterModule(nom, Debut, Fin, heureDebutSpan, heureFinSpan, description, formateur, statut))
                    {
                        MessageBox.Show("Module Ajouté avec Succès", "Ajouter un module", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dashboard.UserControlModule controlModule = new Dashboard.UserControlModule();
                        using (var db = new EclosionHubEntities())
                        {
                            var listModuleConcs =  db.VueModuleConcs.ToList();
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

        private void gunaButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
