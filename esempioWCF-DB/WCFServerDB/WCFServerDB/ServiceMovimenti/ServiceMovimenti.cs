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
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceMovimenti" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceMovimenti : IServiceMovimenti
    {
        public bool CheckImporto(decimal importo, string IBANCommittente) {
            throw new NotImplementedException();
        }

        public bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal Importo) {
            throw new NotImplementedException();
        }

        public bool EseguiDeposito(int idContoCorrente, decimal importo) {
            throw new NotImplementedException();
        }

        public bool EseguiPrelievoDenaro(int idContoCorrente, decimal importo) {
            throw new NotImplementedException();
        }

        public List<Movimento> GetListaMovimenti(int idContoCorrente) {
            List<Movimento> listaMovimenti = new List<Movimento>() { };

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
                    command.CommandText = "SELECT idMovimenti, IBANCommittente, tipo, importo, IBANBeneficiario, dataOra "
                         + "FROM Movimenti, ContoCorrente "
                         + "WHERE (IBANCommittente = IBAN OR IBANBeneficiario = IBAN)"
                         + "AND idContoCorrente = @idContoCorrente";

                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters["@idContoCorrente"].Value = idContoCorrente;

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {

                            var idMovimenti = reader.GetInt32(0);
                            var IBANCommittente = reader.GetString(1);
                            var tipo = reader.GetString(2);
                            var importo = reader.GetDecimal(3);
                            var IBANBeneficiario = reader.GetString(4);
                            var dataOra = reader.GetDateTime(5);

                            listaMovimenti.Add(new Movimento(idMovimenti, IBANCommittente, tipo, importo, IBANBeneficiario, dataOra));
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return listaMovimenti;
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
                    return new List<Movimento>() { };
                }
            }
        }
    }
}
