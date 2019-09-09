using EasyConsole;

namespace WCFClient.Pages
{
    class ProfiloMenu : MenuPage
    {   
        public ProfiloMenu(Program program) 
            : base("Profilo", program, 
                    new Option("Modifica" , ()=> program.NavigateTo<ProfiloModifica>()),
                    new Option("Logout" , ()=> program.NavigateTo<ProfiloLogout>()))
        { 
        }
    }
}
