using EasyConsole;

namespace WCFClient.Pages
{
    class AttivaImpiegato : Page
    {
        public AttivaImpiegato(Program program) : base("Attiva impiegato", program) { }

        public override void Display()
        {
            base.Display();

            string usernameImpiegato;
            bool risultato = false;

            while (!risultato) {
                usernameImpiegato = Input.ReadString("Inserire username impiegato da attivare: ");
                //Richiesta al database
                //bool risultato = Globals.WCFCLient.AttivaImpiegato(string username);                        
                if (risultato) { Output.WriteLine("Impiegato attivato con successo"); risultato = true; } else {
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
