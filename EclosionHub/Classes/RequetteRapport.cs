using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclosionHub.Classes
{
    class RequetteRapport
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public DataTable GetRapportParticipationTousModule()
        {
            DataTable table = new DataTable();
            string query = @"SELECT
                Id AS ModuleId,
                Nom AS NomModule,
                CAST(DATEDIFF(DAY, Date_debut, GETDATE()) + 1 AS NVARCHAR(MAX)) + ' jour' AS Session,
                CASE
                    WHEN GETDATE() BETWEEN Date_debut AND Date_fin THEN Date_debut
                    WHEN Date_debut = CONVERT(DATE, GETDATE()) THEN Date_debut
                    ELSE NULL
                END AS Date
            FROM
                Modules
            WHERE
                (Date_debut <= GETDATE() AND GETDATE() <= Date_fin)
                OR (Date_debut = CONVERT(DATE, GETDATE()));
            ";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable GetModulesTerminer()
        {
            DataTable table = new DataTable();
            string query = "SELECT Id AS ModuleId FROM Modules WHERE Date_fin < GETDATE() and Statut<>'Terminé'";
            table = connexion.getdata(query, null);
            return table;
        }
        public DataTable UpdateStatutModule()
        {
            DataTable table = new DataTable();
            string query = "update Modules set Statut ='Terminer' where Date_fin <GETDATE();";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable GetSessionJourParModule(int IdModule)
        {
            DataTable table = new DataTable();
            string query = @"Select id, Nom, Heure_debut, Heure_fin from Modules where Id=@idmodule";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@idmodule", SqlDbType.Int);
            parameters[0].Value = IdModule;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetDateSessionJourParModule(int idmodule, string jourSession)
        {
            DataTable table = new DataTable();
            string query = @"WITH Dates AS (
                SELECT
                    Id AS ModuleId,
                    Nom AS NomModule,
                    Date_debut AS SessionDate,
                    DATEDIFF(DAY, Date_debut, Date_fin) + 1 AS SessionDuration
                FROM
                    dbo.Modules
            )
            , NumberedSessions AS (
                SELECT
                    ModuleId,
                    NomModule,
                    'Jour ' + CAST(ROW_NUMBER() OVER (PARTITION BY ModuleId ORDER BY SessionDate) AS NVARCHAR(MAX)) AS J_Session,
                    CONVERT(VARCHAR, DATEADD(DAY, RN - 1, SessionDate), 103) AS Date_Seance
                FROM
                    Dates
                CROSS APPLY (
                    SELECT TOP (SessionDuration) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RN
                    FROM master.dbo.spt_values
                ) AS Numbers
            )
            SELECT *
            FROM NumberedSessions
            WHERE ModuleId = @idmodule AND J_Session = @JourSession
            ORDER BY
                ModuleId, Date_Seance, J_Session;";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@idmodule", SqlDbType.Int);
            parameters[0].Value = idmodule;
            parameters[1] = new SqlParameter("@JourSession", SqlDbType.VarChar);
            parameters[1].Value = jourSession;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable CheckModuleASyncroniser(int module,DateTime date)
        {
            DataTable table = new DataTable();
            string query = @"SELECT Module, Date_Scan
                FROM Presence
                WHERE Module = @Module
                      AND  Date_Scan = @DateScan; ";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Module", SqlDbType.Int);
            parameters[0].Value = module;
            parameters[1] = new SqlParameter("@DateScan", SqlDbType.DateTime);
            parameters[1].Value = date;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public Boolean AjouterPresenceSyncroniser(int incube, int module, string presence, TimeSpan heure_arrive, TimeSpan heure_sortie, DateTime datescan)
        {
            string query = "Insert into Presence(Incube, module, present, heure_arrive, heure_sortie, Date_Scan) values (@incube, @module, @present, @heure_arrive, @heure_sortie, @Date_Scan)";

            SqlParameter[] parameter = new SqlParameter[6];

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
                parameter[2].Value = presence;
            }

            parameter[3] = new SqlParameter("@heure_arrive", SqlDbType.Time);
            parameter[3].Value = heure_arrive;

            parameter[4] = new SqlParameter("@heure_sortie", SqlDbType.Time);
            parameter[4].Value = heure_sortie;  // Correction ici

            parameter[5] = new SqlParameter("@Date_Scan", SqlDbType.DateTime);
            parameter[5].Value = datescan;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetIncubesEtProjetParProjet(string projet)
        {
            DataTable table = new DataTable();
            string query = @"Select Incubes.Id as id, CONCAT(Incubes.Nom,' ', Incubes.Postnom, ' ', Incubes.Prenom) as Incube, Entreprise.Nom from Incubes inner join Entreprise on Entreprise.Proprietaire=Incubes.Id where Entreprise.Nom=@Projet";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Projet", SqlDbType.VarChar);
            parameters[0].Value = projet;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetTousIncubes()
        {
            DataTable table = new DataTable();
            string query = "SELECT Id FROM Incubes";
            table = connexion.getdata(query, null);
            return table;
        }
        public DataTable GetEntrepriseByNom(string projet)
        {
            DataTable table = new DataTable();
            string query = "select id from Entreprise where nom=@Projet";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Projet", SqlDbType.VarChar);
            parameters[0].Value = projet;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetNomIncubeByID(int id)
        {
            DataTable table = new DataTable();
            string query = "select Nom from Incubes where id=@id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetNomConcIncubeByID(int id)
        {
            DataTable table = new DataTable();
            string query = "select CONCAT(Incubes.Nom,' ', Incubes.Postnom, ' ', Incubes.Prenom) as Incube from Incubes where id=@id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
        public void ExecuteQuery(string query)
        {
            // Utilisez votre logique pour exécuter la requête ici
            // Assurez-vous de gérer la connexion, la création de la commande, etc.
            // Exemple basique :
            using (SqlCommand command = new SqlCommand(query, connexion.getconnexion()))
            {
                command.ExecuteNonQuery();
            }
        }

    }
}
