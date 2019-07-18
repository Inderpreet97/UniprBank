using EasyConsole;
using WCFClient.Pages;

namespace WCFClient
{
    class ImpiegatoProgram : Program
    {
        public ImpiegatoProgram()
            : base("Menu Impiegato", breadcrumbHeader: true)
        {
            AddPage(new MainMenuImpiegato(this));
            AddPage(new ClientiPage1(this));
            AddPage(new ClientiPage2(this));
            AddPage(new ClientiPage3(this));
            AddPage(new ClientiPage4(this));
            AddPage(new ClientiPage5(this));
            AddPage(new ClientiPage6(this));
            AddPage(new ClientiPage7(this));
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloLogout(this));

            SetPage<MainMenuImpiegato>();
        }
    }
}
