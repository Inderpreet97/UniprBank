using EasyConsole;

namespace WCFClient.Pages
{
    class ClientiPage2 : Page
    {
        public ClientiPage2(Program program) : base("Crea ContoCorrente", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
