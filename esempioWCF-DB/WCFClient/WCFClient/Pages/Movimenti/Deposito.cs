using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class Deposito : Page
    {

        public bool EseguiDeposito(int idContoCorrente, decimal importo) {
            //WCFClient.EseguiDeposito(idContoCorrente, importo);
            return false;
        }

        public Deposito(Program program) : base("Deposito", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            int idContoCorrente = Convert.ToInt32("Numero di conto corrente: ");
            while (!FunzioniMovimento.checkIDConto(idContoCorrente)) {
                Output.WriteLine("Conto non trovato, riprovare");
                idContoCorrente = Convert.ToInt32("Numero di conto corrente: ");
            }

            decimal importo = Convert.ToDecimal("Importo da caricare: ");
            if (importo > 0) {
                if (EseguiDeposito(idContoCorrente, importo)) { Output.WriteLine("Deposito di denaro effettuato con successo"); } else {
                    Output.WriteLine("Deposito non effettuato");
                }
            } else {
                Output.WriteLine("Deposito non effettuato, la cifra non è valida");
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
