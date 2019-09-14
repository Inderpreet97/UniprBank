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
        public ContoCorrente SelectContoCorrente(UInt64 idContoCorrente)
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

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo SelectContoCorrente ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@idContoCorrente = {0}", idContoCorrente);
                    }

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            contoCorrente.idContoCorrente = idContoCorrente;
                            contoCorrente.IBAN = reader.GetString(0);
                            contoCorrente.username = reader.GetString(1);
                            contoCorrente.saldo = reader.GetDecimal(2);
                            contoCorrente.idFiliale = reader.GetString(3);
                        }
                    }

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", contoCorrente.IBAN);
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
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }

        public bool CheckIBAN(string IBAN) {
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

                    command.CommandText = "SELECT count(idContoCorrente) FROM ContoCorrente WHERE IBAN = @IBAN;";
                    command.Parameters.Add("@IBAN", SqlDbType.VarChar);
                    command.Parameters["@IBAN"].Value = IBAN; ;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo CheckIBAN ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@IBAN = {0}", IBAN);
                    }

                    int? conti = (Nullable<int>)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", conti);
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    if (conti == 1) return true;
                    else return false;

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
                    return false;
                }
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }

        public bool CheckIDConto(UInt64 idContoCorrente) {
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

                    command.CommandText = "SELECT count(idContoCorrente) FROM ContoCorrente WHERE idContoCorrente = @idContoCorrente;";
                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters["@idContoCorrente"].Value = idContoCorrente; ;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo CheckIDConto ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@idContoCorrente = {0}", idContoCorrente);
                    }

                    int? conti = (Nullable<int>)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", conti);
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    if (conti == 1) return true;
                    else return false;

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
                    return false;
                }
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }

        public bool AggiungiContoCorrente(string username, string idFiliale, decimal? saldo) {
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
                    command.CommandText = "INSERT INTO ContoCorrente VALUES ('tempValue', @username, @saldo, @idFiliale)";

                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters.Add("@idFiliale", SqlDbType.VarChar);
                    command.Parameters.Add("@saldo", SqlDbType.Decimal);

                    command.Parameters["@username"].Value = username;
                    command.Parameters["@idFiliale"].Value = idFiliale;
                    command.Parameters["@saldo"].Value = saldo;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo AggiungiContoCorrente ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", username);
                        Console.WriteLine("@idFiliale = {0}", idFiliale);
                        Console.WriteLine("@saldo = {0}", saldo);
                    }

                    int result = command.ExecuteNonQuery();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", result);
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    if (result > 0) return true;
                    else return false;

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
                    return false;
                }
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            } 
        }

        public List<ContoCorrente> GetListaContoCorrente(string username) {
            List<ContoCorrente> listaContoCorrente= new List<ContoCorrente>() { };

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
                    command.CommandText = "SELECT idContoCorrente, IBAN, saldo, idFiliale FROM ContoCorrente WHERE username = @username;";

                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetListaContoCorrente ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", username);
                    }

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {

                            var idContoCorrente = (UInt64)reader.GetDecimal(0);
                            var IBAN = reader.GetString(1);
                            var saldo = reader.GetDecimal(2);
                            var idFiliale = reader.GetString(3);

                            listaContoCorrente.Add(new ContoCorrente(idContoCorrente, IBAN, username, saldo, idFiliale));
                        }
                    }

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", listaContoCorrente.Count());
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return listaContoCorrente;
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
                    return new List<ContoCorrente>() { };
                }
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }

        public string GetIBANByIdContoCorrente(ulong idContoCorrente) {
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
                    command.CommandText = "SELECT IBAN FROM ContoCorrente WHERE idContoCorrente = @idContoCorrente;";

                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters["@idContoCorrente"].Value = idContoCorrente;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetIBANByIdContoCorrente ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@idContoCorrente = {0}", idContoCorrente);
                    }

                    var IBAN = (string)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", IBAN);
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return IBAN;
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
                    return String.Empty;
                }
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }    
    }
}
