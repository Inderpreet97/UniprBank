using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaImpiegato : Page
    {
        public ModificaImpiegato(Program program) : base("Visualizza Lista Impiegati", program) { }

        public override void Display()
        {
            base.Display();

            string username = null;
            bool risultato = false;

            while (!risultato) {
                username = Input.ReadString("Inserire username impiegato da modificare: ");
                //risultato = WCFClient.ControllaUsername(usernameImpiegato);"
                if (!risultato) { Output.WriteLine("Impiegato non trovato, riprovare"); }
            }

            //Persona impiegato = WCFClient.GetPersona();
            Persona impiegato = new Persona();

            //ModificaImpiegato();
            //EseguiModifichePersona();

            Output.WriteLine("Impiegato modificato");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}