using EasyConsole;


namespace WCFClient.Pages{

    class ContoMenu : MenuPage {
        public ContoMenu(Program program)
            : base("Clienti", program,
                    new Option("Visualizza i tuoi conto correnti", () => program.NavigateTo<VisualizzaConto>()),
                    new Option("Effetua un Movimento", () => program.NavigateTo<MovimentiMenu>())) {
        }
    }

}
