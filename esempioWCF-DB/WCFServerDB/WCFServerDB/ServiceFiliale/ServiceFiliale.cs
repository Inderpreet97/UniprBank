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
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceFiliale : IServiceFiliale
    {
        public Filiale GetFiliale(string username) {
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
                    command.CommandText = "SELECT idFiliale, nome, indirizzo, CAP, citta, provincia, stato, numTelefono FROM Filiale, Account" +
                        " WHERE Account.filiale = Filiale.idFiliale" +
                        " AND Account.username = @username";

                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    Filiale filiale = new Filiale();

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            filiale.idFiliale = reader.GetString(0);
                            filiale.nome = reader.GetString(1);
                            filiale.indirizzo = reader.GetString(2);
                            filiale.CAP = reader.GetInt32(3);
                            filiale.citta = reader.GetString(4);
                            filiale.provincia = reader.GetString(5);
                            filiale.stato = reader.GetString(6);
                            filiale.numeroDiTelefono = reader.GetString(7);
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return filiale;
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
                    return new Filiale();
                }
            }
        }

        public string GetNameFiliale(string idFiliale) {
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
                    command.CommandText = "SELECT nome FROM Filiale WHERE idFiliale = @idFiliale";

                    command.Parameters.Add("@idFiliale", SqlDbType.VarChar);
                    command.Parameters["@idFiliale"].Value = idFiliale;

                    var nome = (string)command.ExecuteScalar();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return nome;
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
                    return string.Empty;
                }
            }
        }

        public bool ModificaDatiFiliale(string idFiliale, Filiale nuovaFiliale) {
            throw new NotImplementedException();
            /*
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
                        command.CommandText = "UPDATE Filiale SET ";

                        if (nome != string.Empty) {
                            command.CommandText += " nome_prodotti = '" + nome + "'";
                            if (tipologia != string.Empty || prezzo.HasValue || quantita.HasValue) {
                                command.CommandText += ",";
                            }
                        }

                        if (nuovoIdProdotto.HasValue) {
                            command.CommandText += " id_prodotti = " + nuovoIdProdotto;
                            if (nome != string.Empty || tipologia != string.Empty || prezzo.HasValue || quantita.HasValue) {
                                command.CommandText += ",";
                            }
                        }

                        if (nome != string.Empty) {
                            command.CommandText += " nome_prodotti = '" + nome + "'";
                            if (tipologia != string.Empty || prezzo.HasValue || quantita.HasValue) {
                                command.CommandText += ",";
                            }
                        }

                        if (tipologia != string.Empty) {
                            command.CommandText += " tipologia_prodotti = '" + tipologia + "'";
                            if (prezzo.HasValue || quantita.HasValue) {
                                command.CommandText += ",";
                            }
                        }

                        if (prezzo.HasValue) {
                            command.CommandText += " prezzo_prodotti = " + prezzo;
                            if (quantita.HasValue) {
                                command.CommandText += ",";
                            }
                        }

                        if (quantita.HasValue) {
                            command.CommandText += " quantita_prodotti = " + quantita;
                        }


                        command.CommandText += " WHERE id_prodotti = " + idProdotto;

                        int result = command.ExecuteNonQuery();

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
            */
        }
    }
}