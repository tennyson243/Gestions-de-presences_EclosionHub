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

namespace EclosionHub.Rapports
{
    public partial class UserControlRapportParSessionModule : UserControl
    {
        public UserControlRapportParSessionModule()
        {
            InitializeComponent();
        }

        Classes.Module module = new Classes.Module();
        Classes.Presence presence = new Classes.Presence();
        struct DataParameter
        {
            public List<RapportPresenceParSessionParModule> rapportPresenceParSessions;
            public string FileName { get; set; }
        }

        private void ComboBoxTrierModule_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ComboBoxTrierModule.SelectedItem != null)
            {
                // Récupérer l'objet sélectionné
                DataRowView selectedModule = (DataRowView)ComboBoxTrierModule.SelectedItem;

                // Récupérer la valeur de l'ID à partir de l'objet sélectionné
                string Module = Convert.ToString(selectedModule["Nom"]);

                // Utiliser l'ID pour obtenir les données du module
                System.Data.DataTable bleta = presence.GetRapportPartiSessionModule(Module);

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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is DataParameter)
            {
                List<RapportPresenceParSessionParModule> list = ((DataParameter)e.Argument).rapportPresenceParSessions;
                string Filename = ((DataParameter)e.Argument).FileName;

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet Ws = (Worksheet)excel.ActiveSheet;
                excel.Visible = false;
                int index = 1;
                int process = list.Count;


                Ws.Cells[1, 1] = "Module";
                Ws.Cells[1, 2] = "Sessions";
                Ws.Cells[1, 3] = "Date de la Sessions";
                Ws.Cells[1, 4] = "Presences";
                Ws.Cells[1, 5] = "Absences";


                // Rendre les en-têtes en gras
                Ws.Cells[1, 1].Font.Bold = true;
                Ws.Cells[1, 2].Font.Bold = true;
                Ws.Cells[1, 3].Font.Bold = true;
                Ws.Cells[1, 4].Font.Bold = true;
                Ws.Cells[1, 5].Font.Bold = true;

                // Incrémentez index après avoir écrit les en-têtes
                index++;

                // Configurez le BackgroundWorker pour signaler la progression
                backgroundWorker1.WorkerReportsProgress = true;

                foreach (RapportPresenceParSessionParModule sr in list)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        backgroundWorker1.ReportProgress(index * 100 / process);
                        Ws.Cells[index, 1] = sr.Nom_Module?.ToString();
                        Ws.Cells[index, 2] = sr.J_Session?.ToString();
                        Ws.Cells[index, 3] = sr.Date_Seance?.ToString();
                        Ws.Cells[index, 4] = sr.Nombre_Present?.ToString();
                        Ws.Cells[index, 5] = sr.Nombre_Absence?.ToString();

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
                    _InputParameter.rapportPresenceParSessions = DataGridViewTrier.DataSource as List<RapportPresenceParSessionParModule>;

                    bunifuProgressBar.MinimumValue = 1;
                    bunifuProgressBar.Value = 0;

                    // Démarrez le travail en arrière-plan avec le paramètre
                    backgroundWorker1.RunWorkerAsync(_InputParameter);
                }
            }
        }

        private void ButtonModules_Click(object sender, EventArgs e)
        {
            ComboBoxTrierModule.Enabled = true;
            ComboBoxTrierModule.SelectedItem = null;
            DateTimePickerdudate.Enabled = false;
            DateTimeAudate.Enabled = false;
            CircleButtonSearch.Enabled = false;
            DataGridViewTrier.ClearSelection();
        }

        private void ButtonProjet_Click(object sender, EventArgs e)
        {
            ComboBoxTrierModule.Enabled = false;
            ComboBoxTrierModule.SelectedItem = null;
            DateTimePickerdudate.Enabled = true;
            DateTimeAudate.Enabled = true;
            CircleButtonSearch.Enabled = true;
            DataGridViewTrier.ClearSelection();
        }

        private void gunaCircleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new EclosionHubEntities())
                {
                    DataGridViewTrier.DataSource = db.RapportPresenceParSessionParModules.ToList();
                    ComboBoxTrierModule.Enabled = false;
                    ComboBoxTrierModule.SelectedItem = null;
                    DateTimePickerdudate.Enabled = false;
                    DateTimeAudate.Enabled = false;
                    CircleButtonSearch.Enabled = false;
                    DataGridViewTrier.ClearSelection();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
