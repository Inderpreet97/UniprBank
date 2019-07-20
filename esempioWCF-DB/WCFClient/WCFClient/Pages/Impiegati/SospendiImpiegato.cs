using EasyConsole;

namespace WCFClient.Pages
{
    class SospendiImpiegato : Page
    {
        public SospendiImpiegato(Program program) : base("Attiva", program) { }

        public override void Display()
        {
            base.Display();

            string usernameImpiegato;
            bool risultato = false;

            while (!risultato) {
                usernameImpiegato = Input.ReadString("Inserire username impiegato da sospendere: ");
                //Richiesta al database
                //bool risultato = WCFCLient.SospendiImpiegato(string username);                        
                if (risultato) { Output.WriteLine("Impiegato sospeso"); risultato = true; } 
                else {
                    Output.WriteLine("Impiegato non trovato");
                    string esc = Input.ReadString("1-Riprova\nPremere qualsiasi tasto per uscire");
                    if (esc != "1") { risultato = true; }
                }
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
