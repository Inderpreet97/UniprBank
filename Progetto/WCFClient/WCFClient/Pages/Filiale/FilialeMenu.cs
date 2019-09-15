using EasyConsole;

namespace WCFClient.Pages
{
    class FilialeMenu : MenuPage
    {
        public FilialeMenu(Program program)
            : base("Filiale", program,
                    new Option("Modifica Dati Filiale", () => program.NavigateTo<ModificaDatiFiliale>()))
        {
        }
    }
}
