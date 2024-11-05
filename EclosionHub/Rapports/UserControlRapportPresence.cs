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
    public partial class UserControlRapportPresence : UserControl
    {
        public UserControlRapportPresence()
        {
            InitializeComponent();
        }

        Classes.Module module = new Classes.Module();
        Classes.Incubes incubes = new Classes.Incubes();
        Classes.Projet entreprise = new Classes.Projet();
        Classes.Presence presence = new Classes.Presence();
        struct DataParameter
        {
            public List<RapportPresenceParModule> rapportcomplet;
            public string FileName { get; set; }
        }
        public void Rafraichir()
        {
            ComboBoxTrierIncubes.SelectedItem = null;
            //ComboBoxTrierIncubes.Enabled = true;
            ComboboxTrierProjet.SelectedItem = null;
            //ComboboxTrierProjet.Enabled = true;
            ComboboxTrierModule.SelectedItem = null;
            DataGridViewTrier.ClearSelection();
            DataGridViewTrier.DataSource = null;
        }

        private void ComboboxTrierModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxTrierIncubes.SelectedItem = null;
            ComboboxTrierProjet.SelectedItem = null;

            if (ComboboxTrierModule.SelectedItem != null)
            {
                // Récupérer l'objet sélectionné
                DataRowView selectedModule = (DataRowView)ComboboxTrierModule.SelectedItem;

                // Récupérer la valeur de l'ID à partir de l'objet sélectionné
                string module = Convert.ToString(selectedModule["Nom"]);

                // Utiliser l'ID pour obtenir les données du module
                System.Data.DataTable bleta = presence.RapportDePresenceParModule(module);

                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();
                    // Événement RowPrePaint pour personnaliser l'apparence des lignes
                    //DataGridViewTrier.RowPrePaint += (datagridviewSender, rowPrePaintEventArgs) =>
                    //{
                    //    try
                    //    {
                    //        string pourcentage = Convert.ToString(bleta.Rows[rowPrePaintEventArgs.RowIndex][7]);

                    //        // Appliquer la couleur de fond en fonction du pourcentage
                    //        if (pourcentage == "Pas Mal")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.DarkGreen;
                    //        }
                    //        else if (pourcentage == "Mauvais")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    //        }
                    //        else
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //};
                }
            }
        }


        private void DataGridViewTrier_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string module = DataGridViewTrier.CurrentRow.Cells[2].Value.ToString();
            string incube = DataGridViewTrier.CurrentRow.Cells[0].Value.ToString();

            RapportModuleAndIncube rapp = new RapportModuleAndIncube(module, incube);
            rapp.Show();
        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {

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
                List<RapportPresenceParModule> list = ((DataParameter)e.Argument).rapportcomplet;
                string Filename = ((DataParameter)e.Argument).FileName;

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet Ws = (Worksheet)excel.ActiveSheet;
                excel.Visible = false;
                int index = 1;
                int process = list.Count;


                Ws.Cells[1, 1] = "Nom_Incube";
                Ws.Cells[1, 2] = "Projet";
                Ws.Cells[1, 3] = "Module";
                Ws.Cells[1, 4] = "Nombre Total des sessions";
                Ws.Cells[1, 5] = "Nombre Total des Presences";
                Ws.Cells[1, 6] = "Nombre Total d'Absences";
                Ws.Cells[1, 7] = "Pourcentage de participation";
                Ws.Cells[1, 8] = "Observations";

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

                foreach (RapportPresenceParModule sr in list)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        backgroundWorker1.ReportProgress(index * 100 / process);
                        Ws.Cells[index, 1] = sr.Nom_Incube?.ToString();
                        Ws.Cells[index, 2] = sr.Entreprise?.ToString();
                        Ws.Cells[index, 3] = sr.Nom_Module?.ToString();
                        Ws.Cells[index, 4] = sr.N_Sessions?.ToString();
                        Ws.Cells[index, 5] = sr.Presences?.ToString();
                        Ws.Cells[index, 6] = sr.Absences?.ToString();
                        Ws.Cells[index, 7] = sr.P_Participation?.ToString();
                        Ws.Cells[index, 8] = sr.Observation?.ToString();

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
                labelProgrese2.Text = "Effectuer";
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
                    _InputParameter.rapportcomplet = DataGridViewTrier.DataSource as List<RapportPresenceParModule>;

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
                    DataGridViewTrier.DataSource = db.RapportPresenceParModules.ToList();
                    ComboBoxTrierIncubes.Enabled = false;
                    ComboboxTrierProjet.Enabled = false;
                    ComboboxTrierModule.Enabled = false;
                    ComboBoxTrierIncubes.SelectedItem = null;
                    ComboboxTrierProjet.SelectedItem = null;
                    ComboboxTrierModule.SelectedItem = null;
                    DataGridViewTrier.ClearSelection();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ButtonIncubes_Click(object sender, EventArgs e)
        {
            ComboBoxTrierIncubes.Enabled = true;
            ComboboxTrierProjet.Enabled = false;
            ComboboxTrierModule.Enabled = false;
            ComboBoxTrierIncubes.SelectedItem = null;
            ComboboxTrierProjet.SelectedItem = null;
            ComboboxTrierModule.SelectedItem = null;
            DataGridViewTrier.ClearSelection();
        }

        private void ButtonTous_Click(object sender, EventArgs e)
        {
            ComboBoxTrierIncubes.Enabled = false;
            ComboboxTrierProjet.Enabled = false;
            ComboboxTrierModule.Enabled = true;
            ComboBoxTrierIncubes.SelectedItem = null;
            ComboboxTrierProjet.SelectedItem = null;
            ComboboxTrierModule.SelectedItem = null;
            DataGridViewTrier.ClearSelection();
        }

        private void ButtonProjet_Click(object sender, EventArgs e)
        {
            ComboBoxTrierIncubes.Enabled = false;
            ComboboxTrierProjet.Enabled = true;
            ComboboxTrierModule.Enabled = false;
            ComboBoxTrierIncubes.SelectedItem = null;
            ComboboxTrierProjet.SelectedItem = null;
            ComboboxTrierModule.SelectedItem = null;
            DataGridViewTrier.ClearSelection();
        }

        private void ComboBoxTrierIncubes_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboboxTrierProjet.SelectedItem = null;
            ComboboxTrierModule.SelectedItem = null;



            if (ComboBoxTrierIncubes.SelectedItem == null)
            {
                DataGridViewTrier.ClearSelection();
            }
            else
            {
                // Récupérer l'objet sélectionné
                DataRowView selectedModule = (DataRowView)ComboBoxTrierIncubes.SelectedItem;

                // Récupérer la valeur de l'ID à partir de l'objet sélectionné
                string incube = Convert.ToString(selectedModule["Nom"]);

                // Utiliser l'ID pour obtenir les données du module
                System.Data.DataTable bleta = presence.RapportDePresenceParIncube(incube);


                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();
                    // Événement RowPrePaint pour personnaliser l'apparence des lignes
                    //DataGridViewTrier.RowPrePaint += (datagridviewSender, rowPrePaintEventArgs) =>
                    //{
                    //    try
                    //    {
                    //        string pourcentage = Convert.ToString(bleta.Rows[rowPrePaintEventArgs.RowIndex][7]);

                    //        // Appliquer la couleur de fond en fonction du pourcentage
                    //        if (pourcentage == "Pas Mal")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.DarkGreen;
                    //        }
                    //        else if (pourcentage == "Mauvais")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    //        }
                    //        else
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }

                    //};
                }
            }
        }

        private void ComboboxTrierProjet_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBoxTrierIncubes.SelectedItem = null;
            ComboboxTrierModule.SelectedItem = null;


            if (ComboboxTrierProjet.SelectedItem != null)
            {
                // Récupérer l'objet sélectionné
                DataRowView selectedModule = (DataRowView)ComboboxTrierProjet.SelectedItem;

                // Récupérer la valeur de la colonne "Nom" à partir de l'objet sélectionné
                string projetNom = Convert.ToString(selectedModule["Nom"]);

                // Utiliser projetNom pour obtenir les données du module
                System.Data.DataTable bleta = presence.RapportDePresenceParProjet(projetNom);

                if (bleta.Rows.Count > 0)
                {
                    DataGridViewTrier.DataSource = bleta;
                    DataGridViewTrier.ClearSelection();
                    // Événement RowPrePaint pour personnaliser l'apparence des lignes
                    //DataGridViewTrier.RowPrePaint += (datagridviewSender, rowPrePaintEventArgs) =>
                    //{
                    //    try
                    //    {
                    //        string pourcentage = Convert.ToString(bleta.Rows[rowPrePaintEventArgs.RowIndex][7]);

                    //        // Appliquer la couleur de fond en fonction du pourcentage
                    //        if (pourcentage == "Pas Mal")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.DarkGreen;
                    //        }
                    //        else if (pourcentage == "Mauvais")
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    //        }
                    //        else
                    //        {
                    //            DataGridViewTrier.Rows[rowPrePaintEventArgs.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //};
                }
            }
        }
    }
}
