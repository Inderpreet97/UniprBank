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
        public bool Login(string username, string password) {
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

                    command.CommandText = "SELECT count(username) FROM Account " +
                        "WHERE username = @username COLLATE SQL_Latin1_General_CP1_CS_AS " + //Collate serve per rendere la query Case Sensitive
                        "AND password = @password COLLATE SQL_Latin1_General_CP1_CS_AS;";

                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;
                    command.Parameters["@password"].Value = password;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo Login ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", username);
                        Console.WriteLine("@password = {0}", password);
                    }

                    int? users = (Nullable<int>)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", users);
                    }

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
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }

        public string GetPrivilegi(string username) {
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

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetPrivilegi ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", username);
                    }

                    var privilegiString = (string)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", privilegiString);
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return privilegiString.ToLower();
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

        public List<Persona> GetListaPersone(string tipoAccount, string idFiliale) {
            // Lista usata per ritornare i dati
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
                    command.CommandText = "SELECT username, privilegi, Persona.codiceFiscale, nome, cognome, dataDiNascita, "
                         + "sesso, indirizzo, CAP, citta, provincia, stato, numeroDiTelefono, filiale FROM Persona, Account "
                         + "WHERE Persona.codiceFiscale = Account.codiceFiscale "
                         + "AND privilegi = @privilegi "
                         + "AND filiale = @idFiliale";
                    command.Parameters.Add("@privilegi", SqlDbType.VarChar);
                    command.Parameters.Add("@idFiliale", SqlDbType.VarChar);
                    command.Parameters["@privilegi"].Value = tipoAccount;
                    command.Parameters["@idFiliale"].Value = idFiliale;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetListaPersone ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@privilegi = {0}", tipoAccount);
                        Console.WriteLine("@idFiliale = {0}", idFiliale);
                    }

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {

                            var username = reader.GetString(0);
                            var privilegi = reader.GetString(1);
                            var codiceFiscale = reader.GetString(2).ToUpper();
                            var nome = reader.GetString(3);
                            var cognome = reader.GetString(4);
                            var dataDiNascita = reader.GetDateTime(5);
                            var sesso = reader.GetString(6).ToLower();
                            var indirizzo = reader.GetString(7);
                            var CAP = (int)reader.GetDecimal(8);
                            var citta = reader.GetString(9);
                            var provincia = reader.GetString(10).ToUpper();
                            var stato = reader.GetString(11);
                            var numeroDiTelefono = reader.GetString(12);
                            var filiale = reader.GetString(13);
                            
                            listaPersone.Add(new Persona(username, privilegi, codiceFiscale, nome, cognome, dataDiNascita.Date, sesso, indirizzo, CAP, citta, provincia, stato, numeroDiTelefono, filiale));
                        }
                    }

                    if (Globals.debugMode) {
                        Console.WriteLine("Numero di risultati: {0}", listaPersone.Count());
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
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
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
                    // Conta quanti Account sono rimasti legati ad una persona
                    command.CommandText = "SELECT COUNT(username) FROM Account WHERE codicefiscale in (SELECT codicefiscale FROM Account WHERE username = @username);";
                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EliminaAccount: Count Accounts ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", username);
                    }

                    int numAccountPersona = (int)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato Numero Account: {0}", numAccountPersona);
                    }

                    var codiceFiscale = string.Empty;

                    // Se alla persona ha solo un account e non ha conti correnti, allora mi salvo il codice fiscale della persona
                    // così posso eliminare l'account e poi anche la persona.
                    if (numAccountPersona <= 1) {
                        command.Parameters.Clear();
                        command.CommandText = "SELECT codicefiscale FROM Account WHERE username = @username;";
                        command.Parameters.Add("@username", SqlDbType.VarChar);
                        command.Parameters["@username"].Value = username;

                        if (Globals.debugMode) {
                            Console.WriteLine("\n============ Metodo EliminaAccount: Select CodiceFiscale Account ============");
                            Console.WriteLine("Query: {0}", command.CommandText);
                            Console.WriteLine("@username = {0}", username);
                        }

                        // codice fiscale della persona da eliminare
                        codiceFiscale = (string)command.ExecuteScalar();

                        if (Globals.debugMode) {
                            Console.WriteLine("Risultato CodiceFiscale: {0}", codiceFiscale);
                        }
                    }

                    command.Parameters.Clear();

                    // elimino l'account
                    command.CommandText = "DELETE FROM Account WHERE username = @username;";
                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EliminaAccount ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", username);
                    }

                    int result = command.ExecuteNonQuery();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", result);
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    // elimino la persona, dopo aver eliminato l'account per via dei vincoli nel DB
                    if (numAccountPersona <= 1) {
                        EliminaPersona(codiceFiscale);
                    }

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

        // Metodo usato solo nel lato server, non fa parte delle operazioni visibili dal client
        public bool EliminaPersona(string codiceFiscale) {
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
                    command.CommandText = "DELETE FROM Persona WHERE codiceFiscale = @codiceFiscale;";
                    command.Parameters.Add("@codiceFiscale", SqlDbType.VarChar);
                    command.Parameters["@codiceFiscale"].Value = codiceFiscale;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo EliminaPersona ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@codiceFiscale = {0}", codiceFiscale);
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
                    command.CommandText = "SELECT privilegi, Persona.codiceFiscale, nome, cognome, dataDiNascita, "
                         + "sesso, indirizzo, CAP, citta, provincia, stato, numeroDiTelefono, filiale FROM Persona, Account "
                         + "WHERE Persona.codiceFiscale = Account.codiceFiscale "
                         + "AND Account.username = @username";

                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    Persona persona = new Persona();

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo CheckUsername ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", username);
                    }

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            persona.username = username;
                            persona.privilegi = reader.GetString(0);
                            persona.codiceFiscale = reader.GetString(1).ToUpper();
                            persona.nome = reader.GetString(2);
                            persona.cognome = reader.GetString(3);
                            persona.dataDiNascita = reader.GetDateTime(4);
                            persona.sesso = reader.GetString(5).ToLower();
                            persona.indirizzo = reader.GetString(6);
                            persona.CAP = (int)reader.GetDecimal(7);
                            persona.citta = reader.GetString(8);
                            persona.provincia = reader.GetString(9).ToUpper();
                            persona.stato = reader.GetString(10);
                            persona.numeroDiTelefono = reader.GetString(11);
                            persona.filiale = reader.GetString(12);
                        }
                    }

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", persona.codiceFiscale);
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
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
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

                    // Se temPersona è vuoto (cioè tutti campi settati a default) allora devo aggiungere una nuova persona
                    // e un nuovo account. Se il campo codice fiscale ha un valore diverso da string.Empty allora devo 
                    // aggiungere solo un account perchè la persona è già presente nel DB
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

                        command.Parameters["@codiceFiscale"].Value = persona.codiceFiscale.ToUpper();
                        command.Parameters["@nome"].Value = persona.nome;
                        command.Parameters["@cognome"].Value = persona.cognome;
                        command.Parameters["@dataDiNascita"].Value = persona.dataDiNascita;
                        command.Parameters["@sesso"].Value = persona.sesso.ToLower();
                        command.Parameters["@indirizzo"].Value = persona.indirizzo;
                        command.Parameters["@CAP"].Value = persona.CAP;
                        command.Parameters["@citta"].Value = persona.citta;
                        command.Parameters["@provincia"].Value = persona.provincia.ToUpper();
                        command.Parameters["@stato"].Value = persona.stato;
                        command.Parameters["@numeroDiTelefono"].Value = persona.numeroDiTelefono;

                        if (Globals.debugMode) {
                            Console.WriteLine("\n============ Metodo AggiungiPersona: persona ============");
                            Console.WriteLine("Query: {0}", command.CommandText);
                            Console.WriteLine("@codiceFiscale = {0}", persona.codiceFiscale);
                            Console.WriteLine("@nome = {0}", persona.nome);
                            Console.WriteLine("@cognome = {0}", persona.cognome);
                            Console.WriteLine("@dataDiNascita = {0}", persona.dataDiNascita);
                            Console.WriteLine("@sesso = {0}", persona.sesso);
                            Console.WriteLine("@indirizzo = {0}", persona.indirizzo);
                            Console.WriteLine("@CAP = {0}", persona.CAP);
                            Console.WriteLine("@citta = {0}", persona.citta);
                            Console.WriteLine("@provincia = {0}", persona.provincia);
                            Console.WriteLine("@stato = {0}", persona.stato);
                            Console.WriteLine("@numeroDiTelefono = {0}", persona.numeroDiTelefono);
                        }

                        result = command.ExecuteNonQuery();

                        if (Globals.debugMode) {
                            Console.WriteLine("Risultato: {0}", result);
                        }

                        if (result <= 0) throw new Exception("Errore: si è verificato un problema nell'aggiungere una Persona nel DB");
                        command.Parameters.Clear();
                    }

                    command.CommandText = "INSERT INTO Account VALUES ( @username, @password, @privilegi, @codicefiscale, @filiale)";

                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters.Add("@privilegi", SqlDbType.VarChar);
                    command.Parameters.Add("@codicefiscale", SqlDbType.VarChar);
                    command.Parameters.Add("@filiale", SqlDbType.VarChar);

                    command.Parameters["@username"].Value = persona.username;
                    command.Parameters["@password"].Value = password;
                    command.Parameters["@privilegi"].Value = persona.privilegi;
                    command.Parameters["@codicefiscale"].Value = persona.codiceFiscale.ToUpper();
                    command.Parameters["@filiale"].Value = persona.filiale;

                    /*  PROVARE A CRIPTARE LE PASSWORD CON QUESTI METODI
                     *  
                     *  byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
                     *  data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                     *  String hash = System.Text.Encoding.ASCII.GetString(data);
                     *  
                     */

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo AggiungiPersona: account ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", persona.username);
                        Console.WriteLine("@password = {0}", password);
                        Console.WriteLine("@privilegi = {0}", persona.privilegi);
                        Console.WriteLine("@codicefiscale = {0}", persona.codiceFiscale.ToUpper());
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

        public bool ModificaPersona(string usernameOld, Persona persona) {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                int result = -1; // Il numero di righe modificate dall'update 

                try {

                    // Lista delle propietà della persona 
                    List<System.Reflection.PropertyInfo> personaProperties = persona.GetType().GetProperties().ToList();

                    // Lista vuota di properties legata alla tabella Account
                    List<System.Reflection.PropertyInfo> accountProperties = new List<System.Reflection.PropertyInfo>() { };

                    // Lista dei nomi degli attributi legati alla tabella Account
                    List<string> accountPropertiesNames = new List<string>() { "username", "privilegi", "filiale" };

                    // Itero tutte le properties della persona rimuovendo quelle in comune con account e le aggiungo alla lista accountProperties
                    // !!! Non viene modificata la classe ma solo una lista che contiene dei riferimenti alle propietà della classe
                    for (int index = 0; index < personaProperties.Count; index++) {
                        if (accountPropertiesNames.Contains(personaProperties[index].Name)) {
                            accountProperties.Add(personaProperties[index]);
                            personaProperties.RemoveAt(index);
                            // Quando rimuovo un elemento dalla lista ad un determinato indice, gli elementi successivi vengono scalati
                            index--;

                        } else if (personaProperties[index].Name == "codiceFiscale") {
                            // Codice Fiscale è presente sia in account che persona.
                            accountProperties.Add(personaProperties[index]);
                        }
                    }

                    // Prima modifico Persona e poi modifico Account
                    // Non possiamo usare una Query Parametrizza perchè non sappiamo esattamente quali saranno i parametri
                    command.CommandText = "UPDATE Persona SET ";

                    bool propertyAddedToQuery;
                    bool queryDaEseguire = false;

                    for (int index = 0; index < personaProperties.Count; index++) {
                        propertyAddedToQuery = false;

                        // Guardo il tipo di dato (int, string, ...)
                        //Se ha valore -> aggiungere la parte di codice SQL per aggiornarlo

                        if (personaProperties[index].PropertyType == typeof(int?)) {
                            if (((int?)personaProperties[index].GetValue(persona)).HasValue) { 
                                command.CommandText += personaProperties[index].Name + " = " + (int?)personaProperties[index].GetValue(persona);
                                propertyAddedToQuery = true;
                            }
                        } else if (personaProperties[index].PropertyType == typeof(DateTime?)) {
                            if (((DateTime?)personaProperties[index].GetValue(persona)).HasValue) {
                                command.CommandText += personaProperties[index].Name + " = '" + personaProperties[index].GetValue(persona).ToString().Remove(10, 9) + "' ";
                                propertyAddedToQuery = true;
                            }
                        } else {
                            if (personaProperties[index].GetValue(persona).ToString() != string.Empty) {
                                command.CommandText += personaProperties[index].Name + " = '" + personaProperties[index].GetValue(persona).ToString() + "' ";
                                propertyAddedToQuery = true;
                            }
                        }

                        // Se ho aggiunto del codice per aggiornare un dato devo mettere la "," nella query se ci sono altri dati da aggiornare prima del WHERE
                        // altrimenti "," non va messa perchè dopo viene aggiunta la condizione " WHERE CodiceFiscale = ..."
                        if (propertyAddedToQuery) {

                            // Eseguo la query solo se ho aggiunto dei dati da aggiornare
                            queryDaEseguire = true;

                            // tempIndex = index + 1 perchè devo controllare solo i dati successivi a quello che ho appena inserito nella query
                            for (int tempIndex = index + 1; tempIndex < personaProperties.Count; tempIndex++) {
                               
                                if (personaProperties[tempIndex].PropertyType == typeof(string)) {
                                    if (personaProperties[tempIndex].GetValue(persona).ToString() != string.Empty) {
                                        command.CommandText += " , ";
                                        // Basta inserire una sola " , " poi esco dal for
                                        tempIndex = personaProperties.Count;
                                    }

                                } else if (personaProperties[tempIndex].PropertyType == typeof(DateTime?)) {
                                    if (((DateTime?)personaProperties[tempIndex].GetValue(persona)).HasValue) {
                                        command.CommandText += " , ";
                                        // Basta inserire una sola " , " poi esco dal for
                                        tempIndex = personaProperties.Count;
                                    }
                                } else {                                    
                                    if (((int?)personaProperties[tempIndex].GetValue(persona)).HasValue) {
                                        command.CommandText += " , ";
                                        // Basta inserire una sola " , " poi esco dal for
                                        tempIndex = personaProperties.Count;
                                    }
                                }
                            }                            
                        }                                         
                    }

                    if (queryDaEseguire) {
                        command.CommandText += " WHERE codiceFiscale in ( SELECT codiceFiscale FROM Account WHERE username = @username )";
                        command.Parameters.Add("@username", SqlDbType.VarChar);
                        command.Parameters["@username"].Value = usernameOld;

                        if (Globals.debugMode) {
                            Console.WriteLine("\n============ Metodo ModificaPersona: persona ============");
                            Console.WriteLine("Query: {0}", command.CommandText);
                            Console.WriteLine("@username = {0}", usernameOld);
                        }

                        result = command.ExecuteNonQuery();

                        if (Globals.debugMode) {
                            Console.WriteLine("Risultato: {0}", result);
                        }

                        if (result <= 0) throw new Exception("Errore: si è verificato un problema nell'aggiornare una Persona nel DB");

                    } else {
                        if (Globals.debugMode) {
                            Console.WriteLine("\n============ Metodo ModificaPersona: account ============");
                            Console.WriteLine("Nessun dato in persona da aggiornare\n");
                        }
                    }

                    // ------------------- Aggiorno le info nell'account -------------------
                    command.Parameters.Clear();
                    command.CommandText = "UPDATE Account SET ";

                    queryDaEseguire = false;
                    for (int index = 0; index < accountProperties.Count; index++) {

                        //Non c'è controllo sul tipo perchè le properties dell'account sono tutte di tipo string
                        if (accountProperties[index].GetValue(persona).ToString() != string.Empty) {

                            queryDaEseguire = true;

                            command.CommandText += accountProperties[index].Name + " = '" + accountProperties[index].GetValue(persona).ToString() + "' ";

                            for (int tempIndex = index + 1; tempIndex < accountProperties.Count; tempIndex++) {
                                if (accountProperties[tempIndex].GetValue(persona).ToString() != string.Empty) {
                                    command.CommandText += " , ";
                                    tempIndex = accountProperties.Count;
                                }
                            }
                        }
                    }

                    if (queryDaEseguire) {
                        command.CommandText += " WHERE username = @username";
                        command.Parameters.Add("@username", SqlDbType.VarChar);
                        command.Parameters["@username"].Value = usernameOld;

                        if (Globals.debugMode) {
                            Console.WriteLine("\n============ Metodo ModificaPersona: account ============");
                            Console.WriteLine("Query: {0}", command.CommandText);
                            Console.WriteLine("@username = {0}", usernameOld);
                        }

                        result = command.ExecuteNonQuery();

                        if (Globals.debugMode) {
                            Console.WriteLine("Risultato: {0}", result);
                        }

                        // Attempt to commit the transaction.
                        transaction.Commit();

                        if (result > 0) return true;
                        else return false;

                    } else {
                        if (Globals.debugMode) {
                            Console.WriteLine("\n============ Metodo ModificaPersona: account ============");
                            Console.WriteLine("Nessun dato in account da aggiornare\n");
                        }

                        // Potrei aver aggiornato dati relativi solo alla tabella Persona, quindi result contiene il numero 
                        // di righe aggiornate dalla query (se eseguita) update su Persona.
                        // Se anche la query su Persona non è stata eseguita allora result contiene -1, 
                        // quindi non ho aggiornato nessun dato, perciò return False.
                        if (result > 0) {

                            transaction.Commit();
                            return true;

                        } else {

                            return false;

                        }
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

                    command.CommandText = "SELECT nome, cognome, dataDiNascita, sesso, indirizzo, "
                         + "CAP, citta, provincia, stato, numeroDiTelefono FROM Persona "
                         + "WHERE codiceFiscale = @codiceFiscale ";

                    command.Parameters.Add("@codiceFiscale", SqlDbType.VarChar);
                    command.Parameters["@codiceFiscale"].Value = codiceFiscale;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetPersona ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@codiceFiscale = {0}", codiceFiscale);
                    }

                    Persona persona = new Persona();
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {

                            persona.codiceFiscale = codiceFiscale;
                            persona.nome = reader.GetString(0);
                            persona.cognome = reader.GetString(1);
                            persona.dataDiNascita = reader.GetDateTime(2);
                            persona.sesso = reader.GetString(3).ToLower();
                            persona.indirizzo = reader.GetString(4);
                            persona.CAP = (int)reader.GetDecimal(5);
                            persona.citta = reader.GetString(6);
                            persona.provincia = reader.GetString(7).ToUpper();
                            persona.stato = reader.GetString(8);
                            persona.numeroDiTelefono = reader.GetString(9);
                        }
                    }

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", persona.codiceFiscale);
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
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }

        public string GetIdFilialeByUsername(string username) {
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

                    command.CommandText = "SELECT filiale FROM Account WHERE username = @username;";
                    command.Parameters.Add("@username", SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    if (Globals.debugMode) {
                        Console.WriteLine("\n============ Metodo GetIdFilialeByUsername ============");
                        Console.WriteLine("Query: {0}", command.CommandText);
                        Console.WriteLine("@username = {0}", username);
                    }

                    var filiale = (string)command.ExecuteScalar();

                    if (Globals.debugMode) {
                        Console.WriteLine("Risultato: {0}", filiale);
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
                    return String.Empty;
                }
                finally {
                    Console.WriteLine("\nServizio WCF online --- premere un tasto per interrompere...");
                }
            }
        }
    }
}
