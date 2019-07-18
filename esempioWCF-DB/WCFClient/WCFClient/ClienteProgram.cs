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
            AddPage(new ClientiPage6(this));
            AddPage(new MovimentiMenu(this));
            AddPage(new MovimentiPage1(this));
            AddPage(new MovimentiPage2(this));
            AddPage(new MovimentiPage3(this));
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloLogout(this));

            SetPage<MainMenuCliente>();
        }
    }
}
