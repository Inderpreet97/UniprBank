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
                    new Option("Visualizza Lista Clienti", () => program.NavigateTo<ListaClienti>()),
                    new Option("Visualizza Lista Movimenti Cliente", () => program.NavigateTo<ListaMovimenti>()),
                    new Option("Viualizza conti di un cliente", () => program.NavigateTo<VisualizzaConto>()),
                    new Option("Effettua un Movimento", () => program.NavigateTo<MovimentiMenu>()))
        {
        }
    }
}
