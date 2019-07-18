using EasyConsole;
using WCFClient.Pages;

namespace WCFClient
{
    class DirettoreProgram : Program
    {
        public DirettoreProgram()
            : base("Menu Direttore", breadcrumbHeader: true)
        {
            AddPage(new MainMenuDirettore(this));
            AddPage(new ClientiMenu(this));
            AddPage(new ClientiPage1(this));
            AddPage(new ClientiPage2(this));
            AddPage(new ClientiPage3(this));
            AddPage(new ClientiPage4(this));
            AddPage(new ClientiPage5(this));
            AddPage(new ClientiPage6(this));
            AddPage(new ClientiPage7(this));
            AddPage(new FilialeMenu(this));
            AddPage(new FilialePage1(this));
            AddPage(new ImpiegatiMenu(this));
            AddPage(new ImpiegatiPage1(this));
            AddPage(new ImpiegatiPage2(this));
            AddPage(new ImpiegatiPage3(this));
            AddPage(new ImpiegatiPage4(this));
            AddPage(new ImpiegatiPage5(this));
            AddPage(new ImpiegatiPage6(this));
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloLogout(this));

            SetPage<MainMenuDirettore>();
        }
    }
}
