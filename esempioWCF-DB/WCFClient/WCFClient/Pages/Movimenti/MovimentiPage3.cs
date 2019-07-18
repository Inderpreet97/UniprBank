using EasyConsole;

namespace WCFClient.Pages
{
    class MovimentiPage3 : Page
    {
        public MovimentiPage3(Program program) : base("Deposito", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
