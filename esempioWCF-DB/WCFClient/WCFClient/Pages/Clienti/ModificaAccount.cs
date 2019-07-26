using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaAccount : Page
    {
        public ModificaAccount(Program program) : base("Modifica ContoCorrente", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Funzioni.modificaPersona(string.Empty);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
