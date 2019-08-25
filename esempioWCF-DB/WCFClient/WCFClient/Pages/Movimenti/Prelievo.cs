using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class Prelievo : Page
    {

        public bool EseguiPrelievoDenaro(int idContoCorrente, decimal importo) {
            Globals.wcfClient.EseguiPrelievoDenaro(idContoCorrente, importo);
            return false;
        }

        public Prelievo(Program program) : base("Prelievo", program) { }

        public override void Display()
        {
            base.Display();

            int idContoCorrente = Convert.ToInt32("Numero di conto corrente: ");
            while (!Globals.wcfClient.CheckIDConto(idContoCorrente)) {
                Output.WriteLine("Conto non trovato, riprovare");
                idContoCorrente = Convert.ToInt32("Numero di conto corrente: ");
            }

            decimal importo = Convert.ToDecimal("Importo da caricare: ");
            if (importo > 0) {
                if (EseguiPrelievoDenaro(idContoCorrente, importo)) {
                    Output.WriteLine("Prelievo di denaro effettuato con successo");
                } else {
                    Output.WriteLine("Prelievo di denaro non effettuato");
                }
            } else {
                Output.WriteLine("Prelievo di denaro non effettuato, la cifra non è valida");
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
