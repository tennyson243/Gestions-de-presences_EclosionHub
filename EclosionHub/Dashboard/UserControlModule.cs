using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Dashboard
{
    public partial class UserControlModule : UserControl
    {
        public UserControlModule()
        {
            InitializeComponent();
        }
        Classes.Module module = new Classes.Module();
        BDD.Connecteur connecteur = new BDD.Connecteur();
        Classes.Presence presence = new Classes.Presence();
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Modules.ModuleMain moduleMain = new Modules.ModuleMain();
            moduleMain.Show();

        }

        struct DataParameter
        {
            public List<VueModuleConc> moduleConcs;
            public string FileName { get; set; }
        }
    
        private bool liveMainLaunched = false;

        private void CircleButtonExportExcel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                return;

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DataParameter _InputParameter = new DataParameter();
                    _InputParameter.FileName = sfd.FileName;
                    _InputParameter.moduleConcs = DataGridViewTrier.DataSource as List<VueModuleConc>;

                    bunifuProgressBar.MinimumValue = 1;
                    bunifuProgressBar.Value = 0;

                    // Démarrez le travail en arrière-plan avec le paramètre
                    backgroundWorker1.RunWorkerAsync(_InputParameter);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is DataParameter)
            {
                List<VueModuleConc> list = ((DataParameter)e.Argument).moduleConcs;
                string Filename = ((DataParameter)e.Argument).FileName;

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet Ws = (Worksheet)excel.ActiveSheet;
                excel.Visible = false;
                int index = 1;
                int process = list.Count;

                Ws.Cells[1, 1] = "Id";
                Ws.Cells[1, 2] = "Module";
                Ws.Cells[1, 3] = "Debut";
                Ws.Cells[1, 4] = "Fin";
                Ws.Cells[1, 5] = "Sessions";
                Ws.Cells[1, 6] = "Heure Debut";
                Ws.Cells[1, 7] = "Heure Fin";
                Ws.Cells[1, 8] = "Formateur";
                Ws.Cells[1, 9] = "Statut";

                // Rendre les en-têtes en gras
                Ws.Cells[1, 1].Font.Bold = true;
                Ws.Cells[1, 2].Font.Bold = true;
                Ws.Cells[1, 3].Font.Bold = true;
                Ws.Cells[1, 4].Font.Bold = true;
                Ws.Cells[1, 5].Font.Bold = true;
                Ws.Cells[1, 6].Font.Bold = true;
                Ws.Cells[1, 7].Font.Bold = true;
                Ws.Cells[1, 8].Font.Bold = true;
                Ws.Cells[1, 9].Font.Bold = true;

                // Incrémentez index après avoir écrit les en-têtes
                index++;

                // Configurez le BackgroundWorker pour signaler la progression
                backgroundWorker1.WorkerReportsProgress = true;

                foreach (VueModuleConc sr in list)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        backgroundWorker1.ReportProgress(index * 100 / process);
                        Ws.Cells[index, 1] = sr.id.ToString();
                        Ws.Cells[index, 2] = sr.Module?.ToString();
                        Ws.Cells[index, 3] = sr.Debut?.ToString();
                        Ws.Cells[index, 4] = sr.Fin?.ToString();
                        Ws.Cells[index, 5] = sr.Tot_Sessions?.ToString();
                        Ws.Cells[index, 6] = sr.H_Debut?.ToString();
                        Ws.Cells[index, 7] = sr.H_Fin?.ToString();
                        Ws.Cells[index, 8] = sr.Formateur?.ToString();
                        Ws.Cells[index, 9] = sr.Statut?.ToString();
                        // Incrémentez index après la mise à jour des cellules
                        index++;
                    }
                }

                Ws.SaveAs(Filename, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                excel.Quit();
            }
            else
            {
                // Gérer le cas où e.Argument n'est pas de type DataParameter
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            bunifuProgressBar.Value = e.ProgressPercentage;
            labelProgrese2.Text = string.Format("{0}", e.ProgressPercentage);
            bunifuProgressBar.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(100);
                labelProgrese2.Text = "100%";
                MessageBox.Show("Vos donnees on ete Exporter avec Succes Vers Excel", "Exporter vers Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewTrier.FirstDisplayedScrollingRowIndex = DataGridViewTrier.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewTrier_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewTrier.RowCount + 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewTrier_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewTrier.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            Modules.ModuleMain moduleMain = new Modules.ModuleMain();
            moduleMain.Show();
        }

        private void DataGridViewTrier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!ButtonLive.Enabled)
            {
                ButtonLive.Enabled = true;
            }
        }

        private void ButtonLive_Click(object sender, EventArgs e)
        {
            string stat = Convert.ToString(DataGridViewTrier.CurrentRow.Cells["Statut"].Value);
            if (stat == "Terminé")
            {
                MessageBox.Show("Imposible de Lancer le Live sur ce Module car sa date de Fin a deja etais depasser, Si vous voulez absolument lancer le live sur ce module, veillez modifier sa Date de fin Dans le panneau Modification Module", "Lancer le live", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ButtonLive.Enabled = false;
            }
            else
            {
                int moduleId = Convert.ToInt32(DataGridViewTrier.CurrentRow.Cells["Id"].Value);
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

                            System.Data.DataTable bleta = presence.CheckModule(incubeId, moduleId);
                            if (bleta.Rows.Count > 0)
                            {
                                DateTime date_scan = Convert.ToDateTime(bleta.Rows[0][2]);
                                if (date_scan.Date < DateTime.Today)
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

        private void bunifuTileButton5_Click(object sender, EventArgs e)
        {
            int idModule = Convert.ToInt32(DataGridViewTrier.CurrentRow.Cells["id"].Value);
            Modules.Syncronisation syncronisation = new Modules.Syncronisation(idModule);
            syncronisation.Show();
        }

        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            int idModule = Convert.ToInt32(DataGridViewTrier.CurrentRow.Cells["id"].Value);
            Modules.ModifierModule modifier = new Modules.ModifierModule(idModule);
            modifier.Show();

        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            
            try
            {
                int idModule = Convert.ToInt32(DataGridViewTrier.CurrentRow.Cells["id"].Value);

                if (MessageBox.Show("Etez-Vous sure de vouloir vraiment supprimer ce Module Secret?", "Suppression Code Secret", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (module.EffaceModules(idModule))
                    {
                        MessageBox.Show("Module Supprimer avec succes", "Suppressions Module", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        using (var db = new EclosionHubEntities())
                        {
                            var listModuleConcs =  db.VueModuleConcs.ToList();
                            DataGridViewTrier.DataSource = listModuleConcs;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppressions", "Suppression Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Vous ne pouvez pas supprimer ce module car il est utiliser dans plusieur autre table dans la base de donner " + ex.Message, "VIOLATION DE LA CLE ETRANGERE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaCircleButton1_Click(object sender, EventArgs e)
        {
            using (var db = new EclosionHubEntities())
            {
                var listModuleConcs = db.VueModuleConcs.ToList();
                DataGridViewTrier.DataSource = listModuleConcs;
            }
        }
    }
}
