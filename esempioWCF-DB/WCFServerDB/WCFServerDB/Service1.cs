using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1 {

        private SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

        public void openDbConnection()
        {
            try
            {
                conn.Open();
                Console.WriteLine("ok - {0}", conn.State);
            }
            catch (Exception e)
            {
                Console.WriteLine("Errore: " + e.Message);
            }
        }

        public void closeDbConnection()
        {
            try
            {
                conn.Close();
                Console.WriteLine("ok - {0}", conn.State);
            }
            catch (Exception e)
            {
                Console.WriteLine("Errore: " + e.Message);
            }
        }

        public bool Login(string username, string password)
        {
            try
            {
                openDbConnection();

                using (SqlCommand command1 = conn.CreateCommand())
                {
                    command1.CommandText = "SELECT * FROM Account WHERE username = '" + username + "' AND password = '" + password + "';";
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        var counter = 0;
                        while (reader.Read())
                        {
                            Console.WriteLine("Username: " + reader.GetString(0));
                            Console.WriteLine("Password: " + reader.GetString(1));
                            counter++;
                        }

                        if (counter > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                closeDbConnection();
            }

            return false;
        }

        public int GetPrivilegi(string username)
        {
            try
            {
                openDbConnection();

                using (SqlCommand command1 = conn.CreateCommand())
                {
                    command1.CommandText = "SELECT privilegi FROM Account WHERE username = '" + username + "';";
                    var privilegiString = (string)command1.ExecuteScalar();
                    switch (privilegiString.ToUpper())
                    {
                        case "ADMIN":
                            return (int)Privilegi.admin;

                        case "IMPIEGATO":
                            return (int)Privilegi.impiegato;

                        case "CLIENTE":
                            return (int)Privilegi.cliente;
                          
                        default:
                            return (int)Privilegi.unknown;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                closeDbConnection();
            }

            return (int)Privilegi.unknown;
        }

        public ContoCorrente SelectContoCorrente(int idContoCorrente) {
            ContoCorrente returnConto = null;

            using (SqlConnection conn = new SqlConnection("server=DESKTOP-QI7GBPD\\SQLEXPRESS;database=wcfDatabase;integrated security=sspi")) {
                conn.Open();

                using (SqlCommand command1 = conn.CreateCommand()) {
                    command1.CommandText = "SELECT * FROM ContoCorrente WHERE ID =" + idContoCorrente;

                    using (SqlDataReader reader = command1.ExecuteReader()) {
                        Console.WriteLine("Scansione risultati...");
                        int recordIdx = 1;
                        while (reader.Read()) {
                            // Lettura campi singolo record
                            Console.WriteLine("Axcquisizione campi record #{0}...", recordIdx++);

                            String nome = reader.GetString(0);
                            String cognome = reader.GetString(1);
                            int id = reader.GetInt32(2);
                            decimal saldo = reader.GetDecimal(3);

                            returnConto = new ContoCorrente(nome, cognome, id, (double)saldo);

                        }
                    } // Close DataReader
                }
            }

            return returnConto;
        }
    }
      
}

