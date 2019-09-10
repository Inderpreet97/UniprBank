using EasyConsole;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class CreaContoCorrente : Page
    {
        public CreaContoCorrente(Program program) : base("Crea conto corrente", program) { }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("ATTENZIONE! -  Il conto corrente deve essere obbligatoriamente associato a un utente già registrato\n\n");

            string username = Funzioni.digitaUsername();


            bool risultato = Globals.wcfClient.AggiungiContoCorrente(username, LoggedUser.idFiliale, 0);

            List<ContoCorrente> listaContiThisUser = Globals.wcfClient.GetListaContoCorrente(username);

            

            if (risultato) { Output.WriteLine("Conto corrente aggiunto"); } else { Output.WriteLine("Errore, conto corrente non creato"); }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
