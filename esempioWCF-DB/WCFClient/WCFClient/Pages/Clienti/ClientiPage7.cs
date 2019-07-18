using EasyConsole;

namespace WCFClient.Pages
{
    class ClientiPage7 : Page
    {
        public ClientiPage7(Program program) : base("Effetua un Movimento", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
