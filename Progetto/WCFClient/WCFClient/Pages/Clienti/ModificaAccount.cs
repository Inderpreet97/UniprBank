using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaAccount : Page
    {
        public ModificaAccount(Program program) : base("Modifica Account", program) { }

        public override void Display()
        {
            base.Display();

            Funzioni.modificaPersona(string.Empty);

            Input.ReadString("\nPremi [Invio] per ritornare al menu principale");

            Program.NavigateHome();
        }
    }
}
