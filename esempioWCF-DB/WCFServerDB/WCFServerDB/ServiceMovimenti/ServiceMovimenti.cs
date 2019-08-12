﻿using System;
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

                    command.CommandText = "SELECT saldo FROM ContoCorrente WHERE IBAN = @IBANCommittente;";
                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters["@IBANCommittente"].Value = IBANCommittente; ;

                    decimal? saldo = (Nullable<decimal>)command.ExecuteScalar();

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    if (saldo.HasValue) {
                        if ((saldo - importo) >= 0) return true;
                        else return false;
                    } else {
                        throw new Exception("ERRORE: saldo contiene un null, probabilmente il contocorrente non è stato trovato");
                    }
                    

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
            }
        }

        public bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal importo) {
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

                    command.CommandText = "UPDATE ContoCorrente SET saldo = saldo - @importo WHERE IBAN = @IBANCommittente;";
                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;
                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters["@importo"].Value = importo;

                    var result = command.ExecuteNonQuery();

                    if (result <= 0) {
                        throw new Exception("ERRORE: Non è stato possibile aggiornare il contocorrente del Committente");
                    }

                    command.CommandText = "UPDATE ContoCorrente SET saldo = saldo + @importo WHERE IBAN = @IBANBeneficiario;";
                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters["@IBANBeneficiario"].Value = IBANBeneficiario;
                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters["@importo"].Value = importo;

                    result = command.ExecuteNonQuery();

                    if (result <= 0) {
                        throw new Exception("ERRORE: Non è stato possibile aggiornare il contocorrente del Beneficiario");
                    }

                    command.CommandText = "INSERT INTO Movimenti VALUES (@IBANCommittente, @tipo, @importo, @IBANBeneficiario, @dataOra)";

                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;

                    command.Parameters.Add("@tipo", SqlDbType.VarChar);
                    command.Parameters["@tipo"].Value = "bonifico";

                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters["@importo"].Value = importo;

                    command.Parameters.Add("@IBANBeneficiario", SqlDbType.VarChar);
                    command.Parameters["@IBANBeneficiario"].Value = IBANBeneficiario;

                    command.Parameters.Add("@dataOra", SqlDbType.DateTime);
                    command.Parameters["@dataOra"].Value = DateTime.Now;

                    result = command.ExecuteNonQuery();

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
            }
        }

        public bool EseguiDeposito(int IBANCommittente, decimal importo) {
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

                    command.CommandText = "UPDATE ContoCorrente SET saldo = saldo + @importo WHERE IBAN = @IBANCommittente;";
                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;
                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters["@importo"].Value = importo;

                    var result = command.ExecuteNonQuery();

                    if (result <= 0) {
                        throw new Exception("ERRORE: Non è stato possibile aggiornare il contocorrente del Committente");
                    }

                    command.CommandText = "INSERT INTO Movimenti VALUES (@IBANCommittente, @tipo, @importo, @IBANBeneficiario, @dataOra)";

                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;

                    command.Parameters.Add("@tipo", SqlDbType.VarChar);
                    command.Parameters["@tipo"].Value = "deposito";

                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters["@importo"].Value = importo;

                    command.Parameters.Add("@IBANBeneficiario", SqlDbType.VarChar);
                    command.Parameters["@IBANBeneficiario"].Value = null;

                    command.Parameters.Add("@dataOra", SqlDbType.DateTime);
                    command.Parameters["@dataOra"].Value = DateTime.Now;

                    result = command.ExecuteNonQuery();

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
            }
        }

        public bool EseguiPrelievoDenaro(int IBANCommittente, decimal importo) {
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

                    command.CommandText = "UPDATE ContoCorrente SET saldo = saldo - @importo WHERE IBAN = @IBANCommittente;";
                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;
                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters["@importo"].Value = importo;

                    var result = command.ExecuteNonQuery();

                    if (result <= 0) {
                        throw new Exception("ERRORE: Non è stato possibile aggiornare il contocorrente del Committente");
                    }

                    command.CommandText = "INSERT INTO Movimenti VALUES (@IBANCommittente, @tipo, @importo, @IBANBeneficiario, @dataOra)";

                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;

                    command.Parameters.Add("@tipo", SqlDbType.VarChar);
                    command.Parameters["@tipo"].Value = "prelievo";

                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters["@importo"].Value = importo;

                    command.Parameters.Add("@IBANBeneficiario", SqlDbType.VarChar);
                    command.Parameters["@IBANBeneficiario"].Value = null;

                    command.Parameters.Add("@dataOra", SqlDbType.DateTime);
                    command.Parameters["@dataOra"].Value = DateTime.Now;

                    result = command.ExecuteNonQuery();

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
            }
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
