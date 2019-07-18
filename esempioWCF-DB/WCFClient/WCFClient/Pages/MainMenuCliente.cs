using EasyConsole;

namespace WCFClient.Pages
{
    class MainMenuCliente : MenuPage
    {
        public MainMenuCliente(Program program)
            : base("Menu Principale", program,
                  new Option("Visualizza Lista Movimenti Cliente", () => program.NavigateTo<ClientiPage6>()),
                  new Option("Effetua un Movimento", () => program.NavigateTo<MovimentiMenu>()),
                  new Option("Modifica Profilo Cliente", () => program.NavigateTo<ProfiloMenu>()))
        {
        }
    }
}
