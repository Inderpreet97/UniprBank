using EasyConsole;

namespace WCFClient.Pages
{
    class EliminaImpiegato : Page
    {
        public EliminaImpiegato(Program program) : base("Elimina impiegato", program) { }

        public override void Display()
        {
            base.Display();

            string usernameImpiegato;
            bool risultato = false;

            while (!risultato) {
                Output.WriteLine("Inserire username impiegato da rimuovere\n\n");

                //Controlla che l'utente sia effettivamente un impiegato

                usernameImpiegato = Funzioni.digitaUsername();

                if (Globals.wcfClient.GetPrivilegi(usernameImpiegato).ToLower() == "impiegato"){ //E' un impiegato
                    risultato = Globals.wcfClient.EliminaAccount(usernameImpiegato);

                    //Rimosso con successo
                    if (risultato) { 
                        Output.WriteLine("Impiegato rimosso con successo");
                        risultato = true;
                    } else {
                        //Non trovato
                        Output.WriteLine("Impiegato non trovato");
                        string esc = Input.ReadString("1-Riprova\nPremere qualsiasi tasto per uscire");
                        if (esc != "1") { risultato = true; }
                    }

                } else{
                    //Non è un impiegato
                    Output.WriteLine("Attenzione!...L'utente non è un impiegato");

                    string esc = Input.ReadString("1-Riprova\nPremere qualsiasi tasto per uscire");
                    if (esc != "1") { risultato = true; }
                }

                
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
