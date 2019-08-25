using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{

    class ModificaDatiFiliale : Page{

        public ModificaDatiFiliale(Program program) : base("Modifica Dati Filiale", program) { }

        public void ModificaFiliale(Filiale filiale) {
            Output.WriteLine("Per modificare digitare il nuovo valore da assegnare, altrimenti premere INVIO\n\n");

            string nomeFiliale, indirizzo, citta, provincia, stato, numeroDiTelefono, direttore = string.Empty;
            int? CAP;

            string temp = null;
            int tempInt;

            //Nome filiale
            temp = Input.ReadString("Nuovo nome filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { nomeFiliale = temp; }
            temp = string.Empty;

            //Indirizzo
            temp = Input.ReadString("Nuovo indirizzo: ");
            if (!string.IsNullOrWhiteSpace(temp)) { indirizzo = temp; }
            temp = string.Empty;

            //Città
            temp = Input.ReadString("Nuova città filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { citta = temp; }
            temp = string.Empty;

            //CAP
            temp = Input.ReadString("Nuova CAP filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { int.TryParse(temp, out tempInt); CAP = tempInt; }
            temp = string.Empty;

            //Provincia
            temp = Input.ReadString("Nuova provincia filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { provincia = temp; }
            temp = string.Empty;

            //Stato
            temp = Input.ReadString("Nuovo stato filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { stato = temp; }
            temp = string.Empty;

            //Numero di telefono
            temp = Input.ReadString("Nuovo numero di telefono filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { numeroDiTelefono = temp; }
            temp = string.Empty;

            //Direttore
            temp = Input.ReadString("Nuovo direttore filiale: ");
            //Verificare se il direttore sia presente nel Database
            if (!string.IsNullOrWhiteSpace(temp)) { direttore = temp; }
            temp = string.Empty;

            bool risultato = Globals.wcfClient.ModificaDatiFiliale(LoggedUser.idFiliale, filiale);

            if (risultato) { Output.WriteLine("Modifica effettuata correttamente"); } else { Output.WriteLine("Errore"); }
        }

        public override void Display(){
            base.Display();

            Filiale filiale = Globals.wcfClient.GetFiliale(LoggedUser.username);

            //Stampa dati correnti della filiale            
            Funzioni.StampaFiliale(filiale);

            ModificaFiliale(filiale);

            //Stampa nuovamente la filiale
            Funzioni.StampaFiliale(filiale);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();

        }
    }
}
