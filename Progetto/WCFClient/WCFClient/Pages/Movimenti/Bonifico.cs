﻿using EasyConsole;
using ConsoleTables;
using System;

namespace WCFClient.Pages
{
    class Bonifico : Page
    {

        public Bonifico(Program program) : base("Bonifico", program) { }

        public override void Display() {
            base.Display();

            //IMPOSTA IBAN BENEFICIARIO
            string IBANBeneficiario = Input.ReadString("IBAN Beneficiario: ");
            while (!Globals.wcfClient.CheckIBAN(IBANBeneficiario)) {
                Output.WriteLine("IBAN non esistente, riprovare");
                IBANBeneficiario = Input.ReadString("IBAN Beneficiario: ");
            }

            //IMPOSTA IBAN COMMITTENTE
            UInt64? contoScelto = Funzioni.scegliIdContoCorrente(LoggedUser.contoCorrenti);
            string IBANCommittente = Globals.wcfClient.GetIBANByIdContoCorrente(Convert.ToUInt64(contoScelto));
            /*while (!Globals.wcfClient.CheckIBAN(IBANCommittente)) {
                Output.WriteLine("IBAN non esistente, riprovare");
                IBANCommittente = Input.ReadString("IBAN Committente: ");
            }*/

            var scelta = 1;

            do {
                try {
                    //IMPORTO BONIFICO
                    decimal importoBonifico = Convert.ToDecimal(Input.ReadString("Importo bonifico: "));
                   
                    importoBonifico = Funzioni.CheckImportoDisponibile(importoBonifico, IBANCommittente);
                    bool risultato = Globals.wcfClient.EseguiBonifico(IBANCommittente, IBANBeneficiario, importoBonifico);
                    if (risultato) { Output.WriteLine("Bonifico effettuato correttamente"); } else { Output.WriteLine("Bonifico non avvenuto"); }
                                        

                }
                catch (FormatException ex) {

                    Output.WriteLine(ConsoleColor.Red, ex.Message);
                    scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                }
            } while (scelta != 1);

            Input.ReadString("\nPremi [Invio] per ritornare al menu principale");

            Program.NavigateHome();
        }
    }
}
