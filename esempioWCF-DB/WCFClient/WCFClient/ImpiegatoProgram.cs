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
            AddPage(new ListaMovimenti(this));
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloLogout(this));
            AddPage(new MovimentiMenu(this));
            AddPage(new Bonifico(this));
            AddPage(new Deposito(this));
            AddPage(new Prelievo(this));
            AddPage(new ContoMenu(this));
            AddPage(new VisualizzaConto(this));

            SetPage<MainMenuImpiegato>();
        }
    }
}
