using EasyConsole;
using System.Collections.Generic;

namespace WCFClient.Pages
{
    class ListaClienti : Page
    {
        public ListaClienti(Program program) : base("Crea ContoCorrente", program) { }

        public void getListaClienti() {
            List<Persona> listaClienti = new List<Persona>();
            //listaClienti = WCFClient.getListaClienti();
            foreach (Persona p in listaClienti) {
                Output.WriteLine("################################################################");
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
