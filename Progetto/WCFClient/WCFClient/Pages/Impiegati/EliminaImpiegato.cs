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

                if (usernameImpiegato != string.Empty) {
                    if (Globals.wcfClient.GetPrivilegi(usernameImpiegato).ToLower() == "impiegato") { //E' un impiegato
                        risultato = Globals.wcfClient.EliminaAccount(usernameImpiegato);

                        //Rimosso con successo
                        if (risultato) {
                            Output.WriteLine("Impiegato rimosso con successo");
                            risultato = true;
                        } else {
                            //Non trovato
                            Output.WriteLine("Impiegato non trovato");

                            int scelta = Input.ReadInt("\nVuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                            if (scelta == 1) { risultato = true; }
                        }

                    } else {
                        //Non è un impiegato
                        Output.WriteLine("Attenzione!...L'utente non è un impiegato");

                        int scelta = Input.ReadInt("\nVuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                        if (scelta == 1) { risultato = true; }
                    }
                } else {
                    risultato = true;
                }


            }

            Input.ReadString("\nPremi [Invio] per ritornare al menu principale");
            Program.NavigateHome();
        }
    }
}
