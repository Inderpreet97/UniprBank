﻿using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{

    class ModificaDatiFiliale : Page{

        public ModificaDatiFiliale(Program program) : base("Modifica Dati Filiale", program) { }

        public void ModificaFiliale() {
            Output.WriteLine("Per modificare digitare il nuovo valore da assegnare, altrimenti premere INVIO\n\n");

            Filiale filiale = new Filiale();

            //Nome filiale
            string temp = Input.ReadString("Nuovo nome filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { filiale.nome = temp; }
            
            //Indirizzo
            temp = Input.ReadString("Nuovo indirizzo: ");
            if (!string.IsNullOrWhiteSpace(temp)) { filiale.indirizzo = temp; }
            
            //Città
            temp = Input.ReadString("Nuova città filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { filiale.citta = temp; }

            //CAP
            int tempInt;
            temp = Input.ReadString("Nuova CAP filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { int.TryParse(temp, out tempInt); filiale.CAP = tempInt; }
            
            //Provincia
            temp = Input.ReadString("Nuova provincia filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { filiale.provincia = temp; }
            
            //Stato
            temp = Input.ReadString("Nuovo stato filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { filiale.stato = temp; }
            
            //Numero di telefono
            temp = Input.ReadString("Nuovo numero di telefono filiale: ");
            if (!string.IsNullOrWhiteSpace(temp)) { filiale.numeroDiTelefono = temp; }
            

            bool risultato = Globals.wcfClient.ModificaDatiFiliale(LoggedUser.idFiliale, filiale);

            if (risultato) { Output.WriteLine("Modifica effettuata correttamente"); } else { Output.WriteLine("Errore"); }
        }

        public override void Display(){
            base.Display();

            Filiale filiale = Globals.wcfClient.GetFiliale(LoggedUser.username);

            //Stampa dati correnti della filiale            
            Funzioni.StampaFiliale(filiale);

            ModificaFiliale();

            //Stampa nuovamente la filiale
            Funzioni.StampaFiliale(filiale);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();

        }
    }
}
