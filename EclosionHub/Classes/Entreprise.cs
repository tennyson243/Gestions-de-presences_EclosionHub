using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclosionHub.Classes
{
    class Projet
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterEntreprise(string nom, int proprietaire, string secteur, string description)
        {
            string query = "Insert into Entreprise (Nom, Proprietaire, Secteur, Description) values (@Nom, @Proprietaire, @Secteur, @Description)";

            SqlParameter[] parameter = new SqlParameter[4];

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

                parameter[1] = new SqlParameter("@Proprietaire", SqlDbType.Int);
                parameter[1].Value = proprietaire;

            if (string.IsNullOrEmpty(secteur))
            {
                parameter[2] = new SqlParameter("@Secteur", DBNull.Value);
                parameter[2].Value = secteur;
            }
            else
            {
                parameter[2] = new SqlParameter("@Secteur", SqlDbType.VarChar);
                parameter[2].Value = secteur;
            }

            if (string.IsNullOrEmpty(description))
            {
                parameter[3] = new SqlParameter("@Description", DBNull.Value);
                parameter[3].Value = description;
            }
            else
            {
                parameter[3] = new SqlParameter("@Description", SqlDbType.VarChar);
                parameter[3].Value = description;
            }

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierEntreprise(int id,string nom, int proprietaire, string secteur, string description)
        {
            string query = "update Entreprise set Nom=@Nom, Proprietaire=@Proprietaire, Secteur=@Secteur, Description=@Description where id = @id";

            SqlParameter[] parameter = new SqlParameter[5];

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

            parameter[1] = new SqlParameter("@Proprietaire", SqlDbType.Int);
            parameter[1].Value = proprietaire;

            if (string.IsNullOrEmpty(secteur))
            {
                parameter[2] = new SqlParameter("@Secteur", DBNull.Value);
                parameter[2].Value = secteur;
            }
            else
            {
                parameter[2] = new SqlParameter("@Secteur", SqlDbType.VarChar);
                parameter[2].Value = secteur;
            }

            if (string.IsNullOrEmpty(description))
            {
                parameter[3] = new SqlParameter("@Description", DBNull.Value);
                parameter[3].Value = description;
            }
            else
            {
                parameter[3] = new SqlParameter("@Description", SqlDbType.VarChar);
                parameter[3].Value = description;
            }

                parameter[4] = new SqlParameter("@id", SqlDbType.Int);
                parameter[4].Value = id;  

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listEntreprise()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from Entreprise", null);
            return table;
        }

        public DataTable listEntrepriseConcatener()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("SELECT Entreprise.Id as Id, Entreprise.Nom as Nom, CONCAT(Incubes.nom, ' ', Incubes.postnom, ' ', Incubes.Prenom) as proprietaire, Entreprise.secteur from Entreprise inner join Incubes on Entreprise.Proprietaire = Incubes.Id;", null);
            return table;
        }

        public Boolean EffaceEntreprise(int id)
        {
            string query = "Delete from Entreprise where id = @id";
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

        public DataTable getEntreprisebyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Entreprise where id = @id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}
