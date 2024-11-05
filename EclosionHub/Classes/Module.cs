using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclosionHub.Classes
{
    class Module
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterModule(string nom, DateTime date_debut, DateTime date_fin, TimeSpan heureDebut, TimeSpan heureFin, string description, string formateur , string statut)
        {
            string query = "Insert into Modules (Nom, Date_debut, Date_fin, Heure_debut, Heure_fin, Formateur, Description, Statut) values (@Nom, @Date_debut,@Date_fin, @Heure_debut, @Heure_fin, @Formateur, @Description, @Statut)";

            SqlParameter[] parameter = new SqlParameter[8];

            if (string.IsNullOrEmpty(nom))
            {
                parameter[0] = new SqlParameter("@Nom", DBNull.Value);
            }
            else
            {
                parameter[0] = new SqlParameter("@Nom", SqlDbType.VarChar);
            }
            parameter[0].Value = nom;

            parameter[1] = new SqlParameter("@Date_debut", SqlDbType.Date);
            parameter[1].Value = date_debut;

            parameter[7] = new SqlParameter("@Date_fin", SqlDbType.Date);
            parameter[7].Value = date_fin;

            parameter[2] = new SqlParameter("@Heure_debut", SqlDbType.Time);
            parameter[2].Value = heureDebut;

            parameter[3] = new SqlParameter("@Heure_fin", SqlDbType.Time);
            parameter[3].Value = heureFin;

            if (string.IsNullOrEmpty(formateur))
            {
                parameter[4] = new SqlParameter("@Formateur", DBNull.Value);
            }
            else
            {
                parameter[4] = new SqlParameter("@Formateur", SqlDbType.VarChar);
            }
            parameter[4].Value = formateur;

            if (string.IsNullOrEmpty(description))
            {
                parameter[5] = new SqlParameter("@Description", DBNull.Value);
            }
            else
            {
                parameter[5] = new SqlParameter("@Description", SqlDbType.VarChar);
            }
            parameter[5].Value = description;

            if (string.IsNullOrEmpty(statut))
            {
                parameter[6] = new SqlParameter("@Statut", DBNull.Value);
            }
            else
            {
                parameter[6] = new SqlParameter("@Statut", SqlDbType.VarChar);
            }
            parameter[6].Value = statut;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierModule(int id,string nom, DateTime date_debut, DateTime date_fin, TimeSpan heuredebut, TimeSpan heureFin, string description, string formateur)
        {
            string query = "update Modules set Nom=@Nom, Date_debut=@Date_debut,Date_fin=@Date_fin, Heure_debut=@Heure_debut,Heure_fin=@Heure_fin, Formateur=@Formateur, Description=@Description where id=@id";

            SqlParameter[] parameter = new SqlParameter[8];

            if (string.IsNullOrEmpty(nom))
            {
                parameter[0] = new SqlParameter("@Nom", DBNull.Value);
                parameter[0].Value = nom;
            }
            else
            {
                parameter[0] = new SqlParameter("@Nom", SqlDbType.VarChar);
                parameter[0].Value = nom;
            }

            parameter[1] = new SqlParameter("@Date_debut", SqlDbType.Date);
            parameter[1].Value = date_debut;

            parameter[7] = new SqlParameter("@Date_fin", SqlDbType.Date);
            parameter[7].Value = date_fin;

            parameter[2] = new SqlParameter("@Heure_debut", SqlDbType.Time);
            parameter[2].Value = heuredebut;

            parameter[3] = new SqlParameter("@Heure_fin", SqlDbType.Time);
            parameter[3].Value = heureFin;


            if (string.IsNullOrEmpty(formateur))
            {
                parameter[4] = new SqlParameter("@Formateur", DBNull.Value);
                parameter[4].Value = formateur;
            }
            else
            {
                parameter[4] = new SqlParameter("@Formateur", SqlDbType.VarChar);
                parameter[4].Value = formateur;
            }

            if (string.IsNullOrEmpty(description))
            {
                parameter[5] = new SqlParameter("@Description", DBNull.Value);
                parameter[5].Value = description;
            }
            else
            {
                parameter[5] = new SqlParameter("@Description", SqlDbType.VarChar);
                parameter[5].Value = description;
            }

            parameter[6] = new SqlParameter("@id", SqlDbType.Int);
            parameter[6].Value = id;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listModules()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from Modules", null);
            return table;
        }

        public DataTable listModulesEnCoursOuTerminer()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("select * from Modules where Statut IN ('En cours', 'Terminé')", null);
            return table;
        }

        public Boolean EffaceModules(int id)
        {
            string query = "Delete from Modules where id = @id";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("@id", SqlDbType.Int);
            parameter[0].Value = id;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable getModulesbyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Modules where id = @id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public Boolean UpdateModule(int id,string statut)
        {
            string query = "update Modules set Statut=@Statut where id=@id";

            SqlParameter[] parameter = new SqlParameter[2];

            if (string.IsNullOrEmpty(statut))
            {
                parameter[0] = new SqlParameter("@Statut", DBNull.Value);
                parameter[0].Value = statut;
            }
            else
            {
                parameter[0] = new SqlParameter("@Statut", SqlDbType.VarChar);
                parameter[0].Value = statut;
            }
            parameter[1] = new SqlParameter("@id", SqlDbType.Int);
            parameter[1].Value = id;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
