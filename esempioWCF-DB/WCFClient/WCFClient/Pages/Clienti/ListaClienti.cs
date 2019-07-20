using EasyConsole;

namespace WCFClient.Pages
{
    class ListaClienti : Page
    {
        public ListaClienti(Program program) : base("Crea ContoCorrente", program) { }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Lista clienti");



            

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
