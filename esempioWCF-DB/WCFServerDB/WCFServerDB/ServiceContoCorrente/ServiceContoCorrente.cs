using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    command.CommandText = "SELECT IBAN, username, saldo, idFiliale FROM ContoCorrente WHERE idContoCorrente = @idContoCorrente";

                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters["@idContoCorrente"].Value = idContoCorrente;

                    ContoCorrente contoCorrente = new ContoCorrente();

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            contoCorrente.idContoCorrente = idContoCorrente;
                            contoCorrente.IBAN = reader.GetString(0);
                            contoCorrente.username = reader.GetString(1);
                            contoCorrente.saldo = reader.GetDecimal(2);
                            contoCorrente.idFiliale = reader.GetString(3);
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return contoCorrente;
                }
                catch (Exception ex) {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try {
                        transaction.Rollback();
                    }
                    catch (Exception ex2) {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    return new ContoCorrente();
                }
            }
        }
    }
}
