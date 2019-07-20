using EasyConsole;

namespace WCFClient.Pages
{
    class ClientiMenu : MenuPage
    {
        public ClientiMenu(Program program)
            : base("Clienti", program,
                    new Option("Registra Persona/Account", () => program.NavigateTo<RegistraPersona>()),
                    new Option("Crea ContoCorrente", () => program.NavigateTo<CreaContoCorrente>()),
                    new Option("Modifica Persona/Account", () => program.NavigateTo<ModificaAccount>()),
                    new Option("Modifica ContoCorrente", () => program.NavigateTo<ModificaContoCorrente>()),
                    new Option("Visualizza Lista Clienti", () => program.NavigateTo<ListaClienti>()),
                    new Option("Visualizza Lista Movimenti Cliente", () => program.NavigateTo<ListaMovimenti>()),
                    new Option("Effetua un Movimento", () => program.NavigateTo<EseguiMovimento>()))
        {
        }
    }
}
