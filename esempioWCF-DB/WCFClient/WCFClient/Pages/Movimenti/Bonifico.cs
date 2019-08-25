
using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class Bonifico : Page
    {

        public Bonifico(Program program) : base("Bonifico", program) { }

        public override void Display()
        {
            base.Display();
            
            //IBAN
            string IBANBeneficiario = Input.ReadString("IBAN Beneficiario: ");
            while (!Globals.wcfClient.CheckIBAN(IBANBeneficiario)) {
                Output.WriteLine("IBAN non esistente, riprovare");
                IBANBeneficiario = Input.ReadString("IBAN Beneficiario: ");
            }

            //SELEZIONARE IBAN
            string IBANCommittente = Input.ReadString("IBAN conto corrente con il quale effettuare il bonifico: ");

            //IMPORTO BONIFICO
            decimal importoBonifico = Convert.ToDecimal("Importo bonifico: ");
            bool importoDisponibile = Globals.wcfClient.CheckImporto(importoBonifico, IBANCommittente); ;

            while (importoBonifico < 0 || !importoDisponibile) {

                if (importoBonifico < 0) {

                    Output.WriteLine("L'importo deve essere maggiore di 0.0");

                } else {

                    Output.WriteLine("Il saldo non ricopre l'importo");
                }

                importoBonifico = Convert.ToDecimal("Importo bonifico: ");

                importoDisponibile = Globals.wcfClient.CheckImporto(importoBonifico, IBANCommittente);
            };

            bool risultato = Globals.wcfClient.EseguiBonifico(IBANCommittente, IBANBeneficiario, importoBonifico);

            if (risultato) {
                Output.WriteLine("Bonifico effettuato correttamente");
            } else {
                Output.WriteLine("Bonifico non avvenuto");
            }

            Input.ReadString("Press [Enter] to navigate home");

            Program.NavigateHome();
        }
    }
}
