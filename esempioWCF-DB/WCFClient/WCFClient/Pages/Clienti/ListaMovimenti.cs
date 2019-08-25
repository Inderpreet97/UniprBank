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
                    int idContoCorrente = Convert.ToInt32(Input.ReadString("Id conto corrente: "));

                    List<Movimento> listaMovimenti = Globals.wcfClient.GetListaMovimenti(idContoCorrente);

                    var table = new ConsoleTable("Id Movimento", "IBAN Committente", "Tipo Movimento", "Importo", "IBAN Beneficiario", "Data/Ora");

                    listaMovimenti.ForEach(movimento => {
                        table.AddRow(movimento.idMovimenti, movimento.IBANCommittente, movimento.tipo, movimento.importo,
                            movimento.IBANBeneficiario, movimento.dataOra);
                    });

                    table.Write();
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
