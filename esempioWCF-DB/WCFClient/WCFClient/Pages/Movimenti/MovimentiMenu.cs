using EasyConsole;

namespace WCFClient.Pages
{
    class MovimentiMenu : MenuPage
    {
        public MovimentiMenu(Program program)
            : base("Movimenti", program,
                    new Option("Bonifico", () => program.NavigateTo<MovimentiPage1>()),
                    new Option("Prelievo", () => program.NavigateTo<MovimentiPage2>()),
                    new Option("Deposito", () => program.NavigateTo<MovimentiPage3>()))
        {
        }
    }
}
