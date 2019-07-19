using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaDatiFiliale : Page{

        public ModificaDatiFiliale(Program program) : base("Modifica Dati Filiale", program) { }

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

            //ModificaFiliale();
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
