using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Utilisateur
{
    public partial class GererUtilisateur : Form
    {
        public GererUtilisateur()
        {
            InitializeComponent();
        }
        Classes.Utilisateur utilisateur = new Classes.Utilisateur();
        Classes.Controle controle = new Classes.Controle();
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            string nom = TextBoxNom.Text;
            string postnom = TextBoxPostnom.Text;
            string nomUtilisateur = TextBoxNomUtilisateur.Text;
            string motDepasse = TextBoxMotdePasse.Text;

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Le nom ne doit pas etre vide", "Creer un compte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (postnom.Trim().Equals(""))
            {
                MessageBox.Show("Le postnom ne doit pas etre vide", "Creer un compte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (nomUtilisateur.Trim().Equals(""))
            {
                MessageBox.Show("Le Nom d'utilisateur ne doit pas etre vide", "Creer un compte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (motDepasse.Trim().Equals(""))
            {
                MessageBox.Show("Le Mot de passe  ne doit pas etre vide", "Creer un compte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (utilisateur.AjouterUser(nom, postnom, nomUtilisateur, motDepasse))
                {
                    MessageBox.Show("Compte Creer avec succes", "Creer un compte", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("Echec de creation de compte", "Creer un compte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxID.Text);

            string nom = TextBoxNomModifer.Text;
            string post = TextBoxPostNomModifer.Text;
            string nomuti = TextBoxNomUtilisateurModifer.Text;
            string motdepasse = TextBoxMotdepasseModifer.Text;
            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Les Nom n'est peux pas etre vide", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (post.Trim().Equals(""))
            {
                MessageBox.Show("Le postnom n'est peux pas etre vide", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (nomuti.Trim().Equals(""))
            {
                MessageBox.Show("Le Nom d'Utilisateur n'est peux pas etre vide", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (motdepasse.Trim().Equals(""))
            {
                MessageBox.Show("Le mot de passe n'est peux pas etre vide", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (utilisateur.ModifierUtilisateur(id, nom, post, nomuti, motdepasse))
                {
                    MessageBox.Show("Utilisateur Modifier", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridViewUtilisateur.DataSource = utilisateur.listUtilisateur();
                    rafrechir();
                }
                else
                {
                    MessageBox.Show("Echec de modification", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GererUtilisateur_Load(object sender, EventArgs e)
        {
            DataGridViewUtilisateur.DataSource = utilisateur.listUtilisateur();
        }

        private void PanelCodeSecret_Click(object sender, EventArgs e)
        {

        }

        private void DataGridViewUtilisateur_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewUtilisateur.CurrentRow.Cells[0].Value);
            DataTable bletata = utilisateur.GetUtilisateurbyid(id);
            if(bletata.Rows.Count>0)
            {
               TextBoxID.Text =bletata.Rows[0][0].ToString();
               TextBoxNomModifer.Text=bletata.Rows[0][1].ToString();
               TextBoxPostNomModifer.Text = bletata.Rows[0][2].ToString(); ;
               TextBoxNomUtilisateurModifer.Text = bletata.Rows[0][3].ToString(); ;
               TextBoxMotdepasseModifer.Text = bletata.Rows[0][4].ToString();
                
            }

        }

        private void ButtonModif_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewUtilisateur.CurrentRow.Cells[0].Value);
            DataTable bletata = utilisateur.GetUtilisateurbyid(id);
            if (bletata.Rows.Count > 0)
            {
                TextBoxID.Text = bletata.Rows[0][0].ToString();
                TextBoxNomModifer.Text = bletata.Rows[0][1].ToString();
                TextBoxPostNomModifer.Text = bletata.Rows[0][2].ToString(); ;
                TextBoxNomUtilisateurModifer.Text = bletata.Rows[0][3].ToString(); ;
                TextBoxMotdepasseModifer.Text = bletata.Rows[0][4].ToString();
                Panelmodifier.BringToFront();
            }

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(DataGridViewUtilisateur.CurrentRow.Cells[0].Value);

                if (MessageBox.Show("Etez-Vous sure de vouloir vraiment supprimer cet Utilisateur?", "Suppression Utilisateur", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (utilisateur.SupprimerUtilisateur(id))
                    {
                        MessageBox.Show("Utilisateur Supprimer", "Suppressions Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGridViewUtilisateur.DataSource = utilisateur.listUtilisateur();

                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppressions", "Suppression Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Veiller choisir l'Utilisateur a modifier dans le tableau" + ex.Message, "INVALIDE ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            string motdepasse = TextBoxCodeSecret.Text;

            if (motdepasse.Trim().Equals(""))
            {
                MessageBox.Show("Le code secret n'est peux pas etre vide", "Ajout Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (controle.AjouterControle(motdepasse))
                {
                    MessageBox.Show("Code Secret Ajouter", "Ajout Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridViewCode.DataSource = controle.listControle();
                    rafrechir();
                }
                else
                {
                    MessageBox.Show("Echec d'ajout", "Ajout Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TextBoxIdControle.Text);

                string motdepasse = TextBoxCodeSecret.Text;

                if (motdepasse.Trim().Equals(""))
                {
                    MessageBox.Show("Le code secret n'est peux pas etre vide", "Modification Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (controle.ModifierControle(id, motdepasse))
                    {
                        MessageBox.Show("Code Secret Modifier", "Modification Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGridViewCode.DataSource = controle.listControle();
                        rafrechir();
                    }
                    else
                    {
                        MessageBox.Show("Echec de Modification", "Modification Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Veiller choisir l'Utilisateur a modifier dans le tableau" + ex.Message, "INVALIDE ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TextBoxIdControle.Text);

                if (MessageBox.Show("Etez-Vous sure de vouloir vraiment supprimer ce Code Secret?", "Suppression Code Secret", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (controle.SupprimerControle(id))
                    {
                        MessageBox.Show("Code Secret Supprimer", "Suppressions Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGridViewCode.DataSource = controle.listControle();
                        rafrechir();

                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppressions", "Suppression Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Veiller choisir l'Utilisateur a modifier dans le tableau" + ex.Message, "INVALIDE ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void rafrechir()
        {
            TextBoxID.Text = "";
            TextBoxNom.Text = "";
            TextBoxMotdePasse.Text = "";
            TextBoxPostnom.Text = "";
            TextBoxNomUtilisateur.Text = "";
            TextBoxNomModifer.Text = "";
            TextBoxMotdepasseModifer.Text = "";
            TextBoxPostNomModifer.Text = "";
            TextBoxNomUtilisateurModifer.Text = "";
            TextBoxCodeSecret.Text = "";
            TextBoxIdControle.Text = "";
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            PanelCodeSecret.BringToFront();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            PanelList.BringToFront();
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            PanelADD.BringToFront();
        }
    }
}
