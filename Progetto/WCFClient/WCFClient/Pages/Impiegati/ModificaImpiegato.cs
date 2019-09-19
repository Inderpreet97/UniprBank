using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaImpiegato : Page
    {
        public ModificaImpiegato(Program program) : base("Modificaa impiegato", program) { }

        public override void Display()
        {
            base.Display();

            Funzioni.modificaPersona(string.Empty);

            Input.ReadString("\nPremi [Invio] per ritornare al menu principale");
            Program.NavigateHome();
        }
    }
}