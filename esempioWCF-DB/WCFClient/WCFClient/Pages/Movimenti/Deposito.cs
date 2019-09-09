using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class Deposito : Page
    {


        public Deposito(Program program) : base("Deposito", program) { }

        public override void Display()
        {
            base.Display();

            var scelta = 1;

            do {
                try {

                   UInt64 idContoCorrente = Convert.ToUInt64(Input.ReadString("Numero di conto corrente: "));
                    while (!Globals.wcfClient.CheckIDConto(idContoCorrente)) {

                        Output.WriteLine("Conto non trovato, riprovare");
                        idContoCorrente = Convert.ToUInt64(Input.ReadString("Numero di conto corrente: "));

                    }

                    decimal importo = Convert.ToDecimal(Input.ReadString("Importo da caricare: "));
                    if (importo > 0) {

                        if (Globals.wcfClient.EseguiDeposito(idContoCorrente, importo)) {

                            Output.WriteLine("Deposito di denaro effettuato con successo");

                        } else {

                            Output.WriteLine("Deposito non effettuato");
                        }
                    } else {

                        Output.WriteLine("Deposito non effettuato, la cifra non è valida");
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
