using EasyConsole;

namespace WCFClient.Pages
{
    class ListaImpiegati : Page
    {
        public ListaImpiegati(Program program) : base("Sospendi", program) { }

        public override void Display()
        {
            base.Display();

            System.Collections.Generic.List<Persona> listaClienti = new System.Collections.Generic.List<Persona>();

            //listaClienti = WCFClient.getListaClienti(usernameDirettore);

            listaClienti.ForEach(delegate (Persona cliente) {
                cliente.Stampa();
            });

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
