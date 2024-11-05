using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.Rapport_Print
{
    public partial class RapportGeneralI : Form
    {
        int id_incube;
        public RapportGeneralI(int id)
        {
            InitializeComponent();
            this.id_incube = id;
        }
        Rapport_Print.RapportGeneralIncube RapportGeneral = new RapportGeneralIncube();
        private void RapportGeneralI_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["EclosionHub.Properties.Settings.EclosionHubConnectionString"].ToString();

            string query = "select * from rapportGenralIncube where IncubeId=@id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int); // Correction du nom du paramètre
            parameters[0].Value = id_incube;

            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddRange(parameters); // Ajout des paramètres à la commande
            adapter.Fill(ds, "rapportGenralIncube");

            DataTable dt = ds.Tables["rapportGenralIncube"];
            RapportGeneral.SetDataSource(ds.Tables["rapportGenralIncube"]);

            crystalReportViewer1.ReportSource = RapportGeneral;
            crystalReportViewer1.Refresh();
        }

    }
}
