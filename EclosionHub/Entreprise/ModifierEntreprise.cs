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
    public partial class ModifierEntreprise : Form
    {
        int id;
        public ModifierEntreprise(int identifiant)
        {
            InitializeComponent();
            this.id = identifiant;
        }
        Classes.Incubes incubes = new Classes.Incubes();
        Classes.Projet entreprise = new Classes.Projet();
        private void ModifierEntreprise_Load(object sender, EventArgs e)
        {
            DataTable table = entreprise.getEntreprisebyid(id);


            if (table.Rows.Count > 0)
            {
                ComboBoxProprietaireModifier.DataSource = incubes.listIncubes();
                ComboBoxProprietaireModifier.DisplayMember = "Prenom";
                ComboBoxProprietaireModifier.ValueMember = "Id";
                initialisationDonner(table);
            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
        public void actualiser()
        {
            TextBoxid.Text = "";
            TextBoxDescriptionModifier.Text = "";
            TextBoxNomModifier.Text = "";
            ComboBoxProprietaireModifier.SelectedItem = null;
            ComboBoxSecteurModifier.SelectedItem = null;
        }

        public void initialisationDonner(DataTable data)
        {
            TextBoxid.Text = data.Rows[0][0].ToString();
            TextBoxNomModifier.Text = data.Rows[0][1].ToString();
            ComboBoxProprietaireModifier.SelectedValue = Convert.ToInt32(data.Rows[0][2].ToString());
            ComboBoxSecteurModifier.SelectedItem = data.Rows[0][3].ToString();
            TextBoxDescriptionModifier.Text = data.Rows[0][4].ToString();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxid.Text);
            string nom = TextBoxNomModifier.Text;
            int proprietaire = Convert.ToInt32(ComboBoxProprietaireModifier.SelectedValue);
            string secteur = ComboBoxSecteurModifier.Text;
            string description = TextBoxDescriptionModifier.Text;

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
                if (entreprise.ModifierEntreprise(id,nom, proprietaire, secteur, description))
                {
                    MessageBox.Show("Projet Modifier avec succes", "Modifier un projet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    actualiser();
                }
                else
                {
                    MessageBox.Show("Echec de modification de projet", "Modifier un projet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
