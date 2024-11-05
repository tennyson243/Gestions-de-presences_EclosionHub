using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace EclosionHub.Rapport_Print
{
    public partial class Carte_Incube : Form
    {
        private int id_incube;
        private Rapport_Print.Card carte = new Card();

        public Carte_Incube(int id)
        {
            InitializeComponent();
            this.id_incube = id;
        }

        private void Carte_Incube_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EclosionHub.Properties.Settings.EclosionHubConnectionString"].ToString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM ViewCarteIdentite WHERE id=@id";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@id", id_incube);

                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "ViewCarteIdentite");

                        DataTable dt = ds.Tables["ViewCarteIdentite"];
                        carte.SetDataSource(ds.Tables["ViewCarteIdentite"]);

                        crystalReportViewer1.ReportSource = carte;
                        crystalReportViewer1.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        //private void PrintReport()
        //{
        //    // Remplacez la partie du code concernant PrintDialog comme suit :

        //    // Configuration de l'impression
        //    PrinterSettings printerSettings = new PrinterSettings();

        //    try
        //    {
        //        // Vous pouvez ajuster les paramètres d'impression selon vos besoins
        //        PrintDialog printDialog = new PrintDialog();
        //        printDialog.PrinterSettings = printerSettings;

        //        if (printDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            // Configuration du rapport
        //            carte.PrintOptions.PrinterName = printerSettings.PrinterName;

        //            // Obtention des paramètres de page à partir de PrinterSettings
        //            PageSettings pageSettings = new PageSettings(printerSettings);

        //            // Impression directe sans CrystalReportViewer
        //            carte.PrintToPrinter(printerSettings.Copies, false, pageSettings.Margins.Left, pageSettings.Margins.Top);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erreur d'impression : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}
    }
}

