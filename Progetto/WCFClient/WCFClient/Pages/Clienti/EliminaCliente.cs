using EasyConsole;

namespace WCFClient.Pages {
    class EliminaCliente : Page {
        public EliminaCliente(Program program) : base("Elimina cliente", program) { }

        public override void Display() {
            base.Display();

            string usernameCliente;
            bool risultato = false;

            while (!risultato) {
                Output.WriteLine("Inserire username cliente da rimuovere\n\n");

                //Controlla che l'utente sia effettivamente un cliente

                usernameCliente = Funzioni.digitaUsername();

                if (Globals.wcfClient.GetPrivilegi(usernameCliente).ToLower() == "cliente") { //E' un cliente
                    risultato = Globals.wcfClient.EliminaAccount(usernameCliente);

                    //Rimosso con successo
                    if (risultato) {
                        Output.WriteLine("Cliente rimosso con successo");
                        risultato = true;
                    } else {
                        //Non trovato
                        Output.WriteLine("Cliente non trovato");
                        string esc = Input.ReadString("1-Riprova\nPremere qualsiasi tasto per uscire");
                        if (esc != "1") { risultato = true; }
                    }

                } else {
                    //Non è un cliente
                    Output.WriteLine("Attenzione!...L'utente non è un cliente");

                    string esc = Input.ReadString("1-Riprova\nPremere qualsiasi tasto per uscire");
                    if (esc != "1") { risultato = true; }
                }


            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
