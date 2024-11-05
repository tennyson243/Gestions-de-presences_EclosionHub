using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Dashboard
{
    public partial class UserControlPresencecs : UserControl
    {
        public UserControlPresencecs()
        {
            InitializeComponent();
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

        struct DataParameter
        {
            public List<VuePresence> presences;
            public string FileName { get; set; }
        }
        Classes.Presence presence = new Classes.Presence();
        Classes.Module module = new Classes.Module();
        private void UserControlPresencecs_Load(object sender, EventArgs e)
        {
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is DataParameter)
            {
                List<VuePresence> list = ((DataParameter)e.Argument).presences;
                string Filename = ((DataParameter)e.Argument).FileName;

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet Ws = (Worksheet)excel.ActiveSheet;
                excel.Visible = false;
                int index = 1;
                int process = list.Count;


                Ws.Cells[1, 1] = "iD";
                Ws.Cells[1, 2] = "Incubes";
                Ws.Cells[1, 3] = "Projet";
                Ws.Cells[1, 4] = "Participation";
                Ws.Cells[1, 5] = "Arrive";
                Ws.Cells[1, 6] = "Sortie";
                Ws.Cells[1, 7] = "Date";
                Ws.Cells[1, 8] = "Participation";


                // Rendre les en-têtes en gras
                Ws.Cells[1, 1].Font.Bold = true;
                Ws.Cells[1, 2].Font.Bold = true;
                Ws.Cells[1, 3].Font.Bold = true;
                Ws.Cells[1, 4].Font.Bold = true;
                Ws.Cells[1, 5].Font.Bold = true;
                Ws.Cells[1, 6].Font.Bold = true;
                Ws.Cells[1, 7].Font.Bold = true;
                Ws.Cells[1, 8].Font.Bold = true;

                // Incrémentez index après avoir écrit les en-têtes
                index++;

                // Configurez le BackgroundWorker pour signaler la progression
                backgroundWorker1.WorkerReportsProgress = true;

                foreach (VuePresence sr in list)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        backgroundWorker1.ReportProgress(index * 100 / process);
                        Ws.Cells[index, 1] = sr.id.ToString();
                        Ws.Cells[index, 2] = sr.Incubes?.ToString();
                        Ws.Cells[index, 3] = sr.projet?.ToString();
                        Ws.Cells[index, 4] = sr.Participation?.ToString();
                        Ws.Cells[index, 5] = sr.Arrive?.ToString();
                        Ws.Cells[index, 6] = sr.Sortie?.ToString();
                        Ws.Cells[index, 7] = sr.Date?.ToString();
                        Ws.Cells[index, 8] = sr.Justification?.ToString();

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
                    _InputParameter.presences = DataGridViewTrier.DataSource as List<VuePresence>;

                    bunifuProgressBar.MinimumValue = 1;
                    bunifuProgressBar.Value = 0;

                    // Démarrez le travail en arrière-plan avec le paramètre
                    backgroundWorker1.RunWorkerAsync(_InputParameter);
                }
            }
        }

        private void ComboBoxTrierIncube_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxTrierIncube.SelectedItem == null)
            {
                DataGridViewTrier.ClearSelection();
            }
            else
            {
                // Récupérer l'objet sélectionné
                DataRowView selectedModule = (DataRowView)ComboBoxTrierIncube.SelectedItem;

                // Récupérer la valeur de l'ID à partir de l'objet sélectionné
                string incube = Convert.ToString(selectedModule["Incube"]);

                // Utiliser l'ID pour obtenir les données du module
                System.Data.DataTable bleta = presence.GetPresencePartIncube(incube);


                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();
                }
                else
                {
                    DataGridViewTrier.DataSource = null;
                }
            }
        }

        private void ComboBoxTrierParProjet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxTrierParProjet.SelectedItem == null)
            {
                DataGridViewTrier.ClearSelection();
            }
            else
            {
                //// Récupérer l'objet sélectionné
                //DataRowView selectedModule = (DataRowView)ComboBoxTrierParProjet.SelectedItem;

                //// Récupérer la valeur de l'ID à partir de l'objet sélectionné
                //string modulle = Convert.ToString(selectedModule["Nom"]);

                //// Utiliser l'ID pour obtenir les données du module
                //System.Data.DataTable bleta = presence.GetPresencePartModule(modulle);


                //if (bleta.Rows.Count > 0)
                //{
                //    DataGridViewTrier.DataSource = bleta;
                //    DataGridViewTrier.ClearSelection();
                //}
                //else
                //{
                //    DataGridViewTrier.DataSource = null;
                //}
            }
        }

        private void ButtonOui_Click(object sender, EventArgs e)
        {
            string prese = "Oui";
            System.Data.DataTable bleta = presence.GetPresencePartParticipation(prese);


            if (bleta.Rows.Count > 0)
            {
                DataGridViewTrier.DataSource = bleta;
                DataGridViewTrier.ClearSelection();
            }
            else
            {
                DataGridViewTrier.DataSource = null;
            }
        }

        private void ButtonNon_Click(object sender, EventArgs e)
        {
            string prese = "Non";
            System.Data.DataTable bleta = presence.GetPresencePartParticipation(prese);


            if (bleta.Rows.Count > 0)
            {
                DataGridViewTrier.DataSource = bleta;
                DataGridViewTrier.ClearSelection();
            }
            else
            {
                DataGridViewTrier.DataSource = null;
            }
        }

        private void ComboboxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboboxDate.SelectedItem == null)
            {
                DataGridViewTrier.ClearSelection();
            }
            else
            {
                // Récupérer l'objet sélectionné
                DataRowView selectedModule = (DataRowView)ComboboxDate.SelectedItem;

                // Récupérer la valeur de l'ID à partir de l'objet sélectionné
                string dete= Convert.ToString(selectedModule["Date"]);

                // Utiliser l'ID pour obtenir les données du module
                System.Data.DataTable bleta = presence.GetPresencePartDate(dete);


                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();
                }
                else
                {
                    DataGridViewTrier.DataSource = null;
                }
            }
        }

        private void ComboBoxJustification_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxJustification.SelectedItem == null)
            {
                DataGridViewTrier.ClearSelection();
            }
            else
            {
                // Récupérer l'objet sélectionné
                DataRowView selectedModule = (DataRowView)ComboBoxJustification.SelectedItem;

                // Récupérer la valeur de l'ID à partir de l'objet sélectionné
                string justification = Convert.ToString(selectedModule["Justification"]);

                // Utiliser l'ID pour obtenir les données du module
                System.Data.DataTable bleta = presence.GetPresencePartJustification(justification);


                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();
                }
                else
                {
                    DataGridViewTrier.DataSource = null;
                }
            }
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            if (DataGridViewTrier.CurrentRow != null)
            {
                string particip = Convert.ToString(DataGridViewTrier.CurrentRow.Cells["Participation"].Value);
                if (particip=="Oui")
                {
                    MessageBox.Show("Desoler vous ne pouvez pas Justifier L'absence d'un Incube qui a ete present a cette Date et session de ce module", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        int id = Convert.ToInt32(DataGridViewTrier.CurrentRow.Cells["id"].Value);
                        string incubess = Convert.ToString(DataGridViewTrier.CurrentRow.Cells["Incubes"].Value);
                        string projet = Convert.ToString(DataGridViewTrier.CurrentRow.Cells["Projet"].Value);
                        string date = Convert.ToString(DataGridViewTrier.CurrentRow.Cells["Date"].Value);
                        string module = Convert.ToString(DataGridViewTrier.CurrentRow.Cells["Module"].Value);

                        Modules.JustifierAbsence justifierAbsence = new Modules.JustifierAbsence(id, incubess, projet, date, module);
                        justifierAbsence.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Aucune ligne sélectionnée.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
