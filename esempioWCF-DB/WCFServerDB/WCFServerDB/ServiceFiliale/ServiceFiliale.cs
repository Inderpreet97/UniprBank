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
                            filiale.CAP = (int)reader.GetDecimal(3);
                            filiale.citta = reader.GetString(4);
                            filiale.provincia = reader.GetString(5);
                            filiale.stato = reader.GetString(6);
                            filiale.numeroDiTelefono = reader.GetString(7);
                        }
                    }

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetFiliale ============");
                        Console.WriteLine(command.CommandText);
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

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetNameFiliale ============");
                        Console.WriteLine(command.CommandText);
                    }
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

                    //Lista delle propietà della classe Filiale 
                    List<System.Reflection.PropertyInfo> filialeProperties = nuovaFiliale.GetType().GetProperties().ToList();

                    // Non possiamo usare una Query Parametrizza perchè non sappiamo esattamente quali saranno i parametri
                    command.CommandText = "UPDATE Filiale SET ";

                    bool propertyAddedToQuery;

                    for (int index = 0; index < filialeProperties.Count; index++) {
                        propertyAddedToQuery = false;

                        // Guardo il tipo di dato (int, string, ...)
                        //Se ha valore -> aggiungere la parte di codice SQL per aggiornarlo

                        if (filialeProperties[index].PropertyType  == typeof(int?)) {
                            if (((int?)filialeProperties[index].GetValue(nuovaFiliale)).HasValue) {
                                command.CommandText += filialeProperties[index].Name + " = " + (int?)filialeProperties[index].GetValue(nuovaFiliale);
                                propertyAddedToQuery = true;
                            }
                        } else {
                            if (filialeProperties[index].GetValue(nuovaFiliale).ToString() != string.Empty) {
                                command.CommandText += filialeProperties[index].Name + " = '" + filialeProperties[index].GetValue(nuovaFiliale).ToString() + "' ";
                                propertyAddedToQuery = true;
                            }
                        }

                        if (propertyAddedToQuery) {
                            for (int tempIndex = index + 1; tempIndex < filialeProperties.Count; tempIndex++) {

                                if (filialeProperties[tempIndex].PropertyType  == typeof(string)) {
                                    if (filialeProperties[tempIndex].GetValue(nuovaFiliale).ToString() != string.Empty) {
                                        command.CommandText += " , ";
                                        tempIndex = filialeProperties.Count;
                                    }

                                } else {
                                    if (((int?)filialeProperties[tempIndex].GetValue(nuovaFiliale)).HasValue) {
                                        command.CommandText += " , ";
                                        tempIndex = filialeProperties.Count;
                                    }
                                }
                            }
                        }


                    }
                    command.CommandText += " WHERE idFiliale = @idFiliale";
                    command.Parameters.Add("@idFiliale", SqlDbType.VarChar);
                    command.Parameters["@idFiliale"].Value = idFiliale;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo ModificaDatiFiliale ============");
                        Console.WriteLine(command.CommandText);
                    }

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
        }
    }
}