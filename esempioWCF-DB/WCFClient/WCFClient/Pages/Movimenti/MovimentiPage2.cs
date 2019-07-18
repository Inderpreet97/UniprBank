using EasyConsole;

namespace WCFClient.Pages
{
    class MovimentiPage2 : Page
    {
        public MovimentiPage2(Program program) : base("Prelievo", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
