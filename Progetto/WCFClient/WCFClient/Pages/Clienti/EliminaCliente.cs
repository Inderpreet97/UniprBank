using EasyConsole;
using System;

namespace WCFClient.Pages {
    class EliminaCliente : Page {
        public EliminaCliente(Program program) : base("Elimina cliente", program) { }

        public override void Display() {
            base.Display();

            string usernameCliente;
            bool risultato = false;

            while (!risultato) {
                Output.WriteLine("Inserire username cliente da rimuovere\n");

                //Controlla che l'utente sia effettivamente un cliente

                usernameCliente = Funzioni.digitaUsername();

                if (usernameCliente != string.Empty) {
                    if (Globals.wcfClient.GetPrivilegi(usernameCliente).ToLower() == "cliente") { //E' un cliente
                        risultato = Globals.wcfClient.EliminaAccount(usernameCliente);

                        //Rimosso con successo
                        if (risultato) {
                            Output.WriteLine("Cliente rimosso con successo");
                            risultato = true;
                        } else {
                            //Non trovato
                            Output.WriteLine(ConsoleColor.Red, "\nCliente non trovato");

                            int scelta = Input.ReadInt("\nVuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                            if (scelta == 1) { risultato = true; }
                        }

                    } else {
                        //Non è un cliente
                        Output.WriteLine(ConsoleColor.Red, "\nAttenzione!...L'utente non è un cliente");

                        int scelta = Input.ReadInt("\nVuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                        if (scelta == 1) { risultato = true; }
                    }
                } else {
                    risultato = true;
                }
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
