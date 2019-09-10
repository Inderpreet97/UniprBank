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
            AddPage(new RegistraPersona(this));
            AddPage(new ListaClienti(this));
            AddPage(new CreaContoCorrente(this));
            AddPage(new ModificaAccount(this));
            AddPage(new ListaMovimenti(this));
            AddPage(new FilialeMenu(this));
            AddPage(new ModificaDatiFiliale(this));
            AddPage(new ImpiegatiMenu(this));
            AddPage(new ListaImpiegati(this));
            AddPage(new SospendiImpiegato(this));
            AddPage(new AttivaImpiegato(this));
            AddPage(new EliminaImpiegato(this));
            AddPage(new AggiungiImpiegato(this));
            AddPage(new ModificaImpiegato(this));
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloLogout(this));
            AddPage(new MovimentiMenu(this));
            //AddPage(new Bonifico(this));
            //AddPage(new Prelievo(this));
            //AddPage(new Deposito(this));
            AddPage(new ContoMenu(this));
            AddPage(new VisualizzaConto(this));



            SetPage<MainMenuDirettore>();
        }
    }
}
