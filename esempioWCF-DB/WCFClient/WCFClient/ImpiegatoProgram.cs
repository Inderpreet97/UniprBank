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

            //Clienti
            AddPage(new RegistraPersona(this));
            AddPage(new ModificaAccount(this));

            //Conto correnti e movimenti
            AddPage(new CreaContoCorrente(this));
            AddPage(new VisualizzaConto(this));
            AddPage(new Bonifico(this));
            AddPage(new Deposito(this));
            AddPage(new Prelievo(this));
            AddPage(new ContoMenu(this));
            AddPage(new MovimentiMenu(this));
            AddPage(new ListaMovimenti(this));

            //Profilo
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloModifica(this));
            AddPage(new ProfiloLogout(this));

            SetPage<MainMenuImpiegato>();
        }
    }
}
