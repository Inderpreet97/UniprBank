using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiPage2 : Page
    {
        public ImpiegatiPage2(Program program) : base("Attiva", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
