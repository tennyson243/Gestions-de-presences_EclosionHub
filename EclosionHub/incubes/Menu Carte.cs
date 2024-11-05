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
    public partial class Menu_Carte : Form
    {
        int id_incube;
        public Menu_Carte(int id)
        {
            InitializeComponent();
            this.id_incube = id;
        }

        private void Menu_Carte_Load(object sender, EventArgs e)
        {

        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            Rapport_Print.Carte_Incube carte_ = new Rapport_Print.Carte_Incube(id_incube);
            carte_.Show();
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            Rapport_Print.Card_B carte_ = new Rapport_Print.Card_B(id_incube);
            carte_.Show();
        }
    }
}
