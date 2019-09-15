using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiMenu : MenuPage
    {
        public ImpiegatiMenu(Program program)
            : base("Impiegati", program,
                    new Option("Elimina impiegato", () => program.NavigateTo<EliminaImpiegato>()),
                    new Option("Aggiungi", () => program.NavigateTo<AggiungiImpiegato>()),
                    new Option("Modifica Dati", () => program.NavigateTo<ModificaImpiegato>()),
                    new Option("Visualizza Lista Impiegati", () => program.NavigateTo<ListaImpiegati>()))
            
        {
        }
    }
}
