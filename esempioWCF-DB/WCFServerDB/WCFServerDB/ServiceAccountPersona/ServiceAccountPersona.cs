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
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceAccountPersona" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceAccountPersona : IServiceAccountPersona
    {
        public bool Login(string username, string password)
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

                    command.CommandText = "SELECT count(username) FROM Account WHERE username = @username AND password = @password;";
                    command.Parameters.Add("@username",SqlDbType.VarChar);
                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;
                    command.Parameters["@password"].Value = password;
                    
                    int? users = (Nullable<int>)command.ExecuteScalar();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    if (users == 1) return true;
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

        public string GetPrivilegi(string username)
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

                    command.CommandText = "SELECT privilegi FROM Account WHERE username = @username;";
                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    var privilegiString = (string)command.ExecuteScalar();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return privilegiString;
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
            }
        }

        public List<Persona> GetListaPersone(string tipoAccount, string idFiliale) {
            List<Persona> listaPersone = new List<Persona>() { };

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
                    command.CommandText = "SELECT username, privilegi, Persona.codiceFiscale, nome, cognome, dataNascita, "
                         + "sesso, indirizzo, CAP, citta, provincia, stato, numTelefono, filiale FROM Persona, Account"
                         + "WHERE Persona.codiceFiscale = Account.codiceFiscale "
                         + " AND privilegi = @privilegi" 
                         + " AND filiale = @idFiliale";
                    command.Parameters.Add("@privilegi", SqlDbType.VarChar);
                    command.Parameters.Add("@idFiliale", SqlDbType.VarChar);
                    command.Parameters["@privilegi"].Value = tipoAccount;
                    command.Parameters["@idFiliale"].Value = idFiliale;

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {

                            var username = reader.GetString(0);
                            var privilegi = reader.GetString(1);
                            var codiceFiscale = reader.GetString(2);
                            var nome = reader.GetString(3);
                            var cognome = reader.GetString(4);
                            var dataDiNascita = reader.GetDateTime(5);
                            var sesso = reader.GetString(6);
                            var indirizzo = reader.GetString(7);
                            var CAP = (Nullable<int>)reader.GetValue(0);
                            var citta = reader.GetString(8);
                            var provincia = reader.GetString(9);
                            var stato = reader.GetString(10);
                            var numeroDiTelefono = reader.GetString(11);
                            var filiale = reader.GetString(12);

                            listaPersone.Add(new Persona(username,privilegi,codiceFiscale,nome,cognome,dataDiNascita,sesso,indirizzo,CAP,citta,provincia,stato,numeroDiTelefono,filiale));
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return listaPersone;
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
                    return new List<Persona>() { };
                }
            }
        }

        public bool EliminaAccount(string username) {
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

                    command.CommandText = "DELETE FROM Account WHERE username = @username;";
                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

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

        public Persona CheckUsername(string username) {
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
                    command.CommandText = "SELECT privilegi, Persona.codiceFiscale, nome, cognome, dataNascita, "
                         + "sesso, indirizzo, CAP, citta, provincia, stato, numTelefono, filiale FROM Persona, Account"
                         + "WHERE Persona.codiceFiscale = Account.codiceFiscale "
                         + " AND Account.username = @username";

                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    Persona persona = new Persona();
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            persona.username = username;
                            persona.privilegi = reader.GetString(0);
                            persona.codiceFiscale = reader.GetString(1);
                            persona.nome = reader.GetString(2);
                            persona.cognome = reader.GetString(3);
                            persona.dataDiNascita = reader.GetDateTime(4);
                            persona.sesso = reader.GetString(5);
                            persona.indirizzo = reader.GetString(6);
                            persona.CAP = (Nullable<int>)reader.GetValue(7);
                            persona.citta = reader.GetString(8);
                            persona.provincia = reader.GetString(8);
                            persona.stato = reader.GetString(9);
                            persona.numeroDiTelefono = reader.GetString(10);
                            persona.filiale = reader.GetString(11);
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return persona;
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
                    return new Persona();
                }
            }
        }

        public bool AggiungiPersona(Persona persona, string password) {

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
                    // Controllo se esiste già una persona con lo stesso codice fiscale
                    Persona tempPersona = GetPersona(persona.codiceFiscale);
                    int result;

                    if (tempPersona.codiceFiscale == string.Empty) {
                        command.CommandText = "INSERT INTO Persona VALUES ( @codiceFiscale, @nome, @cognome, @dataDiNascita, "
                                                + "@sesso, @indirizzo, @CAP, @citta, @provincia, @stato, @numeroDiTelefono)";

                        command.Parameters.Add("@codiceFiscale", SqlDbType.VarChar);
                        command.Parameters.Add("@nome", SqlDbType.VarChar);
                        command.Parameters.Add("@cognome", SqlDbType.VarChar);
                        command.Parameters.Add("@dataDiNascita", SqlDbType.VarChar);
                        command.Parameters.Add("@sesso", SqlDbType.VarChar);
                        command.Parameters.Add("@indirizzo", SqlDbType.VarChar);
                        command.Parameters.Add("@CAP", SqlDbType.VarChar);
                        command.Parameters.Add("@citta", SqlDbType.VarChar);
                        command.Parameters.Add("@provincia", SqlDbType.VarChar);
                        command.Parameters.Add("@stato", SqlDbType.VarChar);
                        command.Parameters.Add("@numeroDiTelefono", SqlDbType.VarChar);

                        command.Parameters["@codiceFiscale"].Value = persona.codiceFiscale;
                        command.Parameters["@nome"].Value = persona.nome;
                        command.Parameters["@cognome"].Value = persona.cognome;
                        command.Parameters["@dataDiNascita"].Value = persona.dataDiNascita;
                        command.Parameters["@sesso"].Value = persona.sesso;
                        command.Parameters["@indirizzo"].Value = persona.indirizzo;
                        command.Parameters["@CAP"].Value = persona.CAP;
                        command.Parameters["@citta"].Value = persona.citta;
                        command.Parameters["@provincia"].Value = persona.provincia;
                        command.Parameters["@stato"].Value = persona.stato;
                        command.Parameters["@numeroDiTelefono"].Value = persona.numeroDiTelefono;

                        result = command.ExecuteNonQuery();
                                                
                        if (result <= 0) throw new Exception("Errore: si è verificato un problema nell'aggiungere una Persona nel DB");
                    }
                    
                    command.CommandText = "INSERT INTO Persona VALUES ( @username, @password, @privilegi, @codicefiscale)";

                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters.Add("@privilegi", SqlDbType.VarChar);
                    command.Parameters.Add("@codicefiscale", SqlDbType.VarChar);

                    command.Parameters["@username"].Value = persona.username;
                    command.Parameters["@password"].Value = password;
                    command.Parameters["@privilegi"].Value = persona.privilegi;
                    command.Parameters["@codicefiscale"].Value = persona.codiceFiscale;

                    /*  PROVARE A CRIPTARE LE PASSWORD CON QUESTI METODI
                     *  
                     *  byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
                     *  data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                     *  String hash = System.Text.Encoding.ASCII.GetString(data);
                     *  
                     */
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

        public bool ModificaPersona(string identificativo, Persona persona) {
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
                    // Prima modifica Persona e poi modifico Account
                    command.CommandText = "UPDATE Persona SET ";

                    List<System.Reflection.PropertyInfo> propreties = persona.GetType().GetProperties().ToList();
                    // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< WORKING HERE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


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

        public Persona GetPersona(string codiceFiscale) {
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
                    command.CommandText = "SELECT nome, cognome, dataNascita, sesso, indirizzo, "
                         + "CAP, citta, provincia, stato, numTelefono FROM Persona "
                         + "WHERE codiceFiscale = @codiceFiscale ";

                    command.Parameters.Add("@codiceFiscale", SqlDbType.VarChar);
                    command.Parameters["@codiceFiscale"].Value = codiceFiscale;

                    Persona persona = new Persona();
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {

                            persona.codiceFiscale = codiceFiscale;
                            persona.nome = reader.GetString(0);
                            persona.cognome = reader.GetString(1);
                            persona.dataDiNascita = reader.GetDateTime(2);
                            persona.sesso = reader.GetString(3);
                            persona.indirizzo = reader.GetString(4);
                            persona.CAP = (Nullable<int>)reader.GetValue(5);
                            persona.citta = reader.GetString(6);
                            persona.provincia = reader.GetString(7);
                            persona.stato = reader.GetString(8);
                            persona.numeroDiTelefono = reader.GetString(9);
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return persona;
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
                    return new Persona();
                }
            }
        }
    }
}
