using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclosionHub.Classes
{
    class codeQR
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterCodeQR(int proprietaire, byte[] code)
        {
            string query = "Insert into CodeQR (Incube, code) values (@Incube, @code)";

            SqlParameter[] parameter = new SqlParameter[2];

            parameter[0] = new SqlParameter("@Incube", SqlDbType.Int);
            parameter[0].Value = proprietaire;

            parameter[1] = new SqlParameter("@code", SqlDbType.Binary);
            parameter[1].Value = code;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierCodeQR(int id, int proprietaire, byte[] code)
        {
            string query = "update CodeQR set Incube=@Incube, code=@code where id = @id";

            SqlParameter[] parameter = new SqlParameter[3];

            parameter[0] = new SqlParameter("@Incube", SqlDbType.Int);
            parameter[0].Value = proprietaire;

            parameter[1] = new SqlParameter("@code", SqlDbType.Binary);
            parameter[1].Value = code;

            parameter[2] = new SqlParameter("@id", SqlDbType.Int);
            parameter[2].Value = id;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listCodeQr()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from CodeQR", null);
            return table;
        }

        public Boolean EffaceCodeQR(int id)
        {
            string query = "Delete from CodeQR where id = @id";
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

        public DataTable getCodeQRbyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from CodeQR where id = @id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}
