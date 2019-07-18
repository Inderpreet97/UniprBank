using EasyConsole;

namespace WCFClient.Pages
{
    class ClientiPage5 : Page
    {
        public ClientiPage5(Program program) : base("Visualizza Lista Clienti", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
