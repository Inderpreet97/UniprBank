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

                    List<ContoCorrente> listaContiUser = new List<ContoCorrente>(); //Lista contenenti i conti di un utente
                    List<UInt64?> listaNumeriConto = new List<UInt64?>(); //Lista contenente i numeri di conto dei contocorrenti di un utente

                    //-----RICHIESTA USERNAME-------
                    //---L'impiegato o il direttore devono inserire l'username dell'account della quale si vuole ottenere la lista movimenti
                    //---Se invece il LoggedUser è cliente la lista dei conti viene caricata nella fase di login

                    //Impiegato e direttore
                    if (LoggedUser.privilegi != "cliente") { 
                        string username = Funzioni.digitaUsername();
                        listaContiUser = Globals.wcfClient.GetListaContoCorrente(username);
                    } else { //Cliente
                        listaContiUser = LoggedUser.contoCorrenti;
                    }

                    UInt64 idContoCorrente = Funzioni.scegliIdContoCorrente(listaContiUser);
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
