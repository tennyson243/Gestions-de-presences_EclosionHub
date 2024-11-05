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
    public partial class JustifierAbsence : Form
    {
        private int idd;
        private string incubee;
        private string projett;
        private string Datee;
        private string Modulee;

        public JustifierAbsence(int id, string incube, string projet, string date, string module)
        {
            InitializeComponent();
            this.idd = id;
            this.incubee = incube;
            this.projett = projet;
            this.Datee = date;
            this.Modulee = module;
        }

        Classes.Presence presence = new Classes.Presence();
        private void JustifierAbsence_Load(object sender, EventArgs e)
        {
            labelModuleJour.Text = $"Justifier l'absence de {incubee}, propriétaire du projet {projett}, au module {Modulee} en date du {Datee}.";
        }

        private bool eventsEnabled = true;

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            string justification = "";

            if (CheckBoxMaladie.Checked)
            {
                justification = "Maladie";
            }
            else if (CheckBoxConge.Checked)
            {
                justification = "Congé autorisé";
            }
            else if (CheckBoxRendez.Checked)
            {
                justification = "Rendez-vous médical";
            }
            else
            {
                justification = "Autre";
            }

            if (presence.Justification(idd, justification))
            {
                MessageBox.Show("Justification effectuée avec succès", "Justification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Échec d'ajout de la justification", "Justification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckBoxMaladie_CheckedChanged(object sender, EventArgs e)
        {
            if (eventsEnabled)
            {
                DisableEvents();
                UncheckOtherCheckBoxes(CheckBoxMaladie);
                EnableEvents();
            }
        }

        private void CheckBoxConge_CheckedChanged(object sender, EventArgs e)
        {
            if (eventsEnabled)
            {
                DisableEvents();
                UncheckOtherCheckBoxes(CheckBoxConge);
                EnableEvents();
            }
        }

        private void CheckBoxRendez_CheckedChanged(object sender, EventArgs e)
        {
            if (eventsEnabled)
            {
                DisableEvents();
                UncheckOtherCheckBoxes(CheckBoxRendez);
                EnableEvents();
            }
        }

        private void CheckBoxAutre_CheckedChanged(object sender, EventArgs e)
        {
            if (eventsEnabled)
            {
                DisableEvents();
                UncheckOtherCheckBoxes(CheckBoxAutre);
                EnableEvents();
            }
        }

        private void UncheckOtherCheckBoxes(Guna.UI.WinForms.GunaCheckBox checkedBox)
        {
            foreach (Guna.UI.WinForms.GunaCheckBox checkBox in new Guna.UI.WinForms.GunaCheckBox[] { CheckBoxMaladie, CheckBoxConge, CheckBoxRendez, CheckBoxAutre })
            {
                if (checkBox != checkedBox)
                {
                    checkBox.Checked = false;
                }
            }
        }

        private void DisableEvents()
        {
            eventsEnabled = false;
        }

        private void EnableEvents()
        {
            eventsEnabled = true;
        }



    }

}
