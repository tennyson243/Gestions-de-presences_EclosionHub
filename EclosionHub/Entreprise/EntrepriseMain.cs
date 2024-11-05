using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Entreprise
{
    public partial class EntrepriseMain : Form
    {
        public EntrepriseMain()
        {
            InitializeComponent();
        }

        Classes.Projet entreprise = new Classes.Projet();
        Classes.Incubes incubes = new Classes.Incubes();
        private void EntrepriseMain_Load(object sender, EventArgs e)
        {
            ComboBoxProprietaire.DataSource = incubes.listIncubes();
            ComboBoxProprietaire.DisplayMember = "Prenom";
            ComboBoxProprietaire.ValueMember = "Id";
            ComboBoxProprietaire.SelectedItem = null;
        }

        public void actualiser()
        {

            TextBoxDescription.Text = "";
            TextBoxNomEntreprise.Text = "";
            ComboBoxProprietaire.SelectedItem = null;
            ComboBoxSecteur.SelectedItem = null;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            string nom = TextBoxNomEntreprise.Text;
            int proprietaire = Convert.ToInt32(ComboBoxProprietaire.SelectedValue);
            string secteur = ComboBoxSecteur.Text;
            string description = TextBoxDescription.Text;

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("le nom est obligatoire", "Ajouter une entreprise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (secteur.Trim().Equals(""))
            {
                MessageBox.Show("le secteur d'activite est obligatoire", "Ajouter une entreprise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (description.Trim().Equals(""))
            {
                MessageBox.Show("la description de l'activite est obligatoire", "Ajouter une entreprise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (entreprise.AjouterEntreprise(nom, proprietaire, secteur, description))
                {
                    MessageBox.Show("Nouvelle Entreprise Enregistrer avec succes", "Ajouter une entreprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    actualiser();
                }
                else
                {
                    MessageBox.Show("Echec d'enregistrement de l'entreprise", "Ajouter une entreprise", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
