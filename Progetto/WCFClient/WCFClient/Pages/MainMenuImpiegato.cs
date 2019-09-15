using EasyConsole;

namespace WCFClient.Pages
{
    class MainMenuImpiegato : MenuPage
    {
        public MainMenuImpiegato(Program program)
            : base("Menu Principale", program,
                  new Option("Registra Persona/Account", () => program.NavigateTo<RegistraPersona>()),
                  new Option("Modifica Persona/Account", () => program.NavigateTo<ModificaAccount>()),

                  new Option("Crea ContoCorrente", () => program.NavigateTo<CreaContoCorrente>()),
                  new Option("Visualizza Lista Movimenti Cliente", () => program.NavigateTo<ListaMovimenti>()),
                  new Option("Visualizza conti di un cliente", () => program.NavigateTo<VisualizzaConto>()),

                  new Option("Modifica Profilo Impiegato", () => program.NavigateTo<ProfiloMenu>())
                  )
        {
        }
    }
}
