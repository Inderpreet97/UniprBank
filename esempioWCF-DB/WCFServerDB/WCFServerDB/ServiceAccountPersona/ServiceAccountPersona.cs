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

        public bool EliminaImpiegato(string username) {
            throw new NotImplementedException();
        }

        public Persona CheckUsername(string username) {
            throw new NotImplementedException();
        }

        public bool AggiungiPersona(Persona persona, string password) {
            throw new NotImplementedException();
        }

        public bool ModificaPersona(string identificativo, Persona persona) {
            throw new NotImplementedException();
        }

        public Persona GetPersona(string identificativo) {
            throw new NotImplementedException();
        }
    }
}
