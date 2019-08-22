using EasyConsole;

namespace WCFClient.Pages
{
    class MainMenuCliente : MenuPage
    {
        public MainMenuCliente(Program program)
            : base("Menu Principale", program,
                  new Option("Visualizza Lista Movimenti Cliente", () => program.NavigateTo<ListaMovimenti>()),
                  new Option("Effettua un Movimento", () => program.NavigateTo<MovimentiMenu>()),
                  new Option("Visualizza saldo e dati conto", () => program.NavigateTo<ContoMenu>()),
                  new Option("Modifica Profilo Cliente", () => program.NavigateTo<ProfiloMenu>()))
        {
        }
    }
}
