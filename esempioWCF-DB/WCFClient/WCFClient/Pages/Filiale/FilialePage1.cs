using EasyConsole;

namespace WCFClient.Pages
{
    class FilialePage1 : Page
    {
        public FilialePage1(Program program) : base("Modifica Dati Filiale", program) { }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
