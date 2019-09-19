using EasyConsole;
using ConsoleTables;
using System.Collections.Generic;
using System;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages {
    class VisualizzaConto : Page {

        public VisualizzaConto(Program program) : base("Dati conto corrente", program) { }

        public override void Display() {
            base.Display();

            List<ContoCorrente> listaConti = new List<ContoCorrente>();
            string username = "temp";

            if (LoggedUser.privilegi == "cliente") {
                listaConti = Globals.wcfClient.GetListaContoCorrente(LoggedUser.username);
            }
            else if (LoggedUser.privilegi == "admin" || LoggedUser.privilegi == "impiegato") {
                username = Funzioni.digitaUsername();
                if (username != string.Empty) {
                    listaConti = Globals.wcfClient.GetListaContoCorrente(username);
                } else {
                    username = "temp";
                }
            }

            if (listaConti.Count > 0) {
                var table = new ConsoleTable("Numero di conto", "IBAN", "SALDO", "Username", "Filiale di appartenenza");

                listaConti.ForEach(conto => {
                    table.AddRow(conto.idContoCorrente, conto.IBAN, conto.saldo, conto.username, conto.idFiliale);
                });

                table.Write();

            } else {
                if(username == "temp")
                    Output.WriteLine("Operazione annullata");
                else
                    Output.WriteLine("Non ci sono conto correnti aperti con questo username");
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
