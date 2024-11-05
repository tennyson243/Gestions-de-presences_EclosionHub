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
    public partial class Card_B : Form
    {
        int id_incube;
        public Card_B(int id)
        {
            InitializeComponent();
            this.id_incube = id;
        }
        Rapport_Print.Card_Back carte = new Card_Back();

        private void Card_B_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["EclosionHub.Properties.Settings.EclosionHubConnectionString"].ToString();

            string query = "select * from ViewCarteIdentite where id=@id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int); // Correction du nom du paramètre
            parameters[0].Value = id_incube;

            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddRange(parameters); // Ajout des paramètres à la commande
            adapter.Fill(ds, "ViewCarteIdentite");

            DataTable dt = ds.Tables["ViewCarteIdentite"];
            carte.SetDataSource(ds.Tables["ViewCarteIdentite"]);

            crystalReportViewer1.ReportSource = carte;
            crystalReportViewer1.Refresh();
        }
    }
}
