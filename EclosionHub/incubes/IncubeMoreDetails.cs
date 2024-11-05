using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.incubes
{
    public partial class IncubeMoreDetails : Form
    {
        int id;
        public IncubeMoreDetails(int identifiant)
        {
            InitializeComponent();
            this.id = identifiant;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Carte_Incube carte_Incube = new Carte_Incube(id);
            carte_Incube.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Rapport_Print.RapportGeneralI rapportGeneralI = new Rapport_Print.RapportGeneralI(id);
            rapportGeneralI.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Rapports.GetPresenceByIncubes getPresenceBy = new Rapports.GetPresenceByIncubes(id);
            getPresenceBy.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            incubes.Menu_Carte menu_Carte = new Menu_Carte(id);
            menu_Carte.Show();
        }
    }
}
