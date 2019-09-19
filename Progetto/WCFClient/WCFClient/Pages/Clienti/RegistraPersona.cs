using EasyConsole;

namespace WCFClient.Pages
{
    class RegistraPersona : Page
    {
        public RegistraPersona(Program program) : base("Registra Persona/Account", program) {}

        public override void Display()
        {
            base.Display();

            Funzioni.aggiungiPersona("Cliente");

            Input.ReadString("\nPremi [Invio] per ritornare al menu principale");
            Program.NavigateHome();
        }
    }
}
