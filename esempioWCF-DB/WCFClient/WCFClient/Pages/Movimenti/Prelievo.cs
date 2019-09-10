using EasyConsole;
using ConsoleTables;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class Prelievo : Page
    {

        public bool EseguiPrelievoDenaro(UInt64 idContoCorrente, decimal importo) {
            return Globals.wcfClient.EseguiPrelievoDenaro(idContoCorrente, importo);
        }

        public Prelievo(Program program) : base("Prelievo", program) { }

        public override void Display() {
            base.Display();
            var scelta = 1;

            do {
                try {

                    string username;

                    //LISTA CONTI CORRENTI
                    List<ContoCorrente> listaContiUser = Funzioni.getListaContiByPrivilege();

                    //SCELTA CONTO CORRENTE
                    UInt64 idContoCorrente = Funzioni.scegliIdContoCorrente(listaContiUser);

                    /*while (!Globals.wcfClient.CheckIDConto(idContoCorrente)) {

                        Output.WriteLine("Conto non trovato, riprovare");
                        idContoCorrente = Convert.ToUInt64(Input.ReadString("Numero di conto corrente: "));
                    }*/

                    decimal importo = Convert.ToDecimal(Input.ReadString("Importo da prelevare: "));

                    if (importo > 0) {
                        if (EseguiPrelievoDenaro(idContoCorrente, importo)) {
                            Output.WriteLine("Prelievo di denaro effettuato con successo");
                        } else {
                            Output.WriteLine("Prelievo di denaro non effettuato");
                        }
                    } else {
                        Output.WriteLine("Prelievo di denaro non effettuato, la cifra non è valida");
                    }
                }
                catch (FormatException ex) {

                    Output.WriteLine(ConsoleColor.Red, ex.Message);
                    scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                }
            } while (scelta != 1);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
