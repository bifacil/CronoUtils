using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronoUtils
{
   public class SqlClient
    {


        private SqlClient()
        {
        }


        static private string Server;
        static private string User;
        static private string Password;
        static private string Database;

       
        static public void SetDatabase(string server,string database)
        {
            Server = server;
            Database = database;
        }

        static public void SetWindowsCredentials()
        {
            User = null;
            Password = null;
        }

        static public void SetCredentials(string user,string password)
        {
            User=user;
            Password=password;
        }


        static private System.Data.SqlClient.SqlConnection Connection()
        {
            var csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.DataSource = Server;
            csb.InitialCatalog = Database;
            if (User == null)
            {
                csb.IntegratedSecurity = true;
            }
            else
            {
                csb.IntegratedSecurity = false;
                csb.UserID = User;
                csb.Password = Password;
            }
            var conn= new System.Data.SqlClient.SqlConnection(csb.ToString());
            conn.Open();
            return conn;
        }




        
        public static string ExecuteProcedure(string name)
        {
            var s = new CronoUtils.SqlClient();
            return s.ExecuteNonQuery("EXECUTE " + name);
        }



        public static void ExecuteProcedureAndMailResults(string name, string subject)
        {

            string info;
            try
            {
                info = ExecuteProcedure(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                info = e.Message;
                subject = "[ERROR] " + subject;
            }

            CronoUtils.Mail.SendMail(subject, info);
        }


        public static void ExecuteProcedureAndMailMarkdownResults(string name, string subject)
        {
            string info;
              try
            {
                info = ExecuteProcedure(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                info = e.Message;
                subject = "[ERROR] " + subject;
            }

            CronoUtils.Mail.SendMarkdownMail(subject,info);
            
            
        }

        private static StringBuilder log;

        private string ExecuteNonQuery(string sql)
        {
            log = new StringBuilder();
            var Conn = Connection();
            Conn.InfoMessage += new System.Data.SqlClient.SqlInfoMessageEventHandler(InfoMessage);

            var Command = new System.Data.SqlClient.SqlCommand(sql, Conn);
            Command.CommandTimeout = 0;
            Command.ExecuteNonQuery();
            return log.ToString();
        }

        private void InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
            log.AppendLine(e.Message);
        }


    }
}
