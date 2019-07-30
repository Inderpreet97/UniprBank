using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST_APP.ServiceReferenceAccountPersona;

namespace TEST_APP
{
    class Program
    {
        static void Main(string[] args) {
            ServiceReferenceAccountPersona.ServiceAccountPersonaClient ServiceAccountPersonaClient = new ServiceReferenceAccountPersona.ServiceAccountPersonaClient();

// >>>> -------------- TEST Login -------------- <<<<<<<<
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool Login(string username, string password) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Risultato Corretto True
            string username = "indi";
            string password = "prova";
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = "indi";
            password = "rova"; // Manca la p nella password
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = "indi";
            password = "Prova"; // La P deve essere minuscola
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = string.Empty; // username vuoto
            password = "prova";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = string.Empty; // username vuoto
            password = string.Empty; // password vuota
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

// >>>> -------------- TEST GetPrivilegi -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= string GetPrivilegi(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input Corretto, Risultato Corretto Admin
            username = "indi";
            string risultatoStr;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Corretto, risultato atteso Admin");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultatoStr = ServiceAccountPersonaClient.GetPrivilegi(username);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input Sbagliato, Risultato Corretto string.Empty
            username = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Sbagliato, risultato atteso string.Empty");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultatoStr = ServiceAccountPersonaClient.GetPrivilegi(username);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input Sbagliato, Risultato Corretto string.Empty
            username = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Sbagliato, risultato atteso string.Empty");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultatoStr = ServiceAccountPersonaClient.GetPrivilegi(username);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

// >>>> -------------- TEST GetListaPersone -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= List<Persona> GetListaPersone(string tipoAccount, string idFiliale) =--\n");
            Console.ResetColor();

            // ---- Valori Input Corretti, Risultato atteso è una lista di Clienti appartenenti alla filiale con id = 
            string tipoAccount = "cliente";
            string idFiliale = "unId";
            List<Persona> resultList = new List<Persona>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Risultato atteso è una lista di clienti appartenenti alla filiale con id =");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tTipo account: {0}\tidFiliale: {1}", tipoAccount, idFiliale);
            resultList = ServiceAccountPersonaClient.GetListaPersone(tipoAccount, idFiliale).ToList();
            Console.WriteLine("OUTPUT:");

            var table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            resultList.ForEach(persona => {
                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, persona.dataDiNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input Corretti, Risultato atteso è una lista di Impiegati appartenenti alla filiale con id = 
            tipoAccount = "impiegato";
            idFiliale = "unId";
            resultList = new List<Persona>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Risultato atteso è una lista di Impiegati appartenenti alla filiale con id =");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tTipo account: {0}\tidFiliale: {1}", tipoAccount, idFiliale);
            resultList = ServiceAccountPersonaClient.GetListaPersone(tipoAccount, idFiliale).ToList();
            Console.WriteLine("OUTPUT:");

            table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            resultList.ForEach(persona => {
                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, persona.dataDiNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input Sbagliati, Risultato atteso è una lista vuota di persone 
            tipoAccount = string.Empty;
            idFiliale = string.Empty;
            resultList = new List<Persona>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, Risultato atteso è una lista vuota di persone");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tTipo account: {0}\tidFiliale: {1}", tipoAccount, idFiliale);
            resultList = ServiceAccountPersonaClient.GetListaPersone(tipoAccount, idFiliale).ToList();
            Console.WriteLine("OUTPUT:");

            table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            resultList.ForEach(persona => {
                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, persona.dataDiNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }
    }
}
