using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace CronoUtilsTest
{
    [TestClass]
    public class UnitTest1
    {
      
        private void SetMail(){
            var user = ConfigurationManager.AppSettings["user"];
            var pass = ConfigurationManager.AppSettings["password"];
            var toAdd = ConfigurationManager.AppSettings["to"];
         
            var username = String.Format("{0}@gmail.com",user);
            var username2 = String.Format("{0}+1@gmail.com", toAdd);
            var username3 = String.Format("{0}+2@gmail.com", toAdd);

            CronoUtils.Mail.SetGMailCredentials(username, pass);
            CronoUtils.Mail.SetFromAddress(username, "Crono.Utils");
            CronoUtils.Mail.SetToAddresses(username2,username3);
         

        }


        [TestMethod]
        public void SendMail()
        {
            SetMail();
            CronoUtils.Mail.SendMail("PRUEBA1", "CONTENIDO");
            CronoUtils.Mail.SendMail("PRUEBA2", "CONTENIDO2");
            CronoUtils.Mail.SendHtmlMail("PRUEBA3", "<h1>CONTENIDO2</h1>");
            CronoUtils.Mail.SendMarkdownMail("PRUEBA4", "# Esto es un **titulo** \n \n Y esto **no**" );
            Assert.AreEqual(1, 1);           
        }

        [TestMethod]
        public void ExecuteProcedure()
        {

            var server = ConfigurationManager.AppSettings["server"];
            var db = ConfigurationManager.AppSettings["database"];
            var user = ConfigurationManager.AppSettings["usersql"];
            var pass = ConfigurationManager.AppSettings["passsql"];
       
            CronoUtils.SqlClient.SetDatabase(server,db);
            CronoUtils.SqlClient.SetCredentials(user,pass);
            CronoUtils.SqlClient.ExecuteProcedure("stg.CARGAR_EXTRACCIONES");
        
        }


        [TestMethod]
        public void ExecuteProcedureAndMail()
        {

            SetMail();
            var server = ConfigurationManager.AppSettings["server"];
            var db = ConfigurationManager.AppSettings["database"];
            var usersql = ConfigurationManager.AppSettings["usersql"];
            var passsql = ConfigurationManager.AppSettings["passsql"];

            CronoUtils.SqlClient.SetDatabase(server, db);
            CronoUtils.SqlClient.SetCredentials(usersql, passsql);
            CronoUtils.SqlClient.ExecuteProcedureAndMailMarkdownResults("stg.CARGAR_EXTRACCIONES", "Fin carga DWH FINSA");

        }

    }
}
