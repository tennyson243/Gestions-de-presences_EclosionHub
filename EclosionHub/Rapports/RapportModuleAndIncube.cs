using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Rapports
{
    public partial class RapportModuleAndIncube : Form
    {
        string moduleVar;
        string incubeVar;
        public RapportModuleAndIncube( string md, string ic)
        {
            InitializeComponent();
            this.incubeVar = ic;
            this.moduleVar = md;
        }

        Classes.Presence presence = new Classes.Presence();

        private void RapportModuleAndIncube_Load(object sender, EventArgs e)
        {
            DataTable bleta = presence.GetRapportDePresenceParModuleEtIncubes(moduleVar, incubeVar);
            if (bleta.Rows.Count > 0)
            {
                LabelNomIncube.Text = bleta.Rows[0][0].ToString();
                LabelNomDuProjet.Text = bleta.Rows[0][1].ToString();
                LabelNomModule.Text = bleta.Rows[0][2].ToString();
                LabelTotalSession.Text = bleta.Rows[0][3].ToString();
                LabelPresenceSession.Text = bleta.Rows[0][4].ToString();
                LabelAbsenceSession.Text = bleta.Rows[0][5].ToString();
                LabelpOURCENTAGE.Text = bleta.Rows[0][6].ToString();
                    byte[] couverture = (byte[])bleta.Rows[0][7];
                    MemoryStream ms = new MemoryStream(couverture);
                    byte[] image = ms.ToArray();
                    PictureBoxIncube.Image = Image.FromStream(ms);
              
            }
        }
    }
}
