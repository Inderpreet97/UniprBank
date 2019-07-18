using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiPage6 : Page
    {
        public ImpiegatiPage6(Program program) : base("Visualizza Lista Impiegati", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}