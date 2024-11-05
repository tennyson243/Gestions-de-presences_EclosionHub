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
    public partial class RapportParticipationParModule : UserControl
    {
        public RapportParticipationParModule()
        {
            InitializeComponent();
        }

        Classes.Presence presence = new Classes.Presence();
        Classes.Module module = new Classes.Module();

        struct DataParameter
        {
            public List<RapportParticipationTotalParModule> rapportParticipationParModules;
            public string FileName { get; set; }
        }
        private void ComboBoxTrierModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxTrierModule.SelectedItem != null)
            {
                // Récupérer l'objet sélectionné
                DataRowView selectedModule = (DataRowView)ComboBoxTrierModule.SelectedItem;

                // Récupérer la valeur de l'ID à partir de l'objet sélectionné
                string Module = Convert.ToString(selectedModule["Nom"]);

                // Utiliser l'ID pour obtenir les données du module
                System.Data.DataTable bleta = presence.GetRapportParticipationByModule(Module);

                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();
                    // Événement RowPrePaint pour personnaliser l'apparence des lignes
                    DataGridViewTrier.RowPrePaint += (datagridviewSender, rowPrePaintEventArgs) =>
                    {
                        try
                        {
                            string pourcentage = Convert.ToString(bleta.Rows[rowPrePaintEventArgs.RowIndex][6]);

                            // Appliquer la couleur de fond en fonction du pourcentage
                            if (pourcentage == "Pas Mal")
                            {
                                DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.DarkGreen;
                            }
                            else if (pourcentage == "Mauvais")
                            {
                                DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                            }
                            else
                            {
                                DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    };
                }
            }
        }

        private void TextBoxRecherche_TextChange(object sender, EventArgs e)
        {
            string mdl = TextBoxRecherche.Text;
            System.Data.DataTable bleta = presence.TexteBoxRechercherModuleModule(mdl);
            if(bleta.Rows.Count>0)
            {
                DataGridViewTrier.DataSource = bleta;
            }
            else
            {
                DataGridViewTrier.DataSource = null;
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
                List<RapportParticipationTotalParModule> list = ((DataParameter)e.Argument).rapportParticipationParModules;
                string Filename = ((DataParameter)e.Argument).FileName;

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet Ws = (Worksheet)excel.ActiveSheet;
                excel.Visible = false;
                int index = 1;
                int process = list.Count;


                Ws.Cells[1, 1] = "Module";
                Ws.Cells[1, 2] = "Nombres de Sessions";
                Ws.Cells[1, 3] = "Participations";
                Ws.Cells[1, 4] = "Presences";
                Ws.Cells[1, 5] = "Absences";
                Ws.Cells[1, 6] = "% Participation";
                Ws.Cells[1, 7] = "Observations";

                // Rendre les en-têtes en gras
                Ws.Cells[1, 1].Font.Bold = true;
                Ws.Cells[1, 2].Font.Bold = true;
                Ws.Cells[1, 3].Font.Bold = true;
                Ws.Cells[1, 4].Font.Bold = true;
                Ws.Cells[1, 5].Font.Bold = true;
                Ws.Cells[1, 6].Font.Bold = true;
                Ws.Cells[1, 7].Font.Bold = true;
           

                // Incrémentez index après avoir écrit les en-têtes
                index++;

                // Configurez le BackgroundWorker pour signaler la progression
                backgroundWorker1.WorkerReportsProgress = true;

                foreach (RapportParticipationTotalParModule sr in list)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        backgroundWorker1.ReportProgress(index * 100 / process);
                        Ws.Cells[index, 1] = sr.Nom_Module?.ToString();
                        Ws.Cells[index, 2] = sr.N_Sessions?.ToString();
                        Ws.Cells[index, 3] = sr.Tot_Participation?.ToString();
                        Ws.Cells[index, 4] = sr.Nombre_Present?.ToString();
                        Ws.Cells[index, 5] = sr.Nombre_Absence?.ToString();
                        Ws.Cells[index, 6] = sr.P_Participation?.ToString();
                        Ws.Cells[index, 7] = sr.Observation?.ToString();
                       
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
                    _InputParameter.rapportParticipationParModules = DataGridViewTrier.DataSource as List<RapportParticipationTotalParModule>;

                    bunifuProgressBar.MinimumValue = 1;
                    bunifuProgressBar.Value = 0;

                    // Démarrez le travail en arrière-plan avec le paramètre
                    backgroundWorker1.RunWorkerAsync(_InputParameter);
                }
            }
        }
    }
}