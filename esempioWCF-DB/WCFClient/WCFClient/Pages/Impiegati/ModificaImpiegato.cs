using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaImpiegato : Page
    {
        public ModificaImpiegato(Program program) : base("Visualizza Lista Impiegati", program) { }

        public override void Display()
        {
            base.Display();
            bool risultato = false;
            string errorString = string.Empty;
            string username = "";
            Persona impiegato = new Persona();

            do {
                Output.WriteLine(System.ConsoleColor.Red, errorString);
                errorString = string.Empty;

                username = Input.ReadString("Username: ");
                impiegato = Funzioni.checkUsername(username);
                if (impiegato.username == string.Empty) {
                    errorString = "Username non trovato, prego digitare un username valido";
                }
            } while (errorString != string.Empty);

            Funzioni.modificaPersona(impiegato);
            //bool risultato = WCFCLient.ModificaPersona(impiegato);

            if (risultato) { Output.WriteLine("Impiegato modificato correttamente"); } else { Output.WriteLine("Errore"); }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}