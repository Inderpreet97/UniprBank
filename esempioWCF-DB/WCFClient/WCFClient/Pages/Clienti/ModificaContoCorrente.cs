using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaContoCorrente : Page
    {
        public ModificaContoCorrente(Program program) : base("Visualizza Lista Clienti", program) { }

        public override void Display()
        {
            base.Display();

            ContoCorrente contoCorrente = new ContoCorrente();

            Funzioni

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
