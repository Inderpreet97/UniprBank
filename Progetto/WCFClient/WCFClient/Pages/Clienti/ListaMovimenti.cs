using EasyConsole;
using ConsoleTables;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class ListaMovimenti : Page
    {
        public ListaMovimenti(Program program) : base("Visualizza Lista Movimenti Cliente", program) { }

        public override void Display()
        {
            base.Display();

            var scelta = 1;

            do {
                try {

                    List<ContoCorrente> listaContiUser = Funzioni.getListaContiByPrivilege(); // definizione in funzioni.cs

                    if (listaContiUser.Count != 0) {
                        UInt64? idContoCorrente = Funzioni.scegliIdContoCorrente(listaContiUser);

                        List<Movimento> listaMovimenti = Globals.wcfClient.GetListaMovimenti(Convert.ToUInt64(idContoCorrente));

                        var table = new ConsoleTable("Id Movimento", "IBAN Committente", "Tipo Movimento", "Importo", "IBAN Beneficiario", "Data/Ora");

                        listaMovimenti.ForEach(movimento => {
                            table.AddRow(movimento.idMovimenti, movimento.IBANCommittente, movimento.tipo, movimento.importo,
                                movimento.IBANBeneficiario, movimento.dataOra);
                        });

                        table.Write();
                    }

                    scelta = 1;
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
