using EasyConsole;

namespace WCFClient.Pages
{
    class RegistraPersona : Page
    {
        public RegistraPersona(Program program) : base("Registra Persona/Account", program) {}

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
