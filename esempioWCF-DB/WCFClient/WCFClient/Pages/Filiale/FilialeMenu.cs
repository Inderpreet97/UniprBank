using EasyConsole;

namespace WCFClient.Pages
{
    class FilialeMenu : MenuPage
    {
        public FilialeMenu(Program program)
            : base("Filiale", program,
                    new Option("Modifica Dati Filiale", () => program.NavigateTo<FilialePage1>()),
                    new Option("Visualizza Lista Impiegati", () => program.NavigateTo<ImpiegatiPage6>()),
                    new Option("Visualizza Lista Clienti", () => program.NavigateTo<ClientiPage5>()))
        {
        }
    }
}
