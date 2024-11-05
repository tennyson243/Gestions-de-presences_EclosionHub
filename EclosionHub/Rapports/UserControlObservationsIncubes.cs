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
    public partial class UserControlObservationsIncubes : UserControl
    {
        public UserControlObservationsIncubes()
        {
            InitializeComponent();
        }
        Classes.Module module = new Classes.Module();
        Classes.Presence presence = new Classes.Presence();

        struct DataParameter
        {
            public List<VueObservationsIncube> observationsIncubes;
            public string FileName { get; set; }
        }
   

        public void Rafraichir()
        {
            ComboBoxTrierModule.SelectedItem = null;
            DataGridViewTrier.ClearSelection();
            DataGridViewTrier.DataSource = null;
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
                System.Data.DataTable bleta = presence.GetRapportObservationParModule(Module);

                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();
                    // Événement RowPrePaint pour personnaliser l'apparence des lignes
                    //DataGridViewTrier.RowPrePaint += (datagridviewSender, rowPrePaintEventArgs) =>
                    //{
                    //    try
                    //    {
                    //        string pourcentage = Convert.ToString(bleta.Rows[rowPrePaintEventArgs.RowIndex][11]);

                    //        // Appliquer la couleur de fond en fonction du pourcentage
                    //        if (pourcentage == "Ajourneer")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    //        }
                    //        else if(pourcentage == "Distinction")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    //        }
                    //        else if (pourcentage == "Haute Distinction")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    //        }
                    //        else
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.DarkRed;
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //};
                }
                else
                {
                    DataGridViewTrier.DataSource = null;
                }
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string debut = DateTimePickerdudate.Value.ToString("dd/MM/yyyy");
            string fin = DateTimeAudate.Value.ToString("dd/MM/yyyy");

            System.Data.DataTable bleta = presence.GetRapportObservationIncubesParDate(debut, fin);
            if(bleta.Rows.Count>0)
            {
                DataGridViewTrier.DataSource = bleta;
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
                List<VueObservationsIncube> list = ((DataParameter)e.Argument).observationsIncubes;
                string Filename = ((DataParameter)e.Argument).FileName;

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet Ws = (Worksheet)excel.ActiveSheet;
                excel.Visible = false;
                int index = 1;
                int process = list.Count;


                Ws.Cells[1, 1] = "Incube";
                Ws.Cells[1, 2] = "Module";
                Ws.Cells[1, 3] = "Date Scan";
                Ws.Cells[1, 4] = "Sessions";
                Ws.Cells[1, 5] = "Début Session";
                Ws.Cells[1, 6] = "Arrivée Incube";
                Ws.Cells[1, 7] = "Fin Session";
                Ws.Cells[1, 8] = "Départ Incube";
                Ws.Cells[1, 9] = "Total Hrs Form.";
                Ws.Cells[1, 10] = "Hrs Participées";
                Ws.Cells[1, 11] = "% Participation";
                Ws.Cells[1, 12] = "Observations";


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
                Ws.Cells[1, 10].Font.Bold = true;
                Ws.Cells[1, 11].Font.Bold = true;
                Ws.Cells[1, 12].Font.Bold = true;

                // Incrémentez index après avoir écrit les en-têtes
                index++;

                // Configurez le BackgroundWorker pour signaler la progression
                backgroundWorker1.WorkerReportsProgress = true;

                foreach (VueObservationsIncube sr in list)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        backgroundWorker1.ReportProgress(index * 100 / process);
                        Ws.Cells[index, 1] = sr.N_Incube?.ToString();
                        Ws.Cells[index, 2] = sr.N_Module?.ToString();
                        Ws.Cells[index, 3] = sr.D_Scan?.ToString();
                        Ws.Cells[index, 4] = sr.J_Session?.ToString();
                        Ws.Cells[index, 5] = sr.H_D_Module?.ToString();
                        Ws.Cells[index, 6] = sr.H_Arrivee?.ToString();
                        Ws.Cells[index, 7] = sr.H_F_Module?.ToString();
                        Ws.Cells[index, 8] = sr.H_Sortie?.ToString();
                        Ws.Cells[index, 9] = sr.H_Formation?.ToString();
                        Ws.Cells[index, 10] = sr.H_Participation?.ToString();
                        Ws.Cells[index, 11] = sr.P_Participation?.ToString();
                        Ws.Cells[index, 12] = sr.Observation?.ToString();

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
                    _InputParameter.observationsIncubes = DataGridViewTrier.DataSource as List<VueObservationsIncube>;

                    bunifuProgressBar.MinimumValue = 1;
                    bunifuProgressBar.Value = 0;

                    // Démarrez le travail en arrière-plan avec le paramètre
                    backgroundWorker1.RunWorkerAsync(_InputParameter);
                }
            }
        }

        private void gunaCircleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new EclosionHubEntities())
                {
                    DataGridViewTrier.DataSource = db.VueObservationsIncubes.ToList();
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
    }
}
