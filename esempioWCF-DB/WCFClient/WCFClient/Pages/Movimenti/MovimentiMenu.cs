using EasyConsole;

namespace WCFClient.Pages
{
    class MovimentiMenu : MenuPage
    {
        public MovimentiMenu(Program program)
            : base("Movimenti", program,
                    new Option("Bonifico", () => program.NavigateTo<Bonifico>()),
                    new Option("Prelievo", () => program.NavigateTo<Prelievo>()),
                    new Option("Deposito", () => program.NavigateTo<Deposito>()))
        {
        }
    }
}
