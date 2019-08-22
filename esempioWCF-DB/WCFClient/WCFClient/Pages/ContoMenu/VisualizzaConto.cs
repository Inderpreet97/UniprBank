using EasyConsole;
using ConsoleTables;
using System.Collections.Generic;
using System;

namespace WCFClient.Pages {
    class VisualizzaConto : Page {

        public VisualizzaConto(Program program) : base("Dati conto corrente", program) { }

        public override void Display() {
            base.Display();

            List<ContoCorrente> listaConti = new List<ContoCorrente>();

            if (LoggedUser.privilegi == "cliente") {
                //listaConti = WCFClient.GetListaContoCorrente(LoggedUser.username);
            }
            else if (LoggedUser.privilegi == "admin" || LoggedUser.privilegi == "impiegato") {
                string username = Funzioni.digitaUsername();
                //listaConti = WCFClient.GetListaContoCorrente(username);
            }

            if (listaConti.Count > 0) {
                var table = new ConsoleTable("Numero di conto", "IBAN", "SALDO", "Username", "Filiale di appartenenza");

                listaConti.ForEach(conto => {
                    table.AddRow(conto.idContoCorrente, conto.IBAN, conto.saldo, conto.username, conto.idFiliale);
                });

            } else {
                Output.WriteLine("Non ci sono conto correnti aperti con questo username");
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
