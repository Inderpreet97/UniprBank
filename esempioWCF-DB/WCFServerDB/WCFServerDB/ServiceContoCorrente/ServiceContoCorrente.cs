using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceContoCorrente" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceContoCorrente : IServiceContoCorrente
    {
        public ContoCorrente SelectContoCorrente(int idContoCorrente)
        {
            ContoCorrente returnConto = null;

            using (SqlConnection conn = new SqlConnection("server=DESKTOP-QI7GBPD\\SQLEXPRESS;database=wcfDatabase;integrated security=sspi"))
            {
                conn.Open();

                using (SqlCommand command1 = conn.CreateCommand())
                {
                    command1.CommandText = "SELECT * FROM ContoCorrente WHERE ID =" + idContoCorrente;

                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        Console.WriteLine("Scansione risultati...");
                        int recordIdx = 1;
                        while (reader.Read())
                        {
                            // Lettura campi singolo record
                            Console.WriteLine("Axcquisizione campi record #{0}...", recordIdx++);

                            String nome = reader.GetString(0);
                            String cognome = reader.GetString(1);
                            int id = reader.GetInt32(2);
                            decimal saldo = reader.GetDecimal(3);

                            returnConto = new ContoCorrente();

                        }
                    } // Close DataReader
                }
            }

            return returnConto;
        }
    }
}
