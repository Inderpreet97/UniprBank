using EasyConsole;
using System.Collections.Generic;

namespace WCFClient.Pages
{
    class ListaClienti : Page
    {
        public ListaClienti(Program program) : base("Crea ContoCorrente", program) { }

        public void getListaClienti() {
            List<Persona> listaClienti = new List<Persona>();
            string idFiliale = string.Empty;
            //idFiliale = WCFClient.getIdFiliale(string idFiliale)
            //listaClienti = WCFClient.getListaClienti();
            foreach (Persona p in listaClienti) {
                Output.WriteLine("################################################################");
                //Tabella clienti
                p.Stampa();
            }
        }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Lista clienti");

            this.getListaClienti();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
