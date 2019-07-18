using EasyConsole;

namespace WCFClient.Pages
{
    class MainMenuImpiegato : MenuPage
    {
        public MainMenuImpiegato(Program program)
            : base("Menu Principale", program,
                  new Option("Registra Persona/Account", () => program.NavigateTo<ClientiPage1>()),
                  new Option("Crea ContoCorrente", () => program.NavigateTo<ClientiPage2>()),
                  new Option("Modifica Persona/Account", () => program.NavigateTo<ClientiPage3>()),
                  new Option("Modifica ContoCorrente", () => program.NavigateTo<ClientiPage4>()),
                  new Option("Visualizza Lista Clienti", () => program.NavigateTo<ClientiPage5>()),
                  new Option("Visualizza Lista Movimenti Cliente", () => program.NavigateTo<ClientiPage6>()),
                  new Option("Effetua un Movimento", () => program.NavigateTo<ClientiPage7>()),
                  new Option("Modifica Profilo Impiegato", () => program.NavigateTo<ProfiloMenu>()))
        {
        }
    }
}
