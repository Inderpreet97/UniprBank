using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiPage3 : Page
    {
        public ImpiegatiPage3(Program program) : base("Elimina", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
