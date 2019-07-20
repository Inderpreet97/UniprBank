using EasyConsole;

namespace WCFClient.Pages
{
    class Bonifico : Page
    {
        public Bonifico(Program program) : base("Bonifico", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
