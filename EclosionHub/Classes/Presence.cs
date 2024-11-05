using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclosionHub.Classes
{
    class Presence
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterPresence(int incube, int module, string presence, TimeSpan heure_arrive, TimeSpan heure_sortie)
        {
            string query = "Insert into Presence(Incube, module, present, heure_arrive, heure_sortie, Date_Scan) values (@incube, @module, @present, @heure_arrive, @heure_sortie,CONVERT(DATE, GETDATE()))";

            SqlParameter[] parameter = new SqlParameter[5];

            parameter[0] = new SqlParameter("@Incube", SqlDbType.Int);
            parameter[0].Value = incube;

            parameter[1] = new SqlParameter("@module", SqlDbType.Int);
            parameter[1].Value = module;

            if (string.IsNullOrEmpty(presence))
            {
                parameter[2] = new SqlParameter("@present", DBNull.Value);
            }
            else
            {
                parameter[2] = new SqlParameter("@present", SqlDbType.VarChar);
            }
            parameter[2].Value = presence;

            parameter[3] = new SqlParameter("@heure_arrive", SqlDbType.Time);
            parameter[3].Value = heure_arrive;

            parameter[4] = new SqlParameter("@heure_sortie", SqlDbType.Time);
            parameter[4].Value = heure_arrive;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierPresence(int id, int incube, int module, byte[] presence, DateTime heure_arrive, DateTime heure_sortie)
        {
            string query = "update Presence set (incube=@incube, module=@module, presence=@presence, heure_arrive=@heure_arrive, heure_sortie=@heure_sortie where id = @id";

            SqlParameter[] parameter = new SqlParameter[6];

            parameter[0] = new SqlParameter("@Incube", SqlDbType.Int);
            parameter[0].Value = incube;

            parameter[1] = new SqlParameter("@module", SqlDbType.Int);
            parameter[1].Value = module;

            parameter[2] = new SqlParameter("@presence", SqlDbType.Bit);
            parameter[2].Value = presence;

            parameter[3] = new SqlParameter("@heure_arrive", SqlDbType.Time);
            parameter[3].Value = heure_arrive;

            parameter[4] = new SqlParameter("@heure_sortie", SqlDbType.Time);
            parameter[4].Value = heure_arrive;

            parameter[5] = new SqlParameter("@id", SqlDbType.Int);
            parameter[5].Value = id;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listPresence()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from Presence", null);
            return table;
        }

        public DataTable listPresenceConcathener()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("select Presence.id as Id, CONCAT(Incubes.Nom,' ',Incubes.Postnom,' ',Incubes.Prenom) as Incube, Entreprise.Nom as Projet, Presence.heure_arrive as arrive, Presence.heure_sortie as sortie from Presence inner join Incubes  on Presence.Incube=Incubes.Id inner join Entreprise on Entreprise.Proprietaire=Incubes.Id", null);
            return table;
        }

        public Boolean EffacePresence(int id)
        {
            string query = "Delete from Presence where id = @id";
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

        public DataTable GetLive(string nom)
        {
            DataTable table = new DataTable();
            string query = "select Presence.id as Id, CONCAT(Incubes.Nom,' ',Incubes.Postnom,' ',Incubes.Prenom) as Incube, Incubes.Photo as photo, Entreprise.Nom as Projet, Presence.heure_arrive as arrive, Presence.heure_sortie as sortie from Presence inner join Incubes  on Presence.Incube=Incubes.Id inner join Entreprise on Entreprise.Proprietaire=Incubes.Id where CONCAT(Incubes.Nom,' ',Incubes.Postnom,' ',Incubes.Prenom)=@Nom";
            SqlParameter[] parameter = new SqlParameter[1];

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
            table = connexion.getdata(query, parameter);
            return table;
        }

        public Boolean ModifierHeureArrive(int id, int module)
        {
            string query = "UPDATE Presence SET heure_arrive = CONVERT(TIME, GETDATE()), Present='Oui' WHERE Incube = @id and Module = @module and CONVERT(DATE, Date_Scan) = CONVERT(DATE, GETDATE());";
            SqlParameter[] parameter = new SqlParameter[2];

            parameter[0] = new SqlParameter("@id", SqlDbType.Int);
            parameter[0].Value = id;

            parameter[1] = new SqlParameter("@module", SqlDbType.Int);
            parameter[1].Value = module;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierHeureDepart(int id, int module)
        {
            string query = "UPDATE Presence SET heure_sortie = CONVERT(TIME, GETDATE()) WHERE Incube = @id and Module = @module and CONVERT(DATE, Date_Scan) = CONVERT(DATE, GETDATE());";
            SqlParameter[] parameter = new SqlParameter[2];

            parameter[0] = new SqlParameter("@id", SqlDbType.Int);
            parameter[0].Value = id;

            parameter[1] = new SqlParameter("@module", SqlDbType.Int);
            parameter[1].Value = module;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Justification(int id, string justification)
        {
            string query = "UPDATE Presence SET Justification = @justification WHERE Id = @id";
            SqlParameter[] parameter = new SqlParameter[2];

            parameter[0] = new SqlParameter("@id", SqlDbType.Int);
            parameter[0].Value = id;

            parameter[1] = new SqlParameter("@justification", SqlDbType.VarChar);
            parameter[1].Value = justification;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable getPresencebyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Presence where id = @id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable CheckPresence(int incube, int module)
        {
            DataTable table = new DataTable();
            string query = "select present,Date_Scan from Presence where Incube =@Incube and Module=@Module and CONVERT(DATE, Date_Scan) = CONVERT(DATE, GETDATE())";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Incube", SqlDbType.Int);
            parameters[0].Value = incube;
            parameters[1] = new SqlParameter("@Module", SqlDbType.Int);
            parameters[1].Value = module;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable CheckModule(int incube, int module)
        {
            DataTable table = new DataTable();
            string query = "select Incube, Module,Date_Scan from Presence where Incube=@Incube and Module=@Module and CONVERT(DATE, Date_Scan) = CONVERT(DATE, GETDATE())";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Incube", SqlDbType.Int);
            parameters[0].Value = incube;
            parameters[1] = new SqlParameter("@Module", SqlDbType.Int);
            parameters[1].Value = module;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable AfficherPresence(int module)
        {
            DataTable table = new DataTable();
            string query = "select CONCAT( Incubes.Nom, ' ', Incubes.Postnom, ' ', Incubes.Prenom) as Incubes,Entreprise.Nom as Projet, Modules.Nom as Module, Presence.Present, Presence.heure_arrive, Presence.heure_sortie from Presence inner join Incubes on Presence.Incube=Incubes.Id inner join Modules on Modules.Id=Presence.Module inner join Entreprise on Entreprise.Proprietaire=Incubes.Id where Presence.Present='Oui' and Presence.Module=@idmodule and CONVERT(DATE, Date_Scan) = CONVERT(DATE, GETDATE())";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@idmodule", SqlDbType.Int);
            parameters[0].Value = module;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable RapportDePresenceParModule(string module)
        {
            DataTable table = new DataTable();
            string query = "select * from RapportPresenceParModule where Nom_Module = @nomModule";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@nomModule", SqlDbType.VarChar);
            parameters[0].Value = module;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable RapportDePresenceParIncube(string Incube)
        {
            DataTable table = new DataTable();
            string query = "select * from RapportPresenceParModule where Nom_Incube =@nomincube";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@nomincube", SqlDbType.VarChar);
            parameters[0].Value = Incube;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable RapportDePresenceParProjet(string Projet)
        {
            DataTable table = new DataTable();
            string query = "select * from RapportPresenceParModule where Entreprise =@NomProjet";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@NomProjet", SqlDbType.VarChar);
            parameters[0].Value = Projet;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetRapportDePresenceParModuleEtIncubes(string Module, string incube)
        {
            DataTable table = new DataTable();
            string query = @"
        SELECT
            CONCAT(I.Nom, ' ', I.Postnom, ' ', I.Prenom) AS Incube,
            E.Nom AS Entreprise,
            M.Nom AS Module,
            DATEDIFF(DAY, M.Date_debut, M.Date_fin) + 1 AS Tot_Sessions,
            SUM(CASE WHEN P.Present = 'Oui' THEN 1 ELSE 0 END) AS Presences,
            SUM(CASE WHEN P.Present = 'Non' THEN 1 ELSE 0 END) AS Absences,
            CASE
                WHEN COUNT(P.Id) > 0 THEN (SUM(CASE WHEN P.Present = 'Oui' THEN 1 ELSE 0 END) * 100.0 / (DATEDIFF(DAY, M.Date_debut, M.Date_fin) + 1))
                ELSE 0
            END AS Pourcentage,
             I.Photo AS Photo
        FROM
            Incubes I
            INNER JOIN Entreprise E ON E.Proprietaire = I.Id
            CROSS JOIN Modules M
            LEFT JOIN Presence P ON I.Id = P.Incube AND M.Id = P.Module
        WHERE
            M.Nom = @NomModule and I.Nom=@NomIncube
        GROUP BY
           I.Id, I.Nom, I.Postnom, I.Prenom, E.Nom, M.Id, M.Nom, M.Date_debut, M.Date_fin, I.Photo";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@NomModule", SqlDbType.VarChar);
            parameters[0].Value = Module;
            parameters[1] = new SqlParameter("@NomIncube", SqlDbType.VarChar);
            parameters[1].Value = incube;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetRapportObservationParModule(string Module)
        {
            DataTable table = new DataTable();
            string query = "select * from VueObservationsIncubes where N_Module=@NomModule";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@NomModule", SqlDbType.VarChar);
            parameters[0].Value = Module;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetRapportObservationIncubesParDate(string debut, string Fin)
        {
            DataTable table = new DataTable();
            string query = "select * from VueObservationsIncubes where D_Scan between @Debut and @Fin";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Debut", SqlDbType.VarChar);
            parameters[0].Value = debut;

            parameters[1] = new SqlParameter("@Fin", SqlDbType.VarChar);
            parameters[1].Value = Fin;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetRapportParticipationByModule(string Module)
        {
            DataTable table = new DataTable();
            string query = "select * from RapportParticipationTotalParModule where Nom_Module =@nommodule";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@nommodule", SqlDbType.VarChar);
            parameters[0].Value = Module;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetRapportParticipationTousModule()
        {
            DataTable table = new DataTable();
            string query = "select * from RapportParticipationTotalParModule";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable TexteBoxRechercherModuleModule(string Module)
        {
            DataTable table = new DataTable();
            string query = "SELECT * FROM RapportParticipationTotalParModule WHERE Nom_Module LIKE '%' + @nommodule + '%'";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@nommodule", SqlDbType.VarChar);
            parameters[0].Value = Module;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetRapportPartiSessionModule(string Module)
        {
            DataTable table = new DataTable();
            string query = "select * from RapportPresenceParSessionParModule where Nom_Module=@nommodule";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@nommodule", SqlDbType.VarChar);
            parameters[0].Value = Module;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetRapportEligibilite()
        {
            DataTable table = new DataTable();
            string query = "select * from Eligibilite";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable GetGroupeDate()
        {
            DataTable table = new DataTable();
            string query = "select CONVERT(varchar,Date_Scan,103) as Date  from Presence group by CONVERT(varchar,Date_Scan,103)";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable GetGroupeJustification()
        {
            DataTable table = new DataTable();
            string query = "select Justification as Justification  from Presence group by Justification";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable GetGroupeincubeConcatener()
        {
            DataTable table = new DataTable();
            string query = "select CONCAT( Incubes.Nom, ' ', Incubes.Postnom, ' ', Incubes.Prenom) as Incube from Incubes group by CONCAT( Incubes.Nom, ' ', Incubes.Postnom, ' ', Incubes.Prenom)";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable GetEligibilitePartiParIncubes(string incubes)
        {
            DataTable table = new DataTable();
            string query = "select * from Eligibilite where Nom_Incube=@nomincube";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@nomincube", SqlDbType.VarChar);
            parameters[0].Value = incubes;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetEligibilitePartiParProjet(string projet)
        {
            DataTable table = new DataTable();
            string query = "select * from Eligibilite where Projet = @Projet";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Projet", SqlDbType.VarChar);
            parameters[0].Value = projet;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetPresencePartIncube(string incube)
        {
            DataTable table = new DataTable();
            string query = "select * from VuePresence where Incubes=@Incube";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Incube", SqlDbType.VarChar);
            parameters[0].Value = incube;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetPresencePartProjet(string projet)
        {
            DataTable table = new DataTable();
            string query = "select * from VuePresence where projet=@Incube";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Incube", SqlDbType.VarChar);
            parameters[0].Value = projet;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetPresencePartModule(string module)
        {
            DataTable table = new DataTable();
            string query = "select * from VuePresence where Module=@Incube";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Incube", SqlDbType.VarChar);
            parameters[0].Value = module;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetPresencePartParticipation(string participation)
        {
            DataTable table = new DataTable();
            string query = "select * from VuePresence where Justification=@Incube";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Incube", SqlDbType.VarChar);
            parameters[0].Value = participation;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetPresencePartDate(string Date)
        {
            DataTable table = new DataTable();
            string query = "select * from VuePresence where Date=@Incube";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Incube", SqlDbType.VarChar);
            parameters[0].Value = Date;

            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetPresencePartJustification(string Justification)
        {
            DataTable table = new DataTable();
            string query = "select * from VuePresence where Justification=@Incube";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Incube", SqlDbType.VarChar);
            parameters[0].Value = Justification;

            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}
