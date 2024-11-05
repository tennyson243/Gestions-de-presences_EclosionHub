using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace EclosionHub.BDD
{
    class Connecteur
    {

        private SqlConnection con = new SqlConnection(@"Data Source=Tennyson;Initial Catalog=EclosionHub;Integrated Security=True");

        //DEVELOPPEMENT = @"Data Source=Tennyson;Initial Catalog=EclosionHub;Integrated Security=True
        //PRODUCTION=Data Source=DESKTOP-10SU878;Initial Catalog=EclosionHub;User ID=sa;Password=vicent243;Integrated Security=True

        public bool Openconnection()
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                return true; // Indique que la connexion a été ouverte avec succès
            }
            catch (Exception ex)
            {
                // Gérez l'exception ici (peut-être journalisez-la ou affichez un message d'erreur)
                return false; // Indique que la connexion a échoué
            }
        }


        public bool closeconnection()
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                return true; // Indique que la connexion a été ouverte avec succès
            }
            catch (Exception ex)
            {
                // Gérez l'exception ici (peut-être journalisez-la ou affichez un message d'erreur)
                return false; // Indique que la connexion a échoué
            }
        }

        public SqlConnection getconnexion()
        {
            return con;
        }

        public DataTable getdata(string query, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(query, con);

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);
            return table;

        }

        public int setdata(string query, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);

            }

            Openconnection();

            int commandsatate = cmd.ExecuteNonQuery();

            closeconnection();
            return commandsatate;
        }
    }
}
