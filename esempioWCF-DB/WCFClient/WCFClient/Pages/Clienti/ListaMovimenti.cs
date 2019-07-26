using EasyConsole;
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

            Output.WriteLine("Hello from Page 1Ai");

            int contoCorrente = Convert.ToInt32(Input.ReadString("Id conto corrente: "));
            bool risultato = false;

            //List<Movimento> listaMovimenti = WCFClient.GetListaMovimenti(idContoCorrente);
            List<Movimento> listaMovimenti = new List<Movimento>();

            foreach(Movimento movimento in listaMovimenti) { movimento.Stampa(); }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
