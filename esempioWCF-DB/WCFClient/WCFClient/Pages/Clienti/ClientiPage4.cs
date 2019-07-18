using EasyConsole;

namespace WCFClient.Pages
{
    class ClientiPage4 : Page
    {
        public ClientiPage4(Program program) : base("Modifica ContoCorrente", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
