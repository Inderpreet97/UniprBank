using EasyConsole;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class CreaContoCorrente : Page
    {
        public CreaContoCorrente(Program program) : base("Modifica Persona/Account", program) { }

        public override void Display()
        {
            base.Display();

            string username = Funzioni.digitaNuovoUsername();

            bool risultato = Globals.wcfClient.AggiungiContoCorrente(username, LoggedUser.idFiliale, 0);

            if (risultato) {

                Output.WriteLine("Conto corrente aggiunto");

            } else {

                Output.WriteLine("Errore");
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
