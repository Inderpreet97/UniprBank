using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AggiungiImpiegato : Page
    {
        public AggiungiImpiegato(Program program) : base("Modifica Dati", program) { }

        public override void Display()
        {
            base.Display();

            Funzioni.aggiungiPersona("Impiegato");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
