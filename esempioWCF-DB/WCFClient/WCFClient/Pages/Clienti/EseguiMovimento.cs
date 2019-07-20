using EasyConsole;

namespace WCFClient.Pages
{
    class EseguiMovimento : Page
    {
        public EseguiMovimento(Program program) : base("Effetua un Movimento", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
