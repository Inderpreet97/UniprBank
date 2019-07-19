using EasyConsole;

namespace WCFClient.Pages
{
    class ModificaDatiFiliale : Page{

        public ModificaDatiFiliale(Program program) : base("Modifica Dati Filiale", program) { }

        public override void Display(){
            base.Display();

            //WCFClient.getFiliale(string idFiliale)
            Output.WriteLine("Hello from Page 1Ai");



            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();

        }
    }
}
