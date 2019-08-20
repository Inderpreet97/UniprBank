using EasyConsole;
using ConsoleTables;
using System;
using System.Collections.Generic;

namespace WCFClient.Pages
{
    class ListaMovimenti : Page
    {
        public ListaMovimenti(Program program) : base("Visualizza Lista Movimenti Cliente", program) { }

        public override void Display()
        {
            base.Display();

            int contoCorrente = Convert.ToInt32(Input.ReadString("Id conto corrente: "));
            bool risultato = false;

            //List<Movimento> listaMovimenti = WCFClient.GetListaMovimenti(idContoCorrente);
            List<Movimento> listaMovimenti = new List<Movimento>();

            var table = new ConsoleTable("Id Movimento", "IBAN Committente", "Tipo Movimento", "Importo", "IBAN Beneficiario", "Data/Ora");

            listaMovimenti.ForEach(movimento => {
                table.AddRow(movimento.idMovimento, movimento.IBANCommittente, movimento.tipo, movimento.importo,
                    movimento.IBANBeneficiario, movimento.dataOra);
            });

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
