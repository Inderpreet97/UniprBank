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

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ CheckImporto ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@IBANCommittente = {0}", IBANCommittente);
                    }

                    decimal? saldo = (Nullable<decimal>)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", saldo);
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    if (saldo.HasValue) {
                        if (importo <= saldo) return true;
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
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
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

                    // Aggiorno il saldo del committente
                    command.CommandText = "UPDATE ContoCorrente SET saldo = saldo - @importo WHERE IBAN = @IBANCommittente;";
                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters.Add("@importo", SqlDbType.Decimal);

                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;
                    command.Parameters["@importo"].Value = importo;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EseguiBonifico: Update Saldo ContoCorrente Committente ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@IBANCommittente = {0}", IBANCommittente);
                        Console.WriteLine("@importo = {0}", importo);
                    }

                    var result = command.ExecuteNonQuery();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", result);
                    }

                    // Se non dovessi trovare un IBAN uguale a quello dato la query sarebbe giusta ma il result = 0
                    if (result <= 0) {
                        throw new Exception("ERRORE: Non è stato possibile aggiornare il contocorrente del Committente");
                    }

                    command.Parameters.Clear();

                    // Aggiorno il saldo del beneficiario
                    command.CommandText = "UPDATE ContoCorrente SET saldo = saldo + @importo WHERE IBAN = @IBANBeneficiario;";

                    command.Parameters.Add("@IBANBeneficiario", SqlDbType.VarChar);
                    command.Parameters.Add("@importo", SqlDbType.Decimal);

                    command.Parameters["@IBANBeneficiario"].Value = IBANBeneficiario;
                    command.Parameters["@importo"].Value = importo;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EseguiBonifico: Update Saldo ContoCorrente Beneficiario ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@IBANBeneficiario = {0}", IBANBeneficiario);
                        Console.WriteLine("@importo = {0}", importo);
                    }

                    result = command.ExecuteNonQuery();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", result);
                    }

                    // Se non dovessi trovare un IBAN uguale a quello dato la query sarebbe giusta ma il result = 0
                    if (result <= 0) {
                        throw new Exception("ERRORE: Non è stato possibile aggiornare il contocorrente del Beneficiario");
                    }

                    command.Parameters.Clear();

                    // Aggiungo il movimento nella tabella dei movimenti
                    command.CommandText = "INSERT INTO Movimenti VALUES (@IBANCommittente, @tipo, @importo, @IBANBeneficiario, @dataOra)";

                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters.Add("@tipo", SqlDbType.VarChar);
                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters.Add("@IBANBeneficiario", SqlDbType.VarChar);                   
                    command.Parameters.Add("@dataOra", SqlDbType.DateTime);

                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;
                    command.Parameters["@tipo"].Value = "bonifico";
                    command.Parameters["@importo"].Value = importo;
                    command.Parameters["@IBANBeneficiario"].Value = IBANBeneficiario;
                    command.Parameters["@dataOra"].Value = DateTime.Now;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EseguiBonifico: Aggiunta Movimento ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@IBANCommittente = {0}", IBANCommittente);
                        Console.WriteLine("@tipo = {0}", "bonifico");
                        Console.WriteLine("@importo = {0}", importo);
                        Console.WriteLine("@IBANBeneficiario = {0}", IBANBeneficiario);
                        Console.WriteLine("@dataOra = {0}", DateTime.Now);
                    }

                    result = command.ExecuteNonQuery();

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

        public bool EseguiDeposito(UInt64 idContoCorrente, decimal importo) {
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

                    // Aggiorno il saldo del conto corrente
                    command.CommandText = "UPDATE ContoCorrente SET saldo = saldo + @importo WHERE idContoCorrente = @idContoCorrente;";

                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters.Add("@importo", SqlDbType.Decimal);

                    command.Parameters["@idContoCorrente"].Value = idContoCorrente;
                    command.Parameters["@importo"].Value = importo;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EseguiDeposito: Update Saldo ContoCorrente ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@idContoCorrente = {0}", idContoCorrente);
                        Console.WriteLine("@importo = {0}", importo);
                    }

                    var result = command.ExecuteNonQuery();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", result);
                    }

                    // Se non dovessi trovare un conto corrente uguale a quello dato la query sarebbe giusta ma il result = 0
                    if (result <= 0) {
                        throw new Exception("ERRORE: Non è stato possibile aggiornare il contocorrente del Committente");
                    }

                    command.Parameters.Clear();

                    // Cerca l'IBAN legato all'ID del conto corrente dato per poter aggiungere il movimento
                    command.CommandText = "SELECT IBAN FROM ContoCorrente WHERE idContoCorrente = @idContoCorrente;";

                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters["@idContoCorrente"].Value = idContoCorrente;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EseguiDeposito: SELECT IBAN ContoCorrente ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@idContoCorrente = {0}", idContoCorrente);
                    }

                    var IBANCommittente = (string)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", IBANCommittente);
                    }

                    command.Parameters.Clear();

                    // Aggiungo il movimento nella tabella dei movimenti
                    command.CommandText = "INSERT INTO Movimenti VALUES (@IBANCommittente, @tipo, @importo, @IBANBeneficiario, @dataOra)";

                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters.Add("@tipo", SqlDbType.VarChar);
                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters.Add("@IBANBeneficiario", SqlDbType.VarChar);
                    command.Parameters.Add("@dataOra", SqlDbType.DateTime);

                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;
                    command.Parameters["@tipo"].Value = "deposito";
                    command.Parameters["@importo"].Value = importo;
                    command.Parameters["@IBANBeneficiario"].Value = DBNull.Value;
                    command.Parameters["@dataOra"].Value = DateTime.Now;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EseguiDeposito: Aggiunta Movimento ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@IBANCommittente = {0}", IBANCommittente);
                        Console.WriteLine("@tipo = {0}", "deposito");
                        Console.WriteLine("@importo = {0}", importo);
                        Console.WriteLine("@IBANBeneficiario = {0}", string.Empty);
                        Console.WriteLine("@dataOra = {0}", DateTime.Now);
                    }

                    result = command.ExecuteNonQuery();

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

        public bool EseguiPrelievoDenaro(UInt64 idContoCorrente, decimal importo) {
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

                    // Aggiorno il saldo del conto corrente
                    command.CommandText = "UPDATE ContoCorrente SET saldo = saldo - @importo WHERE idContoCorrente = @idContoCorrente;";

                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters.Add("@importo", SqlDbType.Decimal);

                    command.Parameters["@idContoCorrente"].Value = idContoCorrente;
                    command.Parameters["@importo"].Value = importo;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EseguiPrelievoDenaro: Update Saldo ContoCorrente ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@idContoCorrente = {0}", idContoCorrente);
                        Console.WriteLine("@importo = {0}", importo);
                    }

                    var result = command.ExecuteNonQuery();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", result);
                    }

                    // Se non dovessi trovare un conto corrente uguale a quello dato la query sarebbe giusta ma il result = 0
                    if (result <= 0) {
                        throw new Exception("ERRORE: Non è stato possibile aggiornare il contocorrente del Committente");
                    }

                    command.Parameters.Clear();

                    // Cerca l'IBAN legato all'ID del conto corrente dato per poter aggiungere il movimento
                    command.CommandText = "SELECT IBAN FROM ContoCorrente WHERE idContoCorrente = @idContoCorrente;";

                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters["@idContoCorrente"].Value = idContoCorrente;

                    var IBANCommittente = (string)command.ExecuteScalar();

                    command.Parameters.Clear();

                    // Aggiungo il movimento nella tabella dei movimenti
                    command.CommandText = "INSERT INTO Movimenti VALUES (@IBANCommittente, @tipo, @importo, @IBANBeneficiario, @dataOra)";

                    command.Parameters.Add("@IBANCommittente", SqlDbType.VarChar);
                    command.Parameters.Add("@tipo", SqlDbType.VarChar);
                    command.Parameters.Add("@importo", SqlDbType.Decimal);
                    command.Parameters.Add("@IBANBeneficiario", SqlDbType.VarChar);
                    command.Parameters.Add("@dataOra", SqlDbType.DateTime);


                    command.Parameters["@tipo"].Value = "prelievo";
                    command.Parameters["@IBANCommittente"].Value = IBANCommittente;
                    command.Parameters["@importo"].Value = importo;
                    command.Parameters["@IBANBeneficiario"].Value = DBNull.Value;
                    command.Parameters["@dataOra"].Value = DateTime.Now;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EseguiPrelievoDenaro: Aggiunta Movimento ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@IBANCommittente = {0}", IBANCommittente);
                        Console.WriteLine("@tipo = {0}", "prelievo");
                        Console.WriteLine("@importo = {0}", importo);
                        Console.WriteLine("@IBANBeneficiario = {0}", string.Empty);
                        Console.WriteLine("@dataOra = {0}", DateTime.Now);
                    }

                    result = command.ExecuteNonQuery();

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

        public List<Movimento> GetListaMovimenti(UInt64 idContoCorrente) {
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
                         + " AND idContoCorrente = @idContoCorrente";

                    command.Parameters.Add("@idContoCorrente", SqlDbType.VarChar);
                    command.Parameters["@idContoCorrente"].Value = idContoCorrente;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetListaMovimenti ============");
                        Console.WriteLine(command.CommandText);
                        Console.WriteLine("@idContoCorrente = {0}", idContoCorrente);

                    }

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {

                            var idMovimenti = reader.GetDecimal(0).ToString();
                            var IBANCommittente = reader.GetString(1);
                            var tipo = reader.GetString(2);
                            var importo = reader.GetDecimal(3);
                            var IBANBeneficiario = (!reader.IsDBNull(4)) ? reader.GetString(4) : string.Empty;
                            var dataOra = reader.GetDateTime(5);

                            listaMovimenti.Add(new Movimento(idMovimenti, IBANCommittente, tipo, importo, IBANBeneficiario, dataOra));
                        }
                    }
                    
                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", listaMovimenti.Count());
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
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }
    }
}
