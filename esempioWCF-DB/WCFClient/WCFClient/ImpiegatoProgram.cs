using EasyConsole;
using WCFClient.Pages;

namespace WCFClient
{
    class ImpiegatoProgram : Program
    {
        public ImpiegatoProgram()
            : base("Menu Impiegato", breadcrumbHeader: true) //Breadcrumber = percorso dei menu
        {
            AddPage(new MainMenuImpiegato(this));
            AddPage(new RegistraPersona(this));
            AddPage(new ListaClienti(this));
            AddPage(new CreaContoCorrente(this));
            AddPage(new ModificaAccount(this));
            AddPage(new ModificaContoCorrente(this));
            AddPage(new ListaMovimenti(this));
            AddPage(new EseguiMovimento(this));
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloLogout(this));

            SetPage<MainMenuImpiegato>();
        }
    }
}
