using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaImpiegato : Page
    {
        public ModificaImpiegato(Program program) : base("Visualizza Lista Impiegati", program) { }

        public override void Display()
        {
            base.Display();

            Funzioni.modificaPersona("Impiegato");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}