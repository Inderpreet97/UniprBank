using EasyConsole;

namespace WCFClient.Pages
{
    class CreaContoCorrente : Page
    {
        public CreaContoCorrente(Program program) : base("Modifica Persona/Account", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
