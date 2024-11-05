using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Dashboard
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            incubes.mainIncube incub = new incubes.mainIncube();
            incub.Show();
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            Entreprise.EntrepriseMain entre = new Entreprise.EntrepriseMain();
            entre.Show();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            Modules.ModuleMain moduleMain = new Modules.ModuleMain();
            moduleMain.Show();
        }
    }
}
