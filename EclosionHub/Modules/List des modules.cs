using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Modules
{
    public partial class List_des_modules : Form
    {
        public List_des_modules()
        {
            InitializeComponent();
        }

        Classes.Module module = new Classes.Module();
        BDD.Connecteur connecteur = new BDD.Connecteur();
        Classes.Presence presence = new Classes.Presence();
        private void List_des_modules_Load(object sender, EventArgs e)
        {
            DataGridViewModules.RowTemplate.Height = 80;
            DataGridViewColumn IM = new DataGridViewColumn();
            DataGridViewModules.DataSource = module.listModules();
            IM = (DataGridViewColumn)DataGridViewModules.Columns[0];
            IM.Visible = false;
        }

        private bool liveMainLaunched = false;

        private void gunaCircleButton1_Click(object sender, EventArgs e)
        {
            int moduleId = Convert.ToInt32(DataGridViewModules.CurrentRow.Cells["Id"].Value);
            string pre = "Non";
            TimeSpan heureArrive = TimeSpan.Zero;
            TimeSpan heureSortie = TimeSpan.Zero;

            connecteur.Openconnection();

            using (SqlCommand command = new SqlCommand("SELECT Id FROM Incubes", connecteur.getconnexion()))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int incubeId = reader.GetInt32(0);

                        DataTable bleta = presence.CheckModule(incubeId, moduleId);
                        if (bleta.Rows.Count > 0)
                        {
                            DateTime date_scan = Convert.ToDateTime(bleta.Rows[0][2]);
                            if(date_scan.Date < DateTime.Today)
                            {
                                if (presence.AjouterPresence(incubeId, moduleId, pre, heureArrive, heureSortie))
                                {
                                    if (!liveMainLaunched)
                                    {
                                        Live.LiveMain live = new Live.LiveMain(moduleId);
                                        live.Show();
                                        liveMainLaunched = true;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Echec de lancement du live", "Lancer le live", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                if (!liveMainLaunched)
                                {
                                    Live.LiveMain live = new Live.LiveMain(moduleId);
                                    live.Show();
                                    liveMainLaunched = true;
                                }
                            }
                        }
                        else
                        {
                            if (presence.AjouterPresence(incubeId, moduleId, pre, heureArrive, heureSortie))
                            {
                                if (!liveMainLaunched)
                                {
                                    Live.LiveMain live = new Live.LiveMain(moduleId);
                                    live.Show();
                                    liveMainLaunched = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Echec de lancement du live", "Lancer le live", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

            connecteur.closeconnection();
        }

    }
}
