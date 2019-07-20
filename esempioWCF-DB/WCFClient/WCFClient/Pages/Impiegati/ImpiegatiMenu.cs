using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiMenu : MenuPage
    {
        public ImpiegatiMenu(Program program)
            : base("Impiegati", program,
                    new Option("Sospendi", () => program.NavigateTo<SospendiImpiegato>()),
                    new Option("Attiva", () => program.NavigateTo<AttivaImpiegato>()),
                    new Option("Elimina", () => program.NavigateTo<EliminaImpiegato>()),
                    new Option("Aggiungi", () => program.NavigateTo<AggiungiImpiegato>()),
                    new Option("Modifica Dati", () => program.NavigateTo<ModificaImpiegato>()),
                    new Option("Visualizza Lista Impiegati", () => program.NavigateTo<ListaImpiegati>()))
            
        {
        }
    }
}
