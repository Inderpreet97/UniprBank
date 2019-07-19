using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceAccountPersona" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceAccountPersona : IServiceAccountPersona
    {
        public bool Login(string username, string password)
        {
            try
            {
                

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
                
            }

            return false;
        }

        public int GetPrivilegi(string username)
        {
            try
            {
                

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

            }

            return (int)Privilegi.unknown;
        }
    }
}
