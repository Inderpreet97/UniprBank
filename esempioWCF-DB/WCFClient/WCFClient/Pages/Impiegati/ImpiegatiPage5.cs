using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiPage5 : Page
    {
        public ImpiegatiPage5(Program program) : base("Modifica Dati", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
