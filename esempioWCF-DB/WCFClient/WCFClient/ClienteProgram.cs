using EasyConsole;
using WCFClient.Pages;

namespace WCFClient
{
    class ClienteProgram : Program
    {
        public ClienteProgram()
            : base("Menu Cliente", breadcrumbHeader: true)
        {
            AddPage(new MainMenuCliente(this));
            AddPage(new ListaMovimenti(this));
            AddPage(new MovimentiMenu(this));
            AddPage(new Bonifico(this));
            AddPage(new Prelievo(this));
            AddPage(new Deposito(this));
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloLogout(this));

            SetPage<MainMenuCliente>();
        }
    }
}
