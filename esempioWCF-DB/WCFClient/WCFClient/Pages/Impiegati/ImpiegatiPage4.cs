using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiPage4 : Page
    {
        public ImpiegatiPage4(Program program) : base("Aggiungi", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
