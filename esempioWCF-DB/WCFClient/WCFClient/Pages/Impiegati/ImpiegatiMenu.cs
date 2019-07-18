using EasyConsole;

namespace WCFClient.Pages
{
    class ImpiegatiMenu : MenuPage
    {
        public ImpiegatiMenu(Program program)
            : base("Impiegati", program,
                    new Option("Sospendi", () => program.NavigateTo<ImpiegatiPage1>()),
                    new Option("Attiva", () => program.NavigateTo<ImpiegatiPage2>()),
                    new Option("Elimina", () => program.NavigateTo<ImpiegatiPage3>()),
                    new Option("Aggiungi", () => program.NavigateTo<ImpiegatiPage4>()),
                    new Option("Modifica Dati", () => program.NavigateTo<ImpiegatiPage5>()),
                    new Option("Visualizza Lista Impiegati", () => program.NavigateTo<ImpiegatiPage6>()))
            
        {
        }
    }
}
