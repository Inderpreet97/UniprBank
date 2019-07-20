using EasyConsole;
using System;
using System.Collections.Generic;

namespace WCFClient.Pages
{

    class ModificaDatiFiliale : Page{

        public ModificaDatiFiliale(Program program) : base("Modifica Dati Filiale", program) { }

        public void ModificaFiliale(Filiale filiale) {
            Output.WriteLine("Per modificare digitare il nuovo valore da assegnare, altrimenti premere INVIO\n\n");

            string nomeFiliale, indirizzo, citta, provincia, stato, numeroDiTelefono, direttore = string.Empty;
            int CAP;
            string temp = null;

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
            if (!string.IsNullOrWhiteSpace(temp)) { int.TryParse(temp, out CAP); }
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

            //bool risultato = WCFClient.ModificaFiliale(nomeFiliale, indirizzo, citta, provincia, stato, numeroDiTelefono, direttore, CAP);
            bool risultato = false;

            if (risultato) { Output.WriteLine("Modifica effettuata correttamente"); } else { Output.WriteLine("Errore"); }
        }

        public void StampaFiliale(Filiale filiale) {
            Output.WriteLine("Nome filiale: " + filiale.nome);
            Output.WriteLine("Indirizzo: " + filiale.indirizzo);
            Output.WriteLine("CAP: " + filiale.CAP);
            Output.WriteLine("Città: " + filiale.citta);
            Output.WriteLine("Provincia: " + filiale.provincia);
            Output.WriteLine("Stato: " + filiale.stato);
            Output.WriteLine("Numero di telefono: " + filiale.numeroDiTelefono);
            Output.WriteLine("Direttore: " + filiale.direttore);
        }

        public override void Display(){
            base.Display();

            //WCFClient.getFiliale(LoggedUser.username)
            Filiale filiale = new Filiale();

            //Stampa dati correnti della filiale            
            StampaFiliale(filiale);

            ModificaFiliale(filiale);
            //bool risultato = WCFClient.UpdateDatiFilialebool UpdateDatiFiliale([string]nome, [string]indirizzo, [int?]numeroTelefono, ....)
            bool risulato = false;

            if (risulato) {
                Output.WriteLine("Dati filiale modificati correttamente...");

            } else {
                Output.WriteLine("Aggiornamento dati non andato a buon fine...");
            }

            //Stampa nuovamente la filiale
            StampaFiliale(filiale);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();

        }
    }
}
