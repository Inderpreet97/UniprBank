using EasyConsole;

namespace WCFClient.Pages
{
    class MainMenuDirettore : MenuPage
    {
        public MainMenuDirettore(Program program)
            : base("Menu Principale", program,
                  new Option("Filiale", () => program.NavigateTo<FilialeMenu>()),
                  new Option("Impiegati", () => program.NavigateTo<ImpiegatiMenu>()),
                  new Option("Clienti", () => program.NavigateTo<ClientiMenu>()),
                  new Option("Profilo Direttore", () => program.NavigateTo<ProfiloMenu>()))
        {
        }
    }
}
