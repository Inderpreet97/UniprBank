
using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class Bonifico : Page
    {
        public bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal importo) {
            bool risultato = false;
            //risultato = WCFClient.eseguiBonifico(IBANBeneficiario, importo, IBANCommittente);
            return risultato;
        }

        public Bonifico(Program program) : base("Bonifico", program) { }

        public override void Display()
        {
            base.Display();

            string tipoMovimento = "Bonifico";


            //IBAN
            string IBANBeneficiario = Input.ReadString("IBAN Beneficiario: ");
            while (!FunzioniMovimento.checkIBAN(IBANBeneficiario)) {
                Output.WriteLine("IBAN non esistente, riprovare");
                IBANBeneficiario = Input.ReadString("IBAN Beneficiario: ");
            }

            //SELEZIONARE CONTO CORRENTE
            string IBANCommittente = Input.ReadString("ID conto corrente con la quale effettuare un bonifico: ");

            //IMPORTO BONIFICO
            decimal importoBonifico = Convert.ToDecimal("Importo bonifico: ");
            bool importoDisponibile = FunzioniMovimento.checkImportoDisponibile(importoBonifico, IBANCommittente);
            while (importoBonifico < 0 || !importoDisponibile) {
                if (importoBonifico < 0) {
                    Output.WriteLine("L'importo deve essere maggiore di 0.0");
                } else {
                    Output.WriteLine("Il saldo non ricopre l'importo");
                }
                importoBonifico = Convert.ToDecimal("Importo bonifico: ");
                importoDisponibile = FunzioniMovimento.checkImportoDisponibile(importoBonifico, IBANCommittente);
            };

            bool risultato = EseguiBonifico(IBANCommittente, IBANBeneficiario, importoBonifico);
            if (risultato) { Output.WriteLine("Bonifico effettuato correttamente"); } else { Output.WriteLine("Bonifico non avvenuto"); }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
