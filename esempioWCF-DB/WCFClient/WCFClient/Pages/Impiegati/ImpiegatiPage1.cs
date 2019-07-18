using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiPage1 : Page
    {
        public ImpiegatiPage1(Program program) : base("Sospendi", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
