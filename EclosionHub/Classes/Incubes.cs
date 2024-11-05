using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclosionHub.Classes
{
    class Incubes
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterIncube(string nom, string postnom, string prenom,string matricule, byte[] photo, string tel)
        {
            string query = "Insert into incubes (Nom, Postnom, Prenom, Matricule,Photo,Telephone) values (@Nom, @Postnom, @Prenom, @Matricule,@Photo,@Telephone)";

            SqlParameter[] parameter = new SqlParameter[6];

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

            if (string.IsNullOrEmpty(postnom))
            {
                parameter[1] = new SqlParameter("@Postnom", DBNull.Value);
                parameter[1].Value = postnom;
            }
            else
            {
                parameter[1] = new SqlParameter("@Postnom", SqlDbType.VarChar);
                parameter[1].Value = postnom;
            }

            if (string.IsNullOrEmpty(prenom))
            {
                parameter[2] = new SqlParameter("@Prenom", DBNull.Value);
                parameter[2].Value = prenom;
            }
            else
            {
                parameter[2] = new SqlParameter("@Prenom", SqlDbType.VarChar);
                parameter[2].Value = prenom;
            }

            if (string.IsNullOrEmpty(matricule))
            {
                parameter[3] = new SqlParameter("@Matricule", DBNull.Value);
                parameter[3].Value = matricule;
            }
            else
            {
                parameter[3] = new SqlParameter("@Matricule", SqlDbType.VarChar);
                parameter[3].Value = matricule;
            }


            parameter[4] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[4].Value = photo;

            if (string.IsNullOrEmpty(tel))
            {
                parameter[5] = new SqlParameter("@Telephone", DBNull.Value);
                parameter[5].Value = tel;
            }
            else
            {
                parameter[5] = new SqlParameter("@Telephone", SqlDbType.VarChar);
                parameter[5].Value = tel;
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

        public Boolean ModifierIncube(int id,string nom, string postnom, string prenom, string matricule, byte[] photo, string tel)
        {
            string query = "update incubes set Nom=@Nom, Postnom=@Postnom, Prenom=@Prenom, Matricule=@Matricule,Photo=@Photo, Telephone=@Telephone where id=@id";

            SqlParameter[] parameter = new SqlParameter[7];

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

            if (string.IsNullOrEmpty(postnom))
            {
                parameter[1] = new SqlParameter("@Postnom", DBNull.Value);
                parameter[1].Value = postnom;
            }
            else
            {
                parameter[1] = new SqlParameter("@Postnom", SqlDbType.VarChar);
                parameter[1].Value = postnom;
            }

            if (string.IsNullOrEmpty(prenom))
            {
                parameter[2] = new SqlParameter("@Prenom", DBNull.Value);
                parameter[2].Value = prenom;
            }
            else
            {
                parameter[2] = new SqlParameter("@Prenom", SqlDbType.VarChar);
                parameter[2].Value = prenom;
            }

            if (string.IsNullOrEmpty(matricule))
            {
                parameter[3] = new SqlParameter("@Matricule", DBNull.Value);
                parameter[3].Value = matricule;
            }
            else
            {
                parameter[3] = new SqlParameter("@Matricule", SqlDbType.VarChar);
                parameter[3].Value = matricule;
            }


            parameter[4] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[4].Value = photo;

            parameter[5] = new SqlParameter("@id", SqlDbType.Int);
            parameter[5].Value = id;

            if (string.IsNullOrEmpty(tel))
            {
                parameter[6] = new SqlParameter("@Telephone", DBNull.Value);
                parameter[6].Value = tel;
            }
            else
            {
                parameter[6] = new SqlParameter("@Telephone", SqlDbType.VarChar);
                parameter[6].Value = tel;
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

        public DataTable listIncubes()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from incubes", null);
            return table;
        }

        public Boolean EffaceIncube(int id)
        {
            string query = "Delete from Incubes where id = @id";
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

        public DataTable getIncubebyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from incubes where id = @id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable TopdernierDesignationtIncubes()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("select top 1 id from incubes order by Id DESC", null);
            return table;
        }

        public DataTable getCarteIncube(int id)
        {
            DataTable table = new DataTable();
            string query = "Select CONCAT(Incubes.Nom, ' ', Incubes.Postnom, ' ', Incubes.Prenom) as incubes, Incubes.Photo as photo, CodeQR.code as Code from CodeQR inner join Incubes on CodeQR.Incube=Incubes.Id where CodeQR.Incube=@ide";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ide", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable LireLesitems()
        {
            DataTable table = new DataTable();
            string query = @"SELECT
                I.Id,
                I.Nom AS Nom_Incube,
                I.Postnom,
                I.Prenom,
                E.Nom AS Projet,
                CONCAT(CAST(FORMAT((SUM(CASE WHEN P.Present ='Oui' THEN 1 ELSE 0 END) * 100.0 / NULLIF(COUNT(*), 0)), 'N2') AS DECIMAL(10,2)), ' %') AS P_Participation_Total,
                I.Photo
            FROM
                dbo.Incubes I
            JOIN
                dbo.Presence P ON I.Id = P.Incube
            JOIN dbo.Entreprise E on E.Proprietaire = I.Id
            GROUP BY
                I.Nom, E.Nom,I.Postnom,I.Prenom,I.Id,I.Photo";
            table = connexion.getdata(query, null);
            return table;
        }
    }
}
