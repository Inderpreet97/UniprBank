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
        // DA MODIFICARE IL DB e VEDERE SE LA QUERY DIVENTA CASE SENSITIVE
        static void TestLogin(ServiceAccountPersonaClient ServiceAccountPersonaClient) {
            // >>>> -------------- TEST Login -------------- <<<<<<<<
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool Login(string username, string password) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Risultato Corretto True
            string username = "indi97";
            string password = "indi123";
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
            username = "indi97";
            password = "ndi123"; // Manca la i nella password
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = "indi97";
            password = "Indi123"; // La I deve essere minuscola
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
        }

        static void TestGetPrivilegi(ServiceAccountPersonaClient ServiceAccountPersonaClient) {
            // >>>> -------------- TEST GetPrivilegi -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= string GetPrivilegi(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input Corretto, Risultato Corretto Admin
            string username = "indi";
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
        }

        static void TestGetListaPersona(ServiceAccountPersonaClient ServiceAccountPersonaClient) {
            // >>>> -------------- TEST GetListaPersone -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= List<Persona> GetListaPersone(string tipoAccount, string idFiliale) =--\n");
            Console.ResetColor();

            // ---- Valori Input Corretti, Risultato atteso è una lista di Clienti appartenenti alla filiale con id = PR12FID001
            string tipoAccount = "cliente";
            string idFiliale = "PR12FID001";
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
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input Corretti, Risultato atteso è una lista di Impiegati appartenenti alla filiale con id = CR12CRM001
            tipoAccount = "impiegato";
            idFiliale = "CR12CRM001";
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
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
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
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestEliminaAccount(ServiceAccountPersonaClient ServiceAccountPersonaClient) {
            // >>>> -------------- TEST EliminaAccount -------------- <<<<<<<<
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool EliminaAccount(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato Corretto True
            string username = "tempUser";
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = ServiceAccountPersonaClient.EliminaAccount(username);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato Corretto False
            username = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = ServiceAccountPersonaClient.EliminaAccount(username);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato Corretto False
            username = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = ServiceAccountPersonaClient.EliminaAccount(username);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestCheckUsername(ServiceAccountPersonaClient ServiceAccountPersonaClient){
            // >>>> -------------- TEST CheckUsername -------------- <<<<<<<<
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= Persona CheckUsername(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso un oggetto Persona con dati validi
            string username = "indi97";
            Persona risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso un oggetto Persona con dati validi");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = ServiceAccountPersonaClient.CheckUsername(username);
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Persona con attributi settati a default
            username = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = ServiceAccountPersonaClient.CheckUsername(username);
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Persona con attributi settati a default
            username = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = ServiceAccountPersonaClient.CheckUsername(username);
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void StampaPersona(Persona p) {
            Console.WriteLine("#########################################");
            Console.WriteLine("Tipo di utenza: ", p.privilegi);
            Console.WriteLine("\nUsername: ", p.username + "\nNome: ", p.nome + "\nCognome: ", p.cognome);
            Console.WriteLine("\nCodice Fiscale: ", p.codiceFiscale + "\nSesso: ", p.sesso + "\nData di nascita", p.dataDiNascita);
            Console.WriteLine("\nCittà: ", p.citta + "\nProvincia: ", p.provincia + "\nStato: ", p.stato);
            Console.WriteLine("\nNumero di telefono: ", p.numeroDiTelefono);
        }

        // DA INSERIRE UN CODICE FISCALE VALIDO
        static void TestGetPersona(ServiceAccountPersonaClient ServiceAccountPersonaClient) {
            // >>>> -------------- TEST GetPersona -------------- <<<<<<<<
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= Persona GetPersona(string codiceFiscale) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso un oggetto Persona con dati validi
            string codiceFiscale = "asasdadadasdsdssa"; // <<<<<<<<<<<<<<<<<<<<---------------------------==
            Persona risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso un oggetto Persona con dati validi");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", codiceFiscale);
            risultato = ServiceAccountPersonaClient.CheckUsername(codiceFiscale);
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Persona con attributi settati a default
            codiceFiscale = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", codiceFiscale);
            risultato = ServiceAccountPersonaClient.CheckUsername(codiceFiscale);
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Persona con attributi settati a default
            codiceFiscale = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", codiceFiscale);
            risultato = ServiceAccountPersonaClient.CheckUsername(codiceFiscale);
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        // DA FARE
        static void TestAggiungiPersona(ServiceAccountPersonaClient ServiceAccountPersonaClient) {
            // >>>> -------------- TEST Login -------------- <<<<<<<<
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool Login(string username, string password) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Risultato Corretto True
            string username = "indi97";
            string password = "indi123";
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
            username = "indi97";
            password = "ndi123"; // Manca la i nella password
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = "indi97";
            password = "Indi123"; // La I deve essere minuscola
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
        }

        // DA FARE
        static void TestModificaPersona(ServiceAccountPersonaClient ServiceAccountPersonaClient) {

        }


        static void Main(string[] args) {

            ServiceAccountPersonaClient ServiceAccountPersonaClient = new ServiceAccountPersonaClient();

            TestLogin(ServiceAccountPersonaClient);
            TestGetPrivilegi(ServiceAccountPersonaClient);
            TestGetListaPersona(ServiceAccountPersonaClient);
            TestAggiungiPersona(ServiceAccountPersonaClient);
            TestModificaPersona(ServiceAccountPersonaClient);
            TestGetPersona(ServiceAccountPersonaClient);
            TestEliminaAccount(ServiceAccountPersonaClient);
            TestCheckUsername(ServiceAccountPersonaClient);
        }
    }
}
