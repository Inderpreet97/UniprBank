using EasyConsole;

namespace WCFClient.Pages
{
    class ClientiPage6 : Page
    {
        public ClientiPage6(Program program) : base("Visualizza Lista Movimenti Cliente", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
