using EasyConsole;

namespace WCFClient.Pages
{
    class ProfiloMenu : MenuPage
    {   
        public ProfiloMenu(Program program) 
            : base("Profilo", program, 
                    new Option("Menu1" , ()=> program.NavigateTo<MainMenuDirettore>()),
                    new Option("Logout" , ()=> program.NavigateTo<ProfiloLogout>()))
        { 
        }
    }
}
