using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Rapports
{
    public partial class GetPresenceByIncubes : Form
    {
        int id_incube;
        public GetPresenceByIncubes(int id)
        {
            InitializeComponent();
            this.id_incube = id;
        }
        Classes.RequetteRapport rapport = new Classes.RequetteRapport();
        Classes.Presence presence = new Classes.Presence();
        private void GetPresenceByIncubes_Load(object sender, EventArgs e)
        {
            DataTable table = rapport.GetNomIncubeByID(id_incube);
            if(table.Rows.Count>0)
            {
                string nom = table.Rows[0][0].ToString();

                System.Data.DataTable bleta = presence.RapportDePresenceParIncube(nom);


                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();

                    DataTable ceta = rapport.GetNomConcIncubeByID(id_incube);

                    if (ceta.Rows.Count > 0)
                    {
                        string conc = ceta.Rows[0][0].ToString().ToUpper();
                        labelNom.Text = $"RAPPORT DE PARTICIPATION DE L'INCUBE {conc}";
                    }

                    
                }
                
            }

        }
    }
}
